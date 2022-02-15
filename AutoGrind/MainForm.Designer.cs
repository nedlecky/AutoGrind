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
            this.RecipeRTB = new System.Windows.Forms.RichTextBox();
            this.SaveAsRecipeBtn = new System.Windows.Forms.Button();
            this.NewRecipeBtn = new System.Windows.Forms.Button();
            this.LoadRecipeBtn = new System.Windows.Forms.Button();
            this.SaveRecipeBtn = new System.Windows.Forms.Button();
            this.RecipeFilenameLbl = new System.Windows.Forms.Label();
            this.StartBtn = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.ContinueBtn = new System.Windows.Forms.Button();
            this.RecipeRoRTB = new System.Windows.Forms.RichTextBox();
            this.OperationTab.SuspendLayout();
            this.GrindTab.SuspendLayout();
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
            this.OperationTab.Location = new System.Drawing.Point(187, 46);
            this.OperationTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OperationTab.Name = "OperationTab";
            this.OperationTab.SelectedIndex = 0;
            this.OperationTab.Size = new System.Drawing.Size(961, 533);
            this.OperationTab.TabIndex = 3;
            // 
            // GrindTab
            // 
            this.GrindTab.Controls.Add(this.RecipeRoRTB);
            this.GrindTab.Controls.Add(this.ContinueBtn);
            this.GrindTab.Controls.Add(this.LoadBtn);
            this.GrindTab.Controls.Add(this.StopBtn);
            this.GrindTab.Controls.Add(this.PauseBtn);
            this.GrindTab.Controls.Add(this.StartBtn);
            this.GrindTab.Location = new System.Drawing.Point(4, 25);
            this.GrindTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GrindTab.Name = "GrindTab";
            this.GrindTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GrindTab.Size = new System.Drawing.Size(953, 504);
            this.GrindTab.TabIndex = 0;
            this.GrindTab.Text = "Grind";
            this.GrindTab.UseVisualStyleBackColor = true;
            // 
            // EditTab
            // 
            this.EditTab.Controls.Add(this.SaveAsRecipeBtn);
            this.EditTab.Controls.Add(this.NewRecipeBtn);
            this.EditTab.Controls.Add(this.LoadRecipeBtn);
            this.EditTab.Controls.Add(this.SaveRecipeBtn);
            this.EditTab.Controls.Add(this.RecipeRTB);
            this.EditTab.Location = new System.Drawing.Point(4, 25);
            this.EditTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditTab.Name = "EditTab";
            this.EditTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditTab.Size = new System.Drawing.Size(953, 504);
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
            this.SetupTab.Size = new System.Drawing.Size(953, 504);
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
            // RecipeRTB
            // 
            this.RecipeRTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeRTB.Location = new System.Drawing.Point(17, 28);
            this.RecipeRTB.Name = "RecipeRTB";
            this.RecipeRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeRTB.Size = new System.Drawing.Size(607, 348);
            this.RecipeRTB.TabIndex = 71;
            this.RecipeRTB.Text = "";
            // 
            // SaveAsRecipeBtn
            // 
            this.SaveAsRecipeBtn.Location = new System.Drawing.Point(426, 382);
            this.SaveAsRecipeBtn.Name = "SaveAsRecipeBtn";
            this.SaveAsRecipeBtn.Size = new System.Drawing.Size(112, 77);
            this.SaveAsRecipeBtn.TabIndex = 75;
            this.SaveAsRecipeBtn.Text = "Save As...";
            this.SaveAsRecipeBtn.UseVisualStyleBackColor = true;
            this.SaveAsRecipeBtn.Click += new System.EventHandler(this.SaveAsRecipeBtn_Click);
            // 
            // NewRecipeBtn
            // 
            this.NewRecipeBtn.Location = new System.Drawing.Point(90, 382);
            this.NewRecipeBtn.Name = "NewRecipeBtn";
            this.NewRecipeBtn.Size = new System.Drawing.Size(101, 77);
            this.NewRecipeBtn.TabIndex = 74;
            this.NewRecipeBtn.Text = "New";
            this.NewRecipeBtn.UseVisualStyleBackColor = true;
            this.NewRecipeBtn.Click += new System.EventHandler(this.NewRecipeBtn_Click);
            // 
            // LoadRecipeBtn
            // 
            this.LoadRecipeBtn.Location = new System.Drawing.Point(197, 382);
            this.LoadRecipeBtn.Name = "LoadRecipeBtn";
            this.LoadRecipeBtn.Size = new System.Drawing.Size(109, 77);
            this.LoadRecipeBtn.TabIndex = 73;
            this.LoadRecipeBtn.Text = "Load";
            this.LoadRecipeBtn.UseVisualStyleBackColor = true;
            this.LoadRecipeBtn.Click += new System.EventHandler(this.LoadRecipeBtn_Click);
            // 
            // SaveRecipeBtn
            // 
            this.SaveRecipeBtn.Location = new System.Drawing.Point(312, 382);
            this.SaveRecipeBtn.Name = "SaveRecipeBtn";
            this.SaveRecipeBtn.Size = new System.Drawing.Size(108, 77);
            this.SaveRecipeBtn.TabIndex = 72;
            this.SaveRecipeBtn.Text = "Save";
            this.SaveRecipeBtn.UseVisualStyleBackColor = true;
            this.SaveRecipeBtn.Click += new System.EventHandler(this.SaveRecipeBtn_Click);
            // 
            // RecipeFilenameLbl
            // 
            this.RecipeFilenameLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RecipeFilenameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeFilenameLbl.Location = new System.Drawing.Point(191, 9);
            this.RecipeFilenameLbl.Name = "RecipeFilenameLbl";
            this.RecipeFilenameLbl.Size = new System.Drawing.Size(953, 27);
            this.RecipeFilenameLbl.TabIndex = 76;
            this.RecipeFilenameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(757, 23);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(136, 106);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // PauseBtn
            // 
            this.PauseBtn.Location = new System.Drawing.Point(757, 142);
            this.PauseBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(136, 106);
            this.PauseBtn.TabIndex = 2;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = true;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(757, 382);
            this.StopBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(136, 106);
            this.StopBtn.TabIndex = 3;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // LoadBtn
            // 
            this.LoadBtn.Location = new System.Drawing.Point(144, 23);
            this.LoadBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(136, 106);
            this.LoadBtn.TabIndex = 4;
            this.LoadBtn.Text = "Load";
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.Location = new System.Drawing.Point(757, 266);
            this.ContinueBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(136, 106);
            this.ContinueBtn.TabIndex = 5;
            this.ContinueBtn.Text = "Continue";
            this.ContinueBtn.UseVisualStyleBackColor = true;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // RecipeRoRTB
            // 
            this.RecipeRoRTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeRoRTB.Location = new System.Drawing.Point(144, 140);
            this.RecipeRoRTB.Name = "RecipeRoRTB";
            this.RecipeRoRTB.ReadOnly = true;
            this.RecipeRoRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeRoRTB.Size = new System.Drawing.Size(607, 348);
            this.RecipeRoRTB.TabIndex = 72;
            this.RecipeRoRTB.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 720);
            this.Controls.Add(this.RecipeFilenameLbl);
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
            this.GrindTab.ResumeLayout(false);
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
        private System.Windows.Forms.RichTextBox RecipeRTB;
        private System.Windows.Forms.Button SaveAsRecipeBtn;
        private System.Windows.Forms.Button NewRecipeBtn;
        private System.Windows.Forms.Button LoadRecipeBtn;
        private System.Windows.Forms.Button SaveRecipeBtn;
        private System.Windows.Forms.Label RecipeFilenameLbl;
        private System.Windows.Forms.Button ContinueBtn;
        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.RichTextBox RecipeRoRTB;
    }
}

