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
                Image = Resources.IconComments
            };
            itemsSeparator = new ToolStripSeparator();
            itemsMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconItems
            };
            invitesSeparator = new ToolStripSeparator();
            invitesMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconInvites
            };
            giftsSeparator = new ToolStripSeparator();
            giftsMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconGifts
            };
            offlineMessagesSeparator = new ToolStripSeparator();
            offlineMessagesMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconOfflineMessages,
            };
            tradeOffersSeparator = new ToolStripSeparator();
            tradeOffersMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconTradeOffers,
            };
            asyncGameMenuSeparator = new ToolStripSeparator();
            asyncGameMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconAsyncGames,
            };
            moderatorMessageSeparator = new ToolStripSeparator();
            moderatorMessageMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconModeratorMessages,
            };
            helpRequestReplySeparator = new ToolStripSeparator();
            helpRequestReplyMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconModeratorMessages,
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
            updatePopupCounts(new NotificationCounts());

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
            var settings = Settings.Default;
            commentsMenuItem.Text = counts.Comments == 1 ? Resources.CommentsSingular : string.Format(Resources.CommentsPlural, counts.Comments);
            commentsMenuItem.Tag = counts.Comments;
            commentsMenuItem.Visible = counts.Comments > 0 || settings.AlwaysShowComments;

            itemsMenuItem.Text = counts.Items == 1 ? Resources.ItemsSingular : string.Format(Resources.ItemsPlural, counts.Items);
            itemsMenuItem.Tag = counts.Items;
            itemsSeparator.Visible = itemsMenuItem.Visible = counts.Items > 0 || settings.AlwaysShowItems;
 
            invitesMenuItem.Text = counts.Invites == 1 ? Resources.InvitesSingular : string.Format(Resources.InvitesPlural, counts.Invites);
            invitesMenuItem.Tag = counts.Invites;
            invitesSeparator.Visible = invitesMenuItem.Visible = counts.Invites > 0 || settings.AlwaysShowInvites;
 
            giftsMenuItem.Text = counts.Gifts == 1 ? Resources.GiftsSingular : string.Format(Resources.GiftsPlural, counts.Gifts);
            giftsMenuItem.Tag = counts.Gifts;
            giftsSeparator.Visible = giftsMenuItem.Visible = counts.Gifts > 0 || settings.AlwaysShowGifts;
 
            offlineMessagesMenuItem.Text = counts.OfflineMessages == 1 ? Resources.OfflineMessagesSingular : string.Format(Resources.OfflineMessagesPlural, counts.OfflineMessages);
            offlineMessagesMenuItem.Tag = counts.OfflineMessages;
            offlineMessagesSeparator.Visible = offlineMessagesMenuItem.Visible = counts.OfflineMessages > 0 || settings.AlwaysShowOfflineMessages;
   
            tradeOffersMenuItem.Text = counts.TradeOffers == 1 ? Resources.TradeOffersSingular : string.Format(Resources.TradeOffersPlural, counts.TradeOffers);
            tradeOffersMenuItem.Tag = counts.TradeOffers;
            tradeOffersSeparator.Visible = tradeOffersMenuItem.Visible = counts.TradeOffers > 0 || settings.AlwaysShowTradeOffers;
    
            asyncGameMenuItem.Text = counts.AsyncGames == 1 ? Resources.AsyncGamesSingular : string.Format(Resources.AsyncGamesPlural, counts.AsyncGames);
            asyncGameMenuItem.Tag = counts.AsyncGames;
            asyncGameMenuSeparator.Visible = asyncGameMenuItem.Visible = counts.AsyncGames > 0 || settings.AlwaysShowAsyncGames;
   
            moderatorMessageMenuItem.Text = counts.ModeratorMessages == 1 ? Resources.ModeratorMessagesSingular : string.Format(Resources.ModeratorMessagesPlural, counts.ModeratorMessages);
            moderatorMessageMenuItem.Tag = counts.ModeratorMessages;
            moderatorMessageSeparator.Visible = moderatorMessageMenuItem.Visible = counts.ModeratorMessages > 0 || settings.AlwaysShowModeratorMessages;
    
            helpRequestReplyMenuItem.Text = counts.HelpRequestReplies == 1 ? Resources.HelpRequestRepliesSingular : string.Format(Resources.HelpRequestRepliesPlural, counts.HelpRequestReplies);
            helpRequestReplyMenuItem.Tag = counts.HelpRequestReplies;
            helpRequestReplySeparator.Visible = helpRequestReplyMenuItem.Visible = counts.HelpRequestReplies > 0 || settings.AlwaysShowHelpRequestReplies;
        }
    }
}
