namespace AutoGrind
{
    partial class FileSaveAsDialog
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
            this.TitleLbl = new System.Windows.Forms.Label();
            this.FileListBox = new System.Windows.Forms.ListBox();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.FileNameTxt = new System.Windows.Forms.TextBox();
            this.DirectoryNameLbl = new System.Windows.Forms.Label();
            this.DirectoryListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // TitleLbl
            // 
            this.TitleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLbl.Location = new System.Drawing.Point(22, 3);
            this.TitleLbl.Name = "TitleLbl";
            this.TitleLbl.Size = new System.Drawing.Size(1110, 48);
            this.TitleLbl.TabIndex = 85;
            this.TitleLbl.Text = "TitleLbl";
            this.TitleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FileListBox
            // 
            this.FileListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileListBox.FormattingEnabled = true;
            this.FileListBox.ItemHeight = 37;
            this.FileListBox.Location = new System.Drawing.Point(375, 176);
            this.FileListBox.Name = "FileListBox";
            this.FileListBox.Size = new System.Drawing.Size(757, 559);
            this.FileListBox.TabIndex = 84;
            this.FileListBox.Click += new System.EventHandler(this.FileListBox_Click);
            this.FileListBox.DoubleClick += new System.EventHandler(this.FileListBox_DoubleClick);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.Green;
            this.SaveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveBtn.ForeColor = System.Drawing.Color.White;
            this.SaveBtn.Location = new System.Drawing.Point(315, 837);
            this.SaveBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(228, 131);
            this.SaveBtn.TabIndex = 83;
            this.SaveBtn.Text = "&Save";
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.Green;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.ForeColor = System.Drawing.Color.White;
            this.CancelBtn.Location = new System.Drawing.Point(632, 837);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(228, 131);
            this.CancelBtn.TabIndex = 82;
            this.CancelBtn.Text = "&Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // FileNameTxt
            // 
            this.FileNameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileNameTxt.Location = new System.Drawing.Point(375, 746);
            this.FileNameTxt.Name = "FileNameTxt";
            this.FileNameTxt.Size = new System.Drawing.Size(757, 44);
            this.FileNameTxt.TabIndex = 86;
            this.FileNameTxt.Enter += new System.EventHandler(this.FileNameTxt_Enter);
            // 
            // DirectoryNameLbl
            // 
            this.DirectoryNameLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DirectoryNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectoryNameLbl.Location = new System.Drawing.Point(12, 63);
            this.DirectoryNameLbl.Name = "DirectoryNameLbl";
            this.DirectoryNameLbl.Size = new System.Drawing.Size(1120, 110);
            this.DirectoryNameLbl.TabIndex = 88;
            this.DirectoryNameLbl.Text = "DirectoryName";
            this.DirectoryNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DirectoryListBox
            // 
            this.DirectoryListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectoryListBox.FormattingEnabled = true;
            this.DirectoryListBox.ItemHeight = 55;
            this.DirectoryListBox.Location = new System.Drawing.Point(12, 176);
            this.DirectoryListBox.Name = "DirectoryListBox";
            this.DirectoryListBox.Size = new System.Drawing.Size(338, 499);
            this.DirectoryListBox.TabIndex = 87;
            this.DirectoryListBox.DoubleClick += new System.EventHandler(this.DirectoryListBox_DoubleClick);
            // 
            // FileSaveAsDialog
            // 
            this.AcceptButton = this.SaveBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(1160, 979);
            this.Controls.Add(this.DirectoryNameLbl);
            this.Controls.Add(this.DirectoryListBox);
            this.Controls.Add(this.FileNameTxt);
            this.Controls.Add(this.TitleLbl);
            this.Controls.Add(this.FileListBox);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.CancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FileSaveAsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FileSaveAsDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLbl;
        private System.Windows.Forms.ListBox FileListBox;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox FileNameTxt;
        private System.Windows.Forms.Label DirectoryNameLbl;
        private System.Windows.Forms.ListBox DirectoryListBox;
    }
}