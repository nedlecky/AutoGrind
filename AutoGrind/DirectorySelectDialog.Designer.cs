namespace AutoGrind
{
    partial class DirectorySelectDialog
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
            this.DirectoryListBox = new System.Windows.Forms.ListBox();
            this.SelectBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.DirectoryNameLbl = new System.Windows.Forms.Label();
            this.NewFolderBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLbl
            // 
            this.TitleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLbl.Location = new System.Drawing.Point(22, 18);
            this.TitleLbl.Name = "TitleLbl";
            this.TitleLbl.Size = new System.Drawing.Size(1245, 48);
            this.TitleLbl.TabIndex = 90;
            this.TitleLbl.Text = "TitleLbl";
            this.TitleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DirectoryListBox
            // 
            this.DirectoryListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectoryListBox.FormattingEnabled = true;
            this.DirectoryListBox.ItemHeight = 37;
            this.DirectoryListBox.Location = new System.Drawing.Point(22, 120);
            this.DirectoryListBox.Name = "DirectoryListBox";
            this.DirectoryListBox.Size = new System.Drawing.Size(1245, 596);
            this.DirectoryListBox.TabIndex = 89;
            this.DirectoryListBox.Click += new System.EventHandler(this.DirectoryListBox_Click);
            this.DirectoryListBox.DoubleClick += new System.EventHandler(this.DirectoryListBox_DoubleClick);
            // 
            // SelectBtn
            // 
            this.SelectBtn.BackColor = System.Drawing.Color.Green;
            this.SelectBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectBtn.ForeColor = System.Drawing.Color.White;
            this.SelectBtn.Location = new System.Drawing.Point(792, 907);
            this.SelectBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SelectBtn.Name = "SelectBtn";
            this.SelectBtn.Size = new System.Drawing.Size(228, 131);
            this.SelectBtn.TabIndex = 88;
            this.SelectBtn.Text = "&Select";
            this.SelectBtn.UseVisualStyleBackColor = false;
            this.SelectBtn.Click += new System.EventHandler(this.SelectBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.Green;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.ForeColor = System.Drawing.Color.White;
            this.CancelBtn.Location = new System.Drawing.Point(1039, 907);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(228, 131);
            this.CancelBtn.TabIndex = 87;
            this.CancelBtn.Text = "&Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // DirectoryNameLbl
            // 
            this.DirectoryNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectoryNameLbl.Location = new System.Drawing.Point(22, 733);
            this.DirectoryNameLbl.Name = "DirectoryNameLbl";
            this.DirectoryNameLbl.Size = new System.Drawing.Size(1245, 112);
            this.DirectoryNameLbl.TabIndex = 91;
            this.DirectoryNameLbl.Text = "DirectoryNameLbl";
            this.DirectoryNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NewFolderBtn
            // 
            this.NewFolderBtn.BackColor = System.Drawing.Color.Green;
            this.NewFolderBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewFolderBtn.ForeColor = System.Drawing.Color.White;
            this.NewFolderBtn.Location = new System.Drawing.Point(22, 907);
            this.NewFolderBtn.Margin = new System.Windows.Forms.Padding(2);
            this.NewFolderBtn.Name = "NewFolderBtn";
            this.NewFolderBtn.Size = new System.Drawing.Size(338, 131);
            this.NewFolderBtn.TabIndex = 93;
            this.NewFolderBtn.Text = "&New Folder";
            this.NewFolderBtn.UseVisualStyleBackColor = false;
            this.NewFolderBtn.Click += new System.EventHandler(this.NewFolderBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.BackColor = System.Drawing.Color.Green;
            this.DeleteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteBtn.ForeColor = System.Drawing.Color.White;
            this.DeleteBtn.Location = new System.Drawing.Point(373, 907);
            this.DeleteBtn.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(228, 131);
            this.DeleteBtn.TabIndex = 92;
            this.DeleteBtn.Text = "&Delete";
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // DirectorySelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 1049);
            this.Controls.Add(this.NewFolderBtn);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.DirectoryNameLbl);
            this.Controls.Add(this.TitleLbl);
            this.Controls.Add(this.DirectoryListBox);
            this.Controls.Add(this.SelectBtn);
            this.Controls.Add(this.CancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DirectorySelectDialog";
            this.Load += new System.EventHandler(this.DirectorySelectDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label TitleLbl;
        private System.Windows.Forms.ListBox DirectoryListBox;
        private System.Windows.Forms.Button SelectBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label DirectoryNameLbl;
        private System.Windows.Forms.Button NewFolderBtn;
        private System.Windows.Forms.Button DeleteBtn;
    }
}