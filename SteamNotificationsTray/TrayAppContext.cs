using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SteamNotificationsTray
{
    class TrayAppContext : ApplicationContext
    {
        NotifyIcon mainIcon;
        NotifyIcon countIcon;

        public TrayAppContext()
        {
            mainIcon = new NotifyIcon
            {
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Exit", (sender, e) =>{
                        mainIcon.Visible = false;
                        countIcon.Visible = false;
                        Application.Exit();
                    })
                })
            };

            mainIcon.Icon = IconUtils.CreateIconWithBackground(Properties.Resources.NotificationActive, Color.FromArgb(255, 92, 126, 16), SystemInformation.SmallIconSize);

            countIcon = new NotifyIcon();
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
