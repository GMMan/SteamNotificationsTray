using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
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

            commentsMenuItem.Click += commentsMenuItem_Click;
            itemsMenuItem.Click += itemsMenuItem_Click;
            invitesMenuItem.Click += invitesMenuItem_Click;
            giftsMenuItem.Click += giftsMenuItem_Click;
            offlineMessagesMenuItem.Click += offlineMessagesMenuItem_Click;
            tradeOffersMenuItem.Click += tradeOffersMenuItem_Click;
            asyncGameMenuItem.Click += asyncGameMenuItem_Click;
            moderatorMessageMenuItem.Click += moderatorMessageMenuItem_Click;
            helpRequestReplyMenuItem.Click += helpRequestReplyMenuItem_Click;
        }

        void commentsMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/my/commentnotifications/");
        }

        void itemsMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/my/inventory/");
        }

        void invitesMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/my/home/invites/");
        }

        void giftsMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/my/inventory/#pending_gifts");
        }

        void offlineMessagesMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/chat/");
        }

        void tradeOffersMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/my/tradeoffers/");
        }

        void asyncGameMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/my/gamenotifications");
        }

        void moderatorMessageMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/my/moderatormessages");
        }

        void helpRequestReplyMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://help.steampowered.com/en/wizard/HelpRequests");
        }

        void updatePopupColors()
        {
            var settings = Settings.Default;
            notificationsContextMenu.BackColor = settings.NotificationPopupBackgroundColor;
            notificationsContextMenu.ForeColor = settings.NotificationInactiveColor;
        }

        void updatePopupCounts(NotificationCounts counts)
        {
            commentsMenuItem.Text = counts.Comments == 1 ? Resources.CommentsSingular : string.Format(Resources.CommentsPlural, counts.Comments);
            commentsMenuItem.Tag = counts.Comments;
            itemsMenuItem.Text = counts.Items == 1 ? Resources.ItemsSingular : string.Format(Resources.ItemsPlural, counts.Items);
            itemsMenuItem.Tag = counts.Items;
            invitesMenuItem.Text = counts.Invites == 1 ? Resources.InvitesSingular : string.Format(Resources.InvitesPlural, counts.Invites);
            invitesMenuItem.Tag = counts.Invites;
            giftsMenuItem.Text = counts.Gifts == 1 ? Resources.GiftsSingular : string.Format(Resources.GiftsPlural, counts.Gifts);
            giftsMenuItem.Tag = counts.Gifts;
            offlineMessagesMenuItem.Text = counts.OfflineMessages == 1 ? Resources.OfflineMessagesSingular : string.Format(Resources.OfflineMessagesPlural, counts.OfflineMessages);
            offlineMessagesMenuItem.Tag = counts.OfflineMessages;
            tradeOffersMenuItem.Text = counts.TradeOffers == 1 ? Resources.TradeOffersSingular : string.Format(Resources.TradeOffersPlural, counts.TradeOffers);
            tradeOffersMenuItem.Tag = counts.TradeOffers;
            tradeOffersSeparator.Visible = tradeOffersMenuItem.Visible = counts.TradeOffers > 0;
            asyncGameMenuItem.Text = counts.AsyncGames == 1 ? Resources.AsyncGamesSingular : string.Format(Resources.AsyncGamesPlural, counts.AsyncGames);
            asyncGameMenuItem.Tag = counts.AsyncGames;
            asyncGameMenuSeparator.Visible = asyncGameMenuItem.Visible = counts.AsyncGames > 0;
            moderatorMessageMenuItem.Text = counts.ModeratorMessages == 1 ? Resources.ModeratorMessagesSingular : string.Format(Resources.ModeratorMessagesPlural, counts.ModeratorMessages);
            moderatorMessageMenuItem.Tag = counts.ModeratorMessages;
            moderatorMessageSeparator.Visible = moderatorMessageMenuItem.Visible = counts.ModeratorMessages > 0;
            helpRequestReplyMenuItem.Text = counts.HelpRequestReplies == 1 ? Resources.HelpRequestRepliesSingular : string.Format(Resources.HelpRequestRepliesPlural, counts.HelpRequestReplies);
            helpRequestReplyMenuItem.Tag = counts.HelpRequestReplies;
            helpRequestReplySeparator.Visible = helpRequestReplyMenuItem.Visible = counts.HelpRequestReplies > 0;
        }
    }
}
