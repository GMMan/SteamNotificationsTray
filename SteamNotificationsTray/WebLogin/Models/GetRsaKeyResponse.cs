using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SteamNotificationsTray.WebLogin.Models
{
    class GetRsaKeyResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("publickey_mod")]
        public string PublicKeyMod { get; set; }
        [JsonProperty("publickey_exp")]
        public string PublicKeyExp { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        [JsonProperty("token_gid")]
        public string TokenGid { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
