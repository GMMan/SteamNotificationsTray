using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading.Tasks;

namespace SteamNotificationsTray
{
    class TrayAppContext : ApplicationContext
    {
        WindowsFormsSynchronizationContext syncContext;
        NotifyIcon mainIcon = new NotifyIcon();
        NotifyIcon countIcon = new NotifyIcon();
        ContextMenu appContextMenu;
        MenuItem loginMenuItem;
        MenuItem refreshMenuItem;
        ContextMenuStrip notificationsContextMenu;
        Timer refreshTimer = new Timer();
        NotificationsClient client = new NotificationsClient();
        bool newNotifAcknowledged;
        bool hasNotifications;

        public TrayAppContext()
        {
            syncContext = new WindowsFormsSynchronizationContext();

            loginMenuItem = new MenuItem("Log in", (sender, e) =>
            {
                promptLogin();
            }) { Visible = false };
            refreshMenuItem = new MenuItem("Refresh now", (sender, e) =>
            {
                updateNotifications();
            });

            appContextMenu = new ContextMenu(new MenuItem[] {
                loginMenuItem,
                refreshMenuItem,
                new MenuItem("Settings", (sender, e) =>
                {
                    new SettingsForm().Show();
                }),
                new MenuItem("Exit", (sender, e) => {
                    Application.Exit();
                })
            });

            refreshTimer.Interval = Properties.Settings.Default.RefreshInterval;
            refreshTimer.Tick += refreshTimer_Tick;

            // Must do this true/false charade to get context menus associated for some reason
            countIcon.ContextMenu = appContextMenu;
            countIcon.Visible = true;
            countIcon.Visible = false;
            mainIcon.ContextMenu = appContextMenu;
            mainIcon.Text = "Steam Notifications Tray App";
            mainIcon.Visible = true;
            mainIcon.Visible = false;

            mainIcon.Click += notifyIcon_Click;
            mainIcon.DoubleClick += notifyIcon_DoubleClick;
            countIcon.Click += notifyIcon_Click;
            countIcon.DoubleClick += notifyIcon_DoubleClick;

            // If no cookies available, show login form
            //CredentialStore.ClearCredentials();
            if (!CredentialStore.CredentialsAvailable())
            {
                promptLogin();
            }
            else
            {
                finishSetup();
            }
        }

        void promptLogin()
        {
            mainIcon.Visible = false;
            LoginForm loginForm = new LoginForm();
            loginForm.FormClosed += loginForm_FormClosed;
            MainForm = loginForm;
            loginForm.Show();
        }

        void loginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = sender as Form;
            MainForm = null;
            if (form.DialogResult == DialogResult.OK)
                finishSetup(); // Login OK, start the rest of the app
            else
                Application.Exit(); // Login canceled, exit
        }
        
        void finishSetup()
        {
            // Set up cookies
            CookieContainer cookies = CredentialStore.GetCommunityCookies();
            client.SetCookies(cookies);

            // Update right click menu
            loginMenuItem.Visible = false;
            refreshMenuItem.Visible = true;

            // Set main icon visible
            ReplaceNotifyIcon(mainIcon, IconUtils.CreateIconWithBackground(Properties.Resources.NotificationDefault, Properties.Settings.Default.InboxNoneColor, SystemInformation.SmallIconSize));
            mainIcon.Visible = true;

            // Set up timer and fire
            refreshTimer.Start();
            updateNotifications();
        }

        async void refreshTimer_Tick(object sender, EventArgs e)
        {
            await updateNotifications();
        }

