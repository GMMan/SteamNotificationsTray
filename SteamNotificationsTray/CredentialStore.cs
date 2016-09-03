using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using SteamNotificationsTray.WebLogin.Models;
using Newtonsoft.Json;

namespace SteamNotificationsTray
{
    static class CredentialStore
    {
        static byte[] GetStrongNameKey()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            return assembly.GetName().GetPublicKey();
        }

        internal static TransferParameters GetTransferParameters()
        {
            var encryptedParams = Properties.Settings.Default.Credentials;
            if (string.IsNullOrEmpty(encryptedParams)) return null;
            try
            {
                byte[] encryptedBlob = Convert.FromBase64String(encryptedParams);
                byte[] decryptedBlob = ProtectedData.Unprotect(encryptedBlob, GetStrongNameKey(), DataProtectionScope.CurrentUser);
                string decryptedParams = Encoding.UTF8.GetString(decryptedBlob);
                return JsonConvert.DeserializeObject<TransferParameters>(decryptedParams);
            }
            catch
            {
                return null;
            }
        }

        internal static void SaveTransferParameters(TransferParameters transferParams)
        {
            TransferParameters newParams = new TransferParameters
            {
                SteamId = transferParams.SteamId,
                WebCookie = transferParams.WebCookie,
                RememberLoginToken = transferParams.RememberLoginToken
            };
            string serialized = JsonConvert.SerializeObject(newParams);
            byte[] blob = Encoding.UTF8.GetBytes(serialized);
            byte[] cryptedBlob = ProtectedData.Protect(blob, GetStrongNameKey(), DataProtectionScope.CurrentUser);
            string cryptedParams = Convert.ToBase64String(cryptedBlob);
            Properties.Settings.Default.Credentials = cryptedParams;
            Properties.Settings.Default.Save();
        }

        internal static CookieContainer GetCommunityCookies()
        {
            TransferParameters transferParams = GetTransferParameters();
            if (transferParams == null) return null;
            CookieContainer cookies = new CookieContainer();
            // 1. Persistent login cookie
            cookies.Add(new Cookie("steamRememberLogin", WebUtility.UrlEncode(string.Format("{0}||{1}", transferParams.SteamId, transferParams.RememberLoginToken)), "/", "steamcommunity.com"));
            // 2. Machine auth token
            cookies.Add(new Cookie(string.Format("steamMachineAuth{0}", transferParams.SteamId), transferParams.WebCookie, "/", "steamcommunity.com") { Secure = true });
            return cookies;
        }

        internal static void SaveCommunityCookies(CookieContainer cookies)
        {
            TransferParameters transferParams = GetTransferParameters();
            if (transferParams == null) transferParams = new TransferParameters();
            foreach (Cookie cookie in cookies.GetCookies(new Uri("https://steamcommunity.com/")))
            {
                if (cookie.Name == "steamRememberLogin")
                {
                    string[] bits = WebUtility.UrlDecode(cookie.Value).Split(new[] { "||" }, 2, StringSplitOptions.None);
                    transferParams.SteamId = ulong.Parse(bits[0]);
                    transferParams.RememberLoginToken = bits[1];
                }
                else if ( cookie.Name.StartsWith("steamMachineAuth"))
                {
                    string steamId = cookie.Name.Replace("steamMachineAuth", "");
                    transferParams.SteamId = ulong.Parse(steamId);
                    transferParams.WebCookie = cookie.Value;
                }
            }
            SaveTransferParameters(transferParams);
        }

        internal static void ClearCredentials()
        {
            Properties.Settings.Default.Credentials = null;
            Properties.Settings.Default.Save();
        }

        internal static bool CredentialsAvailable()
        {
            return !string.IsNullOrEmpty(Properties.Settings.Default.Credentials);
        }
    }
}
