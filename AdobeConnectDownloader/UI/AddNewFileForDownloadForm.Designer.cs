namespace AdobeConnectDownloader.UI
{
    partial class AddNewFileForDownloadForm
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
            SubmitButton = new System.Windows.Forms.Button();
            UrlLabel = new System.Windows.Forms.Label();
            UrlTextBox = new System.Windows.Forms.TextBox();
            SaveInTextBox = new System.Windows.Forms.TextBox();
            SaveFolderLabel = new System.Windows.Forms.Label();
            SaveDialogButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // SubmitButton
            // 
            SubmitButton.BackColor = System.Drawing.Color.Lime;
            SubmitButton.Enabled = false;
            SubmitButton.FlatAppearance.BorderSize = 0;
            SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            SubmitButton.Location = new System.Drawing.Point(293, 129);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new System.Drawing.Size(92, 38);
            SubmitButton.TabIndex = 0;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = false;
            SubmitButton.Click += SubmitButton_Click;
            // 
            // UrlLabel
            // 
            UrlLabel.AutoSize = true;
            UrlLabel.Location = new System.Drawing.Point(12, 42);
            UrlLabel.Name = "UrlLabel";
            UrlLabel.Size = new System.Drawing.Size(28, 15);
            UrlLabel.TabIndex = 1;
            UrlLabel.Text = "Url :";
            // 
            // UrlTextBox
            // 
            UrlTextBox.Location = new System.Drawing.Point(68, 39);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.Size = new System.Drawing.Size(557, 23);
            UrlTextBox.TabIndex = 3;
            UrlTextBox.TextChanged += UrlTextBox_TextChanged;
            // 
            // SaveInTextBox
            // 
            SaveInTextBox.Enabled = false;
            SaveInTextBox.Location = new System.Drawing.Point(68, 88);
            SaveInTextBox.Name = "SaveInTextBox";
            SaveInTextBox.Size = new System.Drawing.Size(557, 23);
            SaveInTextBox.TabIndex = 5;
            SaveInTextBox.TextChanged += SaveInTextBox_TextChanged;
            // 
            // SaveFolderLabel
            // 
            SaveFolderLabel.AutoSize = true;
            SaveFolderLabel.Location = new System.Drawing.Point(12, 91);
            SaveFolderLabel.Name = "SaveFolderLabel";
            SaveFolderLabel.Size = new System.Drawing.Size(50, 15);
            SaveFolderLabel.TabIndex = 4;
            SaveFolderLabel.Text = "Save in :";
            // 
            // SaveDialogButton
            // 
            SaveDialogButton.Location = new System.Drawing.Point(631, 88);
            SaveDialogButton.Name = "SaveDialogButton";
            SaveDialogButton.Size = new System.Drawing.Size(43, 25);
            SaveDialogButton.TabIndex = 6;
            SaveDialogButton.Text = "...";
            SaveDialogButton.UseVisualStyleBackColor = true;
            SaveDialogButton.Click += SaveDialogButton_Click;
            // 
            // AddNewFileForDownloadForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(687, 179);
            Controls.Add(SaveDialogButton);
            Controls.Add(SaveInTextBox);
            Controls.Add(SaveFolderLabel);
            Controls.Add(UrlTextBox);
            Controls.Add(UrlLabel);
            Controls.Add(SubmitButton);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "AddNewFileForDownloadForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Download File Detail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Label UrlLabel;
        private System.Windows.Forms.TextBox UrlTextBox;
        private System.Windows.Forms.TextBox SaveInTextBox;
        private System.Windows.Forms.Label SaveFolderLabel;
        private System.Windows.Forms.Button SaveDialogButton;
    }
}