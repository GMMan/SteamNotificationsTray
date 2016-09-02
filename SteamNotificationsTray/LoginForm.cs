using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using SteamNotificationsTray.WebLogin;
using SteamNotificationsTray.WebLogin.Models;
using Newtonsoft.Json;

namespace SteamNotificationsTray
{
    public partial class LoginForm : Form
    {
        DoLoginResponse loginResponse;
        SteamWebLogin loginClient = new SteamWebLogin();

        public LoginForm()
        {
            InitializeComponent();
        }

        #region Credential saving
        static byte[] GetStrongNameKey()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            return assembly.GetName().GetPublicKey();
        }

        static TransferParameters GetTransferParameters()
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

        static void SaveTransferParameters(TransferParameters transferParams)
        {
            string serialized = JsonConvert.SerializeObject(transferParams);
            byte[] blob = Encoding.UTF8.GetBytes(serialized);
            byte[] cryptedBlob = ProtectedData.Protect(blob, GetStrongNameKey(), DataProtectionScope.CurrentUser);
            string cryptedParams = Convert.ToBase64String(cryptedBlob);
            Properties.Settings.Default.Credentials = cryptedParams;
            Properties.Settings.Default.Save();
        }
        #endregion

        byte[] hexToBytes(string str)
        {
            if (str.Length % 2 != 0) throw new ArgumentException("Input does not have even bytes.", "str");
            byte[] data = new byte[str.Length / 2];
            for (int i = 0; i < data.Length; ++i)
            {
                string part = str.Substring(i * 2, 2);
                byte b;
                if (!byte.TryParse(part, System.Globalization.NumberStyles.HexNumber, null, out b))
                    throw new ArgumentException("Input contains non-hex characters.", "str");
                data[i] = b;
            }
            return data;
        }

        async Task<bool> doLogin()
        {
            // Assume validity checks have been done
            // 1. Get RSA key
            GetRsaKeyResponse rsaResponse = await loginClient.GetRsaKeyAsync(usernameTextBox.Text);
            if (!rsaResponse.Success)
            {
                setMessage(!string.IsNullOrEmpty(rsaResponse.Message) ? rsaResponse.Message : "Can't get RSA key for sending login info.");
                return false;
            }

            // 2. Encrypt password
            string encryptedPassword;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(new RSAParameters
                {
                    Modulus = hexToBytes(rsaResponse.PublicKeyMod),
                    Exponent = hexToBytes(rsaResponse.PublicKeyExp)
                });

                byte[] passwordBlob = Encoding.UTF8.GetBytes(passwordTextBox.Text);
                byte[] crypted = rsa.Encrypt(passwordBlob, false);
                encryptedPassword = Convert.ToBase64String(crypted);
            }

            // 3. Send request to server
            DoLoginRequest request = new DoLoginRequest
            {
                Password = encryptedPassword,
                Username = usernameTextBox.Text,
                TwoFactorCode = mobileAuthTextBox.Text,
                EmailAuth = emailAuthTextBox.Text,
                LoginFriendlyName = friendlyNameTextBox.Text,
                CaptchaText = captchaTextBox.Text,
                RsaTimeStamp = rsaResponse.Timestamp,
                RememberLogin = true
            };

            if (loginResponse != null)
            {
                request.CaptchaGid = loginResponse.CaptchaGid;
                request.EmailSteamId = loginResponse.EmailSteamId;
            }
            else
            {
                request.CaptchaGid = -1;
            }

