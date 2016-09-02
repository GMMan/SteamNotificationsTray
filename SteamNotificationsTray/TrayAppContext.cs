using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Net;

namespace SteamNotificationsTray
{
    class TrayAppContext : ApplicationContext
    {
        static readonly string AuthCookieName = "steamLoginSecure";
        static readonly string SteamCommunityDomain = "steamcommunity.com";

        NotifyIcon mainIcon = new NotifyIcon();
        NotifyIcon countIcon = new NotifyIcon();

        public TrayAppContext()
        {
            // 1. Get cookies so notification counts can be retrieved
            //string authCookie = GetCookie(SteamCommunityDomain, null);
            // 1a. If no cookies available, show login form
            if (true)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.FormClosed += loginForm_FormClosed;
                MainForm = loginForm;
                loginForm.Show();
            }
            else
            {
                finishSetup();
            }
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
            //cookieContainer.Add(new Cookie(AuthCookieName, GetCookie("https://" + SteamCommunityDomain, AuthCookieName), "/", SteamCommunityDomain) { Secure = true });

            // 2. Set up tray icons
            ShowMockup();
            // 3. Set up timer and fire
        }

        void ShowMockup()
        {
            mainIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Exit", (sender, e) =>{
                    mainIcon.Visible = false;
                    countIcon.Visible = false;
                    Application.Exit();
                })
            });
            mainIcon.Icon = IconUtils.CreateIconWithBackground(Properties.Resources.NotificationActive, Color.FromArgb(255, 92, 126, 16), SystemInformation.SmallIconSize);

            // 7 point for 3 digits
            // 8 point for 2 digits
            // 9 point for 1 digit
            string text = "8";
            countIcon.Icon = IconUtils.CreateIconWithText(text, new Font("Arial", 10 - text.Length, FontStyle.Regular, GraphicsUnit.Point), Color.FromArgb(255, 92, 126, 16), SystemInformation.SmallIconSize);

            // Show icons, count first, main second
            countIcon.Visible = true;
            mainIcon.Visible = true;
        }
    }
}
