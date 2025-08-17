namespace AdobeConnectDownloader.UI
{
    partial class WebBrowser
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
            UrlTextBox = new System.Windows.Forms.TextBox();
            BrowsUrlButton = new System.Windows.Forms.Button();
            WebBrowserUrlLabel = new System.Windows.Forms.Label();
            BrowserGroupBox = new System.Windows.Forms.GroupBox();
            CollectDataButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // UrlTextBox
            // 
            UrlTextBox.Location = new System.Drawing.Point(12, 38);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.PlaceholderText = "url : ";
            UrlTextBox.Size = new System.Drawing.Size(917, 27);
            UrlTextBox.TabIndex = 0;
            // 
            // BrowsUrlButton
            // 
            BrowsUrlButton.Location = new System.Drawing.Point(935, 38);
            BrowsUrlButton.Name = "BrowsUrlButton";
            BrowsUrlButton.Size = new System.Drawing.Size(129, 29);
            BrowsUrlButton.TabIndex = 1;
            BrowsUrlButton.Text = "Browse";
            BrowsUrlButton.UseVisualStyleBackColor = true;
            BrowsUrlButton.Click += BrowsUrlButton_Click;
            // 
            // WebBrowserUrlLabel
            // 
            WebBrowserUrlLabel.AutoSize = true;
            WebBrowserUrlLabel.Location = new System.Drawing.Point(12, 9);
            WebBrowserUrlLabel.Name = "WebBrowserUrlLabel";
            WebBrowserUrlLabel.Size = new System.Drawing.Size(68, 20);
            WebBrowserUrlLabel.TabIndex = 2;
            WebBrowserUrlLabel.Text = "Your Url :";
            // 
            // BrowserGroupBox
            // 
            BrowserGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            BrowserGroupBox.AutoSize = true;
            BrowserGroupBox.Location = new System.Drawing.Point(12, 71);
            BrowserGroupBox.Name = "BrowserGroupBox";
            BrowserGroupBox.Size = new System.Drawing.Size(1158, 572);
            BrowserGroupBox.TabIndex = 3;
            BrowserGroupBox.TabStop = false;
            BrowserGroupBox.Text = "browser";
            // 
            // CollectDataButton
            // 
            CollectDataButton.Location = new System.Drawing.Point(1070, 36);
            CollectDataButton.Name = "CollectDataButton";
            CollectDataButton.Size = new System.Drawing.Size(100, 29);
            CollectDataButton.TabIndex = 4;
            CollectDataButton.Text = "Collect Data";
            CollectDataButton.UseVisualStyleBackColor = true;
            CollectDataButton.Click += CollectDataButton_Click;
            // 
            // WebBrowser
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1187, 665);
            Controls.Add(CollectDataButton);
            Controls.Add(BrowserGroupBox);
            Controls.Add(WebBrowserUrlLabel);
            Controls.Add(BrowsUrlButton);
            Controls.Add(UrlTextBox);
            Name = "WebBrowser";
            Text = "WebBrowser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox UrlTextBox;
        private System.Windows.Forms.Button BrowsUrlButton;
        private System.Windows.Forms.Label WebBrowserUrlLabel;
        private System.Windows.Forms.GroupBox BrowserGroupBox;
        private System.Windows.Forms.Button CollectDataButton;
    }
}