using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamNotificationsTray.WebLogin.Models
{
    class DoLoginRequest
    {
        public string Password { get; set; } // Assume already encrypted by caller
        public string Username { get; set; }
        public string TwoFactorCode { get; set; }
        public string EmailAuth { get; set; }
        public string LoginFriendlyName { get; set; }
        public long CaptchaGid { get; set; }
        public string CaptchaText { get; set; }
        public ulong? EmailSteamId { get; set; }
        public long RsaTimeStamp { get; set; }
        public bool RememberLogin { get; set; }
    }
}
