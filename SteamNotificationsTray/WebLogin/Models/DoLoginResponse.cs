using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SteamNotificationsTray.WebLogin.Models
{
    class DoLoginResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("requires_twofactor")]
        public bool RequiresTwoFactor { get; set; }
        [JsonProperty("clear_password_field")]
        public bool ClearPasswordField { get; set; }
        [JsonProperty("captcha_needed")]
        public bool CaptchaNeeded { get; set; }
        [JsonProperty("captcha_gid")]
        public long CaptchaGid { get; set; }
        [JsonProperty("bad_captcha")]
        public bool IsBadCaptcha { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("emailauth_needed")]
        public bool EmailAuthNeeded { get; set; }
        [JsonProperty("emaildomain")]
        public string EmailDomain { get; set; } // if empty, incorrect code
        [JsonProperty("emailsteamid")]
        public ulong? EmailSteamId { get; set; }

        [JsonProperty("denied_ipt")]
        public bool DeniedIpt { get; set; }

        [JsonProperty("login_complete")]
        public bool LoginComplete { get; set; }
        [JsonProperty("transfer_url")]
        public string TransferUrl { get; set; }
        [JsonProperty("transfer_urls")]
        public List<string> TransferUrls { get; set; }
        [JsonProperty("transfer_parameters")]
        public TransferParameters TransferParameters { get; set; }

        public DoLoginRequest ToRequest()
        {
            return new DoLoginRequest
            {
                CaptchaGid = CaptchaGid,
                EmailSteamId = EmailSteamId
            };
        }
    }
}
