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
        ToolStripSeparator itemsSeparator;
        ToolStripMenuItem itemsMenuItem;
        ToolStripSeparator invitesSeparator;
        ToolStripMenuItem invitesMenuItem;
        ToolStripSeparator giftsSeparator;
        ToolStripMenuItem giftsMenuItem;
        ToolStripSeparator offlineMessagesSeparator;
        ToolStripMenuItem offlineMessagesMenuItem;
        ToolStripSeparator tradeOffersSeparator;
        ToolStripMenuItem tradeOffersMenuItem;
        ToolStripSeparator asyncGameMenuSeparator;
        ToolStripMenuItem asyncGameMenuItem;
        ToolStripSeparator moderatorMessageSeparator;
        ToolStripMenuItem moderatorMessageMenuItem;
        ToolStripSeparator helpRequestReplySeparator;
        ToolStripMenuItem helpRequestReplyMenuItem;

        void setupNotificationsPopup()
        {
            commentsMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.CommentsPlural, 0),
                Image = Resources.IconComments
            };
            itemsSeparator = new ToolStripSeparator();
            itemsMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.ItemsPlural, 0),
                Image = Resources.IconItems
            };
            invitesSeparator = new ToolStripSeparator();
            invitesMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.InvitesPlural, 0),
                Image = Resources.IconInvites
            };
            giftsSeparator = new ToolStripSeparator();
            giftsMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.GiftsPlural, 0),
                Image = Resources.IconGifts
            };
            offlineMessagesSeparator = new ToolStripSeparator();
            offlineMessagesMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.OfflineMessagesPlural, 0),
                Image = Resources.IconOfflineMessages,
            };
            tradeOffersSeparator = new ToolStripSeparator
            {
                Visible = false
            };
            tradeOffersMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.TradeOffersPlural, 0),
                Image = Resources.IconTradeOffers,
                Visible = false
            };
            asyncGameMenuSeparator = new ToolStripSeparator
            {
                Visible = false
            };
            asyncGameMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.AsyncGamesPlural, 0),
                Image = Resources.IconAsyncGames,
                Visible = false
            };
            moderatorMessageSeparator = new ToolStripSeparator
            {
                Visible = false
            };
            moderatorMessageMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.ModeratorMessagesPlural, 0),
                Image = Resources.IconModeratorMessages,
                Visible = false
            };
            helpRequestReplySeparator = new ToolStripSeparator
            {
                Visible = false
            };
            helpRequestReplyMenuItem = new ToolStripMenuItem
            {
                Tag = 0,
                Text = string.Format(Resources.HelpRequestRepliesPlural, 0),
                Image = Resources.IconModeratorMessages,
                Visible = false
            };
            renderer = new NotificationsMenuRenderer();
            notificationsContextMenu = new ContextMenuStrip();
            notificationsContextMenu.Renderer = renderer;
            notificationsContextMenu.Items.AddRange(new ToolStripItem[] {
                 commentsMenuItem,
                 itemsSeparator,
                 itemsMenuItem,
                 invitesSeparator,
                 invitesMenuItem,
                 giftsSeparator,
                 giftsMenuItem,
                 offlineMessagesSeparator,
                 offlineMessagesMenuItem,
                 tradeOffersSeparator,
                 tradeOffersMenuItem,
                 asyncGameMenuSeparator,
                 asyncGameMenuItem,
                 moderatorMessageSeparator,
                 moderatorMessageMenuItem,
                 helpRequestReplySeparator,
                 helpRequestReplyMenuItem,
            });

            updatePopupColors();
        }

        void updatePopupColors()
        {
            var settings = Settings.Default;
            notificationsContextMenu.BackColor = settings.NotificationPopupBackgroundColor;
            notificationsContextMenu.ForeColor = settings.NotificationInactiveColor;
        }
    }
}
