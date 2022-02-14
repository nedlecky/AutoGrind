namespace Auto_Grind
{
    partial class MainForm
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
            this.GrindBtn = new System.Windows.Forms.Button();
            this.EditBtn = new System.Windows.Forms.Button();
            this.SetupBtn = new System.Windows.Forms.Button();
            this.OperationTab = new System.Windows.Forms.TabControl();
            this.GrindTab = new System.Windows.Forms.TabPage();
            this.EditTab = new System.Windows.Forms.TabPage();
            this.SetupTab = new System.Windows.Forms.TabPage();
            this.LogsTab = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.OperationTab.SuspendLayout();
            this.LogsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrindBtn
            // 
            this.GrindBtn.Location = new System.Drawing.Point(21, 29);
            this.GrindBtn.Name = "GrindBtn";
            this.GrindBtn.Size = new System.Drawing.Size(136, 106);
            this.GrindBtn.TabIndex = 0;
            this.GrindBtn.Text = "Grind";
            this.GrindBtn.UseVisualStyleBackColor = true;
            this.GrindBtn.Click += new System.EventHandler(this.GrindBtn_Click);
            // 
            // EditBtn
            // 
            this.EditBtn.Location = new System.Drawing.Point(21, 154);
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(136, 106);
            this.EditBtn.TabIndex = 1;
            this.EditBtn.Text = "Edit";
            this.EditBtn.UseVisualStyleBackColor = true;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // SetupBtn
            // 
            this.SetupBtn.Location = new System.Drawing.Point(21, 278);
            this.SetupBtn.Name = "SetupBtn";
            this.SetupBtn.Size = new System.Drawing.Size(136, 106);
            this.SetupBtn.TabIndex = 2;
            this.SetupBtn.Text = "Setup";
            this.SetupBtn.UseVisualStyleBackColor = true;
            this.SetupBtn.Click += new System.EventHandler(this.SetupBtn_Click);
            // 
            // OperationTab
            // 
            this.OperationTab.Controls.Add(this.GrindTab);
            this.OperationTab.Controls.Add(this.EditTab);
            this.OperationTab.Controls.Add(this.SetupTab);
            this.OperationTab.Controls.Add(this.LogsTab);
            this.OperationTab.Location = new System.Drawing.Point(186, 29);
            this.OperationTab.Name = "OperationTab";
            this.OperationTab.SelectedIndex = 0;
            this.OperationTab.Size = new System.Drawing.Size(720, 549);
            this.OperationTab.TabIndex = 3;
            // 
            // GrindTab
            // 
            this.GrindTab.Location = new System.Drawing.Point(4, 25);
            this.GrindTab.Name = "GrindTab";
            this.GrindTab.Padding = new System.Windows.Forms.Padding(3);
            this.GrindTab.Size = new System.Drawing.Size(712, 520);
            this.GrindTab.TabIndex = 0;
            this.GrindTab.Text = "Grind";
            this.GrindTab.UseVisualStyleBackColor = true;
            // 
            // EditTab
            // 
            this.EditTab.Location = new System.Drawing.Point(4, 25);
            this.EditTab.Name = "EditTab";
            this.EditTab.Padding = new System.Windows.Forms.Padding(3);
            this.EditTab.Size = new System.Drawing.Size(712, 520);
            this.EditTab.TabIndex = 1;
            this.EditTab.Text = "Edit";
            this.EditTab.UseVisualStyleBackColor = true;
            // 
            // SetupTab
            // 
            this.SetupTab.Location = new System.Drawing.Point(4, 25);
            this.SetupTab.Name = "SetupTab";
            this.SetupTab.Size = new System.Drawing.Size(712, 520);
            this.SetupTab.TabIndex = 2;
            this.SetupTab.Text = "Setup";
            this.SetupTab.UseVisualStyleBackColor = true;
            // 
            // LogsTab
            // 
            this.LogsTab.Controls.Add(this.richTextBox2);
            this.LogsTab.Controls.Add(this.richTextBox1);
            this.LogsTab.Location = new System.Drawing.Point(4, 25);
            this.LogsTab.Name = "LogsTab";
            this.LogsTab.Size = new System.Drawing.Size(712, 520);
            this.LogsTab.TabIndex = 3;
            this.LogsTab.Text = "Logs";
            this.LogsTab.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(39, 45);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(619, 209);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(34, 284);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(619, 209);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(190, 584);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(712, 122);
            this.richTextBox3.TabIndex = 4;
            this.richTextBox3.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 720);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.OperationTab);
            this.Controls.Add(this.SetupBtn);
            this.Controls.Add(this.EditBtn);
            this.Controls.Add(this.GrindBtn);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Code Sets This Caption";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.OperationTab.ResumeLayout(false);
            this.LogsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GrindBtn;
        private System.Windows.Forms.Button EditBtn;
        private System.Windows.Forms.Button SetupBtn;
        private System.Windows.Forms.TabControl OperationTab;
        private System.Windows.Forms.TabPage GrindTab;
        private System.Windows.Forms.TabPage EditTab;
        private System.Windows.Forms.TabPage SetupTab;
        private System.Windows.Forms.TabPage LogsTab;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox3;
    }
}

