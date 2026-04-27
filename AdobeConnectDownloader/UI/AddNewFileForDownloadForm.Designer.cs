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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewFileForDownloadForm));
            SubmitButton = new System.Windows.Forms.Button();
            UrlOrPathFileLabel = new System.Windows.Forms.Label();
            UrlOrPathFileTextBox = new System.Windows.Forms.TextBox();
            SaveInTextBox = new System.Windows.Forms.TextBox();
            SaveFolderLabel = new System.Windows.Forms.Label();
            SaveDialogButton = new System.Windows.Forms.Button();
            FileNameTextBox = new System.Windows.Forms.TextBox();
            FileNamelabel = new System.Windows.Forms.Label();
            UrlOrPathFileButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // SubmitButton
            // 
            SubmitButton.BackColor = System.Drawing.Color.Lime;
            SubmitButton.Enabled = false;
            SubmitButton.FlatAppearance.BorderSize = 0;
            SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            SubmitButton.Location = new System.Drawing.Point(334, 141);
            SubmitButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new System.Drawing.Size(105, 51);
            SubmitButton.TabIndex = 5;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = false;
            SubmitButton.Click += SubmitButton_Click;
            // 
            // UrlOrPathFileLabel
            // 
            UrlOrPathFileLabel.AutoSize = true;
            UrlOrPathFileLabel.Location = new System.Drawing.Point(14, 59);
            UrlOrPathFileLabel.Name = "UrlOrPathFileLabel";
            UrlOrPathFileLabel.Size = new System.Drawing.Size(112, 20);
            UrlOrPathFileLabel.TabIndex = 1;
            UrlOrPathFileLabel.Text = "Url or path file :";
            // 
            // UrlOrPathFileTextBox
            // 
            UrlOrPathFileTextBox.Location = new System.Drawing.Point(121, 55);
            UrlOrPathFileTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            UrlOrPathFileTextBox.Name = "UrlOrPathFileTextBox";
            UrlOrPathFileTextBox.Size = new System.Drawing.Size(593, 27);
            UrlOrPathFileTextBox.TabIndex = 2;
            UrlOrPathFileTextBox.TextChanged += UrlTextBox_TextChanged;
            // 
            // SaveInTextBox
            // 
            SaveInTextBox.Enabled = false;
            SaveInTextBox.Location = new System.Drawing.Point(121, 93);
            SaveInTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            SaveInTextBox.Name = "SaveInTextBox";
            SaveInTextBox.Size = new System.Drawing.Size(591, 27);
            SaveInTextBox.TabIndex = 3;
            SaveInTextBox.TextChanged += SaveInTextBox_TextChanged;
            // 
            // SaveFolderLabel
            // 
            SaveFolderLabel.AutoSize = true;
            SaveFolderLabel.Location = new System.Drawing.Point(13, 97);
            SaveFolderLabel.Name = "SaveFolderLabel";
            SaveFolderLabel.Size = new System.Drawing.Size(63, 20);
            SaveFolderLabel.TabIndex = 4;
            SaveFolderLabel.Text = "Save in :";
            // 
            // SaveDialogButton
            // 
            SaveDialogButton.Location = new System.Drawing.Point(720, 91);
            SaveDialogButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            SaveDialogButton.Name = "SaveDialogButton";
            SaveDialogButton.Size = new System.Drawing.Size(49, 33);
            SaveDialogButton.TabIndex = 4;
            SaveDialogButton.Text = "...";
            SaveDialogButton.UseVisualStyleBackColor = true;
            SaveDialogButton.Click += SaveDialogButton_Click;
            // 
            // FileNameTextBox
            // 
            FileNameTextBox.Location = new System.Drawing.Point(121, 16);
            FileNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            FileNameTextBox.Name = "FileNameTextBox";
            FileNameTextBox.Size = new System.Drawing.Size(593, 27);
            FileNameTextBox.TabIndex = 2;
            // 
            // FileNamelabel
            // 
            FileNamelabel.AutoSize = true;
            FileNamelabel.Location = new System.Drawing.Point(13, 20);
            FileNamelabel.Name = "FileNamelabel";
            FileNamelabel.Size = new System.Drawing.Size(87, 20);
            FileNamelabel.TabIndex = 1;
            FileNamelabel.Text = "File Name : ";
            // 
            // UrlOrPathFileButton
            // 
            UrlOrPathFileButton.Location = new System.Drawing.Point(720, 55);
            UrlOrPathFileButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            UrlOrPathFileButton.Name = "UrlOrPathFileButton";
            UrlOrPathFileButton.Size = new System.Drawing.Size(49, 33);
            UrlOrPathFileButton.TabIndex = 6;
            UrlOrPathFileButton.Text = "...";
            UrlOrPathFileButton.UseVisualStyleBackColor = true;
            UrlOrPathFileButton.Click += UrlOrPathFileButton_Click;
            // 
            // AddNewFileForDownloadForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(785, 208);
            Controls.Add(UrlOrPathFileButton);
            Controls.Add(FileNameTextBox);
            Controls.Add(FileNamelabel);
            Controls.Add(SaveDialogButton);
            Controls.Add(SaveInTextBox);
            Controls.Add(SaveFolderLabel);
            Controls.Add(UrlOrPathFileTextBox);
            Controls.Add(UrlOrPathFileLabel);
            Controls.Add(SubmitButton);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "AddNewFileForDownloadForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Download File Detail";
            Load += AddNewFileForDownloadForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Label UrlOrPathFileLabel;
        private System.Windows.Forms.TextBox UrlOrPathFileTextBox;
        private System.Windows.Forms.TextBox SaveInTextBox;
        private System.Windows.Forms.Label SaveFolderLabel;
        private System.Windows.Forms.Button SaveDialogButton;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Label FileNamelabel;
        private System.Windows.Forms.Button UrlOrPathFileButton;
    }
}