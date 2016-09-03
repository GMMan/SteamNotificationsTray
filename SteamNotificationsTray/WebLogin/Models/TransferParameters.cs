using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SteamNotificationsTray.WebLogin.Models
{
    class TransferParameters
    {
        [JsonProperty("steamid")]
        public ulong SteamId { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("auth")]
        public string Auth { get; set; }
        [JsonProperty("remember_login")]
        public bool RememberLogin { get; set; }
        [JsonProperty("webcookie")]
        public string WebCookie { get; set; }
        [JsonProperty("token_secure")]
        public string TokenSecure { get; set; }

        // Special property that's not in the actual response
        public string RememberLoginToken { get; set; }
    }
}
