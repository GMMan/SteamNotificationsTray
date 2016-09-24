using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using SteamNotificationsTray.Properties;

namespace SteamNotificationsTray
{
    public partial class SettingsForm : Form
    {
        public event EventHandler SettingsApplied;
        public event EventHandler LoggingOut;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Set up version info
            using (Icon icon = new Icon(Icon, 64, 64))
                iconPictureBox.Image = icon.ToBitmap();
            productNameLabel.Text = Application.ProductName;
            versionLabel.Text = string.Format(versionLabel.Text, Application.ProductVersion);
            var copyrightAttr = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).FirstOrDefault() as AssemblyCopyrightAttribute;
            if (copyrightAttr != null) copyrightLabel.Text = copyrightAttr.Copyright;

            loadSettings();
        }

        void loadSettings()
        {
            var settings = Settings.Default;

            // Load general settings
            intervalNumericUpDown.Value = settings.RefreshInterval / 1000;
            enableBalloonsCheckBox.Checked = settings.EnableBalloons;
            singleIconCheckBox.Checked = settings.SingleIcon;

            // Load item settings
            commentsCheckBox.Checked = settings.AlwaysShowComments;
            itemsCheckBox.Checked = settings.AlwaysShowItems;
            invitesCheckBox.Checked = settings.AlwaysShowInvites;
            giftsCheckBox.Checked = settings.AlwaysShowGifts;
            offlineMessagesCheckBox.Checked = settings.AlwaysShowOfflineMessages;
            tradeOffersCheckBox.Checked = settings.AlwaysShowTradeOffers;
            asyncGamesCheckBox.Checked = settings.AlwaysShowAsyncGames;
            moderatorMessagesCheckBox.Checked = settings.AlwaysShowModeratorMessages;
            helpRequestRepliesCheckBox.Checked = settings.AlwaysShowHelpRequestReplies;
            accountAlertsCheckBox.Checked = settings.AlwaysShowAccountAlerts;

            // Load colors
            noNotifsButton.BackColor = settings.InboxNoneColor;
            unreadNotifsButton.BackColor = settings.InboxAvailableColor;
            newNotifsButton.BackColor = settings.InboxNewColor;
            textButton.BackColor = settings.NotificationInactiveColor;
            activeTextButton.BackColor = settings.NotificationActiveColor;
            backgroundButton.BackColor = settings.NotificationPopupBackgroundColor;
            borderButton.BackColor = settings.NotificationPopupBorderColor;
            focusedButton.BackColor = settings.NotificationFocusColor;
            separatorButton.BackColor = settings.NotificationPopupSeparatorColor;
            countColorButton.BackColor = settings.NotificationCountColor;
        }

        private void resetbutton_Click(object sender, EventArgs e)
        {
            string auth = Settings.Default.Credentials;
            Settings.Default.Reset();
            Settings.Default.Credentials = auth;
            loadSettings();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
            var handler = LoggingOut;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
            applySettings();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            applySettings();
        }

        void applySettings()
        {
            var settings = Settings.Default;

            // Set general settings
            settings.RefreshInterval = (int)intervalNumericUpDown.Value * 1000;
            settings.EnableBalloons = enableBalloonsCheckBox.Checked;
            settings.SingleIcon = singleIconCheckBox.Checked;

            // Set item settings
            settings.AlwaysShowComments = commentsCheckBox.Checked;
            settings.AlwaysShowItems = itemsCheckBox.Checked;
            settings.AlwaysShowInvites = invitesCheckBox.Checked;
            settings.AlwaysShowGifts = giftsCheckBox.Checked;
            settings.AlwaysShowOfflineMessages = offlineMessagesCheckBox.Checked;
            settings.AlwaysShowTradeOffers = tradeOffersCheckBox.Checked;
            settings.AlwaysShowAsyncGames = asyncGamesCheckBox.Checked;
            settings.AlwaysShowModeratorMessages = moderatorMessagesCheckBox.Checked;
            settings.AlwaysShowHelpRequestReplies = helpRequestRepliesCheckBox.Checked;
            settings.AlwaysShowAccountAlerts = accountAlertsCheckBox.Checked;

            // Set colors
            settings.InboxNoneColor = noNotifsButton.BackColor;
            settings.InboxAvailableColor = unreadNotifsButton.BackColor;
            settings.InboxNewColor = newNotifsButton.BackColor;
            settings.NotificationInactiveColor = textButton.BackColor;
            settings.NotificationActiveColor = activeTextButton.BackColor;
            settings.NotificationPopupBackgroundColor = backgroundButton.BackColor;
            settings.NotificationPopupBorderColor = borderButton.BackColor;
            settings.NotificationFocusColor = focusedButton.BackColor;
            settings.NotificationPopupSeparatorColor = separatorButton.BackColor;
            settings.NotificationCountColor = countColorButton.BackColor;

            settings.Save();
            var handler = SettingsApplied;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void githubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/GMMan/SteamNotificationsTray");
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                Settings.Default.Reload();
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            using (var picker = new Cyotek.Windows.Forms.ColorPickerDialog())
            {
                picker.Color = button.BackColor;
                if (picker.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    button.BackColor = picker.Color;
                }
            }
        }
    }
}
