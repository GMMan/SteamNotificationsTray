using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SteamNotificationsTray;

namespace SteamNotificationsTray
{
    class NotificationsMenuColorTable : ProfessionalColorTable
    {
        Properties.Settings Settings
        {
            get { return Properties.Settings.Default; }
        }

        public override Color MenuBorder
        {
            get
            {
                return Settings.NotificationPopupBorderColor;
            }
        }

        public override Color MenuItemSelected
        {
            get
            {
                return Settings.NotificationFocusColor;
            }
        }

        public override Color MenuItemBorder
        {
            get
            {
                return Settings.NotificationFocusColor;
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return Settings.NotificationPopupBackgroundColor;
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return Settings.NotificationPopupBackgroundColor;
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return Settings.NotificationPopupBackgroundColor;
            }
        }

        public override Color SeparatorLight
        {
            get
            {
                return Settings.NotificationPopupSeparatorColor;
            }
        }

        public override Color SeparatorDark
        {
            get
            {
                return Settings.NotificationPopupSeparatorColor;
            }
        }
    }
}
