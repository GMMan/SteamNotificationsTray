using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading.Tasks;
using System.Reflection;

namespace SteamNotificationsTray
{
    partial class TrayAppContext : ApplicationContext
    {
        NotifyIcon mainIcon = new NotifyIcon();
        NotifyIcon countIcon = new NotifyIcon();
        ContextMenu appContextMenu;
        MenuItem loginMenuItem;
        MenuItem refreshMenuItem;
        Timer refreshTimer = new Timer();
        NotificationsClient client = new NotificationsClient();
        bool newNotifAcknowledged;
        bool hasNotifications;
        MethodInfo NotifyIcon_ShowContextMenu;
        NotificationCounts countsDiff;

        public TrayAppContext()
        {
            NotifyIcon_ShowContextMenu = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.NonPublic | BindingFlags.Instance);

            loginMenuItem = new MenuItem(Properties.Resources.LogIn, (sender, e) =>
            {
                promptLogin();
            }) { Visible = false };
            refreshMenuItem = new MenuItem(Properties.Resources.RefreshNow, (sender, e) =>
            {
                updateNotifications();
            });

            appContextMenu = new ContextMenu(new MenuItem[] {
                loginMenuItem,
                refreshMenuItem,
                new MenuItem(Properties.Resources.Settings, (sender, e) =>
                {
                    var settingsForm = new SettingsForm();
                    settingsForm.SettingsApplied += settingsForm_SettingsApplied;
                    settingsForm.LoggingOut += settingsForm_LoggingOut;
                    settingsForm.Show();
                }),
                new MenuItem(Properties.Resources.Exit, (sender, e) => {
                    client.SaveCredentials();
                    Application.Exit();
                })
            });

            setupNotificationsPopup();

            refreshTimer.Interval = Properties.Settings.Default.RefreshInterval;
            refreshTimer.Tick += refreshTimer_Tick;

            // Must do this true/false charade to get context menus associated for some reason
            countIcon.ContextMenu = appContextMenu;
            countIcon.Visible = true;
            countIcon.Visible = false;
            mainIcon.ContextMenu = appContextMenu;
            mainIcon.Text = Application.ProductName;
            mainIcon.Visible = true;
            mainIcon.Visible = false;

