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
            this.CurrentLineLbl = new System.Windows.Forms.Label();
            this.JogBtn = new System.Windows.Forms.Button();
            this.RecipeRTB = new System.Windows.Forms.RichTextBox();
            this.ContinueBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.RecipeFilenameLbl = new System.Windows.Forms.Label();
            this.SaveAsRecipeBtn = new System.Windows.Forms.Button();
            this.NewRecipeBtn = new System.Windows.Forms.Button();
            this.LoadRecipeBtn = new System.Windows.Forms.Button();
            this.SaveRecipeBtn = new System.Windows.Forms.Button();
            this.HeartbeatTmr = new System.Windows.Forms.Timer(this.components);
            this.timeLbl = new System.Windows.Forms.Label();
            this.StartupTmr = new System.Windows.Forms.Timer(this.components);
            this.CloseTmr = new System.Windows.Forms.Timer(this.components);
            this.ExecTmr = new System.Windows.Forms.Timer(this.components);
            this.MessageTmr = new System.Windows.Forms.Timer(this.components);
            this.RobotCommandStatusLbl = new System.Windows.Forms.Label();
            this.GrindReadyLbl = new System.Windows.Forms.Label();
            this.RobotReadyLbl = new System.Windows.Forms.Label();
            this.GrindContactEnabledBtn = new System.Windows.Forms.Button();
            this.MonitorTab = new System.Windows.Forms.TabControl();
            this.positionsPage = new System.Windows.Forms.TabPage();
            this.PositionMoveArmBtn = new System.Windows.Forms.Button();
            this.PositionMovePoseBtn = new System.Windows.Forms.Button();
            this.PositionSetBtn = new System.Windows.Forms.Button();
            this.ClearAllPositionsBtn = new System.Windows.Forms.Button();
            this.LoadPositionsBtn = new System.Windows.Forms.Button();
            this.SavePositionsBtn = new System.Windows.Forms.Button();
            this.ClearPositionsBtn = new System.Windows.Forms.Button();
            this.PositionsGrd = new System.Windows.Forms.DataGridView();
            this.variablesPage = new System.Windows.Forms.TabPage();
            this.ClearAllVariablesBtn = new System.Windows.Forms.Button();
            this.LoadVariablesBtn = new System.Windows.Forms.Button();
            this.SaveVariablesBtn = new System.Windows.Forms.Button();
            this.ClearVariablesBtn = new System.Windows.Forms.Button();
            this.VariablesGrd = new System.Windows.Forms.DataGridView();
            this.manualPage = new System.Windows.Forms.TabPage();
            this.LoadManualBtn = new System.Windows.Forms.Button();
            this.SaveManualBtn = new System.Windows.Forms.Button();
            this.InstructionsRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.ClearExecLogRtbBtn = new System.Windows.Forms.Button();
            this.ExecLogRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ClearUrLogRtbBtn = new System.Windows.Forms.Button();
            this.UrLogRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ClearErrorLogRtbBtn = new System.Windows.Forms.Button();
            this.ErrorLogRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ClearAllLogRtbBtn = new System.Windows.Forms.Button();
            this.AllLogRTB = new System.Windows.Forms.RichTextBox();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.MountedToolBox = new System.Windows.Forms.ComboBox();
            this.RobotDashboardStatusLbl = new System.Windows.Forms.Label();
            this.OperatorModeBox = new System.Windows.Forms.ComboBox();
            this.RobotModeBtn = new System.Windows.Forms.Button();
            this.SafetyStatusBtn = new System.Windows.Forms.Button();
            this.ProgramStateBtn = new System.Windows.Forms.Button();
            this.SetJointAccelBtn = new System.Windows.Forms.Button();
            this.SetJointSpeedBtn = new System.Windows.Forms.Button();
            this.SetLinearSpeedBtn = new System.Windows.Forms.Button();
            this.SetLinearAccelBtn = new System.Windows.Forms.Button();
            this.SetBlendRadiusBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.DiameterDimLbl = new System.Windows.Forms.Label();
            this.MainTab = new System.Windows.Forms.TabControl();
            this.RunPage = new System.Windows.Forms.TabPage();
            this.JogRunBtn = new System.Windows.Forms.Button();
            this.RecipeRTBCopy = new System.Windows.Forms.RichTextBox();
            this.Grind = new System.Windows.Forms.Label();
            this.GrindNCyclesLbl = new System.Windows.Forms.Label();
            this.GrindCycleLbl = new System.Windows.Forms.Label();
            this.CurrentLineLblCopy = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.RecipeFilenameOnlyLblCopy = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.RobotModelLbl = new System.Windows.Forms.Label();
            this.RobotSerialNumberLbl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DashboardMessageTxt = new System.Windows.Forms.TextBox();
            this.DashboardSendBtn = new System.Windows.Forms.Button();
            this.RobotConnectBtn = new System.Windows.Forms.Button();
            this.RobotDisconnectBtn = new System.Windows.Forms.Button();
            this.RobotSendBtn = new System.Windows.Forms.Button();
            this.RobotMessageTxt = new System.Windows.Forms.TextBox();
            this.ProgramPage = new System.Windows.Forms.TabPage();
            this.MovePage = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.SetTouchRetractBtn = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.SetupPage = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.SelectToolBtn = new System.Windows.Forms.Button();
            this.ToolsGrd = new System.Windows.Forms.DataGridView();
            this.LoadToolsBtn = new System.Windows.Forms.Button();
            this.SaveToolsBtn = new System.Windows.Forms.Button();
            this.ClearToolsBtn = new System.Windows.Forms.Button();
            this.DefaultConfigBtn = new System.Windows.Forms.Button();
            this.LoadConfigBtn = new System.Windows.Forms.Button();
            this.SaveConfigBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RobotProgramTxt = new System.Windows.Forms.TextBox();
            this.ServerIpTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UtcTimeChk = new System.Windows.Forms.CheckBox();
            this.RobotIpTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AutoGrindRootLbl = new System.Windows.Forms.Label();
            this.ChangeRootDirectoryBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DebugLevelCombo = new System.Windows.Forms.ComboBox();
            this.IoPage = new System.Windows.Forms.TabPage();
            this.LogPage = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ClearUrDashboardRtbBtn = new System.Windows.Forms.Button();
            this.UrDashboardLogRTB = new System.Windows.Forms.RichTextBox();
            this.DiameterLbl = new System.Windows.Forms.Label();
            this.PartGeometryBox = new System.Windows.Forms.ComboBox();
            this.RecipeFilenameOnlyLbl = new System.Windows.Forms.Label();
            this.MonitorTab.SuspendLayout();
            this.positionsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PositionsGrd)).BeginInit();
            this.variablesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VariablesGrd)).BeginInit();
            this.manualPage.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.RunPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.ProgramPage.SuspendLayout();
            this.MovePage.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SetupPage.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsGrd)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.LogPage.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentLineLbl
            // 
            this.CurrentLineLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentLineLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLineLbl.Location = new System.Drawing.Point(5, 66);
            this.CurrentLineLbl.Name = "CurrentLineLbl";
            this.CurrentLineLbl.Size = new System.Drawing.Size(786, 44);
            this.CurrentLineLbl.TabIndex = 79;
            this.CurrentLineLbl.TextChanged += new System.EventHandler(this.CurrentLineLbl_TextChanged);
            // 
            // JogBtn
            // 
            this.JogBtn.BackColor = System.Drawing.Color.Green;
            this.JogBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JogBtn.ForeColor = System.Drawing.Color.White;
            this.JogBtn.Location = new System.Drawing.Point(1257, 450);
            this.JogBtn.Margin = new System.Windows.Forms.Padding(2);
            this.JogBtn.Name = "JogBtn";
            this.JogBtn.Size = new System.Drawing.Size(545, 256);
            this.JogBtn.TabIndex = 73;
            this.JogBtn.Text = "Jog Robot";
            this.JogBtn.UseVisualStyleBackColor = false;
            this.JogBtn.Click += new System.EventHandler(this.JogBtn_Click);
            // 
            // RecipeRTB
            // 
            this.RecipeRTB.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeRTB.Location = new System.Drawing.Point(5, 122);
            this.RecipeRTB.Margin = new System.Windows.Forms.Padding(2);
            this.RecipeRTB.Name = "RecipeRTB";
            this.RecipeRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeRTB.Size = new System.Drawing.Size(788, 1084);
            this.RecipeRTB.TabIndex = 72;
            this.RecipeRTB.Text = "";
            this.RecipeRTB.TextChanged += new System.EventHandler(this.RecipeRTB_TextChanged);
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.BackColor = System.Drawing.Color.Gray;
            this.ContinueBtn.Enabled = false;
            this.ContinueBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueBtn.ForeColor = System.Drawing.Color.White;
            this.ContinueBtn.Location = new System.Drawing.Point(430, 1343);
            this.ContinueBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(197, 85);
            this.ContinueBtn.TabIndex = 5;
            this.ContinueBtn.Text = "Continue";
            this.ContinueBtn.UseVisualStyleBackColor = false;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.BackColor = System.Drawing.Color.Gray;
            this.StopBtn.Enabled = false;
            this.StopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopBtn.ForeColor = System.Drawing.Color.White;
            this.StopBtn.Location = new System.Drawing.Point(631, 1343);
            this.StopBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(197, 85);
            this.StopBtn.TabIndex = 3;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // PauseBtn
            // 
            this.PauseBtn.BackColor = System.Drawing.Color.Gray;
            this.PauseBtn.Enabled = false;
            this.PauseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseBtn.ForeColor = System.Drawing.Color.White;
            this.PauseBtn.Location = new System.Drawing.Point(214, 1344);
            this.PauseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(197, 85);
            this.PauseBtn.TabIndex = 2;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = false;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.Color.Gray;
            this.StartBtn.Enabled = false;
            this.StartBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartBtn.ForeColor = System.Drawing.Color.White;
            this.StartBtn.Location = new System.Drawing.Point(11, 1343);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(197, 86);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // RecipeFilenameLbl
            // 
            this.RecipeFilenameLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RecipeFilenameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeFilenameLbl.Location = new System.Drawing.Point(5, 15);
            this.RecipeFilenameLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RecipeFilenameLbl.Name = "RecipeFilenameLbl";
            this.RecipeFilenameLbl.Size = new System.Drawing.Size(786, 42);
            this.RecipeFilenameLbl.TabIndex = 77;
            this.RecipeFilenameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RecipeFilenameLbl.TextChanged += new System.EventHandler(this.RecipeFilenameLbl_TextChanged);
            // 
            // SaveAsRecipeBtn
            // 
            this.SaveAsRecipeBtn.Enabled = false;
            this.SaveAsRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveAsRecipeBtn.ForeColor = System.Drawing.Color.White;
            this.SaveAsRecipeBtn.Location = new System.Drawing.Point(1516, 11);
            this.SaveAsRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SaveAsRecipeBtn.Name = "SaveAsRecipeBtn";
            this.SaveAsRecipeBtn.Size = new System.Drawing.Size(188, 95);
            this.SaveAsRecipeBtn.TabIndex = 75;
            this.SaveAsRecipeBtn.Text = "Save As...";
            this.SaveAsRecipeBtn.UseVisualStyleBackColor = true;
            this.SaveAsRecipeBtn.Click += new System.EventHandler(this.SaveAsRecipeBtn_Click);
            // 
            // NewRecipeBtn
            // 
            this.NewRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewRecipeBtn.ForeColor = System.Drawing.Color.White;
            this.NewRecipeBtn.Location = new System.Drawing.Point(1297, 11);
            this.NewRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.NewRecipeBtn.Name = "NewRecipeBtn";
            this.NewRecipeBtn.Size = new System.Drawing.Size(99, 95);
            this.NewRecipeBtn.TabIndex = 74;
            this.NewRecipeBtn.Text = "New";
            this.NewRecipeBtn.UseVisualStyleBackColor = true;
            this.NewRecipeBtn.Click += new System.EventHandler(this.NewRecipeBtn_Click);
            // 
            // LoadRecipeBtn
            // 
            this.LoadRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadRecipeBtn.ForeColor = System.Drawing.Color.White;
            this.LoadRecipeBtn.Location = new System.Drawing.Point(1183, 11);
            this.LoadRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.LoadRecipeBtn.Name = "LoadRecipeBtn";
            this.LoadRecipeBtn.Size = new System.Drawing.Size(102, 95);
            this.LoadRecipeBtn.TabIndex = 73;
            this.LoadRecipeBtn.Text = "Load";
            this.LoadRecipeBtn.UseVisualStyleBackColor = true;
            this.LoadRecipeBtn.Click += new System.EventHandler(this.LoadRecipeBtn_Click);
            // 
            // SaveRecipeBtn
            // 
            this.SaveRecipeBtn.Enabled = false;
            this.SaveRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveRecipeBtn.ForeColor = System.Drawing.Color.White;
            this.SaveRecipeBtn.Location = new System.Drawing.Point(1405, 11);
            this.SaveRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SaveRecipeBtn.Name = "SaveRecipeBtn";
            this.SaveRecipeBtn.Size = new System.Drawing.Size(107, 95);
            this.SaveRecipeBtn.TabIndex = 72;
            this.SaveRecipeBtn.Text = "Save";
            this.SaveRecipeBtn.UseVisualStyleBackColor = true;
            this.SaveRecipeBtn.Click += new System.EventHandler(this.SaveRecipeBtn_Click);
            // 
            // HeartbeatTmr
            // 
            this.HeartbeatTmr.Tick += new System.EventHandler(this.HeartbeatTmr_Tick);
            // 
            // timeLbl
            // 
            this.timeLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLbl.Location = new System.Drawing.Point(683, 12);
            this.timeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeLbl.Name = "timeLbl";
            this.timeLbl.Size = new System.Drawing.Size(459, 41);
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
            // ExecTmr
            // 
            this.ExecTmr.Tick += new System.EventHandler(this.ExecTmr_Tick);
            // 
            // MessageTmr
            // 
            this.MessageTmr.Tick += new System.EventHandler(this.MessageTmr_Tick);
            // 
            // RobotCommandStatusLbl
            // 
            this.RobotCommandStatusLbl.BackColor = System.Drawing.Color.Gray;
            this.RobotCommandStatusLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotCommandStatusLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotCommandStatusLbl.ForeColor = System.Drawing.Color.White;
            this.RobotCommandStatusLbl.Location = new System.Drawing.Point(1888, 570);
            this.RobotCommandStatusLbl.Name = "RobotCommandStatusLbl";
            this.RobotCommandStatusLbl.Size = new System.Drawing.Size(187, 115);
            this.RobotCommandStatusLbl.TabIndex = 78;
            this.RobotCommandStatusLbl.Text = "Command Status";
            this.RobotCommandStatusLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GrindReadyLbl
            // 
            this.GrindReadyLbl.BackColor = System.Drawing.Color.Gray;
            this.GrindReadyLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GrindReadyLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindReadyLbl.ForeColor = System.Drawing.Color.White;
            this.GrindReadyLbl.Location = new System.Drawing.Point(1888, 826);
            this.GrindReadyLbl.Name = "GrindReadyLbl";
            this.GrindReadyLbl.Size = new System.Drawing.Size(187, 125);
            this.GrindReadyLbl.TabIndex = 88;
            this.GrindReadyLbl.Text = "Grind Ready";
            this.GrindReadyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RobotReadyLbl
            // 
            this.RobotReadyLbl.BackColor = System.Drawing.Color.Gray;
            this.RobotReadyLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotReadyLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotReadyLbl.ForeColor = System.Drawing.Color.White;
            this.RobotReadyLbl.Location = new System.Drawing.Point(1888, 687);
            this.RobotReadyLbl.Name = "RobotReadyLbl";
            this.RobotReadyLbl.Size = new System.Drawing.Size(187, 129);
            this.RobotReadyLbl.TabIndex = 89;
            this.RobotReadyLbl.Text = "Robot Ready";
            this.RobotReadyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GrindContactEnabledBtn
            // 
            this.GrindContactEnabledBtn.BackColor = System.Drawing.Color.Gray;
            this.GrindContactEnabledBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindContactEnabledBtn.ForeColor = System.Drawing.Color.White;
            this.GrindContactEnabledBtn.Location = new System.Drawing.Point(1874, 1344);
            this.GrindContactEnabledBtn.Name = "GrindContactEnabledBtn";
            this.GrindContactEnabledBtn.Size = new System.Drawing.Size(248, 85);
            this.GrindContactEnabledBtn.TabIndex = 93;
            this.GrindContactEnabledBtn.Text = "Grind Contact Enabled";
            this.GrindContactEnabledBtn.UseVisualStyleBackColor = false;
            this.GrindContactEnabledBtn.Click += new System.EventHandler(this.GrindContactEnabledBtn_Click);
            // 
            // MonitorTab
            // 
            this.MonitorTab.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.MonitorTab.Controls.Add(this.positionsPage);
            this.MonitorTab.Controls.Add(this.variablesPage);
            this.MonitorTab.Controls.Add(this.manualPage);
            this.MonitorTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonitorTab.ItemSize = new System.Drawing.Size(150, 60);
            this.MonitorTab.Location = new System.Drawing.Point(799, 35);
            this.MonitorTab.Name = "MonitorTab";
            this.MonitorTab.SelectedIndex = 0;
            this.MonitorTab.Size = new System.Drawing.Size(1316, 1278);
            this.MonitorTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MonitorTab.TabIndex = 94;
            // 
            // positionsPage
            // 
            this.positionsPage.Controls.Add(this.PositionMoveArmBtn);
            this.positionsPage.Controls.Add(this.PositionMovePoseBtn);
            this.positionsPage.Controls.Add(this.PositionSetBtn);
            this.positionsPage.Controls.Add(this.ClearAllPositionsBtn);
            this.positionsPage.Controls.Add(this.LoadPositionsBtn);
            this.positionsPage.Controls.Add(this.SavePositionsBtn);
            this.positionsPage.Controls.Add(this.ClearPositionsBtn);
            this.positionsPage.Controls.Add(this.PositionsGrd);
            this.positionsPage.Location = new System.Drawing.Point(4, 64);
            this.positionsPage.Name = "positionsPage";
            this.positionsPage.Size = new System.Drawing.Size(1308, 1210);
            this.positionsPage.TabIndex = 2;
            this.positionsPage.Text = "Positions";
            this.positionsPage.UseVisualStyleBackColor = true;
            // 
            // PositionMoveArmBtn
            // 
            this.PositionMoveArmBtn.BackColor = System.Drawing.Color.Green;
            this.PositionMoveArmBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionMoveArmBtn.ForeColor = System.Drawing.Color.White;
            this.PositionMoveArmBtn.Location = new System.Drawing.Point(364, 10);
            this.PositionMoveArmBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PositionMoveArmBtn.Name = "PositionMoveArmBtn";
            this.PositionMoveArmBtn.Size = new System.Drawing.Size(347, 181);
            this.PositionMoveArmBtn.TabIndex = 98;
            this.PositionMoveArmBtn.Text = "Joint Move to Position";
            this.PositionMoveArmBtn.UseVisualStyleBackColor = false;
            this.PositionMoveArmBtn.Click += new System.EventHandler(this.PositionMoveArmBtn_Click);
            // 
            // PositionMovePoseBtn
            // 
            this.PositionMovePoseBtn.BackColor = System.Drawing.Color.Green;
            this.PositionMovePoseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionMovePoseBtn.ForeColor = System.Drawing.Color.White;
            this.PositionMovePoseBtn.Location = new System.Drawing.Point(6, 10);
            this.PositionMovePoseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PositionMovePoseBtn.Name = "PositionMovePoseBtn";
            this.PositionMovePoseBtn.Size = new System.Drawing.Size(316, 181);
            this.PositionMovePoseBtn.TabIndex = 97;
            this.PositionMovePoseBtn.Text = "Linear Move to Pose";
            this.PositionMovePoseBtn.UseVisualStyleBackColor = false;
            this.PositionMovePoseBtn.Click += new System.EventHandler(this.PositionMovePoseBtn_Click);
            // 
            // PositionSetBtn
            // 
            this.PositionSetBtn.BackColor = System.Drawing.Color.Green;
            this.PositionSetBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionSetBtn.ForeColor = System.Drawing.Color.White;
            this.PositionSetBtn.Location = new System.Drawing.Point(964, 10);
            this.PositionSetBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PositionSetBtn.Name = "PositionSetBtn";
            this.PositionSetBtn.Size = new System.Drawing.Size(323, 181);
            this.PositionSetBtn.TabIndex = 96;
            this.PositionSetBtn.Text = "Set Position";
            this.PositionSetBtn.UseVisualStyleBackColor = false;
            this.PositionSetBtn.Click += new System.EventHandler(this.PositionSetBtn_Click);
            // 
            // ClearAllPositionsBtn
            // 
            this.ClearAllPositionsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearAllPositionsBtn.Location = new System.Drawing.Point(660, 1007);
            this.ClearAllPositionsBtn.Name = "ClearAllPositionsBtn";
            this.ClearAllPositionsBtn.Size = new System.Drawing.Size(258, 100);
            this.ClearAllPositionsBtn.TabIndex = 95;
            this.ClearAllPositionsBtn.Text = "Clear All";
            this.ClearAllPositionsBtn.UseVisualStyleBackColor = true;
            this.ClearAllPositionsBtn.Click += new System.EventHandler(this.ClearAllPositionsBtn_Click);
            // 
            // LoadPositionsBtn
            // 
            this.LoadPositionsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadPositionsBtn.Location = new System.Drawing.Point(10, 1007);
            this.LoadPositionsBtn.Name = "LoadPositionsBtn";
            this.LoadPositionsBtn.Size = new System.Drawing.Size(208, 100);
            this.LoadPositionsBtn.TabIndex = 94;
            this.LoadPositionsBtn.Text = "Reload";
            this.LoadPositionsBtn.UseVisualStyleBackColor = true;
            this.LoadPositionsBtn.Click += new System.EventHandler(this.LoadPositionsBtn_Click);
            // 
            // SavePositionsBtn
            // 
            this.SavePositionsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SavePositionsBtn.Location = new System.Drawing.Point(254, 1007);
            this.SavePositionsBtn.Name = "SavePositionsBtn";
            this.SavePositionsBtn.Size = new System.Drawing.Size(155, 100);
            this.SavePositionsBtn.TabIndex = 93;
            this.SavePositionsBtn.Text = "Save";
            this.SavePositionsBtn.UseVisualStyleBackColor = true;
            this.SavePositionsBtn.Click += new System.EventHandler(this.SavePositionsBtn_Click);
            // 
            // ClearPositionsBtn
            // 
            this.ClearPositionsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearPositionsBtn.Location = new System.Drawing.Point(457, 1007);
            this.ClearPositionsBtn.Name = "ClearPositionsBtn";
            this.ClearPositionsBtn.Size = new System.Drawing.Size(155, 100);
            this.ClearPositionsBtn.TabIndex = 92;
            this.ClearPositionsBtn.Text = "Clear";
            this.ClearPositionsBtn.UseVisualStyleBackColor = true;
            this.ClearPositionsBtn.Click += new System.EventHandler(this.ClearPositionsBtn_Click);
            // 
            // PositionsGrd
            // 
            this.PositionsGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PositionsGrd.Location = new System.Drawing.Point(6, 217);
            this.PositionsGrd.Name = "PositionsGrd";
            this.PositionsGrd.RowTemplate.Height = 34;
            this.PositionsGrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PositionsGrd.Size = new System.Drawing.Size(1281, 772);
            this.PositionsGrd.TabIndex = 85;
            // 
            // variablesPage
            // 
            this.variablesPage.Controls.Add(this.ClearAllVariablesBtn);
            this.variablesPage.Controls.Add(this.LoadVariablesBtn);
            this.variablesPage.Controls.Add(this.SaveVariablesBtn);
            this.variablesPage.Controls.Add(this.ClearVariablesBtn);
            this.variablesPage.Controls.Add(this.VariablesGrd);
            this.variablesPage.Location = new System.Drawing.Point(4, 64);
            this.variablesPage.Name = "variablesPage";
            this.variablesPage.Padding = new System.Windows.Forms.Padding(3);
            this.variablesPage.Size = new System.Drawing.Size(1308, 1210);
            this.variablesPage.TabIndex = 0;
            this.variablesPage.Text = "Variables";
            this.variablesPage.UseVisualStyleBackColor = true;
            // 
            // ClearAllVariablesBtn
            // 
            this.ClearAllVariablesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearAllVariablesBtn.Location = new System.Drawing.Point(816, 911);
            this.ClearAllVariablesBtn.Name = "ClearAllVariablesBtn";
            this.ClearAllVariablesBtn.Size = new System.Drawing.Size(239, 108);
            this.ClearAllVariablesBtn.TabIndex = 91;
            this.ClearAllVariablesBtn.Text = "Clear All";
            this.ClearAllVariablesBtn.UseVisualStyleBackColor = true;
            this.ClearAllVariablesBtn.Click += new System.EventHandler(this.ClearAllVariablesBtn_Click);
            // 
            // LoadVariablesBtn
            // 
            this.LoadVariablesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadVariablesBtn.Location = new System.Drawing.Point(6, 911);
            this.LoadVariablesBtn.Name = "LoadVariablesBtn";
            this.LoadVariablesBtn.Size = new System.Drawing.Size(239, 108);
            this.LoadVariablesBtn.TabIndex = 90;
            this.LoadVariablesBtn.Text = "Reload";
            this.LoadVariablesBtn.UseVisualStyleBackColor = true;
            this.LoadVariablesBtn.Click += new System.EventHandler(this.LoadVariablesBtn_Click);
            // 
            // SaveVariablesBtn
            // 
            this.SaveVariablesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveVariablesBtn.Location = new System.Drawing.Point(276, 911);
            this.SaveVariablesBtn.Name = "SaveVariablesBtn";
            this.SaveVariablesBtn.Size = new System.Drawing.Size(239, 108);
            this.SaveVariablesBtn.TabIndex = 89;
            this.SaveVariablesBtn.Text = "Save";
            this.SaveVariablesBtn.UseVisualStyleBackColor = true;
            this.SaveVariablesBtn.Click += new System.EventHandler(this.SaveVariablesBtn_Click);
            // 
            // ClearVariablesBtn
            // 
            this.ClearVariablesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearVariablesBtn.Location = new System.Drawing.Point(546, 911);
            this.ClearVariablesBtn.Name = "ClearVariablesBtn";
            this.ClearVariablesBtn.Size = new System.Drawing.Size(239, 108);
            this.ClearVariablesBtn.TabIndex = 88;
            this.ClearVariablesBtn.Text = "Clear";
            this.ClearVariablesBtn.UseVisualStyleBackColor = true;
            this.ClearVariablesBtn.Click += new System.EventHandler(this.ClearVariablesBtn_Click);
            // 
            // VariablesGrd
            // 
            this.VariablesGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VariablesGrd.Location = new System.Drawing.Point(6, 6);
            this.VariablesGrd.Name = "VariablesGrd";
            this.VariablesGrd.RowTemplate.Height = 34;
            this.VariablesGrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VariablesGrd.Size = new System.Drawing.Size(1296, 885);
            this.VariablesGrd.TabIndex = 84;
            // 
            // manualPage
            // 
            this.manualPage.Controls.Add(this.LoadManualBtn);
            this.manualPage.Controls.Add(this.SaveManualBtn);
            this.manualPage.Controls.Add(this.InstructionsRTB);
            this.manualPage.Location = new System.Drawing.Point(4, 64);
            this.manualPage.Name = "manualPage";
            this.manualPage.Size = new System.Drawing.Size(1308, 1210);
            this.manualPage.TabIndex = 3;
            this.manualPage.Text = "Manual";
            this.manualPage.UseVisualStyleBackColor = true;
            // 
            // LoadManualBtn
            // 
            this.LoadManualBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadManualBtn.Location = new System.Drawing.Point(18, 1040);
            this.LoadManualBtn.Name = "LoadManualBtn";
            this.LoadManualBtn.Size = new System.Drawing.Size(192, 108);
            this.LoadManualBtn.TabIndex = 106;
            this.LoadManualBtn.Text = "Reload";
            this.LoadManualBtn.UseVisualStyleBackColor = true;
            this.LoadManualBtn.Click += new System.EventHandler(this.LoadManualBtn_Click);
            // 
            // SaveManualBtn
            // 
            this.SaveManualBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveManualBtn.Location = new System.Drawing.Point(230, 1040);
            this.SaveManualBtn.Name = "SaveManualBtn";
            this.SaveManualBtn.Size = new System.Drawing.Size(155, 108);
            this.SaveManualBtn.TabIndex = 105;
            this.SaveManualBtn.Text = "Save";
            this.SaveManualBtn.UseVisualStyleBackColor = true;
            this.SaveManualBtn.Click += new System.EventHandler(this.SaveManualBtn_Click);
            // 
            // InstructionsRTB
            // 
            this.InstructionsRTB.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstructionsRTB.Location = new System.Drawing.Point(18, 18);
            this.InstructionsRTB.Margin = new System.Windows.Forms.Padding(2);
            this.InstructionsRTB.Name = "InstructionsRTB";
            this.InstructionsRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.InstructionsRTB.Size = new System.Drawing.Size(1276, 996);
            this.InstructionsRTB.TabIndex = 104;
            this.InstructionsRTB.Text = "";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.ClearExecLogRtbBtn);
            this.groupBox10.Controls.Add(this.ExecLogRTB);
            this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(8, 262);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(1644, 267);
            this.groupBox10.TabIndex = 90;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Recipe Execution Messages";
            // 
            // ClearExecLogRtbBtn
            // 
            this.ClearExecLogRtbBtn.BackColor = System.Drawing.Color.Green;
            this.ClearExecLogRtbBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearExecLogRtbBtn.ForeColor = System.Drawing.Color.White;
            this.ClearExecLogRtbBtn.Location = new System.Drawing.Point(1512, 28);
            this.ClearExecLogRtbBtn.Name = "ClearExecLogRtbBtn";
            this.ClearExecLogRtbBtn.Size = new System.Drawing.Size(102, 87);
            this.ClearExecLogRtbBtn.TabIndex = 80;
            this.ClearExecLogRtbBtn.Text = "Clear";
            this.ClearExecLogRtbBtn.UseVisualStyleBackColor = false;
            this.ClearExecLogRtbBtn.Click += new System.EventHandler(this.ClearExecLogRtbBtn_Click);
            // 
            // ExecLogRTB
            // 
            this.ExecLogRTB.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecLogRTB.Location = new System.Drawing.Point(5, 27);
            this.ExecLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.ExecLogRTB.Name = "ExecLogRTB";
            this.ExecLogRTB.Size = new System.Drawing.Size(1502, 234);
            this.ExecLogRTB.TabIndex = 1;
            this.ExecLogRTB.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ClearUrLogRtbBtn);
            this.groupBox5.Controls.Add(this.UrLogRTB);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(8, 528);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1644, 242);
            this.groupBox5.TabIndex = 89;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Robot Command Messages";
            // 
            // ClearUrLogRtbBtn
            // 
            this.ClearUrLogRtbBtn.BackColor = System.Drawing.Color.Green;
            this.ClearUrLogRtbBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearUrLogRtbBtn.ForeColor = System.Drawing.Color.White;
            this.ClearUrLogRtbBtn.Location = new System.Drawing.Point(1512, 28);
            this.ClearUrLogRtbBtn.Name = "ClearUrLogRtbBtn";
            this.ClearUrLogRtbBtn.Size = new System.Drawing.Size(102, 87);
            this.ClearUrLogRtbBtn.TabIndex = 80;
            this.ClearUrLogRtbBtn.Text = "Clear";
            this.ClearUrLogRtbBtn.UseVisualStyleBackColor = false;
            this.ClearUrLogRtbBtn.Click += new System.EventHandler(this.ClearUrLogRtbBtn_Click);
            // 
            // UrLogRTB
            // 
            this.UrLogRTB.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UrLogRTB.Location = new System.Drawing.Point(5, 27);
            this.UrLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.UrLogRTB.Name = "UrLogRTB";
            this.UrLogRTB.Size = new System.Drawing.Size(1502, 206);
            this.UrLogRTB.TabIndex = 1;
            this.UrLogRTB.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ClearErrorLogRtbBtn);
            this.groupBox6.Controls.Add(this.ErrorLogRTB);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(8, 1023);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1644, 196);
            this.groupBox6.TabIndex = 84;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Error Messages";
            // 
            // ClearErrorLogRtbBtn
            // 
            this.ClearErrorLogRtbBtn.BackColor = System.Drawing.Color.Green;
            this.ClearErrorLogRtbBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearErrorLogRtbBtn.ForeColor = System.Drawing.Color.White;
            this.ClearErrorLogRtbBtn.Location = new System.Drawing.Point(1512, 28);
            this.ClearErrorLogRtbBtn.Name = "ClearErrorLogRtbBtn";
            this.ClearErrorLogRtbBtn.Size = new System.Drawing.Size(102, 87);
            this.ClearErrorLogRtbBtn.TabIndex = 81;
            this.ClearErrorLogRtbBtn.Text = "Clear";
            this.ClearErrorLogRtbBtn.UseVisualStyleBackColor = false;
            this.ClearErrorLogRtbBtn.Click += new System.EventHandler(this.ClearErrorLogRtbBtn_Click);
            // 
            // ErrorLogRTB
            // 
            this.ErrorLogRTB.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLogRTB.Location = new System.Drawing.Point(5, 27);
            this.ErrorLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.ErrorLogRTB.Name = "ErrorLogRTB";
            this.ErrorLogRTB.Size = new System.Drawing.Size(1505, 164);
            this.ErrorLogRTB.TabIndex = 0;
            this.ErrorLogRTB.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ClearAllLogRtbBtn);
            this.groupBox4.Controls.Add(this.AllLogRTB);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(8, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1644, 254);
            this.groupBox4.TabIndex = 88;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "All Log Messages";
            // 
            // ClearAllLogRtbBtn
            // 
            this.ClearAllLogRtbBtn.BackColor = System.Drawing.Color.Green;
            this.ClearAllLogRtbBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearAllLogRtbBtn.ForeColor = System.Drawing.Color.White;
            this.ClearAllLogRtbBtn.Location = new System.Drawing.Point(1512, 28);
            this.ClearAllLogRtbBtn.Name = "ClearAllLogRtbBtn";
            this.ClearAllLogRtbBtn.Size = new System.Drawing.Size(102, 87);
            this.ClearAllLogRtbBtn.TabIndex = 79;
            this.ClearAllLogRtbBtn.Text = "Clear";
            this.ClearAllLogRtbBtn.UseVisualStyleBackColor = false;
            this.ClearAllLogRtbBtn.Click += new System.EventHandler(this.ClearAllLogRtbBtn_Click);
            // 
            // AllLogRTB
            // 
            this.AllLogRTB.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllLogRTB.Location = new System.Drawing.Point(6, 27);
            this.AllLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.AllLogRTB.Name = "AllLogRTB";
            this.AllLogRTB.Size = new System.Drawing.Size(1501, 219);
            this.AllLogRTB.TabIndex = 4;
            this.AllLogRTB.Text = "";
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.Green;
            this.ExitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBtn.ForeColor = System.Drawing.Color.White;
            this.ExitBtn.Location = new System.Drawing.Point(1971, 12);
            this.ExitBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(160, 94);
            this.ExitBtn.TabIndex = 96;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Green;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1175, 1148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(667, 72);
            this.label6.TabIndex = 97;
            this.label6.Text = "Mounted Tool";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MountedToolBox
            // 
            this.MountedToolBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MountedToolBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MountedToolBox.FormattingEnabled = true;
            this.MountedToolBox.Location = new System.Drawing.Point(878, 1344);
            this.MountedToolBox.Name = "MountedToolBox";
            this.MountedToolBox.Size = new System.Drawing.Size(305, 63);
            this.MountedToolBox.TabIndex = 99;
            this.MountedToolBox.SelectedIndexChanged += new System.EventHandler(this.MountedToolBox_SelectedIndexChanged);
            // 
            // RobotDashboardStatusLbl
            // 
            this.RobotDashboardStatusLbl.BackColor = System.Drawing.Color.Gray;
            this.RobotDashboardStatusLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotDashboardStatusLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotDashboardStatusLbl.ForeColor = System.Drawing.Color.White;
            this.RobotDashboardStatusLbl.Location = new System.Drawing.Point(1637, 570);
            this.RobotDashboardStatusLbl.Name = "RobotDashboardStatusLbl";
            this.RobotDashboardStatusLbl.Size = new System.Drawing.Size(234, 115);
            this.RobotDashboardStatusLbl.TabIndex = 102;
            this.RobotDashboardStatusLbl.Text = "Dashboard Status";
            this.RobotDashboardStatusLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OperatorModeBox
            // 
            this.OperatorModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperatorModeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OperatorModeBox.FormattingEnabled = true;
            this.OperatorModeBox.Items.AddRange(new object[] {
            "Operator",
            "Editor",
            "Engineering"});
            this.OperatorModeBox.Location = new System.Drawing.Point(1726, 39);
            this.OperatorModeBox.Name = "OperatorModeBox";
            this.OperatorModeBox.Size = new System.Drawing.Size(221, 47);
            this.OperatorModeBox.TabIndex = 103;
            this.OperatorModeBox.SelectedIndexChanged += new System.EventHandler(this.OperatorModeBox_SelectedIndexChanged);
            // 
            // RobotModeBtn
            // 
            this.RobotModeBtn.BackColor = System.Drawing.Color.Gray;
            this.RobotModeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotModeBtn.ForeColor = System.Drawing.Color.White;
            this.RobotModeBtn.Location = new System.Drawing.Point(1637, 687);
            this.RobotModeBtn.Name = "RobotModeBtn";
            this.RobotModeBtn.Size = new System.Drawing.Size(234, 129);
            this.RobotModeBtn.TabIndex = 106;
            this.RobotModeBtn.Text = "Robot Mode";
            this.RobotModeBtn.UseVisualStyleBackColor = false;
            this.RobotModeBtn.Click += new System.EventHandler(this.RobotModeBtn_Click);
            // 
            // SafetyStatusBtn
            // 
            this.SafetyStatusBtn.BackColor = System.Drawing.Color.Gray;
            this.SafetyStatusBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SafetyStatusBtn.ForeColor = System.Drawing.Color.White;
            this.SafetyStatusBtn.Location = new System.Drawing.Point(1637, 823);
            this.SafetyStatusBtn.Name = "SafetyStatusBtn";
            this.SafetyStatusBtn.Size = new System.Drawing.Size(234, 128);
            this.SafetyStatusBtn.TabIndex = 107;
            this.SafetyStatusBtn.Text = "Safety Status";
            this.SafetyStatusBtn.UseVisualStyleBackColor = false;
            this.SafetyStatusBtn.Click += new System.EventHandler(this.SafetyStatusBtn_Click);
            // 
            // ProgramStateBtn
            // 
            this.ProgramStateBtn.BackColor = System.Drawing.Color.Gray;
            this.ProgramStateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgramStateBtn.ForeColor = System.Drawing.Color.White;
            this.ProgramStateBtn.Location = new System.Drawing.Point(1637, 958);
            this.ProgramStateBtn.Name = "ProgramStateBtn";
            this.ProgramStateBtn.Size = new System.Drawing.Size(232, 129);
            this.ProgramStateBtn.TabIndex = 108;
            this.ProgramStateBtn.Text = "Program State";
            this.ProgramStateBtn.UseVisualStyleBackColor = false;
            this.ProgramStateBtn.Click += new System.EventHandler(this.ProgramStateBtn_Click);
            // 
            // SetJointAccelBtn
            // 
            this.SetJointAccelBtn.BackColor = System.Drawing.Color.Transparent;
            this.SetJointAccelBtn.Location = new System.Drawing.Point(336, 270);
            this.SetJointAccelBtn.Name = "SetJointAccelBtn";
            this.SetJointAccelBtn.Size = new System.Drawing.Size(286, 130);
            this.SetJointAccelBtn.TabIndex = 113;
            this.SetJointAccelBtn.Text = "Set Joint Accel";
            this.SetJointAccelBtn.UseVisualStyleBackColor = false;
            this.SetJointAccelBtn.Click += new System.EventHandler(this.SetJointAccelBtn_Click);
            // 
            // SetJointSpeedBtn
            // 
            this.SetJointSpeedBtn.BackColor = System.Drawing.Color.Transparent;
            this.SetJointSpeedBtn.Location = new System.Drawing.Point(49, 270);
            this.SetJointSpeedBtn.Name = "SetJointSpeedBtn";
            this.SetJointSpeedBtn.Size = new System.Drawing.Size(243, 130);
            this.SetJointSpeedBtn.TabIndex = 112;
            this.SetJointSpeedBtn.Text = "Set Joint Speed";
            this.SetJointSpeedBtn.UseVisualStyleBackColor = false;
            this.SetJointSpeedBtn.Click += new System.EventHandler(this.SetJointSpeedBtn_Click);
            // 
            // SetLinearSpeedBtn
            // 
            this.SetLinearSpeedBtn.BackColor = System.Drawing.Color.Transparent;
            this.SetLinearSpeedBtn.Location = new System.Drawing.Point(49, 59);
            this.SetLinearSpeedBtn.Name = "SetLinearSpeedBtn";
            this.SetLinearSpeedBtn.Size = new System.Drawing.Size(243, 130);
            this.SetLinearSpeedBtn.TabIndex = 109;
            this.SetLinearSpeedBtn.Text = "Set Linear Speed";
            this.SetLinearSpeedBtn.UseVisualStyleBackColor = false;
            this.SetLinearSpeedBtn.Click += new System.EventHandler(this.SetLinearSpeedBtn_Click);
            // 
            // SetLinearAccelBtn
            // 
            this.SetLinearAccelBtn.BackColor = System.Drawing.Color.Transparent;
            this.SetLinearAccelBtn.Location = new System.Drawing.Point(336, 59);
            this.SetLinearAccelBtn.Name = "SetLinearAccelBtn";
            this.SetLinearAccelBtn.Size = new System.Drawing.Size(286, 130);
            this.SetLinearAccelBtn.TabIndex = 110;
            this.SetLinearAccelBtn.Text = "Set Linear Accel";
            this.SetLinearAccelBtn.UseVisualStyleBackColor = false;
            this.SetLinearAccelBtn.Click += new System.EventHandler(this.SetLinearAccelBtn_Click);
            // 
            // SetBlendRadiusBtn
            // 
            this.SetBlendRadiusBtn.BackColor = System.Drawing.Color.Transparent;
            this.SetBlendRadiusBtn.Location = new System.Drawing.Point(656, 161);
            this.SetBlendRadiusBtn.Name = "SetBlendRadiusBtn";
            this.SetBlendRadiusBtn.Size = new System.Drawing.Size(243, 130);
            this.SetBlendRadiusBtn.TabIndex = 111;
            this.SetBlendRadiusBtn.Text = "Set Blend Radius";
            this.SetBlendRadiusBtn.UseVisualStyleBackColor = false;
            this.SetBlendRadiusBtn.Click += new System.EventHandler(this.SetBlendRadiusBtn_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Green;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(864, 1148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(305, 67);
            this.label5.TabIndex = 115;
            this.label5.Text = "Geometry";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DiameterDimLbl
            // 
            this.DiameterDimLbl.AutoSize = true;
            this.DiameterDimLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiameterDimLbl.Location = new System.Drawing.Point(1672, 1351);
            this.DiameterDimLbl.Name = "DiameterDimLbl";
            this.DiameterDimLbl.Size = new System.Drawing.Size(184, 55);
            this.DiameterDimLbl.TabIndex = 114;
            this.DiameterDimLbl.Text = "mmDIA";
            // 
            // MainTab
            // 
            this.MainTab.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.MainTab.Controls.Add(this.RunPage);
            this.MainTab.Controls.Add(this.ProgramPage);
            this.MainTab.Controls.Add(this.MovePage);
            this.MainTab.Controls.Add(this.SetupPage);
            this.MainTab.Controls.Add(this.IoPage);
            this.MainTab.Controls.Add(this.LogPage);
            this.MainTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainTab.ItemSize = new System.Drawing.Size(96, 96);
            this.MainTab.Location = new System.Drawing.Point(8, 11);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(2129, 1326);
            this.MainTab.TabIndex = 116;
            this.MainTab.SelectedIndexChanged += new System.EventHandler(this.MainTab_SelectedIndexChanged);
            // 
            // RunPage
            // 
            this.RunPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RunPage.Controls.Add(this.JogRunBtn);
            this.RunPage.Controls.Add(this.RecipeRTBCopy);
            this.RunPage.Controls.Add(this.Grind);
            this.RunPage.Controls.Add(this.GrindNCyclesLbl);
            this.RunPage.Controls.Add(this.GrindCycleLbl);
            this.RunPage.Controls.Add(this.CurrentLineLblCopy);
            this.RunPage.Controls.Add(this.label10);
            this.RunPage.Controls.Add(this.RecipeFilenameOnlyLblCopy);
            this.RunPage.Controls.Add(this.label9);
            this.RunPage.Controls.Add(this.label8);
            this.RunPage.Controls.Add(this.label7);
            this.RunPage.Controls.Add(this.RobotModelLbl);
            this.RunPage.Controls.Add(this.RobotSerialNumberLbl);
            this.RunPage.Controls.Add(this.groupBox2);
            this.RunPage.Controls.Add(this.RobotCommandStatusLbl);
            this.RunPage.Controls.Add(this.GrindReadyLbl);
            this.RunPage.Controls.Add(this.RobotReadyLbl);
            this.RunPage.Controls.Add(this.RobotDashboardStatusLbl);
            this.RunPage.Controls.Add(this.RobotModeBtn);
            this.RunPage.Controls.Add(this.SafetyStatusBtn);
            this.RunPage.Controls.Add(this.label5);
            this.RunPage.Controls.Add(this.ProgramStateBtn);
            this.RunPage.Controls.Add(this.label6);
            this.RunPage.Location = new System.Drawing.Point(4, 100);
            this.RunPage.Name = "RunPage";
            this.RunPage.Padding = new System.Windows.Forms.Padding(3);
            this.RunPage.Size = new System.Drawing.Size(2121, 1222);
            this.RunPage.TabIndex = 0;
            this.RunPage.Text = "Run";
            this.RunPage.UseVisualStyleBackColor = true;
            // 
            // JogRunBtn
            // 
            this.JogRunBtn.BackColor = System.Drawing.Color.Green;
            this.JogRunBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JogRunBtn.ForeColor = System.Drawing.Color.White;
            this.JogRunBtn.Location = new System.Drawing.Point(820, 502);
            this.JogRunBtn.Margin = new System.Windows.Forms.Padding(2);
            this.JogRunBtn.Name = "JogRunBtn";
            this.JogRunBtn.Size = new System.Drawing.Size(630, 125);
            this.JogRunBtn.TabIndex = 130;
            this.JogRunBtn.Text = "Jog Robot";
            this.JogRunBtn.UseVisualStyleBackColor = false;
            this.JogRunBtn.Click += new System.EventHandler(this.JogRunBtn_Click);
            // 
            // RecipeRTBCopy
            // 
            this.RecipeRTBCopy.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeRTBCopy.Location = new System.Drawing.Point(5, 121);
            this.RecipeRTBCopy.Margin = new System.Windows.Forms.Padding(2);
            this.RecipeRTBCopy.Name = "RecipeRTBCopy";
            this.RecipeRTBCopy.ReadOnly = true;
            this.RecipeRTBCopy.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeRTBCopy.Size = new System.Drawing.Size(788, 1084);
            this.RecipeRTBCopy.TabIndex = 129;
            this.RecipeRTBCopy.Text = "";
            // 
            // Grind
            // 
            this.Grind.AutoSize = true;
            this.Grind.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grind.Location = new System.Drawing.Point(1198, 352);
            this.Grind.Name = "Grind";
            this.Grind.Size = new System.Drawing.Size(64, 55);
            this.Grind.TabIndex = 128;
            this.Grind.Text = "of";
            // 
            // GrindNCyclesLbl
            // 
            this.GrindNCyclesLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GrindNCyclesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindNCyclesLbl.Location = new System.Drawing.Point(1269, 352);
            this.GrindNCyclesLbl.Name = "GrindNCyclesLbl";
            this.GrindNCyclesLbl.Size = new System.Drawing.Size(100, 52);
            this.GrindNCyclesLbl.TabIndex = 127;
            this.GrindNCyclesLbl.Text = "1";
            // 
            // GrindCycleLbl
            // 
            this.GrindCycleLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GrindCycleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindCycleLbl.Location = new System.Drawing.Point(1097, 351);
            this.GrindCycleLbl.Name = "GrindCycleLbl";
            this.GrindCycleLbl.Size = new System.Drawing.Size(95, 52);
            this.GrindCycleLbl.TabIndex = 126;
            this.GrindCycleLbl.Text = "1";
            // 
            // CurrentLineLblCopy
            // 
            this.CurrentLineLblCopy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentLineLblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLineLblCopy.Location = new System.Drawing.Point(6, 23);
            this.CurrentLineLblCopy.Name = "CurrentLineLblCopy";
            this.CurrentLineLblCopy.Size = new System.Drawing.Size(787, 94);
            this.CurrentLineLblCopy.TabIndex = 125;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(886, 349);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(205, 55);
            this.label10.TabIndex = 124;
            this.label10.Text = "Grinding";
            // 
            // RecipeFilenameOnlyLblCopy
            // 
            this.RecipeFilenameOnlyLblCopy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RecipeFilenameOnlyLblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeFilenameOnlyLblCopy.Location = new System.Drawing.Point(820, 181);
            this.RecipeFilenameOnlyLblCopy.Name = "RecipeFilenameOnlyLblCopy";
            this.RecipeFilenameOnlyLblCopy.Size = new System.Drawing.Size(630, 146);
            this.RecipeFilenameOnlyLblCopy.TabIndex = 123;
            this.RecipeFilenameOnlyLblCopy.Text = "Recipe";
            this.RecipeFilenameOnlyLblCopy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Green;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(820, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(630, 94);
            this.label9.TabIndex = 122;
            this.label9.Text = "Currently Loaded";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(1611, 12);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 61);
            this.label8.TabIndex = 121;
            this.label8.Text = "S/N:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(1223, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 61);
            this.label7.TabIndex = 120;
            this.label7.Text = "Robot Model:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RobotModelLbl
            // 
            this.RobotModelLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotModelLbl.Location = new System.Drawing.Point(1437, 12);
            this.RobotModelLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RobotModelLbl.Name = "RobotModelLbl";
            this.RobotModelLbl.Size = new System.Drawing.Size(157, 61);
            this.RobotModelLbl.TabIndex = 118;
            this.RobotModelLbl.Text = "Model";
            this.RobotModelLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RobotSerialNumberLbl
            // 
            this.RobotSerialNumberLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotSerialNumberLbl.Location = new System.Drawing.Point(1695, 12);
            this.RobotSerialNumberLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RobotSerialNumberLbl.Name = "RobotSerialNumberLbl";
            this.RobotSerialNumberLbl.Size = new System.Drawing.Size(279, 61);
            this.RobotSerialNumberLbl.TabIndex = 119;
            this.RobotSerialNumberLbl.Text = "Serial Number";
            this.RobotSerialNumberLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DashboardMessageTxt);
            this.groupBox2.Controls.Add(this.DashboardSendBtn);
            this.groupBox2.Controls.Add(this.RobotConnectBtn);
            this.groupBox2.Controls.Add(this.RobotDisconnectBtn);
            this.groupBox2.Controls.Add(this.RobotSendBtn);
            this.groupBox2.Controls.Add(this.RobotMessageTxt);
            this.groupBox2.Location = new System.Drawing.Point(1659, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 424);
            this.groupBox2.TabIndex = 109;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Robot Testing";
            // 
            // DashboardMessageTxt
            // 
            this.DashboardMessageTxt.Location = new System.Drawing.Point(229, 331);
            this.DashboardMessageTxt.Name = "DashboardMessageTxt";
            this.DashboardMessageTxt.Size = new System.Drawing.Size(220, 44);
            this.DashboardMessageTxt.TabIndex = 88;
            this.DashboardMessageTxt.Text = "programstate";
            // 
            // DashboardSendBtn
            // 
            this.DashboardSendBtn.BackColor = System.Drawing.Color.Transparent;
            this.DashboardSendBtn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.DashboardSendBtn.Location = new System.Drawing.Point(19, 294);
            this.DashboardSendBtn.Name = "DashboardSendBtn";
            this.DashboardSendBtn.Size = new System.Drawing.Size(190, 117);
            this.DashboardSendBtn.TabIndex = 87;
            this.DashboardSendBtn.Text = "Send Dashboard";
            this.DashboardSendBtn.UseVisualStyleBackColor = false;
            this.DashboardSendBtn.Click += new System.EventHandler(this.DashboardSendBtn_Click);
            // 
            // RobotConnectBtn
            // 
            this.RobotConnectBtn.Location = new System.Drawing.Point(19, 43);
            this.RobotConnectBtn.Name = "RobotConnectBtn";
            this.RobotConnectBtn.Size = new System.Drawing.Size(144, 103);
            this.RobotConnectBtn.TabIndex = 73;
            this.RobotConnectBtn.Text = "Wait for Connect";
            this.RobotConnectBtn.UseVisualStyleBackColor = true;
            this.RobotConnectBtn.Click += new System.EventHandler(this.RobotConnectBtn_Click);
            // 
            // RobotDisconnectBtn
            // 
            this.RobotDisconnectBtn.Location = new System.Drawing.Point(183, 43);
            this.RobotDisconnectBtn.Name = "RobotDisconnectBtn";
            this.RobotDisconnectBtn.Size = new System.Drawing.Size(189, 103);
            this.RobotDisconnectBtn.TabIndex = 74;
            this.RobotDisconnectBtn.Text = "Disconnect";
            this.RobotDisconnectBtn.UseVisualStyleBackColor = true;
            this.RobotDisconnectBtn.Click += new System.EventHandler(this.RobotDisconnectBtn_Click);
            // 
            // RobotSendBtn
            // 
            this.RobotSendBtn.BackColor = System.Drawing.Color.Transparent;
            this.RobotSendBtn.Location = new System.Drawing.Point(19, 174);
            this.RobotSendBtn.Name = "RobotSendBtn";
            this.RobotSendBtn.Size = new System.Drawing.Size(190, 103);
            this.RobotSendBtn.TabIndex = 76;
            this.RobotSendBtn.Text = "Send Command";
            this.RobotSendBtn.UseVisualStyleBackColor = false;
            this.RobotSendBtn.Click += new System.EventHandler(this.RobotSendBtn_Click);
            // 
            // RobotMessageTxt
            // 
            this.RobotMessageTxt.Location = new System.Drawing.Point(229, 204);
            this.RobotMessageTxt.Name = "RobotMessageTxt";
            this.RobotMessageTxt.Size = new System.Drawing.Size(220, 44);
            this.RobotMessageTxt.TabIndex = 75;
            this.RobotMessageTxt.Text = "(10)";
            // 
            // ProgramPage
            // 
            this.ProgramPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProgramPage.Controls.Add(this.RecipeRTB);
            this.ProgramPage.Controls.Add(this.CurrentLineLbl);
            this.ProgramPage.Controls.Add(this.RecipeFilenameLbl);
            this.ProgramPage.Controls.Add(this.MonitorTab);
            this.ProgramPage.Location = new System.Drawing.Point(4, 100);
            this.ProgramPage.Name = "ProgramPage";
            this.ProgramPage.Padding = new System.Windows.Forms.Padding(3);
            this.ProgramPage.Size = new System.Drawing.Size(2121, 1222);
            this.ProgramPage.TabIndex = 1;
            this.ProgramPage.Text = "Program";
            this.ProgramPage.UseVisualStyleBackColor = true;
            // 
            // MovePage
            // 
            this.MovePage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MovePage.Controls.Add(this.groupBox11);
            this.MovePage.Controls.Add(this.groupBox9);
            this.MovePage.Controls.Add(this.JogBtn);
            this.MovePage.Location = new System.Drawing.Point(4, 100);
            this.MovePage.Name = "MovePage";
            this.MovePage.Size = new System.Drawing.Size(2121, 1222);
            this.MovePage.TabIndex = 3;
            this.MovePage.Text = "Move";
            this.MovePage.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.SetTouchRetractBtn);
            this.groupBox11.Location = new System.Drawing.Point(216, 632);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(655, 373);
            this.groupBox11.TabIndex = 116;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Grind Move Control";
            // 
            // SetTouchRetractBtn
            // 
            this.SetTouchRetractBtn.BackColor = System.Drawing.Color.Transparent;
            this.SetTouchRetractBtn.Location = new System.Drawing.Point(37, 62);
            this.SetTouchRetractBtn.Name = "SetTouchRetractBtn";
            this.SetTouchRetractBtn.Size = new System.Drawing.Size(243, 130);
            this.SetTouchRetractBtn.TabIndex = 114;
            this.SetTouchRetractBtn.Text = "Set Touch Retract";
            this.SetTouchRetractBtn.UseVisualStyleBackColor = false;
            this.SetTouchRetractBtn.Click += new System.EventHandler(this.SetTouchRetractBtn_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.SetLinearAccelBtn);
            this.groupBox9.Controls.Add(this.SetBlendRadiusBtn);
            this.groupBox9.Controls.Add(this.SetJointSpeedBtn);
            this.groupBox9.Controls.Add(this.SetJointAccelBtn);
            this.groupBox9.Controls.Add(this.SetLinearSpeedBtn);
            this.groupBox9.Location = new System.Drawing.Point(216, 77);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(927, 498);
            this.groupBox9.TabIndex = 115;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Default (non-Grind) Moves";
            // 
            // SetupPage
            // 
            this.SetupPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SetupPage.Controls.Add(this.groupBox8);
            this.SetupPage.Controls.Add(this.DefaultConfigBtn);
            this.SetupPage.Controls.Add(this.LoadConfigBtn);
            this.SetupPage.Controls.Add(this.SaveConfigBtn);
            this.SetupPage.Controls.Add(this.groupBox1);
            this.SetupPage.Location = new System.Drawing.Point(4, 100);
            this.SetupPage.Name = "SetupPage";
            this.SetupPage.Size = new System.Drawing.Size(2121, 1222);
            this.SetupPage.TabIndex = 2;
            this.SetupPage.Text = "Setup";
            this.SetupPage.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.SelectToolBtn);
            this.groupBox8.Controls.Add(this.ToolsGrd);
            this.groupBox8.Controls.Add(this.LoadToolsBtn);
            this.groupBox8.Controls.Add(this.SaveToolsBtn);
            this.groupBox8.Controls.Add(this.ClearToolsBtn);
            this.groupBox8.Location = new System.Drawing.Point(3, 659);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(2112, 475);
            this.groupBox8.TabIndex = 101;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Tools";
            // 
            // SelectToolBtn
            // 
            this.SelectToolBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectToolBtn.Location = new System.Drawing.Point(43, 373);
            this.SelectToolBtn.Name = "SelectToolBtn";
            this.SelectToolBtn.Size = new System.Drawing.Size(155, 70);
            this.SelectToolBtn.TabIndex = 95;
            this.SelectToolBtn.Text = "Select";
            this.SelectToolBtn.UseVisualStyleBackColor = true;
            this.SelectToolBtn.Click += new System.EventHandler(this.SelectToolBtn_Click);
            // 
            // ToolsGrd
            // 
            this.ToolsGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ToolsGrd.Location = new System.Drawing.Point(6, 43);
            this.ToolsGrd.Name = "ToolsGrd";
            this.ToolsGrd.RowTemplate.Height = 34;
            this.ToolsGrd.Size = new System.Drawing.Size(2100, 308);
            this.ToolsGrd.TabIndex = 85;
            // 
            // LoadToolsBtn
            // 
            this.LoadToolsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadToolsBtn.Location = new System.Drawing.Point(288, 373);
            this.LoadToolsBtn.Name = "LoadToolsBtn";
            this.LoadToolsBtn.Size = new System.Drawing.Size(155, 70);
            this.LoadToolsBtn.TabIndex = 94;
            this.LoadToolsBtn.Text = "Reload";
            this.LoadToolsBtn.UseVisualStyleBackColor = true;
            this.LoadToolsBtn.Click += new System.EventHandler(this.LoadToolsBtn_Click);
            // 
            // SaveToolsBtn
            // 
            this.SaveToolsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveToolsBtn.Location = new System.Drawing.Point(533, 373);
            this.SaveToolsBtn.Name = "SaveToolsBtn";
            this.SaveToolsBtn.Size = new System.Drawing.Size(155, 70);
            this.SaveToolsBtn.TabIndex = 93;
            this.SaveToolsBtn.Text = "Save";
            this.SaveToolsBtn.UseVisualStyleBackColor = true;
            this.SaveToolsBtn.Click += new System.EventHandler(this.SaveToolsBtn_Click);
            // 
            // ClearToolsBtn
            // 
            this.ClearToolsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearToolsBtn.Location = new System.Drawing.Point(778, 373);
            this.ClearToolsBtn.Name = "ClearToolsBtn";
            this.ClearToolsBtn.Size = new System.Drawing.Size(155, 70);
            this.ClearToolsBtn.TabIndex = 92;
            this.ClearToolsBtn.Text = "Clear";
            this.ClearToolsBtn.UseVisualStyleBackColor = true;
            this.ClearToolsBtn.Click += new System.EventHandler(this.ClearToolsBtn_Click);
            // 
            // DefaultConfigBtn
            // 
            this.DefaultConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DefaultConfigBtn.Location = new System.Drawing.Point(1082, 1140);
            this.DefaultConfigBtn.Name = "DefaultConfigBtn";
            this.DefaultConfigBtn.Size = new System.Drawing.Size(217, 72);
            this.DefaultConfigBtn.TabIndex = 99;
            this.DefaultConfigBtn.Text = "Set Default";
            this.DefaultConfigBtn.UseVisualStyleBackColor = true;
            this.DefaultConfigBtn.Click += new System.EventHandler(this.DefaultConfigBtn_Click);
            // 
            // LoadConfigBtn
            // 
            this.LoadConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadConfigBtn.Location = new System.Drawing.Point(564, 1140);
            this.LoadConfigBtn.Name = "LoadConfigBtn";
            this.LoadConfigBtn.Size = new System.Drawing.Size(217, 72);
            this.LoadConfigBtn.TabIndex = 98;
            this.LoadConfigBtn.Text = "Reload";
            this.LoadConfigBtn.UseVisualStyleBackColor = true;
            this.LoadConfigBtn.Click += new System.EventHandler(this.LoadConfigBtn_Click);
            // 
            // SaveConfigBtn
            // 
            this.SaveConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveConfigBtn.Location = new System.Drawing.Point(823, 1140);
            this.SaveConfigBtn.Name = "SaveConfigBtn";
            this.SaveConfigBtn.Size = new System.Drawing.Size(217, 72);
            this.SaveConfigBtn.TabIndex = 97;
            this.SaveConfigBtn.Text = "Save";
            this.SaveConfigBtn.UseVisualStyleBackColor = true;
            this.SaveConfigBtn.Click += new System.EventHandler(this.SaveConfigBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.RobotProgramTxt);
            this.groupBox1.Controls.Add(this.ServerIpTxt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.UtcTimeChk);
            this.groupBox1.Controls.Add(this.RobotIpTxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.AutoGrindRootLbl);
            this.groupBox1.Controls.Add(this.ChangeRootDirectoryBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(9, 20);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(2106, 339);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GeneralConfig";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(352, 37);
            this.label4.TabIndex = 88;
            this.label4.Text = "Robot Program to Load";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RobotProgramTxt
            // 
            this.RobotProgramTxt.Location = new System.Drawing.Point(397, 113);
            this.RobotProgramTxt.Name = "RobotProgramTxt";
            this.RobotProgramTxt.Size = new System.Drawing.Size(802, 44);
            this.RobotProgramTxt.TabIndex = 87;
            // 
            // ServerIpTxt
            // 
            this.ServerIpTxt.Location = new System.Drawing.Point(338, 167);
            this.ServerIpTxt.Name = "ServerIpTxt";
            this.ServerIpTxt.Size = new System.Drawing.Size(227, 44);
            this.ServerIpTxt.TabIndex = 79;
            this.ServerIpTxt.Text = "192.168.25.1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(98, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 42);
            this.label2.TabIndex = 78;
            this.label2.Text = "UR Robot IP Address";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UtcTimeChk
            // 
            this.UtcTimeChk.AutoSize = true;
            this.UtcTimeChk.Location = new System.Drawing.Point(15, 281);
            this.UtcTimeChk.Name = "UtcTimeChk";
            this.UtcTimeChk.Size = new System.Drawing.Size(496, 41);
            this.UtcTimeChk.TabIndex = 77;
            this.UtcTimeChk.Text = "Use UTC Time in Time Stamps?";
            this.UtcTimeChk.UseVisualStyleBackColor = true;
            // 
            // RobotIpTxt
            // 
            this.RobotIpTxt.Location = new System.Drawing.Point(338, 217);
            this.RobotIpTxt.Name = "RobotIpTxt";
            this.RobotIpTxt.Size = new System.Drawing.Size(227, 44);
            this.RobotIpTxt.TabIndex = 72;
            this.RobotIpTxt.Text = "192.168.25.1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(318, 40);
            this.label3.TabIndex = 71;
            this.label3.Text = "Local IP for Server";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AutoGrindRootLbl
            // 
            this.AutoGrindRootLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AutoGrindRootLbl.Location = new System.Drawing.Point(397, 57);
            this.AutoGrindRootLbl.Name = "AutoGrindRootLbl";
            this.AutoGrindRootLbl.Size = new System.Drawing.Size(802, 46);
            this.AutoGrindRootLbl.TabIndex = 69;
            this.AutoGrindRootLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChangeRootDirectoryBtn
            // 
            this.ChangeRootDirectoryBtn.Location = new System.Drawing.Point(1220, 57);
            this.ChangeRootDirectoryBtn.Name = "ChangeRootDirectoryBtn";
            this.ChangeRootDirectoryBtn.Size = new System.Drawing.Size(60, 42);
            this.ChangeRootDirectoryBtn.TabIndex = 70;
            this.ChangeRootDirectoryBtn.Text = "...";
            this.ChangeRootDirectoryBtn.UseVisualStyleBackColor = true;
            this.ChangeRootDirectoryBtn.Click += new System.EventHandler(this.ChangeRootDirectoryBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 37);
            this.label1.TabIndex = 68;
            this.label1.Text = "AutoGrind Root Directory";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DebugLevelCombo);
            this.groupBox3.Location = new System.Drawing.Point(1723, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(331, 110);
            this.groupBox3.TabIndex = 86;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "On Screen Logging";
            // 
            // DebugLevelCombo
            // 
            this.DebugLevelCombo.FormattingEnabled = true;
            this.DebugLevelCombo.Items.AddRange(new object[] {
            "Error",
            "Warn",
            "Info",
            "Debug",
            "Trace"});
            this.DebugLevelCombo.Location = new System.Drawing.Point(23, 43);
            this.DebugLevelCombo.Name = "DebugLevelCombo";
            this.DebugLevelCombo.Size = new System.Drawing.Size(291, 45);
            this.DebugLevelCombo.TabIndex = 80;
            this.DebugLevelCombo.SelectedIndexChanged += new System.EventHandler(this.DebugLevelCombo_SelectedIndexChanged);
            // 
            // IoPage
            // 
            this.IoPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.IoPage.Location = new System.Drawing.Point(4, 100);
            this.IoPage.Name = "IoPage";
            this.IoPage.Size = new System.Drawing.Size(2121, 1222);
            this.IoPage.TabIndex = 4;
            this.IoPage.Text = "I/O";
            this.IoPage.UseVisualStyleBackColor = true;
            // 
            // LogPage
            // 
            this.LogPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LogPage.Controls.Add(this.groupBox7);
            this.LogPage.Controls.Add(this.groupBox6);
            this.LogPage.Controls.Add(this.groupBox5);
            this.LogPage.Controls.Add(this.groupBox10);
            this.LogPage.Controls.Add(this.groupBox4);
            this.LogPage.Location = new System.Drawing.Point(4, 100);
            this.LogPage.Name = "LogPage";
            this.LogPage.Size = new System.Drawing.Size(2121, 1222);
            this.LogPage.TabIndex = 5;
            this.LogPage.Text = "Log";
            this.LogPage.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ClearUrDashboardRtbBtn);
            this.groupBox7.Controls.Add(this.UrDashboardLogRTB);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(8, 776);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1644, 252);
            this.groupBox7.TabIndex = 90;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Robot Dashboard Messages";
            // 
            // ClearUrDashboardRtbBtn
            // 
            this.ClearUrDashboardRtbBtn.BackColor = System.Drawing.Color.Green;
            this.ClearUrDashboardRtbBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearUrDashboardRtbBtn.ForeColor = System.Drawing.Color.White;
            this.ClearUrDashboardRtbBtn.Location = new System.Drawing.Point(1512, 28);
            this.ClearUrDashboardRtbBtn.Name = "ClearUrDashboardRtbBtn";
            this.ClearUrDashboardRtbBtn.Size = new System.Drawing.Size(102, 87);
            this.ClearUrDashboardRtbBtn.TabIndex = 80;
            this.ClearUrDashboardRtbBtn.Text = "Clear";
            this.ClearUrDashboardRtbBtn.UseVisualStyleBackColor = false;
            this.ClearUrDashboardRtbBtn.Click += new System.EventHandler(this.ClearUrDashboardLogRtbBtn_Click);
            // 
            // UrDashboardLogRTB
            // 
            this.UrDashboardLogRTB.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UrDashboardLogRTB.Location = new System.Drawing.Point(5, 27);
            this.UrDashboardLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.UrDashboardLogRTB.Name = "UrDashboardLogRTB";
            this.UrDashboardLogRTB.Size = new System.Drawing.Size(1506, 216);
            this.UrDashboardLogRTB.TabIndex = 1;
            this.UrDashboardLogRTB.Text = "";
            // 
            // DiameterLbl
            // 
            this.DiameterLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DiameterLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiameterLbl.Location = new System.Drawing.Point(1500, 1351);
            this.DiameterLbl.Name = "DiameterLbl";
            this.DiameterLbl.Size = new System.Drawing.Size(166, 52);
            this.DiameterLbl.TabIndex = 116;
            this.DiameterLbl.Text = "25.0";
            this.DiameterLbl.Click += new System.EventHandler(this.DiameterLbl_Click);
            // 
            // PartGeometryBox
            // 
            this.PartGeometryBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PartGeometryBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartGeometryBox.FormattingEnabled = true;
            this.PartGeometryBox.Items.AddRange(new object[] {
            "FLAT",
            "CYLINDER",
            "SPHERE"});
            this.PartGeometryBox.Location = new System.Drawing.Point(1189, 1344);
            this.PartGeometryBox.Name = "PartGeometryBox";
            this.PartGeometryBox.Size = new System.Drawing.Size(305, 63);
            this.PartGeometryBox.TabIndex = 110;
            this.PartGeometryBox.SelectedIndexChanged += new System.EventHandler(this.PartGeometryBox_SelectedIndexChanged);
            // 
            // RecipeFilenameOnlyLbl
            // 
            this.RecipeFilenameOnlyLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RecipeFilenameOnlyLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeFilenameOnlyLbl.Location = new System.Drawing.Point(683, 60);
            this.RecipeFilenameOnlyLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RecipeFilenameOnlyLbl.Name = "RecipeFilenameOnlyLbl";
            this.RecipeFilenameOnlyLbl.Size = new System.Drawing.Size(459, 42);
            this.RecipeFilenameOnlyLbl.TabIndex = 95;
            this.RecipeFilenameOnlyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RecipeFilenameOnlyLbl.TextChanged += new System.EventHandler(this.RecipeFilenameOnlyLbl_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2160, 1440);
            this.ControlBox = false;
            this.Controls.Add(this.timeLbl);
            this.Controls.Add(this.RecipeFilenameOnlyLbl);
            this.Controls.Add(this.OperatorModeBox);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.SaveAsRecipeBtn);
            this.Controls.Add(this.NewRecipeBtn);
            this.Controls.Add(this.LoadRecipeBtn);
            this.Controls.Add(this.SaveRecipeBtn);
            this.Controls.Add(this.MainTab);
            this.Controls.Add(this.ContinueBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.PauseBtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.DiameterLbl);
            this.Controls.Add(this.GrindContactEnabledBtn);
            this.Controls.Add(this.PartGeometryBox);
            this.Controls.Add(this.MountedToolBox);
            this.Controls.Add(this.DiameterDimLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "The Code Sets This Caption";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MonitorTab.ResumeLayout(false);
            this.positionsPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PositionsGrd)).EndInit();
            this.variablesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VariablesGrd)).EndInit();
            this.manualPage.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.MainTab.ResumeLayout(false);
            this.RunPage.ResumeLayout(false);
            this.RunPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ProgramPage.ResumeLayout(false);
            this.MovePage.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.SetupPage.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ToolsGrd)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.LogPage.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer HeartbeatTmr;
        private System.Windows.Forms.Label timeLbl;
        private System.Windows.Forms.Timer StartupTmr;
        private System.Windows.Forms.Timer CloseTmr;
        private System.Windows.Forms.Button SaveAsRecipeBtn;
        private System.Windows.Forms.Button NewRecipeBtn;
        private System.Windows.Forms.Button LoadRecipeBtn;
        private System.Windows.Forms.Button SaveRecipeBtn;
        private System.Windows.Forms.Button ContinueBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.RichTextBox RecipeRTB;
        private System.Windows.Forms.Button JogBtn;
        private System.Windows.Forms.Timer ExecTmr;
        private System.Windows.Forms.Timer MessageTmr;
        private System.Windows.Forms.Label RobotCommandStatusLbl;
        private System.Windows.Forms.Label GrindReadyLbl;
        private System.Windows.Forms.Label RobotReadyLbl;
        private System.Windows.Forms.Button GrindContactEnabledBtn;
        private System.Windows.Forms.Label CurrentLineLbl;
        private System.Windows.Forms.TabControl MonitorTab;
        private System.Windows.Forms.TabPage variablesPage;
        private System.Windows.Forms.Button ClearAllVariablesBtn;
        private System.Windows.Forms.Button LoadVariablesBtn;
        private System.Windows.Forms.Button SaveVariablesBtn;
        private System.Windows.Forms.Button ClearVariablesBtn;
        private System.Windows.Forms.DataGridView VariablesGrd;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox UrLogRTB;
        private System.Windows.Forms.Button ClearUrLogRtbBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button ClearAllLogRtbBtn;
        private System.Windows.Forms.RichTextBox AllLogRTB;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RichTextBox ErrorLogRTB;
        private System.Windows.Forms.Button ClearErrorLogRtbBtn;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button ClearExecLogRtbBtn;
        private System.Windows.Forms.RichTextBox ExecLogRTB;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox MountedToolBox;
        private System.Windows.Forms.TabPage positionsPage;
        private System.Windows.Forms.Button PositionMoveArmBtn;
        private System.Windows.Forms.Button PositionMovePoseBtn;
        private System.Windows.Forms.Button PositionSetBtn;
        private System.Windows.Forms.Button ClearAllPositionsBtn;
        private System.Windows.Forms.Button LoadPositionsBtn;
        private System.Windows.Forms.Button SavePositionsBtn;
        private System.Windows.Forms.Button ClearPositionsBtn;
        private System.Windows.Forms.DataGridView PositionsGrd;
        private System.Windows.Forms.Label RobotDashboardStatusLbl;
        private System.Windows.Forms.ComboBox OperatorModeBox;
        private System.Windows.Forms.Label RecipeFilenameLbl;
        private System.Windows.Forms.RichTextBox InstructionsRTB;
        private System.Windows.Forms.TabPage manualPage;
        private System.Windows.Forms.Button LoadManualBtn;
        private System.Windows.Forms.Button SaveManualBtn;
        private System.Windows.Forms.Button RobotModeBtn;
        private System.Windows.Forms.Button SafetyStatusBtn;
        private System.Windows.Forms.Button ProgramStateBtn;
        private System.Windows.Forms.Button SetJointAccelBtn;
        private System.Windows.Forms.Button SetJointSpeedBtn;
        private System.Windows.Forms.Button SetLinearSpeedBtn;
        private System.Windows.Forms.Button SetLinearAccelBtn;
        private System.Windows.Forms.Button SetBlendRadiusBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label DiameterDimLbl;
        private System.Windows.Forms.TabControl MainTab;
        private System.Windows.Forms.TabPage RunPage;
        private System.Windows.Forms.TabPage ProgramPage;
        private System.Windows.Forms.TabPage SetupPage;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button SelectToolBtn;
        private System.Windows.Forms.DataGridView ToolsGrd;
        private System.Windows.Forms.Button LoadToolsBtn;
        private System.Windows.Forms.Button SaveToolsBtn;
        private System.Windows.Forms.Button ClearToolsBtn;
        private System.Windows.Forms.Button DefaultConfigBtn;
        private System.Windows.Forms.Button LoadConfigBtn;
        private System.Windows.Forms.Button SaveConfigBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox RobotProgramTxt;
        private System.Windows.Forms.TextBox ServerIpTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox UtcTimeChk;
        private System.Windows.Forms.TextBox RobotIpTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AutoGrindRootLbl;
        private System.Windows.Forms.Button ChangeRootDirectoryBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox DebugLevelCombo;
        private System.Windows.Forms.TabPage MovePage;
        private System.Windows.Forms.TabPage IoPage;
        private System.Windows.Forms.TabPage LogPage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox DashboardMessageTxt;
        private System.Windows.Forms.Button DashboardSendBtn;
        private System.Windows.Forms.Button RobotConnectBtn;
        private System.Windows.Forms.Button RobotDisconnectBtn;
        private System.Windows.Forms.Button RobotSendBtn;
        private System.Windows.Forms.TextBox RobotMessageTxt;
        private System.Windows.Forms.Label RecipeFilenameOnlyLbl;
        private System.Windows.Forms.ComboBox PartGeometryBox;
        private System.Windows.Forms.Label DiameterLbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label RobotModelLbl;
        private System.Windows.Forms.Label RobotSerialNumberLbl;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button ClearUrDashboardRtbBtn;
        private System.Windows.Forms.RichTextBox UrDashboardLogRTB;
        private System.Windows.Forms.Label RecipeFilenameOnlyLblCopy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Grind;
        private System.Windows.Forms.Label GrindNCyclesLbl;
        private System.Windows.Forms.Label GrindCycleLbl;
        private System.Windows.Forms.Label CurrentLineLblCopy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RichTextBox RecipeRTBCopy;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button SetTouchRetractBtn;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button JogRunBtn;
    }
}

