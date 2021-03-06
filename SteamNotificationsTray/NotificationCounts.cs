﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamNotificationsTray
{
    class NotificationCounts
    {
        public int Comments { get; set; } // 4
        public int Items { get; set; } // 5
        public int Invites { get; set; } // 6
        public int Gifts { get; set; } // 8
        public int OfflineMessages { get; set; } // 9
        public int TradeOffers { get; set; } // 1
        public int AsyncGames { get; set; } // 2
        public int ModeratorMessages { get; set; } // 3
        public int HelpRequestReplies { get; set; } // 10
        public int AccountAlerts { get; set; } // 11

        public int TotalNotifications { get; set; }
    }
}