            mainIcon.MouseDown += notifyIcon_MouseDown;
            mainIcon.MouseClick += notifyIcon_MouseClick;
            mainIcon.Click += notifyIcon_Click;
            mainIcon.DoubleClick += notifyIcon_DoubleClick;
            countIcon.MouseDown += notifyIcon_MouseDown;
            countIcon.MouseClick += notifyIcon_MouseClick;
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
                if (counts != null) updateUi(counts);
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                {
                    // Login info expired
                    logOut();
                }
                else
                {
                    markException(Properties.Resources.ErrorPolling + ex.Message);
                }
            }
            catch (Exception ex)
            {
                markException(Properties.Resources.Exception + ex.Message);
            }
        }

        void logOut()
        {
            refreshTimer.Stop();
            client.SetCookies(null);
            Properties.Settings.Default.Credentials = null;
            Properties.Settings.Default.Save();
            ReplaceNotifyIcon(mainIcon, IconUtils.CreateIconWithBackground(Properties.Resources.NotificationDisabled, Properties.Settings.Default.InboxNoneColor, SystemInformation.SmallIconSize));
            countIcon.Visible = false;
            loginMenuItem.Visible = true;
            refreshMenuItem.Visible = false;
            markException(Properties.Resources.NotLoggedIn);
        }

        void updateUi(NotificationCounts counts)
        {
            try
            {
                if (client.PrevCounts == null)
                {
                    countsDiff = new NotificationCounts();
                }
                else
                {
                    var prev = client.PrevCounts;
                    countsDiff = new NotificationCounts
                    {
                        Comments = counts.Comments - prev.Comments,
                        Items = counts.Items - prev.Items,
                        Invites = counts.Invites - prev.Invites,
                        Gifts = counts.Gifts - prev.Gifts,
                        OfflineMessages = counts.OfflineMessages - prev.OfflineMessages,
                        TradeOffers = counts.TradeOffers - prev.TradeOffers,
                        AsyncGames = counts.AsyncGames - prev.AsyncGames,
                        ModeratorMessages = counts.ModeratorMessages - prev.ModeratorMessages,
                        HelpRequestReplies = counts.HelpRequestReplies - prev.HelpRequestReplies,
                        TotalNotifications = counts.TotalNotifications - prev.TotalNotifications,
                    };
                }

                updatePopupCounts(counts);
                mainIcon.Text = Application.ProductName;
                countIcon.Text = string.Format(counts.TotalNotifications == 1 ? Properties.Resources.UnreadNotificationsSingular : Properties.Resources.UnreadNotificationsPlural, counts.TotalNotifications);

                if (counts.TotalNotifications == 0)
                {
                    hasNotifications = false;
                    countIcon.Visible = false;
                    ReplaceNotifyIcon(mainIcon, IconUtils.CreateIconWithBackground(Properties.Resources.NotificationDefault, Properties.Settings.Default.InboxNoneColor, SystemInformation.SmallIconSize));
                    mainIcon.Visible = true;
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

                    if (!countIcon.Visible)
                    {
                        // Hide main icon first, then show in this order so the count is on the left
                        mainIcon.Visible = false;
                        countIcon.Visible = true;
                    }
                    mainIcon.Visible = !Properties.Settings.Default.SingleIcon;

                    if (Properties.Settings.Default.EnableBalloons)
                    {
                        List<string> notifications = new List<string>();
                        if (countsDiff.Comments > 0) notifications.Add(countsDiff.Comments == 1 ? Properties.Resources.CommentsSingular : string.Format(Properties.Resources.CommentsPlural, countsDiff.Comments));
                        if (countsDiff.Items > 0) notifications.Add(countsDiff.Items == 1 ? Properties.Resources.ItemsSingular : string.Format(Properties.Resources.ItemsPlural, countsDiff.Items));
                        if (countsDiff.Invites > 0) notifications.Add(countsDiff.Invites == 1 ? Properties.Resources.InvitesSingular : string.Format(Properties.Resources.InvitesPlural, countsDiff.Invites));
                        if (countsDiff.Gifts > 0) notifications.Add(countsDiff.Gifts == 1 ? Properties.Resources.GiftsSingular : string.Format(Properties.Resources.GiftsPlural, countsDiff.Gifts));
                        if (countsDiff.OfflineMessages > 0) notifications.Add(countsDiff.OfflineMessages == 1 ? Properties.Resources.OfflineMessagesSingular : string.Format(Properties.Resources.OfflineMessagesPlural, countsDiff.OfflineMessages));
                        if (countsDiff.TradeOffers > 0) notifications.Add(countsDiff.TradeOffers == 1 ? Properties.Resources.TradeOffersSingular : string.Format(Properties.Resources.TradeOffersPlural, countsDiff.TradeOffers));
                        if (countsDiff.AsyncGames > 0) notifications.Add(countsDiff.AsyncGames == 1 ? Properties.Resources.AsyncGamesSingular : string.Format(Properties.Resources.AsyncGamesPlural, countsDiff.AsyncGames));
                        if (countsDiff.ModeratorMessages > 0) notifications.Add(countsDiff.ModeratorMessages == 1 ? Properties.Resources.ModeratorMessagesSingular : string.Format(Properties.Resources.ModeratorMessagesPlural, countsDiff.ModeratorMessages));
                        if (countsDiff.HelpRequestReplies > 0) notifications.Add(countsDiff.HelpRequestReplies == 1 ? Properties.Resources.HelpRequestRepliesSingular : string.Format(Properties.Resources.HelpRequestRepliesPlural, countsDiff.HelpRequestReplies));

                        if (notifications.Count > 0)
                        {
                            countIcon.BalloonTipIcon = ToolTipIcon.Info;
                            countIcon.BalloonTipTitle = countIcon.Text;

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(Properties.Resources.NewNotifsSince);
                            sb.AppendLine();
                            foreach (string notif in notifications)
                                sb.AppendLine(notif);
                            countIcon.BalloonTipText = sb.ToString();
                            countIcon.ShowBalloonTip(10000); // Per MSDN, timeout doesn't make a difference (since Vista)
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                markException(Properties.Resources.Exception + ex.Message);
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

        void notifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mainIcon.ContextMenu = null;
                mainIcon.ContextMenuStrip = notificationsContextMenu;
                countIcon.ContextMenu = null;
                countIcon.ContextMenuStrip = notificationsContextMenu;
            }
            else if (e.Button == MouseButtons.Right)
            {
                mainIcon.ContextMenu = appContextMenu;
                mainIcon.ContextMenuStrip = null;
                countIcon.ContextMenu = appContextMenu;
                countIcon.ContextMenuStrip = null;
            }
        }

        void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender is NotifyIcon && refreshTimer.Enabled)
            {
                NotifyIcon_ShowContextMenu.Invoke(sender, null);
            }
        }

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("steam://open/main");
        }

        void settingsForm_SettingsApplied(object sender, EventArgs e)
        {
            refreshTimer.Interval = Properties.Settings.Default.RefreshInterval;
            updatePopupColors();
            updateUi(client.CurrentCounts);
            if (!refreshTimer.Enabled)
            {
                // Logged out, hide count and show main
                countIcon.Visible = false;
                mainIcon.Visible = true;
            }
        }

        void settingsForm_LoggingOut(object sender, EventArgs e)
        {
            logOut();
            client.SetCookies(null);
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
