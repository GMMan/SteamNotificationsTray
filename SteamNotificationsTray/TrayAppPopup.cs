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
        ToolStripSeparator accountAlertReplySeparator;
        ToolStripMenuItem accountAlertReplyMenuItem;

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
            accountAlertReplySeparator = new ToolStripSeparator();
            accountAlertReplyMenuItem = new ToolStripMenuItem
            {
                Image = Resources.IconAccountAlerts,
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
                 accountAlertReplySeparator,
                 accountAlertReplyMenuItem,
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
            accountAlertReplyMenuItem.Click += accountAlertReplyMenuItem_Click;
        }

        void commentsMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                Process.Start("steam://url/CommentNotifications");
            else
                Process.Start("https://steamcommunity.com/my/commentnotifications/");
        }

        void itemsMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                Process.Start("steam://url/CommunityInventory");
            else
                Process.Start("https://steamcommunity.com/my/inventory/");
        }

        void invitesMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                Process.Start("steam://url/SteamIDInvitesPage");
            else
                Process.Start("https://steamcommunity.com/my/home/invites/");
        }

        void giftsMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                Process.Start("steam://url/ManageGiftsPage");
            else
                Process.Start("https://steamcommunity.com/my/inventory/#pending_gifts");
        }

        void offlineMessagesMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                // Web chat has a friends list, so we open the Friends window here
                Process.Start("steam://open/friends");
            else
                Process.Start("https://steamcommunity.com/chat/");
        }

        void tradeOffersMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                Process.Start("steam://url/TradeOffers");
            else
                Process.Start("https://steamcommunity.com/my/tradeoffers/");
        }

        void asyncGameMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                Process.Start("steam://url/AsyncGames");
            else
                Process.Start("https://steamcommunity.com/my/gamenotifications");
        }

        void moderatorMessageMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                Process.Start("steam://url/ModeratorMessages");
            else
                Process.Start("https://steamcommunity.com/my/moderatormessages");
        }

        void helpRequestReplyMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.UseSteamBrowserProtocolLinks)
                Process.Start("steam://url/MyHelpRequests");
            else
                Process.Start("https://help.steampowered.com/en/wizard/HelpRequests");
        }

        void accountAlertReplyMenuItem_Click(object sender, EventArgs e)
        {
            // No Steam Browser Protocol link needed here, because the window will remain
            // open until the alert is dismissed. Should we open Steam or the browser?
            Process.Start("https://store.steampowered.com/supportmessages/");
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
            commentsMenuItem.Available = counts.Comments > 0 || settings.AlwaysShowComments;

            itemsMenuItem.Text = counts.Items == 1 ? Resources.ItemsSingular : string.Format(Resources.ItemsPlural, counts.Items);
            itemsMenuItem.Tag = counts.Items;
            itemsSeparator.Available = itemsMenuItem.Available = counts.Items > 0 || settings.AlwaysShowItems;

            invitesMenuItem.Text = counts.Invites == 1 ? Resources.InvitesSingular : string.Format(Resources.InvitesPlural, counts.Invites);
            invitesMenuItem.Tag = counts.Invites;
            invitesSeparator.Available = invitesMenuItem.Available = counts.Invites > 0 || settings.AlwaysShowInvites;

            giftsMenuItem.Text = counts.Gifts == 1 ? Resources.GiftsSingular : string.Format(Resources.GiftsPlural, counts.Gifts);
            giftsMenuItem.Tag = counts.Gifts;
            giftsSeparator.Available = giftsMenuItem.Available = counts.Gifts > 0 || settings.AlwaysShowGifts;

            offlineMessagesMenuItem.Text = counts.OfflineMessages == 1 ? Resources.OfflineMessagesSingular : string.Format(Resources.OfflineMessagesPlural, counts.OfflineMessages);
            offlineMessagesMenuItem.Tag = counts.OfflineMessages;
            offlineMessagesSeparator.Available = offlineMessagesMenuItem.Available = counts.OfflineMessages > 0 || settings.AlwaysShowOfflineMessages;

            tradeOffersMenuItem.Text = counts.TradeOffers == 1 ? Resources.TradeOffersSingular : string.Format(Resources.TradeOffersPlural, counts.TradeOffers);
            tradeOffersMenuItem.Tag = counts.TradeOffers;
            tradeOffersSeparator.Available = tradeOffersMenuItem.Available = counts.TradeOffers > 0 || settings.AlwaysShowTradeOffers;

            asyncGameMenuItem.Text = counts.AsyncGames == 1 ? Resources.AsyncGamesSingular : string.Format(Resources.AsyncGamesPlural, counts.AsyncGames);
            asyncGameMenuItem.Tag = counts.AsyncGames;
            asyncGameMenuSeparator.Available = asyncGameMenuItem.Available = counts.AsyncGames > 0 || settings.AlwaysShowAsyncGames;

            moderatorMessageMenuItem.Text = counts.ModeratorMessages == 1 ? Resources.ModeratorMessagesSingular : string.Format(Resources.ModeratorMessagesPlural, counts.ModeratorMessages);
            moderatorMessageMenuItem.Tag = counts.ModeratorMessages;
            moderatorMessageSeparator.Available = moderatorMessageMenuItem.Available = counts.ModeratorMessages > 0 || settings.AlwaysShowModeratorMessages;

            helpRequestReplyMenuItem.Text = counts.HelpRequestReplies == 1 ? Resources.HelpRequestRepliesSingular : string.Format(Resources.HelpRequestRepliesPlural, counts.HelpRequestReplies);
            helpRequestReplyMenuItem.Tag = counts.HelpRequestReplies;
            helpRequestReplySeparator.Available = helpRequestReplyMenuItem.Available = counts.HelpRequestReplies > 0 || settings.AlwaysShowHelpRequestReplies;

            accountAlertReplyMenuItem.Text = counts.AccountAlerts == 1 ? Resources.AccountAlertsSingular : string.Format(Resources.AccountAlertsPlural, counts.AccountAlerts);
            accountAlertReplyMenuItem.Tag = counts.AccountAlerts;
            accountAlertReplySeparator.Available = accountAlertReplyMenuItem.Available = counts.AccountAlerts > 0 || settings.AlwaysShowAccountAlerts;

            // Hide top separator if it's the first item
            foreach (ToolStripItem item in notificationsContextMenu.Items)
            {
                if (item.Available)
                {
                    if (item is ToolStripMenuItem) break;
                    item.Available = false;
                    break;
                }
            }
        }
    }
}
