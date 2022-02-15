namespace AutoGrind
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
            this.components = new System.ComponentModel.Container();
            this.GrindBtn = new System.Windows.Forms.Button();
            this.EditBtn = new System.Windows.Forms.Button();
            this.SetupBtn = new System.Windows.Forms.Button();
            this.OperationTab = new System.Windows.Forms.TabControl();
            this.GrindTab = new System.Windows.Forms.TabPage();
            this.EditTab = new System.Windows.Forms.TabPage();
            this.SetupTab = new System.Windows.Forms.TabPage();
            this.DefaultConfigBtn = new System.Windows.Forms.Button();
            this.LoadConfigBtn = new System.Windows.Forms.Button();
            this.SaveConfigBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AutoGrindRootLbl = new System.Windows.Forms.Label();
            this.ChangeLEonardRootBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LogsTab = new System.Windows.Forms.TabPage();
            this.UrLogRTB = new System.Windows.Forms.RichTextBox();
            this.ErrorLogRTB = new System.Windows.Forms.RichTextBox();
            this.AllLogRTB = new System.Windows.Forms.RichTextBox();
            this.HeartbeatTmr = new System.Windows.Forms.Timer(this.components);
            this.timeLbl = new System.Windows.Forms.Label();
            this.StartupTmr = new System.Windows.Forms.Timer(this.components);
            this.CloseTmr = new System.Windows.Forms.Timer(this.components);
            this.JavaScriptCodeRTB = new System.Windows.Forms.RichTextBox();
            this.OperationTab.SuspendLayout();
            this.EditTab.SuspendLayout();
            this.SetupTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.LogsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrindBtn
            // 
            this.GrindBtn.Location = new System.Drawing.Point(12, 54);
            this.GrindBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GrindBtn.Name = "GrindBtn";
            this.GrindBtn.Size = new System.Drawing.Size(136, 106);
            this.GrindBtn.TabIndex = 0;
            this.GrindBtn.Text = "Grind";
            this.GrindBtn.UseVisualStyleBackColor = true;
            this.GrindBtn.Click += new System.EventHandler(this.GrindBtn_Click);
            // 
            // EditBtn
            // 
            this.EditBtn.Location = new System.Drawing.Point(12, 190);
            this.EditBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(136, 106);
            this.EditBtn.TabIndex = 1;
            this.EditBtn.Text = "Edit";
            this.EditBtn.UseVisualStyleBackColor = true;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // SetupBtn
            // 
            this.SetupBtn.Location = new System.Drawing.Point(12, 325);
            this.SetupBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.OperationTab.Location = new System.Drawing.Point(187, 30);
            this.OperationTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OperationTab.Name = "OperationTab";
            this.OperationTab.SelectedIndex = 0;
            this.OperationTab.Size = new System.Drawing.Size(961, 549);
            this.OperationTab.TabIndex = 3;
            // 
            // GrindTab
            // 
            this.GrindTab.Location = new System.Drawing.Point(4, 25);
            this.GrindTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GrindTab.Name = "GrindTab";
            this.GrindTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GrindTab.Size = new System.Drawing.Size(953, 520);
            this.GrindTab.TabIndex = 0;
            this.GrindTab.Text = "Grind";
            this.GrindTab.UseVisualStyleBackColor = true;
            // 
            // EditTab
            // 
            this.EditTab.Controls.Add(this.JavaScriptCodeRTB);
            this.EditTab.Location = new System.Drawing.Point(4, 25);
            this.EditTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditTab.Name = "EditTab";
            this.EditTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditTab.Size = new System.Drawing.Size(953, 520);
            this.EditTab.TabIndex = 1;
            this.EditTab.Text = "Edit";
            this.EditTab.UseVisualStyleBackColor = true;
            // 
            // SetupTab
            // 
            this.SetupTab.Controls.Add(this.DefaultConfigBtn);
            this.SetupTab.Controls.Add(this.LoadConfigBtn);
            this.SetupTab.Controls.Add(this.SaveConfigBtn);
            this.SetupTab.Controls.Add(this.groupBox1);
            this.SetupTab.Location = new System.Drawing.Point(4, 25);
            this.SetupTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SetupTab.Name = "SetupTab";
            this.SetupTab.Size = new System.Drawing.Size(953, 520);
            this.SetupTab.TabIndex = 2;
            this.SetupTab.Text = "Setup";
            this.SetupTab.UseVisualStyleBackColor = true;
            // 
            // DefaultConfigBtn
            // 
            this.DefaultConfigBtn.Location = new System.Drawing.Point(21, 192);
            this.DefaultConfigBtn.Margin = new System.Windows.Forms.Padding(4);
            this.DefaultConfigBtn.Name = "DefaultConfigBtn";
            this.DefaultConfigBtn.Size = new System.Drawing.Size(100, 28);
            this.DefaultConfigBtn.TabIndex = 74;
            this.DefaultConfigBtn.Text = "Default";
            this.DefaultConfigBtn.UseVisualStyleBackColor = true;
            this.DefaultConfigBtn.Click += new System.EventHandler(this.DefaultConfigBtn_Click);
            // 
            // LoadConfigBtn
            // 
            this.LoadConfigBtn.Location = new System.Drawing.Point(130, 192);
            this.LoadConfigBtn.Margin = new System.Windows.Forms.Padding(4);
            this.LoadConfigBtn.Name = "LoadConfigBtn";
            this.LoadConfigBtn.Size = new System.Drawing.Size(100, 28);
            this.LoadConfigBtn.TabIndex = 73;
            this.LoadConfigBtn.Text = "Load";
            this.LoadConfigBtn.UseVisualStyleBackColor = true;
            this.LoadConfigBtn.Click += new System.EventHandler(this.LoadConfigBtn_Click);
            // 
            // SaveConfigBtn
            // 
            this.SaveConfigBtn.Location = new System.Drawing.Point(237, 192);
            this.SaveConfigBtn.Margin = new System.Windows.Forms.Padding(4);
            this.SaveConfigBtn.Name = "SaveConfigBtn";
            this.SaveConfigBtn.Size = new System.Drawing.Size(100, 28);
            this.SaveConfigBtn.TabIndex = 72;
            this.SaveConfigBtn.Text = "Save";
            this.SaveConfigBtn.UseVisualStyleBackColor = true;
            this.SaveConfigBtn.Click += new System.EventHandler(this.SaveConfigBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AutoGrindRootLbl);
            this.groupBox1.Controls.Add(this.ChangeLEonardRootBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(21, 65);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(763, 121);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // AutoGrindRootLbl
            // 
            this.AutoGrindRootLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AutoGrindRootLbl.Location = new System.Drawing.Point(165, 31);
            this.AutoGrindRootLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AutoGrindRootLbl.Name = "AutoGrindRootLbl";
            this.AutoGrindRootLbl.Size = new System.Drawing.Size(513, 28);
            this.AutoGrindRootLbl.TabIndex = 69;
            this.AutoGrindRootLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChangeLEonardRootBtn
            // 
            this.ChangeLEonardRootBtn.Location = new System.Drawing.Point(687, 31);
            this.ChangeLEonardRootBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ChangeLEonardRootBtn.Name = "ChangeLEonardRootBtn";
            this.ChangeLEonardRootBtn.Size = new System.Drawing.Size(32, 28);
            this.ChangeLEonardRootBtn.TabIndex = 70;
            this.ChangeLEonardRootBtn.Text = "...";
            this.ChangeLEonardRootBtn.UseVisualStyleBackColor = true;
            this.ChangeLEonardRootBtn.Click += new System.EventHandler(this.ChangeLEonardRootBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 16);
            this.label1.TabIndex = 68;
            this.label1.Text = "AutoGrind Root Directory";
            // 
            // LogsTab
            // 
            this.LogsTab.Controls.Add(this.UrLogRTB);
            this.LogsTab.Controls.Add(this.ErrorLogRTB);
            this.LogsTab.Location = new System.Drawing.Point(4, 25);
            this.LogsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LogsTab.Name = "LogsTab";
            this.LogsTab.Size = new System.Drawing.Size(953, 520);
            this.LogsTab.TabIndex = 3;
            this.LogsTab.Text = "Logs";
            this.LogsTab.UseVisualStyleBackColor = true;
            // 
            // UrLogRTB
            // 
            this.UrLogRTB.Location = new System.Drawing.Point(35, 284);
            this.UrLogRTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UrLogRTB.Name = "UrLogRTB";
            this.UrLogRTB.Size = new System.Drawing.Size(901, 210);
            this.UrLogRTB.TabIndex = 1;
            this.UrLogRTB.Text = "";
            // 
            // ErrorLogRTB
            // 
            this.ErrorLogRTB.Location = new System.Drawing.Point(39, 46);
            this.ErrorLogRTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ErrorLogRTB.Name = "ErrorLogRTB";
            this.ErrorLogRTB.Size = new System.Drawing.Size(896, 210);
            this.ErrorLogRTB.TabIndex = 0;
            this.ErrorLogRTB.Text = "";
            // 
            // AllLogRTB
            // 
            this.AllLogRTB.Location = new System.Drawing.Point(12, 583);
            this.AllLogRTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllLogRTB.Name = "AllLogRTB";
            this.AllLogRTB.Size = new System.Drawing.Size(1131, 122);
            this.AllLogRTB.TabIndex = 4;
            this.AllLogRTB.Text = "";
            // 
            // HeartbeatTmr
            // 
            this.HeartbeatTmr.Tick += new System.EventHandler(this.HeartbeatTmr_Tick);
            // 
            // timeLbl
            // 
            this.timeLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeLbl.Location = new System.Drawing.Point(12, 9);
            this.timeLbl.Name = "timeLbl";
            this.timeLbl.Size = new System.Drawing.Size(167, 27);
            this.timeLbl.TabIndex = 5;
            this.timeLbl.Text = "timeLbl";
            this.timeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartupTmr
            // 
            this.StartupTmr.Tick += new System.EventHandler(this.StartupTmr_Tick);
            // 
            // CloseTmr
            // 
            this.CloseTmr.Tick += new System.EventHandler(this.CloseTmr_Tick);
            // 
            // JavaScriptCodeRTB
            // 
            this.JavaScriptCodeRTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JavaScriptCodeRTB.Location = new System.Drawing.Point(17, 22);
            this.JavaScriptCodeRTB.Name = "JavaScriptCodeRTB";
            this.JavaScriptCodeRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.JavaScriptCodeRTB.Size = new System.Drawing.Size(607, 354);
            this.JavaScriptCodeRTB.TabIndex = 71;
            this.JavaScriptCodeRTB.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 720);
            this.Controls.Add(this.timeLbl);
            this.Controls.Add(this.AllLogRTB);
            this.Controls.Add(this.OperationTab);
            this.Controls.Add(this.SetupBtn);
            this.Controls.Add(this.EditBtn);
            this.Controls.Add(this.GrindBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Code Sets This Caption";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.OperationTab.ResumeLayout(false);
            this.EditTab.ResumeLayout(false);
            this.SetupTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.RichTextBox UrLogRTB;
        private System.Windows.Forms.RichTextBox ErrorLogRTB;
        private System.Windows.Forms.RichTextBox AllLogRTB;
        private System.Windows.Forms.Timer HeartbeatTmr;
        private System.Windows.Forms.Label timeLbl;
        private System.Windows.Forms.Timer StartupTmr;
        private System.Windows.Forms.Button DefaultConfigBtn;
        private System.Windows.Forms.Button LoadConfigBtn;
        private System.Windows.Forms.Button SaveConfigBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label AutoGrindRootLbl;
        private System.Windows.Forms.Button ChangeLEonardRootBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer CloseTmr;
        private System.Windows.Forms.RichTextBox JavaScriptCodeRTB;
    }
}

