using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                Icon = Properties.Resources.NotificationActive,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Exit", (sender, e) =>{
                        mainIcon.Visible = false;
                        Application.Exit();
                    })
                }),
                Visible = true
            };
        }

    }
}
