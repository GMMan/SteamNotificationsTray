using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SteamNotificationsTray.Properties;

namespace SteamNotificationsTray
{
    partial class TrayAppContext
    {
        ContextMenuStrip notificationsContextMenu;
        ToolStripProfessionalRenderer renderer;
        ToolStripMenuItem commentsMenuItem;
        ToolStripMenuItem itemsMenuItem;
        ToolStripMenuItem invitesMenuItem;
        ToolStripMenuItem giftsMenuItem;
        ToolStripMenuItem offlineMessagesMenuItem;
        ToolStripMenuItem tradeOffersMenuItem;
        ToolStripMenuItem asyncGameMenuItem;
        ToolStripMenuItem moderatorMessageMenuItem;
        ToolStripMenuItem helpRequestReplyMenuItem;

        void setupNotificationsPopup()
        {
            commentsMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.CommentsPlural, 0)
            };
            itemsMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.ItemsPlural, 0)
            };
            invitesMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.InvitesPlural, 0)
            };
            giftsMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.GiftsPlural, 0)
            };
            offlineMessagesMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.OfflineMessagesPlural, 0)
            };
            tradeOffersMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.TradeOffersPlural, 0),
                Visible = false
            };
            asyncGameMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.AsyncGamesPlural, 0),
                Visible = false
            };
            moderatorMessageMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.ModeratorMessagesPlural, 0),
                Visible = false
            };
            helpRequestReplyMenuItem = new ToolStripMenuItem
            {
                Text = string.Format(Resources.HelpRequestRepliesPlural, 0),
                Visible = false
            };
            renderer = new ToolStripProfessionalRenderer();
            notificationsContextMenu = new ContextMenuStrip();
            notificationsContextMenu.Renderer = renderer;
            notificationsContextMenu.Items.AddRange(new[] {
                commentsMenuItem,
                itemsMenuItem,
                invitesMenuItem,
                giftsMenuItem,
                offlineMessagesMenuItem,
                tradeOffersMenuItem,
                asyncGameMenuItem,
                moderatorMessageMenuItem,
                helpRequestReplyMenuItem,
            });
        }
    }
}
