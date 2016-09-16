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
        CookieContainer cookies;

        public NotificationCounts PrevCounts { get; private set; }
        public NotificationCounts CurrentCounts { get; private set; }

        public async Task<NotificationCounts> PollNotificationCountsAsync()
        {
            HttpClientHandler handler = new HttpClientHandler { CookieContainer = cookies };
            using (HttpClient client = new HttpClient(handler))
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
                            counts.AsyncGames = (int)notif.Value;
                            break;
                        case "3":
                            counts.ModeratorMessages = (int)notif.Value;
                            break;
                        case "10":
                            counts.HelpRequestReplies = (int)notif.Value;
                            break;
                        case "11":
                            counts.AccountAlerts = (int)notif.Value;
                            break;
                    }
                    counts.TotalNotifications += (int)notif.Value;
                }
                PrevCounts = CurrentCounts;
                CurrentCounts = counts;
                return counts;
            }
        }

        public void SetCookies(CookieContainer cookies)
        {
            this.cookies = cookies;
        }

        public void SaveCredentials()
        {
            if (cookies != null) CredentialStore.SaveCommunityCookies(cookies);
        }
    }
}