        async Task updateNotifications()
        {
            try
            {
                NotificationCounts counts = await client.PollNotificationCountsAsync();
                if (counts != null)
                {
                    if (counts.TotalNotifications == 0)
                    {
                        hasNotifications = false;
                        countIcon.Visible = false;
                        ReplaceNotifyIcon(mainIcon, IconUtils.CreateIconWithBackground(Properties.Resources.NotificationDefault, Properties.Settings.Default.InboxNoneColor, SystemInformation.SmallIconSize));
                    }
                    else
                    {
                        hasNotifications = true;
                        NotificationCounts oldCounts = client.PrevCounts;
                        Color newColor;
                        if (oldCounts == null)
                        {
                            newNotifAcknowledged = true;
                            newColor = Properties.Settings.Default.InboxAvailableColor;
                        }
                        else
                        {
                            if (counts.TotalNotifications > oldCounts.TotalNotifications)
                            {
                                newNotifAcknowledged = false;
                                newColor = Properties.Settings.Default.InboxNewColor;
                            }
                            else if (counts.TotalNotifications == oldCounts.TotalNotifications)
                            {
                                newColor = newNotifAcknowledged ? Properties.Settings.Default.InboxAvailableColor : Properties.Settings.Default.InboxNewColor;
                            }
                            else
                            {
                                newNotifAcknowledged = true;
                                newColor = Properties.Settings.Default.InboxAvailableColor;
                            }                            
                        }

                        ReplaceNotifyIcon(mainIcon, IconUtils.CreateIconWithBackground(Properties.Resources.NotificationActive, newColor, SystemInformation.SmallIconSize));
                        
                        // 7 point for 3 digits
                        // 8 point for 2 digits
                        // 9 point for 1 digit
                        string text = counts.TotalNotifications.ToString();
                        ReplaceNotifyIcon(countIcon, IconUtils.CreateIconWithText(text, new Font("Arial", 10 - text.Length, FontStyle.Regular, GraphicsUnit.Point), newColor, SystemInformation.SmallIconSize));

                        countIcon.Text = string.Format("{0} unread Steam notification{1}", counts.TotalNotifications, counts.TotalNotifications != 1 ? "s" : string.Empty);

                        if (!countIcon.Visible)
                        {
                            // Hide main icon first, then show in this order so the count is on the left
                            mainIcon.Visible = false;
                            countIcon.Visible = true;
                            mainIcon.Visible = true;
                        }
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                {
                    // Login info expired
                    refreshTimer.Stop();
                    ReplaceNotifyIcon(mainIcon, IconUtils.CreateIconWithBackground(Properties.Resources.NotificationDisabled, Properties.Settings.Default.InboxNoneColor, SystemInformation.SmallIconSize));
                    countIcon.Visible = false;
                    loginMenuItem.Visible = true;
                    refreshMenuItem.Visible = false;
                }
                else
                {
                    markException("Error polling for notifications: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                markException("Exception: " + ex.Message);
            }
        }

        void markException(string message)
        {
            if (message.Length >= 64) message = message.Substring(0, 62) + "…";
            if (mainIcon.Visible)
            {
                mainIcon.Text = message;
            }
            else
            {
                countIcon.Text = message;
            }
        }

        void notifyIcon_Click(object sender, EventArgs e)
        {
            if (hasNotifications)
            {
                // Make icon normal colored
                newNotifAcknowledged = true;
                ReplaceNotifyIcon(mainIcon, IconUtils.CreateIconWithBackground(Properties.Resources.NotificationActive, Properties.Settings.Default.InboxAvailableColor, SystemInformation.SmallIconSize));
                string text = client.CurrentCounts.TotalNotifications.ToString();
                ReplaceNotifyIcon(countIcon, IconUtils.CreateIconWithText(text, new Font("Arial", 10 - text.Length, FontStyle.Regular, GraphicsUnit.Point), Properties.Settings.Default.InboxAvailableColor, SystemInformation.SmallIconSize));
            }
        }

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("steam://open/main");
        }
       
        protected override void ExitThreadCore()
        {
            mainIcon.Visible = false;
            countIcon.Visible = false;
            base.ExitThreadCore();
        }

        static void ReplaceNotifyIcon(NotifyIcon notify, Icon newIcon)
        {
            if (notify.Icon != null) DestroyIcon(notify.Icon.Handle);
            notify.Icon = newIcon;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
    }
}
