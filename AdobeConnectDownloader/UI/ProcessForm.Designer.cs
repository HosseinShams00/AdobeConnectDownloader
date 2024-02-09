namespace AdobeConnectDownloader.UI
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
            components = new System.ComponentModel.Container();
            DownloadProcessTitleLabel = new System.Windows.Forms.Label();
            DownloadProcessLabel = new System.Windows.Forms.Label();
            StatusLabel = new System.Windows.Forms.Label();
            CancelButton = new System.Windows.Forms.Button();
            DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            CancelButtonDesignComponent = new DesignComponent(components);
            StatusProgressBar = new System.Windows.Forms.ProgressBar();
            SuspendLayout();
            // 
            // DownloadProcessTitleLabel
            // 
            DownloadProcessTitleLabel.AutoSize = true;
            DownloadProcessTitleLabel.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            DownloadProcessTitleLabel.Location = new System.Drawing.Point(12, 9);
            DownloadProcessTitleLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            DownloadProcessTitleLabel.Name = "DownloadProcessTitleLabel";
            DownloadProcessTitleLabel.Size = new System.Drawing.Size(163, 23);
            DownloadProcessTitleLabel.TabIndex = 0;
            DownloadProcessTitleLabel.Text = "Download Process";
            // 
            // DownloadProcessLabel
            // 
            DownloadProcessLabel.AutoSize = true;
            DownloadProcessLabel.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            DownloadProcessLabel.Location = new System.Drawing.Point(226, 14);
            DownloadProcessLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            DownloadProcessLabel.Name = "DownloadProcessLabel";
            DownloadProcessLabel.Size = new System.Drawing.Size(38, 23);
            DownloadProcessLabel.TabIndex = 1;
            DownloadProcessLabel.Text = "0%";
            // 
            // StatusLabel
            // 
            StatusLabel.AutoSize = true;
            StatusLabel.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            StatusLabel.ForeColor = System.Drawing.Color.Black;
            StatusLabel.Location = new System.Drawing.Point(12, 107);
            StatusLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new System.Drawing.Size(0, 26);
            StatusLabel.TabIndex = 2;
            // 
            // CancelButton
            // 
            CancelButton.BackColor = System.Drawing.Color.FromArgb(0, 105, 92);
            CancelButton.FlatAppearance.BorderSize = 0;
            CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            CancelButton.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            CancelButton.ForeColor = System.Drawing.Color.FromArgb(250, 250, 250);
            CancelButton.Location = new System.Drawing.Point(149, 147);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new System.Drawing.Size(117, 38);
            CancelButton.TabIndex = 7;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = false;
            CancelButton.Click += CancelButton_Click;
            // 
            // DownloadProgressBar
            // 
            DownloadProgressBar.Location = new System.Drawing.Point(12, 40);
            DownloadProgressBar.Name = "DownloadProgressBar";
            DownloadProgressBar.Size = new System.Drawing.Size(253, 23);
            DownloadProgressBar.TabIndex = 8;
            // 
            // CancelButtonDesignComponent
            // 
            CancelButtonDesignComponent._SetButton = CancelButton;
            CancelButtonDesignComponent.BackGroundHtmlColorCode = "#00695C";
            CancelButtonDesignComponent.ForeGroundHtmlColorCode = "#FAFAFA";
            CancelButtonDesignComponent.LowLeft = true;
            CancelButtonDesignComponent.LowRight = true;
            CancelButtonDesignComponent.UpLeft = true;
            CancelButtonDesignComponent.UpRight = true;
            CancelButtonDesignComponent.XRadius = 15F;
            CancelButtonDesignComponent.YRadius = 15F;
            // 
            // StatusProgressBar
            // 
            StatusProgressBar.Location = new System.Drawing.Point(13, 69);
            StatusProgressBar.Name = "StatusProgressBar";
            StatusProgressBar.Size = new System.Drawing.Size(253, 23);
            StatusProgressBar.Step = 13;
            StatusProgressBar.TabIndex = 9;
            // 
            // ProcessForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(278, 195);
            Controls.Add(StatusProgressBar);
            Controls.Add(DownloadProgressBar);
            Controls.Add(CancelButton);
            Controls.Add(StatusLabel);
            Controls.Add(DownloadProcessLabel);
            Controls.Add(DownloadProcessTitleLabel);
            Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(5);
            Name = "ProcessForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Process ";
            FormClosing += ProcessForm_FormClosing;
            Load += ProcessForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label DownloadProcessTitleLabel;
        private System.Windows.Forms.Label DownloadProcessLabel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private DesignComponent CancelButtonDesignComponent;
        private System.Windows.Forms.ProgressBar StatusProgressBar;
    }
}