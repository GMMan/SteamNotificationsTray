using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace SteamNotificationsTray
{
    class NotificationsClient
    {
        HttpClient client;
        HttpClientHandler handler;

        public NotificationCounts PrevCounts { get; private set; }
        public NotificationCounts CurrentCounts { get; private set; }

        public NotificationsClient()
        {
            handler = new HttpClientHandler();
            client = new HttpClient(handler);
        }

        public async Task<NotificationCounts> PollNotificationCountsAsync()
        {
            string response = await client.GetStringAsync("https://steamcommunity.com/actions/GetNotificationCounts");
            if (response == "null") return null;
            NotificationCounts counts = new NotificationCounts();
            JObject respObj = JObject.Parse(response);
            JToken notifsObj = respObj["notifications"];
            foreach (JProperty notif in notifsObj)
            {
                switch (notif.Name)
                {
                    case "4":
                        counts.Comments = (int)notif.Value;
                        break;
                    case "5":
                        counts.Items = (int)notif.Value;
                        break;
                    case "6":
                        counts.Invites = (int)notif.Value;
                        break;
                    case "8":
                        counts.Gifts = (int)notif.Value;
                        break;
                    case "9":
                        counts.OfflineMessages = (int)notif.Value;
                        break;
                    case "1":
                        counts.TradeOffers = (int)notif.Value;
                        break;
                    case "2":
                        counts.AsyncGame = (int)notif.Value;
                        break;
                    case "3":
                        counts.ModeratorMessage = (int)notif.Value;
                        break;
                    case "10":
                        counts.HelpRequestReply = (int)notif.Value;
                        break;
                }
                ++counts.TotalNotifications;
            }
            PrevCounts = CurrentCounts;
            CurrentCounts = counts;
            return counts;
        }

        public void SetCookies(CookieContainer cookies)
        {
            handler.CookieContainer = cookies;
        }
    }
}
