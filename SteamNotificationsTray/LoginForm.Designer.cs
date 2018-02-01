﻿namespace SteamNotificationsTray
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.emailCodePanel = new System.Windows.Forms.Panel();
            this.friendlyNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.emailAuthTextBox = new System.Windows.Forms.TextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.mobileAuthPanel = new System.Windows.Forms.Panel();
            this.mobileAuthTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.captchaPanel = new System.Windows.Forms.Panel();
            this.captchaTextBox = new System.Windows.Forms.TextBox();
            this.captchaRefreshButton = new System.Windows.Forms.Button();
            this.captchaPictureBox = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mainLayoutPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.emailCodePanel.SuspendLayout();
            this.mobileAuthPanel.SuspendLayout();
            this.captchaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captchaPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTextBox.Location = new System.Drawing.Point(16, 31);
            this.usernameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(332, 22);
            this.usernameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(16, 79);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(332, 22);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainLayoutPanel.AutoSize = true;
            this.mainLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainLayoutPanel.ColumnCount = 1;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.Controls.Add(this.buttonsPanel, 0, 4);
            this.mainLayoutPanel.Controls.Add(this.emailCodePanel, 0, 1);
            this.mainLayoutPanel.Controls.Add(this.messageLabel, 0, 0);
            this.mainLayoutPanel.Controls.Add(this.mobileAuthPanel, 0, 2);
            this.mainLayoutPanel.Controls.Add(this.captchaPanel, 0, 3);
            this.mainLayoutPanel.Location = new System.Drawing.Point(8, 111);
            this.mainLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 5;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.Size = new System.Drawing.Size(349, 371);
            this.mainLayoutPanel.TabIndex = 4;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.AutoSize = true;
            this.buttonsPanel.Controls.Add(this.cancelButton);
            this.buttonsPanel.Controls.Add(this.loginButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.Location = new System.Drawing.Point(4, 331);
            this.buttonsPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(341, 36);
            this.buttonsPanel.TabIndex = 4;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(237, 4);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // loginButton
            // 
            this.loginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loginButton.Location = new System.Drawing.Point(129, 4);
            this.loginButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(100, 28);
            this.loginButton.TabIndex = 0;
            this.loginButton.Text = "Sign in";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // emailCodePanel
            // 
            this.emailCodePanel.AutoSize = true;
            this.emailCodePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.emailCodePanel.Controls.Add(this.friendlyNameTextBox);
            this.emailCodePanel.Controls.Add(this.label5);
            this.emailCodePanel.Controls.Add(this.label3);
            this.emailCodePanel.Controls.Add(this.emailAuthTextBox);
            this.emailCodePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailCodePanel.Location = new System.Drawing.Point(4, 29);
            this.emailCodePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.emailCodePanel.Name = "emailCodePanel";
            this.emailCodePanel.Size = new System.Drawing.Size(341, 94);
            this.emailCodePanel.TabIndex = 1;
            this.emailCodePanel.Visible = false;
            // 
            // friendlyNameTextBox
            // 
            this.friendlyNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.friendlyNameTextBox.Location = new System.Drawing.Point(4, 68);
            this.friendlyNameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.friendlyNameTextBox.Name = "friendlyNameTextBox";
            this.friendlyNameTextBox.Size = new System.Drawing.Size(332, 22);
            this.friendlyNameTextBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Friendly Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Steam Guard Code:";
            // 
            // emailAuthTextBox
            // 
            this.emailAuthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailAuthTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.emailAuthTextBox.Location = new System.Drawing.Point(4, 20);
            this.emailAuthTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.emailAuthTextBox.Name = "emailAuthTextBox";
            this.emailAuthTextBox.Size = new System.Drawing.Size(332, 22);
            this.emailAuthTextBox.TabIndex = 1;
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            this.messageLabel.Location = new System.Drawing.Point(4, 0);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.messageLabel.Size = new System.Drawing.Size(76, 25);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "Message!";
            this.messageLabel.Visible = false;
            // 
            // mobileAuthPanel
            // 
            this.mobileAuthPanel.AutoSize = true;
            this.mobileAuthPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mobileAuthPanel.Controls.Add(this.mobileAuthTextBox);
            this.mobileAuthPanel.Controls.Add(this.label4);
            this.mobileAuthPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobileAuthPanel.Location = new System.Drawing.Point(4, 131);
            this.mobileAuthPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mobileAuthPanel.Name = "mobileAuthPanel";
            this.mobileAuthPanel.Size = new System.Drawing.Size(341, 46);
            this.mobileAuthPanel.TabIndex = 2;
            this.mobileAuthPanel.Visible = false;
            // 
            // mobileAuthTextBox
            // 
            this.mobileAuthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mobileAuthTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mobileAuthTextBox.Location = new System.Drawing.Point(4, 20);
            this.mobileAuthTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mobileAuthTextBox.Name = "mobileAuthTextBox";
            this.mobileAuthTextBox.Size = new System.Drawing.Size(332, 22);
            this.mobileAuthTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Mobile Authenticator Code:";
            // 
            // captchaPanel
            // 
            this.captchaPanel.AutoSize = true;
            this.captchaPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.captchaPanel.Controls.Add(this.captchaTextBox);
            this.captchaPanel.Controls.Add(this.captchaRefreshButton);
            this.captchaPanel.Controls.Add(this.captchaPictureBox);
            this.captchaPanel.Controls.Add(this.label6);
            this.captchaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captchaPanel.Location = new System.Drawing.Point(4, 185);
            this.captchaPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.captchaPanel.Name = "captchaPanel";
            this.captchaPanel.Size = new System.Drawing.Size(341, 138);
            this.captchaPanel.TabIndex = 3;
            this.captchaPanel.Visible = false;
            // 
            // captchaTextBox
            // 
            this.captchaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.captchaTextBox.Location = new System.Drawing.Point(4, 112);
            this.captchaTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.captchaTextBox.Name = "captchaTextBox";
            this.captchaTextBox.Size = new System.Drawing.Size(332, 22);
            this.captchaTextBox.TabIndex = 2;
            // 
            // captchaRefreshButton
            // 
            this.captchaRefreshButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.captchaRefreshButton.Location = new System.Drawing.Point(208, 76);
            this.captchaRefreshButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.captchaRefreshButton.Name = "captchaRefreshButton";
            this.captchaRefreshButton.Size = new System.Drawing.Size(100, 28);
            this.captchaRefreshButton.TabIndex = 1;
            this.captchaRefreshButton.Text = "Refresh";
            this.captchaRefreshButton.UseVisualStyleBackColor = true;
            this.captchaRefreshButton.Click += new System.EventHandler(this.captchaRefreshButton_Click);
            // 
            // captchaPictureBox
            // 
            this.captchaPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.captchaPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.captchaPictureBox.Location = new System.Drawing.Point(33, 20);
            this.captchaPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.captchaPictureBox.Name = "captchaPictureBox";
            this.captchaPictureBox.Size = new System.Drawing.Size(274, 49);
            this.captchaPictureBox.TabIndex = 1;
            this.captchaPictureBox.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Type these characters below:";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.loginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(365, 485);
            this.Controls.Add(this.mainLayoutPanel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Text = "Log into Steam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.mainLayoutPanel.ResumeLayout(false);
            this.mainLayoutPanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.emailCodePanel.ResumeLayout(false);
            this.emailCodePanel.PerformLayout();
            this.mobileAuthPanel.ResumeLayout(false);
            this.mobileAuthPanel.PerformLayout();
            this.captchaPanel.ResumeLayout(false);
            this.captchaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captchaPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Panel emailCodePanel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox emailAuthTextBox;
        private System.Windows.Forms.Panel mobileAuthPanel;
        private System.Windows.Forms.TextBox mobileAuthTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox friendlyNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel captchaPanel;
        private System.Windows.Forms.TextBox captchaTextBox;
        private System.Windows.Forms.Button captchaRefreshButton;
        private System.Windows.Forms.PictureBox captchaPictureBox;
        private System.Windows.Forms.Label label6;

    }
}