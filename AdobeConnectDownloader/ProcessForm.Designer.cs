namespace AdobeConnectDownloader
{
    partial class ProcessForm
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
            this.DownloadProcessTitleLabel = new System.Windows.Forms.Label();
            this.DownloadProcessLabel = new System.Windows.Forms.Label();
            this.ExtractZipDataLabel = new System.Windows.Forms.Label();
            this.GetStreamsDataLabel = new System.Windows.Forms.Label();
            this.FixAudiosLabel = new System.Windows.Forms.Label();
            this.FixVideosLabel = new System.Windows.Forms.Label();
            this.SyncAllDataLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // DownloadProcessTitleLabel
            // 
            this.DownloadProcessTitleLabel.AutoSize = true;
            this.DownloadProcessTitleLabel.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DownloadProcessTitleLabel.Location = new System.Drawing.Point(12, 9);
            this.DownloadProcessTitleLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.DownloadProcessTitleLabel.Name = "DownloadProcessTitleLabel";
            this.DownloadProcessTitleLabel.Size = new System.Drawing.Size(163, 23);
            this.DownloadProcessTitleLabel.TabIndex = 0;
            this.DownloadProcessTitleLabel.Text = "Download Process";
            // 
            // DownloadProcessLabel
            // 
            this.DownloadProcessLabel.AutoSize = true;
            this.DownloadProcessLabel.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DownloadProcessLabel.Location = new System.Drawing.Point(12, 70);
            this.DownloadProcessLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.DownloadProcessLabel.Name = "DownloadProcessLabel";
            this.DownloadProcessLabel.Size = new System.Drawing.Size(38, 23);
            this.DownloadProcessLabel.TabIndex = 1;
            this.DownloadProcessLabel.Text = "0%";
            // 
            // ExtractZipDataLabel
            // 
            this.ExtractZipDataLabel.AutoSize = true;
            this.ExtractZipDataLabel.ForeColor = System.Drawing.Color.Red;
            this.ExtractZipDataLabel.Location = new System.Drawing.Point(12, 109);
            this.ExtractZipDataLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.ExtractZipDataLabel.Name = "ExtractZipDataLabel";
            this.ExtractZipDataLabel.Size = new System.Drawing.Size(211, 31);
            this.ExtractZipDataLabel.TabIndex = 2;
            this.ExtractZipDataLabel.Text = "Extract Zip Data";
            // 
            // GetStreamsDataLabel
            // 
            this.GetStreamsDataLabel.AutoSize = true;
            this.GetStreamsDataLabel.ForeColor = System.Drawing.Color.Red;
            this.GetStreamsDataLabel.Location = new System.Drawing.Point(12, 166);
            this.GetStreamsDataLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.GetStreamsDataLabel.Name = "GetStreamsDataLabel";
            this.GetStreamsDataLabel.Size = new System.Drawing.Size(222, 31);
            this.GetStreamsDataLabel.TabIndex = 3;
            this.GetStreamsDataLabel.Text = "Get Streams Data";
            // 
            // FixAudiosLabel
            // 
            this.FixAudiosLabel.AutoSize = true;
            this.FixAudiosLabel.ForeColor = System.Drawing.Color.Red;
            this.FixAudiosLabel.Location = new System.Drawing.Point(16, 215);
            this.FixAudiosLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.FixAudiosLabel.Name = "FixAudiosLabel";
            this.FixAudiosLabel.Size = new System.Drawing.Size(140, 31);
            this.FixAudiosLabel.TabIndex = 4;
            this.FixAudiosLabel.Text = "Fix Audios";
            // 
            // FixVideosLabel
            // 
            this.FixVideosLabel.AutoSize = true;
            this.FixVideosLabel.ForeColor = System.Drawing.Color.Red;
            this.FixVideosLabel.Location = new System.Drawing.Point(14, 265);
            this.FixVideosLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.FixVideosLabel.Name = "FixVideosLabel";
            this.FixVideosLabel.Size = new System.Drawing.Size(229, 31);
            this.FixVideosLabel.TabIndex = 5;
            this.FixVideosLabel.Text = "Fix Videos If Exist";
            // 
            // SyncAllDataLabel
            // 
            this.SyncAllDataLabel.AutoSize = true;
            this.SyncAllDataLabel.ForeColor = System.Drawing.Color.Red;
            this.SyncAllDataLabel.Location = new System.Drawing.Point(14, 307);
            this.SyncAllDataLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.SyncAllDataLabel.Name = "SyncAllDataLabel";
            this.SyncAllDataLabel.Size = new System.Drawing.Size(174, 31);
            this.SyncAllDataLabel.TabIndex = 6;
            this.SyncAllDataLabel.Text = "Sync All Data";
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.CancelButton.FlatAppearance.BorderSize = 0;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CancelButton.Location = new System.Drawing.Point(16, 363);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(73, 35);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(12, 40);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(231, 23);
            this.DownloadProgressBar.TabIndex = 8;
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 450);
            this.Controls.Add(this.DownloadProgressBar);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SyncAllDataLabel);
            this.Controls.Add(this.FixVideosLabel);
            this.Controls.Add(this.FixAudiosLabel);
            this.Controls.Add(this.GetStreamsDataLabel);
            this.Controls.Add(this.ExtractZipDataLabel);
            this.Controls.Add(this.DownloadProcessLabel);
            this.Controls.Add(this.DownloadProcessTitleLabel);
            this.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ProcessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Form";
            this.Load += new System.EventHandler(this.ProcessForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DownloadProcessTitleLabel;
        private System.Windows.Forms.Label DownloadProcessLabel;
        private System.Windows.Forms.Label ExtractZipDataLabel;
        private System.Windows.Forms.Label GetStreamsDataLabel;
        private System.Windows.Forms.Label FixAudiosLabel;
        private System.Windows.Forms.Label FixVideosLabel;
        private System.Windows.Forms.Label SyncAllDataLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
    }
}