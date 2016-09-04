using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SteamNotificationsTray
{
    class NotificationsMenuRenderer : ToolStripProfessionalRenderer
    {
        public NotificationsMenuRenderer()
            : base(new NotificationsMenuColorTable())
        { }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if ((int)e.Item.Tag > 0) e.TextColor = Properties.Settings.Default.NotificationActiveColor;
            base.OnRenderItemText(e);
        }
    }
}