            loginResponse = await loginClient.DoLoginAsync(request);
            if (loginResponse == null) return false;
            return loginResponse.Success && loginResponse.LoginComplete;
        }

        void updateGuiFromResponse()
        {
            if (loginResponse == null)
            {
                emailAuthTextBox.Text = string.Empty;
                emailCodePanel.Visible = false;
                mobileAuthTextBox.Text = string.Empty;
                mobileAuthPanel.Visible = false;
                if (captchaPictureBox.Image != null) captchaPictureBox.Image.Dispose();
                captchaPictureBox.Image = null;
                captchaTextBox.Text = string.Empty;
                captchaPanel.Visible = false;
            }
            else
            {
                emailCodePanel.Visible = loginResponse.EmailAuthNeeded;
                mobileAuthPanel.Visible = loginResponse.RequiresTwoFactor;
                captchaPanel.Visible = loginResponse.CaptchaNeeded || loginResponse.IsBadCaptcha;
                if (loginResponse.ClearPasswordField) passwordTextBox.Text = string.Empty;
            }
        }

        void setMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(setMessage), message);
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                messageLabel.Visible = false;
            }
            else
            {
                messageLabel.Visible = true;
                messageLabel.Text = message;
            }
        }

        bool validateEntry()
        {
            List<string> messages = new List<string>();
            if (usernameTextBox.TextLength == 0)
                messages.Add("your username");
            if (passwordTextBox.TextLength == 0)
                messages.Add("your password");

            if (loginResponse != null)
            {
                if (loginResponse.EmailAuthNeeded && emailAuthTextBox.TextLength == 0)
                    messages.Add("the Steam Guard code from your email");
                if (loginResponse.RequiresTwoFactor && mobileAuthTextBox.TextLength == 0)
                    messages.Add("the Mobile Authenticator code");
                if (loginResponse.CaptchaNeeded && captchaTextBox.TextLength == 0)
                    messages.Add("the CAPTCHA");
            }

            if (messages.Count > 0)
            {
                MessageBox.Show(this, string.Format("Please enter {0}.", string.Join(", ", messages)), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        async Task loadNewCaptcha()
        {
            if (loginResponse == null) return;
            await Task.Run(() => captchaPictureBox.LoadAsync(loginClient.GetRenderCaptchaUrl(loginResponse.CaptchaGid)));
        }

        async Task refreshCaptcha()
        {
            if (loginResponse == null) return;
            loginResponse.CaptchaGid = await loginClient.RefreshCaptchaAsync();
            if (loginResponse.CaptchaGid == -1)
            {
                captchaPanel.Visible = false;
            }
            else
            {
                await Task.Run(() => captchaPictureBox.LoadAsync(loginClient.GetRenderCaptchaUrl(loginResponse.CaptchaGid)));
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            loginButton.Enabled = false;
            cancelButton.Enabled = false;
            try
            {
                if (!validateEntry()) return;
                bool success = await Task.Run<bool>(() => doLogin());
                if (success)
                {
                    SaveTransferParameters(loginResponse.TransferParameters);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    updateGuiFromResponse();
                    if (loginResponse == null)
                    {
                        messageLabel.Text = "Could not communicate with Steam Community.";
                    }
                    else if (!string.IsNullOrEmpty(loginResponse.Message))
                    {
                        messageLabel.Text = loginResponse.Message;
                    }
                    else
                    {
                        messageLabel.Text = string.Empty;
                        if (loginResponse.EmailAuthNeeded)
                        {
                            if (string.IsNullOrEmpty(loginResponse.EmailDomain))
                            {
                                messageLabel.Text += "Steam Guard code incorrect. ";
                            }
                            else
                            {
                                messageLabel.Text += "Steam Guard code required. ";
                            }
                        }
                        if (loginResponse.RequiresTwoFactor)
                            messageLabel.Text += "Mobile Authenticator code required. ";
                        if (loginResponse.CaptchaNeeded && !loginResponse.IsBadCaptcha)
                            messageLabel.Text += "CAPTCHA entry required. ";
                        if (loginResponse.IsBadCaptcha)
                            messageLabel.Text += "CAPTCHA entry incorrect. ";
                        if (loginResponse.DeniedIpt)
                            messageLabel.Text += "Intel® Identity Protection Technology access denied. ";
                    }
                    messageLabel.Visible = messageLabel.Text.Length > 0;
                    if (loginResponse != null && loginResponse.CaptchaNeeded)
                        await loadNewCaptcha();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not log in: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loginButton.Enabled = true;
                cancelButton.Enabled = true;
            }
        }

        private void captchaRefreshButton_Click(object sender, EventArgs e)
        {
            Task.Run(() => refreshCaptcha());
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            loginResponse = null;
            updateGuiFromResponse();
        }
    }
}
