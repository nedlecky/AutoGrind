﻿namespace AutoGrind
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
            this.AngleLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DiameterLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.JogBtn = new System.Windows.Forms.Button();
            this.RecipeRoRTB = new System.Windows.Forms.RichTextBox();
            this.ContinueBtn = new System.Windows.Forms.Button();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.EditTab = new System.Windows.Forms.TabPage();
            this.SaveAsRecipeBtn = new System.Windows.Forms.Button();
            this.NewRecipeBtn = new System.Windows.Forms.Button();
            this.LoadRecipeBtn = new System.Windows.Forms.Button();
            this.SaveRecipeBtn = new System.Windows.Forms.Button();
            this.RecipeRTB = new System.Windows.Forms.RichTextBox();
            this.SetupTab = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.DefaultConfigBtn = new System.Windows.Forms.Button();
            this.LoadConfigBtn = new System.Windows.Forms.Button();
            this.SaveConfigBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RobotSendBtn = new System.Windows.Forms.Button();
            this.RobotMessageTxt = new System.Windows.Forms.TextBox();
            this.RobotDisconnectBtn = new System.Windows.Forms.Button();
            this.RobotConnectBtn = new System.Windows.Forms.Button();
            this.RobotIpPortTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.RecipeFilenameLbl = new System.Windows.Forms.Label();
            this.ExecTmr = new System.Windows.Forms.Timer(this.components);
            this.KeyboardBtn = new System.Windows.Forms.Button();
            this.MessageTmr = new System.Windows.Forms.Timer(this.components);
            this.VariableTab = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ReadVariableBtn = new System.Windows.Forms.Button();
            this.WriteStringValueTxt = new System.Windows.Forms.TextBox();
            this.WriteStringValueBtn = new System.Windows.Forms.Button();
            this.VariableNameTxt = new System.Windows.Forms.TextBox();
            this.VariablesGrd = new System.Windows.Forms.DataGridView();
            this.LoadVariablesBtn = new System.Windows.Forms.Button();
            this.SaveVariablesBtn = new System.Windows.Forms.Button();
            this.ClearVariablesBtn = new System.Windows.Forms.Button();
            this.UtcTimeChk = new System.Windows.Forms.CheckBox();
            this.OperationTab.SuspendLayout();
            this.GrindTab.SuspendLayout();
            this.EditTab.SuspendLayout();
            this.SetupTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.LogsTab.SuspendLayout();
            this.VariableTab.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VariablesGrd)).BeginInit();
            this.SuspendLayout();
            // 
            // GrindBtn
            // 
            this.GrindBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindBtn.ForeColor = System.Drawing.Color.White;
            this.GrindBtn.Location = new System.Drawing.Point(9, 44);
            this.GrindBtn.Margin = new System.Windows.Forms.Padding(2);
            this.GrindBtn.Name = "GrindBtn";
            this.GrindBtn.Size = new System.Drawing.Size(102, 86);
            this.GrindBtn.TabIndex = 0;
            this.GrindBtn.Text = "Grind";
            this.GrindBtn.UseVisualStyleBackColor = true;
            this.GrindBtn.Click += new System.EventHandler(this.GrindBtn_Click);
            // 
            // EditBtn
            // 
            this.EditBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.EditBtn.ForeColor = System.Drawing.Color.White;
            this.EditBtn.Location = new System.Drawing.Point(9, 154);
            this.EditBtn.Margin = new System.Windows.Forms.Padding(2);
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(102, 86);
            this.EditBtn.TabIndex = 1;
            this.EditBtn.Text = "Edit";
            this.EditBtn.UseVisualStyleBackColor = true;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // SetupBtn
            // 
            this.SetupBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.SetupBtn.ForeColor = System.Drawing.Color.White;
            this.SetupBtn.Location = new System.Drawing.Point(9, 264);
            this.SetupBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SetupBtn.Name = "SetupBtn";
            this.SetupBtn.Size = new System.Drawing.Size(102, 86);
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
            this.OperationTab.Controls.Add(this.VariableTab);
            this.OperationTab.Controls.Add(this.LogsTab);
            this.OperationTab.Location = new System.Drawing.Point(140, 37);
            this.OperationTab.Margin = new System.Windows.Forms.Padding(2);
            this.OperationTab.Name = "OperationTab";
            this.OperationTab.SelectedIndex = 0;
            this.OperationTab.Size = new System.Drawing.Size(745, 493);
            this.OperationTab.TabIndex = 3;
            this.OperationTab.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.OperationTab_Selecting);
            // 
            // GrindTab
            // 
            this.GrindTab.Controls.Add(this.AngleLbl);
            this.GrindTab.Controls.Add(this.label4);
            this.GrindTab.Controls.Add(this.DiameterLbl);
            this.GrindTab.Controls.Add(this.label2);
            this.GrindTab.Controls.Add(this.JogBtn);
            this.GrindTab.Controls.Add(this.RecipeRoRTB);
            this.GrindTab.Controls.Add(this.ContinueBtn);
            this.GrindTab.Controls.Add(this.LoadBtn);
            this.GrindTab.Controls.Add(this.StopBtn);
            this.GrindTab.Controls.Add(this.PauseBtn);
            this.GrindTab.Controls.Add(this.StartBtn);
            this.GrindTab.Location = new System.Drawing.Point(4, 22);
            this.GrindTab.Margin = new System.Windows.Forms.Padding(2);
            this.GrindTab.Name = "GrindTab";
            this.GrindTab.Padding = new System.Windows.Forms.Padding(2);
            this.GrindTab.Size = new System.Drawing.Size(737, 467);
            this.GrindTab.TabIndex = 0;
            this.GrindTab.Text = "Grind";
            this.GrindTab.UseVisualStyleBackColor = true;
            // 
            // AngleLbl
            // 
            this.AngleLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AngleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AngleLbl.Location = new System.Drawing.Point(553, 7);
            this.AngleLbl.Name = "AngleLbl";
            this.AngleLbl.Size = new System.Drawing.Size(170, 37);
            this.AngleLbl.TabIndex = 77;
            this.AngleLbl.Text = "0.00";
            this.AngleLbl.Click += new System.EventHandler(this.AngleLbl_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(377, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 37);
            this.label4.TabIndex = 76;
            this.label4.Text = "Angle, deg";
            // 
            // DiameterLbl
            // 
            this.DiameterLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DiameterLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiameterLbl.Location = new System.Drawing.Point(229, 7);
            this.DiameterLbl.Name = "DiameterLbl";
            this.DiameterLbl.Size = new System.Drawing.Size(142, 37);
            this.DiameterLbl.TabIndex = 75;
            this.DiameterLbl.Text = "25.0";
            this.DiameterLbl.Click += new System.EventHandler(this.DiameterLbl_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 37);
            this.label2.TabIndex = 74;
            this.label2.Text = "Diameter, mm";
            // 
            // JogBtn
            // 
            this.JogBtn.BackColor = System.Drawing.Color.Green;
            this.JogBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.JogBtn.ForeColor = System.Drawing.Color.White;
            this.JogBtn.Location = new System.Drawing.Point(12, 355);
            this.JogBtn.Margin = new System.Windows.Forms.Padding(2);
            this.JogBtn.Name = "JogBtn";
            this.JogBtn.Size = new System.Drawing.Size(247, 86);
            this.JogBtn.TabIndex = 73;
            this.JogBtn.Text = "Jog Robot";
            this.JogBtn.UseVisualStyleBackColor = false;
            this.JogBtn.Click += new System.EventHandler(this.JogBtn_Click);
            // 
            // RecipeRoRTB
            // 
            this.RecipeRoRTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeRoRTB.Location = new System.Drawing.Point(12, 60);
            this.RecipeRoRTB.Margin = new System.Windows.Forms.Padding(2);
            this.RecipeRoRTB.Name = "RecipeRoRTB";
            this.RecipeRoRTB.ReadOnly = true;
            this.RecipeRoRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeRoRTB.Size = new System.Drawing.Size(560, 284);
            this.RecipeRoRTB.TabIndex = 72;
            this.RecipeRoRTB.Text = "";
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.BackColor = System.Drawing.Color.Gray;
            this.ContinueBtn.Enabled = false;
            this.ContinueBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.ContinueBtn.ForeColor = System.Drawing.Color.White;
            this.ContinueBtn.Location = new System.Drawing.Point(585, 257);
            this.ContinueBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(129, 86);
            this.ContinueBtn.TabIndex = 5;
            this.ContinueBtn.Text = "Continue";
            this.ContinueBtn.UseVisualStyleBackColor = false;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // LoadBtn
            // 
            this.LoadBtn.BackColor = System.Drawing.Color.Gray;
            this.LoadBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.LoadBtn.ForeColor = System.Drawing.Color.White;
            this.LoadBtn.Location = new System.Drawing.Point(325, 353);
            this.LoadBtn.Margin = new System.Windows.Forms.Padding(2);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(247, 86);
            this.LoadBtn.TabIndex = 4;
            this.LoadBtn.Text = "Load Recipe";
            this.LoadBtn.UseVisualStyleBackColor = false;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.BackColor = System.Drawing.Color.Gray;
            this.StopBtn.Enabled = false;
            this.StopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.StopBtn.ForeColor = System.Drawing.Color.White;
            this.StopBtn.Location = new System.Drawing.Point(585, 351);
            this.StopBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(129, 86);
            this.StopBtn.TabIndex = 3;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // PauseBtn
            // 
            this.PauseBtn.BackColor = System.Drawing.Color.Gray;
            this.PauseBtn.Enabled = false;
            this.PauseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.PauseBtn.ForeColor = System.Drawing.Color.White;
            this.PauseBtn.Location = new System.Drawing.Point(585, 156);
            this.PauseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(129, 86);
            this.PauseBtn.TabIndex = 2;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = false;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.Color.Gray;
            this.StartBtn.Enabled = false;
            this.StartBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.StartBtn.ForeColor = System.Drawing.Color.White;
            this.StartBtn.Location = new System.Drawing.Point(585, 60);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(129, 86);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // EditTab
            // 
            this.EditTab.Controls.Add(this.SaveAsRecipeBtn);
            this.EditTab.Controls.Add(this.NewRecipeBtn);
            this.EditTab.Controls.Add(this.LoadRecipeBtn);
            this.EditTab.Controls.Add(this.SaveRecipeBtn);
            this.EditTab.Controls.Add(this.RecipeRTB);
            this.EditTab.Location = new System.Drawing.Point(4, 22);
            this.EditTab.Margin = new System.Windows.Forms.Padding(2);
            this.EditTab.Name = "EditTab";
            this.EditTab.Padding = new System.Windows.Forms.Padding(2);
            this.EditTab.Size = new System.Drawing.Size(737, 467);
            this.EditTab.TabIndex = 1;
            this.EditTab.Text = "Edit";
            this.EditTab.UseVisualStyleBackColor = true;
            // 
            // SaveAsRecipeBtn
            // 
            this.SaveAsRecipeBtn.Enabled = false;
            this.SaveAsRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.SaveAsRecipeBtn.Location = new System.Drawing.Point(265, 343);
            this.SaveAsRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SaveAsRecipeBtn.Name = "SaveAsRecipeBtn";
            this.SaveAsRecipeBtn.Size = new System.Drawing.Size(84, 63);
            this.SaveAsRecipeBtn.TabIndex = 75;
            this.SaveAsRecipeBtn.Text = "Save As...";
            this.SaveAsRecipeBtn.UseVisualStyleBackColor = true;
            this.SaveAsRecipeBtn.Click += new System.EventHandler(this.SaveAsRecipeBtn_Click);
            // 
            // NewRecipeBtn
            // 
            this.NewRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.NewRecipeBtn.Location = new System.Drawing.Point(13, 343);
            this.NewRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.NewRecipeBtn.Name = "NewRecipeBtn";
            this.NewRecipeBtn.Size = new System.Drawing.Size(76, 63);
            this.NewRecipeBtn.TabIndex = 74;
            this.NewRecipeBtn.Text = "New";
            this.NewRecipeBtn.UseVisualStyleBackColor = true;
            this.NewRecipeBtn.Click += new System.EventHandler(this.NewRecipeBtn_Click);
            // 
            // LoadRecipeBtn
            // 
            this.LoadRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.LoadRecipeBtn.Location = new System.Drawing.Point(93, 343);
            this.LoadRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.LoadRecipeBtn.Name = "LoadRecipeBtn";
            this.LoadRecipeBtn.Size = new System.Drawing.Size(82, 63);
            this.LoadRecipeBtn.TabIndex = 73;
            this.LoadRecipeBtn.Text = "Load";
            this.LoadRecipeBtn.UseVisualStyleBackColor = true;
            this.LoadRecipeBtn.Click += new System.EventHandler(this.LoadRecipeBtn_Click);
            // 
            // SaveRecipeBtn
            // 
            this.SaveRecipeBtn.Enabled = false;
            this.SaveRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.SaveRecipeBtn.Location = new System.Drawing.Point(179, 343);
            this.SaveRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SaveRecipeBtn.Name = "SaveRecipeBtn";
            this.SaveRecipeBtn.Size = new System.Drawing.Size(81, 63);
            this.SaveRecipeBtn.TabIndex = 72;
            this.SaveRecipeBtn.Text = "Save";
            this.SaveRecipeBtn.UseVisualStyleBackColor = true;
            this.SaveRecipeBtn.Click += new System.EventHandler(this.SaveRecipeBtn_Click);
            // 
            // RecipeRTB
            // 
            this.RecipeRTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeRTB.Location = new System.Drawing.Point(13, 23);
            this.RecipeRTB.Margin = new System.Windows.Forms.Padding(2);
            this.RecipeRTB.Name = "RecipeRTB";
            this.RecipeRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeRTB.Size = new System.Drawing.Size(701, 316);
            this.RecipeRTB.TabIndex = 71;
            this.RecipeRTB.Text = "";
            this.RecipeRTB.ModifiedChanged += new System.EventHandler(this.RecipeRTB_ModifiedChanged);
            // 
            // SetupTab
            // 
            this.SetupTab.Controls.Add(this.button3);
            this.SetupTab.Controls.Add(this.button2);
            this.SetupTab.Controls.Add(this.button1);
            this.SetupTab.Controls.Add(this.DefaultConfigBtn);
            this.SetupTab.Controls.Add(this.LoadConfigBtn);
            this.SetupTab.Controls.Add(this.SaveConfigBtn);
            this.SetupTab.Controls.Add(this.groupBox1);
            this.SetupTab.Location = new System.Drawing.Point(4, 22);
            this.SetupTab.Margin = new System.Windows.Forms.Padding(2);
            this.SetupTab.Name = "SetupTab";
            this.SetupTab.Size = new System.Drawing.Size(737, 467);
            this.SetupTab.TabIndex = 2;
            this.SetupTab.Text = "Setup";
            this.SetupTab.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Green;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(516, 146);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(161, 107);
            this.button3.TabIndex = 77;
            this.button3.Text = "Set Top of Dome";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Green;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(204, 146);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 107);
            this.button2.TabIndex = 76;
            this.button2.Text = "Set Right End of Cylinder";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Green;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(29, 146);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 107);
            this.button1.TabIndex = 75;
            this.button1.Text = "Set Left End of Cylinder";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // DefaultConfigBtn
            // 
            this.DefaultConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.DefaultConfigBtn.Location = new System.Drawing.Point(13, 396);
            this.DefaultConfigBtn.Name = "DefaultConfigBtn";
            this.DefaultConfigBtn.Size = new System.Drawing.Size(155, 55);
            this.DefaultConfigBtn.TabIndex = 74;
            this.DefaultConfigBtn.Text = "Set Default";
            this.DefaultConfigBtn.UseVisualStyleBackColor = true;
            this.DefaultConfigBtn.Click += new System.EventHandler(this.DefaultConfigBtn_Click);
            // 
            // LoadConfigBtn
            // 
            this.LoadConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.LoadConfigBtn.Location = new System.Drawing.Point(174, 396);
            this.LoadConfigBtn.Name = "LoadConfigBtn";
            this.LoadConfigBtn.Size = new System.Drawing.Size(118, 55);
            this.LoadConfigBtn.TabIndex = 73;
            this.LoadConfigBtn.Text = "Reload";
            this.LoadConfigBtn.UseVisualStyleBackColor = true;
            this.LoadConfigBtn.Click += new System.EventHandler(this.LoadConfigBtn_Click);
            // 
            // SaveConfigBtn
            // 
            this.SaveConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.SaveConfigBtn.Location = new System.Drawing.Point(298, 396);
            this.SaveConfigBtn.Name = "SaveConfigBtn";
            this.SaveConfigBtn.Size = new System.Drawing.Size(134, 55);
            this.SaveConfigBtn.TabIndex = 72;
            this.SaveConfigBtn.Text = "Save";
            this.SaveConfigBtn.UseVisualStyleBackColor = true;
            this.SaveConfigBtn.Click += new System.EventHandler(this.SaveConfigBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UtcTimeChk);
            this.groupBox1.Controls.Add(this.RobotSendBtn);
            this.groupBox1.Controls.Add(this.RobotMessageTxt);
            this.groupBox1.Controls.Add(this.RobotDisconnectBtn);
            this.groupBox1.Controls.Add(this.RobotConnectBtn);
            this.groupBox1.Controls.Add(this.RobotIpPortTxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.AutoGrindRootLbl);
            this.groupBox1.Controls.Add(this.ChangeLEonardRootBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(710, 98);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // RobotSendBtn
            // 
            this.RobotSendBtn.Location = new System.Drawing.Point(311, 59);
            this.RobotSendBtn.Name = "RobotSendBtn";
            this.RobotSendBtn.Size = new System.Drawing.Size(75, 23);
            this.RobotSendBtn.TabIndex = 76;
            this.RobotSendBtn.Text = "Send";
            this.RobotSendBtn.UseVisualStyleBackColor = true;
            this.RobotSendBtn.Click += new System.EventHandler(this.RobotSendBtn_Click);
            // 
            // RobotMessageTxt
            // 
            this.RobotMessageTxt.Location = new System.Drawing.Point(135, 62);
            this.RobotMessageTxt.Name = "RobotMessageTxt";
            this.RobotMessageTxt.Size = new System.Drawing.Size(170, 20);
            this.RobotMessageTxt.TabIndex = 75;
            this.RobotMessageTxt.Text = "(1,0,0,0,0)";
            // 
            // RobotDisconnectBtn
            // 
            this.RobotDisconnectBtn.Location = new System.Drawing.Point(392, 34);
            this.RobotDisconnectBtn.Name = "RobotDisconnectBtn";
            this.RobotDisconnectBtn.Size = new System.Drawing.Size(75, 23);
            this.RobotDisconnectBtn.TabIndex = 74;
            this.RobotDisconnectBtn.Text = "Disconnect";
            this.RobotDisconnectBtn.UseVisualStyleBackColor = true;
            this.RobotDisconnectBtn.Click += new System.EventHandler(this.RobotDisconnectBtn_Click);
            // 
            // RobotConnectBtn
            // 
            this.RobotConnectBtn.Location = new System.Drawing.Point(311, 34);
            this.RobotConnectBtn.Name = "RobotConnectBtn";
            this.RobotConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.RobotConnectBtn.TabIndex = 73;
            this.RobotConnectBtn.Text = "Connect";
            this.RobotConnectBtn.UseVisualStyleBackColor = true;
            this.RobotConnectBtn.Click += new System.EventHandler(this.RobotConnectBtn_Click);
            // 
            // RobotIpPortTxt
            // 
            this.RobotIpPortTxt.Location = new System.Drawing.Point(135, 36);
            this.RobotIpPortTxt.Name = "RobotIpPortTxt";
            this.RobotIpPortTxt.Size = new System.Drawing.Size(170, 20);
            this.RobotIpPortTxt.TabIndex = 72;
            this.RobotIpPortTxt.Text = "192.168.25.1:30000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Robot IP:Port";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AutoGrindRootLbl
            // 
            this.AutoGrindRootLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AutoGrindRootLbl.Location = new System.Drawing.Point(136, 10);
            this.AutoGrindRootLbl.Name = "AutoGrindRootLbl";
            this.AutoGrindRootLbl.Size = new System.Drawing.Size(385, 23);
            this.AutoGrindRootLbl.TabIndex = 69;
            this.AutoGrindRootLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChangeLEonardRootBtn
            // 
            this.ChangeLEonardRootBtn.Location = new System.Drawing.Point(527, 10);
            this.ChangeLEonardRootBtn.Name = "ChangeLEonardRootBtn";
            this.ChangeLEonardRootBtn.Size = new System.Drawing.Size(24, 23);
            this.ChangeLEonardRootBtn.TabIndex = 70;
            this.ChangeLEonardRootBtn.Text = "...";
            this.ChangeLEonardRootBtn.UseVisualStyleBackColor = true;
            this.ChangeLEonardRootBtn.Click += new System.EventHandler(this.ChangeLEonardRootBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "AutoGrind Root Directory";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LogsTab
            // 
            this.LogsTab.Controls.Add(this.UrLogRTB);
            this.LogsTab.Controls.Add(this.ErrorLogRTB);
            this.LogsTab.Location = new System.Drawing.Point(4, 22);
            this.LogsTab.Margin = new System.Windows.Forms.Padding(2);
            this.LogsTab.Name = "LogsTab";
            this.LogsTab.Size = new System.Drawing.Size(737, 467);
            this.LogsTab.TabIndex = 3;
            this.LogsTab.Text = "Logs";
            this.LogsTab.UseVisualStyleBackColor = true;
            // 
            // UrLogRTB
            // 
            this.UrLogRTB.Location = new System.Drawing.Point(26, 231);
            this.UrLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.UrLogRTB.Name = "UrLogRTB";
            this.UrLogRTB.Size = new System.Drawing.Size(677, 171);
            this.UrLogRTB.TabIndex = 1;
            this.UrLogRTB.Text = "";
            // 
            // ErrorLogRTB
            // 
            this.ErrorLogRTB.Location = new System.Drawing.Point(29, 37);
            this.ErrorLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.ErrorLogRTB.Name = "ErrorLogRTB";
            this.ErrorLogRTB.Size = new System.Drawing.Size(673, 171);
            this.ErrorLogRTB.TabIndex = 0;
            this.ErrorLogRTB.Text = "";
            // 
            // AllLogRTB
            // 
            this.AllLogRTB.Location = new System.Drawing.Point(9, 534);
            this.AllLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.AllLogRTB.Name = "AllLogRTB";
            this.AllLogRTB.Size = new System.Drawing.Size(872, 100);
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
            this.timeLbl.Location = new System.Drawing.Point(9, 7);
            this.timeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeLbl.Name = "timeLbl";
            this.timeLbl.Size = new System.Drawing.Size(126, 22);
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
            // RecipeFilenameLbl
            // 
            this.RecipeFilenameLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RecipeFilenameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeFilenameLbl.Location = new System.Drawing.Point(143, 7);
            this.RecipeFilenameLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RecipeFilenameLbl.Name = "RecipeFilenameLbl";
            this.RecipeFilenameLbl.Size = new System.Drawing.Size(715, 22);
            this.RecipeFilenameLbl.TabIndex = 76;
            this.RecipeFilenameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExecTmr
            // 
            this.ExecTmr.Tick += new System.EventHandler(this.ExecTmr_Tick);
            // 
            // KeyboardBtn
            // 
            this.KeyboardBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyboardBtn.ForeColor = System.Drawing.Color.White;
            this.KeyboardBtn.Location = new System.Drawing.Point(11, 424);
            this.KeyboardBtn.Margin = new System.Windows.Forms.Padding(2);
            this.KeyboardBtn.Name = "KeyboardBtn";
            this.KeyboardBtn.Size = new System.Drawing.Size(102, 86);
            this.KeyboardBtn.TabIndex = 77;
            this.KeyboardBtn.Text = "Keyboard";
            this.KeyboardBtn.UseVisualStyleBackColor = true;
            this.KeyboardBtn.Click += new System.EventHandler(this.KeyboardBtn_Click);
            // 
            // MessageTmr
            // 
            this.MessageTmr.Tick += new System.EventHandler(this.MessageTmr_Tick);
            // 
            // VariableTab
            // 
            this.VariableTab.Controls.Add(this.LoadVariablesBtn);
            this.VariableTab.Controls.Add(this.SaveVariablesBtn);
            this.VariableTab.Controls.Add(this.ClearVariablesBtn);
            this.VariableTab.Controls.Add(this.VariablesGrd);
            this.VariableTab.Controls.Add(this.groupBox7);
            this.VariableTab.Location = new System.Drawing.Point(4, 22);
            this.VariableTab.Name = "VariableTab";
            this.VariableTab.Size = new System.Drawing.Size(737, 467);
            this.VariableTab.TabIndex = 4;
            this.VariableTab.Text = "Variables";
            this.VariableTab.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ReadVariableBtn);
            this.groupBox7.Controls.Add(this.WriteStringValueTxt);
            this.groupBox7.Controls.Add(this.WriteStringValueBtn);
            this.groupBox7.Controls.Add(this.VariableNameTxt);
            this.groupBox7.Location = new System.Drawing.Point(20, 22);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(650, 49);
            this.groupBox7.TabIndex = 82;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Manual";
            // 
            // ReadVariableBtn
            // 
            this.ReadVariableBtn.Location = new System.Drawing.Point(140, 17);
            this.ReadVariableBtn.Name = "ReadVariableBtn";
            this.ReadVariableBtn.Size = new System.Drawing.Size(75, 23);
            this.ReadVariableBtn.TabIndex = 2;
            this.ReadVariableBtn.Text = "Read";
            this.ReadVariableBtn.UseVisualStyleBackColor = true;
            this.ReadVariableBtn.Click += new System.EventHandler(this.ReadVariableBtn_Click);
            // 
            // WriteStringValueTxt
            // 
            this.WriteStringValueTxt.Location = new System.Drawing.Point(338, 22);
            this.WriteStringValueTxt.Name = "WriteStringValueTxt";
            this.WriteStringValueTxt.Size = new System.Drawing.Size(209, 20);
            this.WriteStringValueTxt.TabIndex = 7;
            this.WriteStringValueTxt.Text = "Test String";
            // 
            // WriteStringValueBtn
            // 
            this.WriteStringValueBtn.Location = new System.Drawing.Point(257, 19);
            this.WriteStringValueBtn.Name = "WriteStringValueBtn";
            this.WriteStringValueBtn.Size = new System.Drawing.Size(75, 23);
            this.WriteStringValueBtn.TabIndex = 4;
            this.WriteStringValueBtn.Text = "Write String";
            this.WriteStringValueBtn.UseVisualStyleBackColor = true;
            this.WriteStringValueBtn.Click += new System.EventHandler(this.WriteStringValueBtn_Click);
            // 
            // VariableNameTxt
            // 
            this.VariableNameTxt.Location = new System.Drawing.Point(6, 19);
            this.VariableNameTxt.Name = "VariableNameTxt";
            this.VariableNameTxt.Size = new System.Drawing.Size(128, 20);
            this.VariableNameTxt.TabIndex = 5;
            this.VariableNameTxt.Text = "X";
            // 
            // VariablesGrd
            // 
            this.VariablesGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VariablesGrd.Location = new System.Drawing.Point(20, 77);
            this.VariablesGrd.Name = "VariablesGrd";
            this.VariablesGrd.Size = new System.Drawing.Size(673, 342);
            this.VariablesGrd.TabIndex = 83;
            // 
            // LoadVariablesBtn
            // 
            this.LoadVariablesBtn.Location = new System.Drawing.Point(18, 428);
            this.LoadVariablesBtn.Name = "LoadVariablesBtn";
            this.LoadVariablesBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadVariablesBtn.TabIndex = 86;
            this.LoadVariablesBtn.Text = "Reload";
            this.LoadVariablesBtn.UseVisualStyleBackColor = true;
            this.LoadVariablesBtn.Click += new System.EventHandler(this.LoadVariablesBtn_Click);
            // 
            // SaveVariablesBtn
            // 
            this.SaveVariablesBtn.Location = new System.Drawing.Point(99, 428);
            this.SaveVariablesBtn.Name = "SaveVariablesBtn";
            this.SaveVariablesBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveVariablesBtn.TabIndex = 85;
            this.SaveVariablesBtn.Text = "Save";
            this.SaveVariablesBtn.UseVisualStyleBackColor = true;
            this.SaveVariablesBtn.Click += new System.EventHandler(this.SaveVariablesBtn_Click);
            // 
            // ClearVariablesBtn
            // 
            this.ClearVariablesBtn.Location = new System.Drawing.Point(180, 428);
            this.ClearVariablesBtn.Name = "ClearVariablesBtn";
            this.ClearVariablesBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearVariablesBtn.TabIndex = 84;
            this.ClearVariablesBtn.Text = "Clear";
            this.ClearVariablesBtn.UseVisualStyleBackColor = true;
            this.ClearVariablesBtn.Click += new System.EventHandler(this.ClearVariablesBtn_Click);
            // 
            // UtcTimeChk
            // 
            this.UtcTimeChk.AutoSize = true;
            this.UtcTimeChk.Location = new System.Drawing.Point(524, 65);
            this.UtcTimeChk.Name = "UtcTimeChk";
            this.UtcTimeChk.Size = new System.Drawing.Size(177, 17);
            this.UtcTimeChk.TabIndex = 77;
            this.UtcTimeChk.Text = "Use UTC Time in Time Stamps?";
            this.UtcTimeChk.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 645);
            this.Controls.Add(this.KeyboardBtn);
            this.Controls.Add(this.RecipeFilenameLbl);
            this.Controls.Add(this.timeLbl);
            this.Controls.Add(this.AllLogRTB);
            this.Controls.Add(this.OperationTab);
            this.Controls.Add(this.SetupBtn);
            this.Controls.Add(this.EditBtn);
            this.Controls.Add(this.GrindBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Code Sets This Caption";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.OperationTab.ResumeLayout(false);
            this.GrindTab.ResumeLayout(false);
            this.GrindTab.PerformLayout();
            this.EditTab.ResumeLayout(false);
            this.SetupTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.LogsTab.ResumeLayout(false);
            this.VariableTab.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VariablesGrd)).EndInit();
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
        private System.Windows.Forms.Button JogBtn;
        private System.Windows.Forms.Label DiameterLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label AngleLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer ExecTmr;
        private System.Windows.Forms.Button KeyboardBtn;
        private System.Windows.Forms.Button RobotDisconnectBtn;
        private System.Windows.Forms.Button RobotConnectBtn;
        private System.Windows.Forms.TextBox RobotIpPortTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RobotSendBtn;
        private System.Windows.Forms.TextBox RobotMessageTxt;
        private System.Windows.Forms.Timer MessageTmr;
        private System.Windows.Forms.TabPage VariableTab;
        private System.Windows.Forms.Button LoadVariablesBtn;
        private System.Windows.Forms.Button SaveVariablesBtn;
        private System.Windows.Forms.Button ClearVariablesBtn;
        private System.Windows.Forms.DataGridView VariablesGrd;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button ReadVariableBtn;
        private System.Windows.Forms.TextBox WriteStringValueTxt;
        private System.Windows.Forms.Button WriteStringValueBtn;
        private System.Windows.Forms.TextBox VariableNameTxt;
        private System.Windows.Forms.CheckBox UtcTimeChk;
    }
}

