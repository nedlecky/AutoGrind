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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CurrentLineLbl = new System.Windows.Forms.Label();
            this.RecipeRTB = new System.Windows.Forms.RichTextBox();
            this.StepBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.RecipeFilenameLbl = new System.Windows.Forms.Label();
            this.SaveAsRecipeBtn = new System.Windows.Forms.Button();
            this.NewRecipeBtn = new System.Windows.Forms.Button();
            this.LoadRecipeBtn = new System.Windows.Forms.Button();
            this.SaveRecipeBtn = new System.Windows.Forms.Button();
            this.HeartbeatTmr = new System.Windows.Forms.Timer(this.components);
            this.TimeLbl = new System.Windows.Forms.Label();
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
            this.JogBtn = new System.Windows.Forms.Button();
            this.PositionTestButtonGrp = new System.Windows.Forms.GroupBox();
            this.LoadPositionsBtn = new System.Windows.Forms.Button();
            this.ClearPositionsBtn = new System.Windows.Forms.Button();
            this.SavePositionsBtn = new System.Windows.Forms.Button();
            this.ClearAllPositionsBtn = new System.Windows.Forms.Button();
            this.PositionMoveArmBtn = new System.Windows.Forms.Button();
            this.PositionMovePoseBtn = new System.Windows.Forms.Button();
            this.PositionSetBtn = new System.Windows.Forms.Button();
            this.PositionsGrd = new System.Windows.Forms.DataGridView();
            this.variablesPage = new System.Windows.Forms.TabPage();
            this.VariableTestButtonGrp = new System.Windows.Forms.GroupBox();
            this.LoadVariablesBtn = new System.Windows.Forms.Button();
            this.ClearAllVariablesBtn = new System.Windows.Forms.Button();
            this.ClearVariablesBtn = new System.Windows.Forms.Button();
            this.SaveVariablesBtn = new System.Windows.Forms.Button();
            this.VariablesGrd = new System.Windows.Forms.DataGridView();
            this.manualPage = new System.Windows.Forms.TabPage();
            this.FullManualBtn = new System.Windows.Forms.Button();
            this.RecipeCommandsRTB = new System.Windows.Forms.RichTextBox();
            this.revhistPage = new System.Windows.Forms.TabPage();
            this.RevHistRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.ExecLogRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.UrLogRTB = new System.Windows.Forms.RichTextBox();
            this.UrDashboardLogRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ErrorLogRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.AllLogRTB = new System.Windows.Forms.RichTextBox();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.MountedToolBox = new System.Windows.Forms.ComboBox();
            this.UserModeBox = new System.Windows.Forms.ComboBox();
            this.RobotModeBtn = new System.Windows.Forms.Button();
            this.SafetyStatusBtn = new System.Windows.Forms.Button();
            this.ProgramStateBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.DiameterDimLbl = new System.Windows.Forms.Label();
            this.MainTab = new System.Windows.Forms.TabControl();
            this.RunPage = new System.Windows.Forms.TabPage();
            this.label22 = new System.Windows.Forms.Label();
            this.GrindForceReportZLbl = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.RobotSentLbl = new System.Windows.Forms.Label();
            this.StepTimeRemainingLbl = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.StepTimeEstimateLbl = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.StepElapsedTimeLbl = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.MoveToolMountBtn = new System.Windows.Forms.Button();
            this.MoveToolHomeBtn = new System.Windows.Forms.Button();
            this.GrindProcessStateLbl = new System.Windows.Forms.Label();
            this.RobotCompletedLbl = new System.Windows.Forms.Label();
            this.RunStartedTimeLbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.RobotConnectBtn = new System.Windows.Forms.Button();
            this.RunElapsedTimeLbl = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.RecipeRTBCopy = new System.Windows.Forms.RichTextBox();
            this.Grind = new System.Windows.Forms.Label();
            this.GrindNCyclesLbl = new System.Windows.Forms.Label();
            this.GrindCycleLbl = new System.Windows.Forms.Label();
            this.CurrentLineLblCopy = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.RunStateLbl = new System.Windows.Forms.Label();
            this.ProgramPage = new System.Windows.Forms.TabPage();
            this.BigEditBtn = new System.Windows.Forms.Button();
            this.SetupPage = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.RobotPolyscopeVersionLbl = new System.Windows.Forms.Label();
            this.RobotSerialNumberLbl = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.RobotModelLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DefaultMoveSetupGrp = new System.Windows.Forms.GroupBox();
            this.SetMoveDefaultsBtn = new System.Windows.Forms.Button();
            this.SetLinearAccelBtn = new System.Windows.Forms.Button();
            this.SetBlendRadiusBtn = new System.Windows.Forms.Button();
            this.SetJointSpeedBtn = new System.Windows.Forms.Button();
            this.SetJointAccelBtn = new System.Windows.Forms.Button();
            this.SetLinearSpeedBtn = new System.Windows.Forms.Button();
            this.GrindingMoveSetupGrp = new System.Windows.Forms.GroupBox();
            this.SetForceModeDampingBtn = new System.Windows.Forms.Button();
            this.SetForceModeGainScalingBtn = new System.Windows.Forms.Button();
            this.SetGrindJogAccelBtn = new System.Windows.Forms.Button();
            this.SetGrindJogSpeedBtn = new System.Windows.Forms.Button();
            this.SetPointFrequencyBtn = new System.Windows.Forms.Button();
            this.SetGrindDefaultsBtn = new System.Windows.Forms.Button();
            this.SetGrindAccelBtn = new System.Windows.Forms.Button();
            this.SetTrialSpeedBtn = new System.Windows.Forms.Button();
            this.SetMaxGrindBlendRadiusBtn = new System.Windows.Forms.Button();
            this.SetMaxWaitBtn = new System.Windows.Forms.Button();
            this.SetForceDwellBtn = new System.Windows.Forms.Button();
            this.SetTouchSpeedBtn = new System.Windows.Forms.Button();
            this.SetTouchRetractBtn = new System.Windows.Forms.Button();
            this.ToolSetupGrp = new System.Windows.Forms.GroupBox();
            this.CoolantOffBtn = new System.Windows.Forms.Button();
            this.CoolantTestBtn = new System.Windows.Forms.Button();
            this.ToolOffBtn = new System.Windows.Forms.Button();
            this.ToolTestBtn = new System.Windows.Forms.Button();
            this.DoorClosedInputLbl = new System.Windows.Forms.Label();
            this.FootswitchPressedInputLbl = new System.Windows.Forms.Label();
            this.FootswitchPressedInputTxt = new System.Windows.Forms.TextBox();
            this.SetFootswitchPressedInputBtn = new System.Windows.Forms.Button();
            this.JointMoveMountBtn = new System.Windows.Forms.Button();
            this.JointMoveHomeBtn = new System.Windows.Forms.Button();
            this.DoorClosedInputTxt = new System.Windows.Forms.TextBox();
            this.SetDoorClosedInputBtn = new System.Windows.Forms.Button();
            this.SelectToolBtn = new System.Windows.Forms.Button();
            this.ToolsGrd = new System.Windows.Forms.DataGridView();
            this.LoadToolsBtn = new System.Windows.Forms.Button();
            this.SaveToolsBtn = new System.Windows.Forms.Button();
            this.ClearToolsBtn = new System.Windows.Forms.Button();
            this.GeneralConfigGrp = new System.Windows.Forms.GroupBox();
            this.SaveConfigBtn = new System.Windows.Forms.Button();
            this.AllowRunningOfflineChk = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RobotProgramTxt = new System.Windows.Forms.TextBox();
            this.LoadConfigBtn = new System.Windows.Forms.Button();
            this.DefaultConfigBtn = new System.Windows.Forms.Button();
            this.ServerIpTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RobotIpTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AutoGrindRootLbl = new System.Windows.Forms.Label();
            this.ChangeRootDirectoryBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LogPage = new System.Windows.Forms.TabPage();
            this.ClearAllLogRtbBtn = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.AboutBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LogLevelCombo = new System.Windows.Forms.ComboBox();
            this.JogRunBtn = new System.Windows.Forms.Button();
            this.DiameterLbl = new System.Windows.Forms.Label();
            this.PartGeometryBox = new System.Windows.Forms.ComboBox();
            this.DoorClosedLbl = new System.Windows.Forms.Label();
            this.VersionLbl = new System.Windows.Forms.Label();
            this.FootswitchPressedLbl = new System.Windows.Forms.Label();
            this.Time2Lbl = new System.Windows.Forms.Label();
            this.MonitorTab.SuspendLayout();
            this.positionsPage.SuspendLayout();
            this.PositionTestButtonGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PositionsGrd)).BeginInit();
            this.variablesPage.SuspendLayout();
            this.VariableTestButtonGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VariablesGrd)).BeginInit();
            this.manualPage.SuspendLayout();
            this.revhistPage.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.RunPage.SuspendLayout();
            this.ProgramPage.SuspendLayout();
            this.SetupPage.SuspendLayout();
            this.DefaultMoveSetupGrp.SuspendLayout();
            this.GrindingMoveSetupGrp.SuspendLayout();
            this.ToolSetupGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsGrd)).BeginInit();
            this.GeneralConfigGrp.SuspendLayout();
            this.LogPage.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentLineLbl
            // 
            this.CurrentLineLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentLineLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLineLbl.Location = new System.Drawing.Point(5, 998);
            this.CurrentLineLbl.Name = "CurrentLineLbl";
            this.CurrentLineLbl.Size = new System.Drawing.Size(788, 44);
            this.CurrentLineLbl.TabIndex = 79;
            this.CurrentLineLbl.TextChanged += new System.EventHandler(this.CurrentLineLbl_TextChanged);
            // 
            // RecipeRTB
            // 
            this.RecipeRTB.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeRTB.Location = new System.Drawing.Point(5, 5);
            this.RecipeRTB.Margin = new System.Windows.Forms.Padding(2);
            this.RecipeRTB.Name = "RecipeRTB";
            this.RecipeRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeRTB.Size = new System.Drawing.Size(788, 977);
            this.RecipeRTB.TabIndex = 72;
            this.RecipeRTB.Text = "";
            this.RecipeRTB.VScroll += new System.EventHandler(this.RecipeRTB_VScroll);
            this.RecipeRTB.TextChanged += new System.EventHandler(this.RecipeRTB_TextChanged);
            // 
            // StepBtn
            // 
            this.StepBtn.BackColor = System.Drawing.Color.Gray;
            this.StepBtn.Enabled = false;
            this.StepBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepBtn.ForeColor = System.Drawing.Color.White;
            this.StepBtn.Location = new System.Drawing.Point(226, 1289);
            this.StepBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StepBtn.Name = "StepBtn";
            this.StepBtn.Size = new System.Drawing.Size(197, 140);
            this.StepBtn.TabIndex = 5;
            this.StepBtn.Text = "Step";
            this.StepBtn.UseVisualStyleBackColor = false;
            this.StepBtn.Click += new System.EventHandler(this.StepBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.BackColor = System.Drawing.Color.Gray;
            this.StopBtn.Enabled = false;
            this.StopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopBtn.ForeColor = System.Drawing.Color.White;
            this.StopBtn.Location = new System.Drawing.Point(656, 1288);
            this.StopBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(197, 140);
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
            this.PauseBtn.Location = new System.Drawing.Point(441, 1289);
            this.PauseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(197, 140);
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
            this.StartBtn.Location = new System.Drawing.Point(11, 1288);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(197, 141);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // RecipeFilenameLbl
            // 
            this.RecipeFilenameLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RecipeFilenameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeFilenameLbl.Location = new System.Drawing.Point(5, 1054);
            this.RecipeFilenameLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RecipeFilenameLbl.Name = "RecipeFilenameLbl";
            this.RecipeFilenameLbl.Size = new System.Drawing.Size(788, 90);
            this.RecipeFilenameLbl.TabIndex = 77;
            this.RecipeFilenameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RecipeFilenameLbl.TextChanged += new System.EventHandler(this.RecipeFilenameLbl_TextChanged);
            // 
            // SaveAsRecipeBtn
            // 
            this.SaveAsRecipeBtn.BackColor = System.Drawing.Color.Gray;
            this.SaveAsRecipeBtn.Enabled = false;
            this.SaveAsRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveAsRecipeBtn.ForeColor = System.Drawing.Color.White;
            this.SaveAsRecipeBtn.Location = new System.Drawing.Point(1348, 9);
            this.SaveAsRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SaveAsRecipeBtn.Name = "SaveAsRecipeBtn";
            this.SaveAsRecipeBtn.Size = new System.Drawing.Size(188, 95);
            this.SaveAsRecipeBtn.TabIndex = 75;
            this.SaveAsRecipeBtn.Text = "Save As...";
            this.SaveAsRecipeBtn.UseVisualStyleBackColor = false;
            this.SaveAsRecipeBtn.Click += new System.EventHandler(this.SaveAsRecipeBtn_Click);
            // 
            // NewRecipeBtn
            // 
            this.NewRecipeBtn.BackColor = System.Drawing.Color.Gray;
            this.NewRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewRecipeBtn.ForeColor = System.Drawing.Color.White;
            this.NewRecipeBtn.Location = new System.Drawing.Point(1126, 9);
            this.NewRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.NewRecipeBtn.Name = "NewRecipeBtn";
            this.NewRecipeBtn.Size = new System.Drawing.Size(99, 95);
            this.NewRecipeBtn.TabIndex = 74;
            this.NewRecipeBtn.Text = "New";
            this.NewRecipeBtn.UseVisualStyleBackColor = false;
            this.NewRecipeBtn.Click += new System.EventHandler(this.NewRecipeBtn_Click);
            // 
            // LoadRecipeBtn
            // 
            this.LoadRecipeBtn.BackColor = System.Drawing.Color.Gray;
            this.LoadRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadRecipeBtn.ForeColor = System.Drawing.Color.White;
            this.LoadRecipeBtn.Location = new System.Drawing.Point(456, 9);
            this.LoadRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.LoadRecipeBtn.Name = "LoadRecipeBtn";
            this.LoadRecipeBtn.Size = new System.Drawing.Size(663, 95);
            this.LoadRecipeBtn.TabIndex = 73;
            this.LoadRecipeBtn.Text = "Untitled";
            this.LoadRecipeBtn.UseVisualStyleBackColor = false;
            this.LoadRecipeBtn.Click += new System.EventHandler(this.LoadRecipeBtn_Click);
            // 
            // SaveRecipeBtn
            // 
            this.SaveRecipeBtn.BackColor = System.Drawing.Color.Gray;
            this.SaveRecipeBtn.Enabled = false;
            this.SaveRecipeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveRecipeBtn.ForeColor = System.Drawing.Color.White;
            this.SaveRecipeBtn.Location = new System.Drawing.Point(1233, 9);
            this.SaveRecipeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SaveRecipeBtn.Name = "SaveRecipeBtn";
            this.SaveRecipeBtn.Size = new System.Drawing.Size(107, 95);
            this.SaveRecipeBtn.TabIndex = 72;
            this.SaveRecipeBtn.Text = "Save";
            this.SaveRecipeBtn.UseVisualStyleBackColor = false;
            this.SaveRecipeBtn.Click += new System.EventHandler(this.SaveRecipeBtn_Click);
            // 
            // HeartbeatTmr
            // 
            this.HeartbeatTmr.Tick += new System.EventHandler(this.HeartbeatTmr_Tick);
            // 
            // TimeLbl
            // 
            this.TimeLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TimeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLbl.Location = new System.Drawing.Point(1250, 20);
            this.TimeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TimeLbl.Name = "TimeLbl";
            this.TimeLbl.Size = new System.Drawing.Size(353, 52);
            this.TimeLbl.TabIndex = 5;
            this.TimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.RobotCommandStatusLbl.Location = new System.Drawing.Point(1933, 250);
            this.RobotCommandStatusLbl.Name = "RobotCommandStatusLbl";
            this.RobotCommandStatusLbl.Size = new System.Drawing.Size(187, 125);
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
            this.GrindReadyLbl.Location = new System.Drawing.Point(1933, 652);
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
            this.RobotReadyLbl.Location = new System.Drawing.Point(1933, 518);
            this.RobotReadyLbl.Name = "RobotReadyLbl";
            this.RobotReadyLbl.Size = new System.Drawing.Size(187, 125);
            this.RobotReadyLbl.TabIndex = 89;
            this.RobotReadyLbl.Text = "Robot Ready";
            this.RobotReadyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GrindContactEnabledBtn
            // 
            this.GrindContactEnabledBtn.BackColor = System.Drawing.Color.Gray;
            this.GrindContactEnabledBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindContactEnabledBtn.ForeColor = System.Drawing.Color.White;
            this.GrindContactEnabledBtn.Location = new System.Drawing.Point(871, 1289);
            this.GrindContactEnabledBtn.Name = "GrindContactEnabledBtn";
            this.GrindContactEnabledBtn.Size = new System.Drawing.Size(248, 140);
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
            this.MonitorTab.Controls.Add(this.revhistPage);
            this.MonitorTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonitorTab.ItemSize = new System.Drawing.Size(150, 60);
            this.MonitorTab.Location = new System.Drawing.Point(799, 35);
            this.MonitorTab.Name = "MonitorTab";
            this.MonitorTab.SelectedIndex = 0;
            this.MonitorTab.Size = new System.Drawing.Size(1316, 1113);
            this.MonitorTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MonitorTab.TabIndex = 94;
            // 
            // positionsPage
            // 
            this.positionsPage.Controls.Add(this.JogBtn);
            this.positionsPage.Controls.Add(this.PositionTestButtonGrp);
            this.positionsPage.Controls.Add(this.PositionMoveArmBtn);
            this.positionsPage.Controls.Add(this.PositionMovePoseBtn);
            this.positionsPage.Controls.Add(this.PositionSetBtn);
            this.positionsPage.Controls.Add(this.PositionsGrd);
            this.positionsPage.Location = new System.Drawing.Point(4, 64);
            this.positionsPage.Name = "positionsPage";
            this.positionsPage.Size = new System.Drawing.Size(1308, 1045);
            this.positionsPage.TabIndex = 2;
            this.positionsPage.Text = "Positions";
            this.positionsPage.UseVisualStyleBackColor = true;
            // 
            // JogBtn
            // 
            this.JogBtn.BackColor = System.Drawing.Color.Green;
            this.JogBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JogBtn.ForeColor = System.Drawing.Color.White;
            this.JogBtn.Location = new System.Drawing.Point(1039, 10);
            this.JogBtn.Margin = new System.Windows.Forms.Padding(2);
            this.JogBtn.Name = "JogBtn";
            this.JogBtn.Size = new System.Drawing.Size(248, 181);
            this.JogBtn.TabIndex = 100;
            this.JogBtn.Text = "Jog Only";
            this.JogBtn.UseVisualStyleBackColor = false;
            this.JogBtn.Click += new System.EventHandler(this.JogBtn_Click);
            // 
            // PositionTestButtonGrp
            // 
            this.PositionTestButtonGrp.Controls.Add(this.LoadPositionsBtn);
            this.PositionTestButtonGrp.Controls.Add(this.ClearPositionsBtn);
            this.PositionTestButtonGrp.Controls.Add(this.SavePositionsBtn);
            this.PositionTestButtonGrp.Controls.Add(this.ClearAllPositionsBtn);
            this.PositionTestButtonGrp.Location = new System.Drawing.Point(6, 879);
            this.PositionTestButtonGrp.Name = "PositionTestButtonGrp";
            this.PositionTestButtonGrp.Size = new System.Drawing.Size(1273, 163);
            this.PositionTestButtonGrp.TabIndex = 99;
            this.PositionTestButtonGrp.TabStop = false;
            this.PositionTestButtonGrp.Text = "Position Test Buttons";
            // 
            // LoadPositionsBtn
            // 
            this.LoadPositionsBtn.BackColor = System.Drawing.Color.Gray;
            this.LoadPositionsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadPositionsBtn.ForeColor = System.Drawing.Color.White;
            this.LoadPositionsBtn.Location = new System.Drawing.Point(6, 38);
            this.LoadPositionsBtn.Name = "LoadPositionsBtn";
            this.LoadPositionsBtn.Size = new System.Drawing.Size(240, 110);
            this.LoadPositionsBtn.TabIndex = 94;
            this.LoadPositionsBtn.Text = "Reload";
            this.LoadPositionsBtn.UseVisualStyleBackColor = false;
            this.LoadPositionsBtn.Click += new System.EventHandler(this.LoadPositionsBtn_Click);
            // 
            // ClearPositionsBtn
            // 
            this.ClearPositionsBtn.BackColor = System.Drawing.Color.Gray;
            this.ClearPositionsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearPositionsBtn.ForeColor = System.Drawing.Color.White;
            this.ClearPositionsBtn.Location = new System.Drawing.Point(546, 38);
            this.ClearPositionsBtn.Name = "ClearPositionsBtn";
            this.ClearPositionsBtn.Size = new System.Drawing.Size(240, 110);
            this.ClearPositionsBtn.TabIndex = 92;
            this.ClearPositionsBtn.Text = "Clear";
            this.ClearPositionsBtn.UseVisualStyleBackColor = false;
            this.ClearPositionsBtn.Click += new System.EventHandler(this.ClearPositionsBtn_Click);
            // 
            // SavePositionsBtn
            // 
            this.SavePositionsBtn.BackColor = System.Drawing.Color.Gray;
            this.SavePositionsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SavePositionsBtn.ForeColor = System.Drawing.Color.White;
            this.SavePositionsBtn.Location = new System.Drawing.Point(276, 38);
            this.SavePositionsBtn.Name = "SavePositionsBtn";
            this.SavePositionsBtn.Size = new System.Drawing.Size(240, 110);
            this.SavePositionsBtn.TabIndex = 93;
            this.SavePositionsBtn.Text = "Save";
            this.SavePositionsBtn.UseVisualStyleBackColor = false;
            this.SavePositionsBtn.Click += new System.EventHandler(this.SavePositionsBtn_Click);
            // 
            // ClearAllPositionsBtn
            // 
            this.ClearAllPositionsBtn.BackColor = System.Drawing.Color.Gray;
            this.ClearAllPositionsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearAllPositionsBtn.ForeColor = System.Drawing.Color.White;
            this.ClearAllPositionsBtn.Location = new System.Drawing.Point(816, 38);
            this.ClearAllPositionsBtn.Name = "ClearAllPositionsBtn";
            this.ClearAllPositionsBtn.Size = new System.Drawing.Size(240, 110);
            this.ClearAllPositionsBtn.TabIndex = 95;
            this.ClearAllPositionsBtn.Text = "Clear All";
            this.ClearAllPositionsBtn.UseVisualStyleBackColor = false;
            this.ClearAllPositionsBtn.Click += new System.EventHandler(this.ClearAllPositionsBtn_Click);
            // 
            // PositionMoveArmBtn
            // 
            this.PositionMoveArmBtn.BackColor = System.Drawing.Color.Green;
            this.PositionMoveArmBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionMoveArmBtn.ForeColor = System.Drawing.Color.White;
            this.PositionMoveArmBtn.Location = new System.Drawing.Point(340, 10);
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
            this.PositionSetBtn.Location = new System.Drawing.Point(700, 10);
            this.PositionSetBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PositionSetBtn.Name = "PositionSetBtn";
            this.PositionSetBtn.Size = new System.Drawing.Size(323, 181);
            this.PositionSetBtn.TabIndex = 96;
            this.PositionSetBtn.Text = "Set Position";
            this.PositionSetBtn.UseVisualStyleBackColor = false;
            this.PositionSetBtn.Click += new System.EventHandler(this.PositionSetBtn_Click);
            // 
            // PositionsGrd
            // 
            this.PositionsGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.PositionsGrd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PositionsGrd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PositionsGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PositionsGrd.Location = new System.Drawing.Point(6, 196);
            this.PositionsGrd.Name = "PositionsGrd";
            this.PositionsGrd.RowTemplate.Height = 34;
            this.PositionsGrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PositionsGrd.Size = new System.Drawing.Size(1281, 677);
            this.PositionsGrd.TabIndex = 85;
            // 
            // variablesPage
            // 
            this.variablesPage.Controls.Add(this.VariableTestButtonGrp);
            this.variablesPage.Controls.Add(this.VariablesGrd);
            this.variablesPage.Location = new System.Drawing.Point(4, 64);
            this.variablesPage.Name = "variablesPage";
            this.variablesPage.Padding = new System.Windows.Forms.Padding(3);
            this.variablesPage.Size = new System.Drawing.Size(1308, 1045);
            this.variablesPage.TabIndex = 0;
            this.variablesPage.Text = "Variables";
            this.variablesPage.UseVisualStyleBackColor = true;
            // 
            // VariableTestButtonGrp
            // 
            this.VariableTestButtonGrp.Controls.Add(this.LoadVariablesBtn);
            this.VariableTestButtonGrp.Controls.Add(this.ClearAllVariablesBtn);
            this.VariableTestButtonGrp.Controls.Add(this.ClearVariablesBtn);
            this.VariableTestButtonGrp.Controls.Add(this.SaveVariablesBtn);
            this.VariableTestButtonGrp.Location = new System.Drawing.Point(6, 880);
            this.VariableTestButtonGrp.Name = "VariableTestButtonGrp";
            this.VariableTestButtonGrp.Size = new System.Drawing.Size(1296, 159);
            this.VariableTestButtonGrp.TabIndex = 92;
            this.VariableTestButtonGrp.TabStop = false;
            this.VariableTestButtonGrp.Text = "Variable Test Buttons";
            // 
            // LoadVariablesBtn
            // 
            this.LoadVariablesBtn.BackColor = System.Drawing.Color.Gray;
            this.LoadVariablesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadVariablesBtn.ForeColor = System.Drawing.Color.White;
            this.LoadVariablesBtn.Location = new System.Drawing.Point(6, 38);
            this.LoadVariablesBtn.Name = "LoadVariablesBtn";
            this.LoadVariablesBtn.Size = new System.Drawing.Size(240, 110);
            this.LoadVariablesBtn.TabIndex = 90;
            this.LoadVariablesBtn.Text = "Reload";
            this.LoadVariablesBtn.UseVisualStyleBackColor = false;
            this.LoadVariablesBtn.Click += new System.EventHandler(this.LoadVariablesBtn_Click);
            // 
            // ClearAllVariablesBtn
            // 
            this.ClearAllVariablesBtn.BackColor = System.Drawing.Color.Gray;
            this.ClearAllVariablesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearAllVariablesBtn.ForeColor = System.Drawing.Color.White;
            this.ClearAllVariablesBtn.Location = new System.Drawing.Point(816, 38);
            this.ClearAllVariablesBtn.Name = "ClearAllVariablesBtn";
            this.ClearAllVariablesBtn.Size = new System.Drawing.Size(240, 110);
            this.ClearAllVariablesBtn.TabIndex = 91;
            this.ClearAllVariablesBtn.Text = "Clear All";
            this.ClearAllVariablesBtn.UseVisualStyleBackColor = false;
            this.ClearAllVariablesBtn.Click += new System.EventHandler(this.ClearAllVariablesBtn_Click);
            // 
            // ClearVariablesBtn
            // 
            this.ClearVariablesBtn.BackColor = System.Drawing.Color.Gray;
            this.ClearVariablesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearVariablesBtn.ForeColor = System.Drawing.Color.White;
            this.ClearVariablesBtn.Location = new System.Drawing.Point(546, 38);
            this.ClearVariablesBtn.Name = "ClearVariablesBtn";
            this.ClearVariablesBtn.Size = new System.Drawing.Size(240, 110);
            this.ClearVariablesBtn.TabIndex = 88;
            this.ClearVariablesBtn.Text = "Clear";
            this.ClearVariablesBtn.UseVisualStyleBackColor = false;
            this.ClearVariablesBtn.Click += new System.EventHandler(this.ClearVariablesBtn_Click);
            // 
            // SaveVariablesBtn
            // 
            this.SaveVariablesBtn.BackColor = System.Drawing.Color.Gray;
            this.SaveVariablesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveVariablesBtn.ForeColor = System.Drawing.Color.White;
            this.SaveVariablesBtn.Location = new System.Drawing.Point(276, 38);
            this.SaveVariablesBtn.Name = "SaveVariablesBtn";
            this.SaveVariablesBtn.Size = new System.Drawing.Size(240, 110);
            this.SaveVariablesBtn.TabIndex = 89;
            this.SaveVariablesBtn.Text = "Save";
            this.SaveVariablesBtn.UseVisualStyleBackColor = false;
            this.SaveVariablesBtn.Click += new System.EventHandler(this.SaveVariablesBtn_Click);
            // 
            // VariablesGrd
            // 
            this.VariablesGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.VariablesGrd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VariablesGrd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.VariablesGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VariablesGrd.Location = new System.Drawing.Point(6, 6);
            this.VariablesGrd.Name = "VariablesGrd";
            this.VariablesGrd.RowTemplate.Height = 34;
            this.VariablesGrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VariablesGrd.Size = new System.Drawing.Size(1296, 868);
            this.VariablesGrd.TabIndex = 84;
            // 
            // manualPage
            // 
            this.manualPage.Controls.Add(this.FullManualBtn);
            this.manualPage.Controls.Add(this.RecipeCommandsRTB);
            this.manualPage.Location = new System.Drawing.Point(4, 64);
            this.manualPage.Name = "manualPage";
            this.manualPage.Size = new System.Drawing.Size(1308, 1045);
            this.manualPage.TabIndex = 3;
            this.manualPage.Text = "Manual";
            this.manualPage.UseVisualStyleBackColor = true;
            // 
            // FullManualBtn
            // 
            this.FullManualBtn.Location = new System.Drawing.Point(364, 985);
            this.FullManualBtn.Name = "FullManualBtn";
            this.FullManualBtn.Size = new System.Drawing.Size(609, 46);
            this.FullManualBtn.TabIndex = 105;
            this.FullManualBtn.Text = "Show Full Manual PDF in Chrome";
            this.FullManualBtn.UseVisualStyleBackColor = true;
            this.FullManualBtn.Click += new System.EventHandler(this.FullManualBtn_Click);
            // 
            // RecipeCommandsRTB
            // 
            this.RecipeCommandsRTB.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeCommandsRTB.Location = new System.Drawing.Point(16, 16);
            this.RecipeCommandsRTB.Margin = new System.Windows.Forms.Padding(2);
            this.RecipeCommandsRTB.Name = "RecipeCommandsRTB";
            this.RecipeCommandsRTB.ReadOnly = true;
            this.RecipeCommandsRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeCommandsRTB.Size = new System.Drawing.Size(1276, 964);
            this.RecipeCommandsRTB.TabIndex = 104;
            this.RecipeCommandsRTB.Text = "";
            // 
            // revhistPage
            // 
            this.revhistPage.Controls.Add(this.RevHistRTB);
            this.revhistPage.Location = new System.Drawing.Point(4, 64);
            this.revhistPage.Name = "revhistPage";
            this.revhistPage.Size = new System.Drawing.Size(1308, 1045);
            this.revhistPage.TabIndex = 4;
            this.revhistPage.Text = "RevHist";
            this.revhistPage.UseVisualStyleBackColor = true;
            // 
            // RevHistRTB
            // 
            this.RevHistRTB.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RevHistRTB.Location = new System.Drawing.Point(16, 16);
            this.RevHistRTB.Margin = new System.Windows.Forms.Padding(2);
            this.RevHistRTB.Name = "RevHistRTB";
            this.RevHistRTB.ReadOnly = true;
            this.RevHistRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RevHistRTB.Size = new System.Drawing.Size(1276, 1013);
            this.RevHistRTB.TabIndex = 105;
            this.RevHistRTB.Text = "";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.ExecLogRTB);
            this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(1058, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(1067, 574);
            this.groupBox10.TabIndex = 90;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Recipe Execution Messages";
            // 
            // ExecLogRTB
            // 
            this.ExecLogRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecLogRTB.Location = new System.Drawing.Point(7, 27);
            this.ExecLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.ExecLogRTB.MaxLength = 1000000;
            this.ExecLogRTB.Name = "ExecLogRTB";
            this.ExecLogRTB.ReadOnly = true;
            this.ExecLogRTB.Size = new System.Drawing.Size(1055, 542);
            this.ExecLogRTB.TabIndex = 1;
            this.ExecLogRTB.Text = "";
            this.ExecLogRTB.WordWrap = false;
            this.ExecLogRTB.DoubleClick += new System.EventHandler(this.ExecLogRTB_DoubleClick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.UrLogRTB);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(1, 577);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1051, 309);
            this.groupBox5.TabIndex = 89;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Robot Commands and Responses";
            // 
            // UrLogRTB
            // 
            this.UrLogRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UrLogRTB.Location = new System.Drawing.Point(5, 27);
            this.UrLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.UrLogRTB.MaxLength = 1000000;
            this.UrLogRTB.Name = "UrLogRTB";
            this.UrLogRTB.ReadOnly = true;
            this.UrLogRTB.Size = new System.Drawing.Size(1041, 268);
            this.UrLogRTB.TabIndex = 1;
            this.UrLogRTB.Text = "";
            this.UrLogRTB.WordWrap = false;
            this.UrLogRTB.DoubleClick += new System.EventHandler(this.UrLogRTB_DoubleClick);
            // 
            // UrDashboardLogRTB
            // 
            this.UrDashboardLogRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UrDashboardLogRTB.Location = new System.Drawing.Point(7, 27);
            this.UrDashboardLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.UrDashboardLogRTB.MaxLength = 1000000;
            this.UrDashboardLogRTB.Name = "UrDashboardLogRTB";
            this.UrDashboardLogRTB.ReadOnly = true;
            this.UrDashboardLogRTB.Size = new System.Drawing.Size(1060, 268);
            this.UrDashboardLogRTB.TabIndex = 1;
            this.UrDashboardLogRTB.Text = "";
            this.UrDashboardLogRTB.WordWrap = false;
            this.UrDashboardLogRTB.DoubleClick += new System.EventHandler(this.UrDashboardLogRTB_DoubleClick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ErrorLogRTB);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(3, 892);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1754, 269);
            this.groupBox6.TabIndex = 84;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Errors and Warnings";
            // 
            // ErrorLogRTB
            // 
            this.ErrorLogRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLogRTB.Location = new System.Drawing.Point(5, 27);
            this.ErrorLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.ErrorLogRTB.MaxLength = 1000000;
            this.ErrorLogRTB.Name = "ErrorLogRTB";
            this.ErrorLogRTB.ReadOnly = true;
            this.ErrorLogRTB.Size = new System.Drawing.Size(1731, 231);
            this.ErrorLogRTB.TabIndex = 0;
            this.ErrorLogRTB.Text = "";
            this.ErrorLogRTB.WordWrap = false;
            this.ErrorLogRTB.DoubleClick += new System.EventHandler(this.ErrorLogRTB_DoubleClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AllLogRTB);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(1, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1051, 574);
            this.groupBox4.TabIndex = 88;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "All Log Messages (Double-click to clear any of these)";
            // 
            // AllLogRTB
            // 
            this.AllLogRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllLogRTB.Location = new System.Drawing.Point(5, 27);
            this.AllLogRTB.Margin = new System.Windows.Forms.Padding(2);
            this.AllLogRTB.MaxLength = 1000000;
            this.AllLogRTB.Name = "AllLogRTB";
            this.AllLogRTB.ReadOnly = true;
            this.AllLogRTB.Size = new System.Drawing.Size(1041, 542);
            this.AllLogRTB.TabIndex = 4;
            this.AllLogRTB.Text = "";
            this.AllLogRTB.WordWrap = false;
            this.AllLogRTB.DoubleClick += new System.EventHandler(this.AllLogRTB_DoubleClick);
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.Gray;
            this.ExitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBtn.ForeColor = System.Drawing.Color.White;
            this.ExitBtn.Location = new System.Drawing.Point(1946, 9);
            this.ExitBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(185, 95);
            this.ExitBtn.TabIndex = 96;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1128, 1086);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(305, 72);
            this.label6.TabIndex = 97;
            this.label6.Text = "Tool";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MountedToolBox
            // 
            this.MountedToolBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MountedToolBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MountedToolBox.FormattingEnabled = true;
            this.MountedToolBox.Location = new System.Drawing.Point(1142, 1288);
            this.MountedToolBox.Name = "MountedToolBox";
            this.MountedToolBox.Size = new System.Drawing.Size(305, 63);
            this.MountedToolBox.TabIndex = 99;
            this.MountedToolBox.SelectedIndexChanged += new System.EventHandler(this.MountedToolBox_SelectedIndexChanged);
            // 
            // UserModeBox
            // 
            this.UserModeBox.BackColor = System.Drawing.SystemColors.Control;
            this.UserModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserModeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserModeBox.FormattingEnabled = true;
            this.UserModeBox.Items.AddRange(new object[] {
            "OPERATOR",
            "EDITOR",
            "ENGINEERING"});
            this.UserModeBox.Location = new System.Drawing.Point(1833, 38);
            this.UserModeBox.Name = "UserModeBox";
            this.UserModeBox.Size = new System.Drawing.Size(284, 47);
            this.UserModeBox.TabIndex = 103;
            this.UserModeBox.SelectedIndexChanged += new System.EventHandler(this.OperatorModeBox_SelectedIndexChanged);
            // 
            // RobotModeBtn
            // 
            this.RobotModeBtn.BackColor = System.Drawing.Color.Gray;
            this.RobotModeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotModeBtn.ForeColor = System.Drawing.Color.White;
            this.RobotModeBtn.Location = new System.Drawing.Point(1638, 384);
            this.RobotModeBtn.Name = "RobotModeBtn";
            this.RobotModeBtn.Size = new System.Drawing.Size(275, 125);
            this.RobotModeBtn.TabIndex = 106;
            this.RobotModeBtn.Text = "Robot Mode";
            this.RobotModeBtn.UseVisualStyleBackColor = false;
            this.RobotModeBtn.Click += new System.EventHandler(this.RobotModeBtn_Click);
            // 
            // SafetyStatusBtn
            // 
            this.SafetyStatusBtn.BackColor = System.Drawing.Color.Gray;
            this.SafetyStatusBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SafetyStatusBtn.ForeColor = System.Drawing.Color.White;
            this.SafetyStatusBtn.Location = new System.Drawing.Point(1638, 518);
            this.SafetyStatusBtn.Name = "SafetyStatusBtn";
            this.SafetyStatusBtn.Size = new System.Drawing.Size(275, 125);
            this.SafetyStatusBtn.TabIndex = 107;
            this.SafetyStatusBtn.Text = "Safety Status";
            this.SafetyStatusBtn.UseVisualStyleBackColor = false;
            this.SafetyStatusBtn.Click += new System.EventHandler(this.SafetyStatusBtn_Click);
            // 
            // ProgramStateBtn
            // 
            this.ProgramStateBtn.BackColor = System.Drawing.Color.Gray;
            this.ProgramStateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgramStateBtn.ForeColor = System.Drawing.Color.White;
            this.ProgramStateBtn.Location = new System.Drawing.Point(1638, 652);
            this.ProgramStateBtn.Name = "ProgramStateBtn";
            this.ProgramStateBtn.Size = new System.Drawing.Size(275, 125);
            this.ProgramStateBtn.TabIndex = 108;
            this.ProgramStateBtn.Text = "Program State";
            this.ProgramStateBtn.UseVisualStyleBackColor = false;
            this.ProgramStateBtn.Click += new System.EventHandler(this.ProgramStateBtn_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Gray;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1439, 1086);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(667, 72);
            this.label5.TabIndex = 115;
            this.label5.Text = "Part Geometry";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DiameterDimLbl
            // 
            this.DiameterDimLbl.AutoSize = true;
            this.DiameterDimLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiameterDimLbl.Location = new System.Drawing.Point(1970, 1296);
            this.DiameterDimLbl.Name = "DiameterDimLbl";
            this.DiameterDimLbl.Size = new System.Drawing.Size(162, 42);
            this.DiameterDimLbl.TabIndex = 114;
            this.DiameterDimLbl.Text = "dia (mm)";
            this.DiameterDimLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainTab
            // 
            this.MainTab.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.MainTab.Controls.Add(this.RunPage);
            this.MainTab.Controls.Add(this.ProgramPage);
            this.MainTab.Controls.Add(this.SetupPage);
            this.MainTab.Controls.Add(this.LogPage);
            this.MainTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainTab.ItemSize = new System.Drawing.Size(96, 96);
            this.MainTab.Location = new System.Drawing.Point(8, 11);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(2140, 1272);
            this.MainTab.TabIndex = 116;
            this.MainTab.SelectedIndexChanged += new System.EventHandler(this.MainTab_SelectedIndexChanged);
            this.MainTab.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MainTab_Selecting);
            // 
            // RunPage
            // 
            this.RunPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RunPage.Controls.Add(this.label22);
            this.RunPage.Controls.Add(this.GrindForceReportZLbl);
            this.RunPage.Controls.Add(this.label21);
            this.RunPage.Controls.Add(this.label18);
            this.RunPage.Controls.Add(this.label9);
            this.RunPage.Controls.Add(this.TimeLbl);
            this.RunPage.Controls.Add(this.UserModeBox);
            this.RunPage.Controls.Add(this.RobotSentLbl);
            this.RunPage.Controls.Add(this.StepTimeRemainingLbl);
            this.RunPage.Controls.Add(this.label17);
            this.RunPage.Controls.Add(this.StepTimeEstimateLbl);
            this.RunPage.Controls.Add(this.label13);
            this.RunPage.Controls.Add(this.StepElapsedTimeLbl);
            this.RunPage.Controls.Add(this.label16);
            this.RunPage.Controls.Add(this.label15);
            this.RunPage.Controls.Add(this.label11);
            this.RunPage.Controls.Add(this.MoveToolMountBtn);
            this.RunPage.Controls.Add(this.MoveToolHomeBtn);
            this.RunPage.Controls.Add(this.GrindProcessStateLbl);
            this.RunPage.Controls.Add(this.RobotCompletedLbl);
            this.RunPage.Controls.Add(this.RunStartedTimeLbl);
            this.RunPage.Controls.Add(this.label14);
            this.RunPage.Controls.Add(this.RobotConnectBtn);
            this.RunPage.Controls.Add(this.RunElapsedTimeLbl);
            this.RunPage.Controls.Add(this.label12);
            this.RunPage.Controls.Add(this.RecipeRTBCopy);
            this.RunPage.Controls.Add(this.Grind);
            this.RunPage.Controls.Add(this.GrindNCyclesLbl);
            this.RunPage.Controls.Add(this.GrindCycleLbl);
            this.RunPage.Controls.Add(this.CurrentLineLblCopy);
            this.RunPage.Controls.Add(this.label10);
            this.RunPage.Controls.Add(this.RunStateLbl);
            this.RunPage.Controls.Add(this.RobotCommandStatusLbl);
            this.RunPage.Controls.Add(this.GrindReadyLbl);
            this.RunPage.Controls.Add(this.RobotReadyLbl);
            this.RunPage.Controls.Add(this.RobotModeBtn);
            this.RunPage.Controls.Add(this.SafetyStatusBtn);
            this.RunPage.Controls.Add(this.label5);
            this.RunPage.Controls.Add(this.ProgramStateBtn);
            this.RunPage.Controls.Add(this.label6);
            this.RunPage.Location = new System.Drawing.Point(4, 100);
            this.RunPage.Name = "RunPage";
            this.RunPage.Padding = new System.Windows.Forms.Padding(3);
            this.RunPage.Size = new System.Drawing.Size(2132, 1168);
            this.RunPage.TabIndex = 0;
            this.RunPage.Text = "Run";
            this.RunPage.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(1362, 695);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(49, 46);
            this.label22.TabIndex = 158;
            this.label22.Text = "N";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GrindForceReportZLbl
            // 
            this.GrindForceReportZLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GrindForceReportZLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindForceReportZLbl.Location = new System.Drawing.Point(1250, 693);
            this.GrindForceReportZLbl.Name = "GrindForceReportZLbl";
            this.GrindForceReportZLbl.Size = new System.Drawing.Size(112, 52);
            this.GrindForceReportZLbl.TabIndex = 157;
            this.GrindForceReportZLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(832, 695);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(419, 46);
            this.label21.TabIndex = 156;
            this.label21.Text = "Last Reported Z Force";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(994, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(251, 46);
            this.label18.TabIndex = 155;
            this.label18.Text = "Current Time";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(1724, 35);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 50);
            this.label9.TabIndex = 154;
            this.label9.Text = "User";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RobotSentLbl
            // 
            this.RobotSentLbl.BackColor = System.Drawing.Color.Green;
            this.RobotSentLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotSentLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotSentLbl.ForeColor = System.Drawing.Color.White;
            this.RobotSentLbl.Location = new System.Drawing.Point(1933, 388);
            this.RobotSentLbl.Name = "RobotSentLbl";
            this.RobotSentLbl.Size = new System.Drawing.Size(187, 52);
            this.RobotSentLbl.TabIndex = 149;
            this.RobotSentLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StepTimeRemainingLbl
            // 
            this.StepTimeRemainingLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StepTimeRemainingLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepTimeRemainingLbl.Location = new System.Drawing.Point(1250, 583);
            this.StepTimeRemainingLbl.Name = "StepTimeRemainingLbl";
            this.StepTimeRemainingLbl.Size = new System.Drawing.Size(353, 52);
            this.StepTimeRemainingLbl.TabIndex = 148;
            this.StepTimeRemainingLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(849, 586);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(402, 46);
            this.label17.TabIndex = 147;
            this.label17.Text = "Step Time Remaining";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StepTimeEstimateLbl
            // 
            this.StepTimeEstimateLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StepTimeEstimateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepTimeEstimateLbl.Location = new System.Drawing.Point(1250, 528);
            this.StepTimeEstimateLbl.Name = "StepTimeEstimateLbl";
            this.StepTimeEstimateLbl.Size = new System.Drawing.Size(353, 52);
            this.StepTimeEstimateLbl.TabIndex = 146;
            this.StepTimeEstimateLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(884, 531);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(367, 46);
            this.label13.TabIndex = 145;
            this.label13.Text = "Step Time Estimate";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StepElapsedTimeLbl
            // 
            this.StepElapsedTimeLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StepElapsedTimeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepElapsedTimeLbl.Location = new System.Drawing.Point(1250, 473);
            this.StepElapsedTimeLbl.Name = "StepElapsedTimeLbl";
            this.StepElapsedTimeLbl.Size = new System.Drawing.Size(353, 52);
            this.StepElapsedTimeLbl.TabIndex = 144;
            this.StepElapsedTimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(922, 476);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(329, 46);
            this.label16.TabIndex = 143;
            this.label16.Text = "Time in This Step";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1550, 957);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(285, 110);
            this.label15.TabIndex = 141;
            this.label15.Text = "Joint Move to\r\nTool Home Pose:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1550, 838);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(333, 115);
            this.label11.TabIndex = 139;
            this.label11.Text = "Joint Move to\r\nTool Mount Pose:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MoveToolMountBtn
            // 
            this.MoveToolMountBtn.BackColor = System.Drawing.Color.Green;
            this.MoveToolMountBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveToolMountBtn.ForeColor = System.Drawing.Color.White;
            this.MoveToolMountBtn.Location = new System.Drawing.Point(1126, 838);
            this.MoveToolMountBtn.Margin = new System.Windows.Forms.Padding(2);
            this.MoveToolMountBtn.Name = "MoveToolMountBtn";
            this.MoveToolMountBtn.Size = new System.Drawing.Size(419, 115);
            this.MoveToolMountBtn.TabIndex = 138;
            this.MoveToolMountBtn.Text = "tool_mount";
            this.MoveToolMountBtn.UseVisualStyleBackColor = false;
            this.MoveToolMountBtn.Click += new System.EventHandler(this.MoveToolMountBtn_Click);
            // 
            // MoveToolHomeBtn
            // 
            this.MoveToolHomeBtn.BackColor = System.Drawing.Color.Green;
            this.MoveToolHomeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveToolHomeBtn.ForeColor = System.Drawing.Color.White;
            this.MoveToolHomeBtn.Location = new System.Drawing.Point(1128, 957);
            this.MoveToolHomeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.MoveToolHomeBtn.Name = "MoveToolHomeBtn";
            this.MoveToolHomeBtn.Size = new System.Drawing.Size(417, 110);
            this.MoveToolHomeBtn.TabIndex = 137;
            this.MoveToolHomeBtn.Text = "tool_home";
            this.MoveToolHomeBtn.UseVisualStyleBackColor = false;
            this.MoveToolHomeBtn.Click += new System.EventHandler(this.MoveToolHomeBtn_Click);
            // 
            // GrindProcessStateLbl
            // 
            this.GrindProcessStateLbl.BackColor = System.Drawing.Color.Gray;
            this.GrindProcessStateLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GrindProcessStateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindProcessStateLbl.ForeColor = System.Drawing.Color.White;
            this.GrindProcessStateLbl.Location = new System.Drawing.Point(1933, 786);
            this.GrindProcessStateLbl.Name = "GrindProcessStateLbl";
            this.GrindProcessStateLbl.Size = new System.Drawing.Size(187, 125);
            this.GrindProcessStateLbl.TabIndex = 136;
            this.GrindProcessStateLbl.Text = "Grind Process State";
            this.GrindProcessStateLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RobotCompletedLbl
            // 
            this.RobotCompletedLbl.BackColor = System.Drawing.Color.Gray;
            this.RobotCompletedLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotCompletedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotCompletedLbl.ForeColor = System.Drawing.Color.White;
            this.RobotCompletedLbl.Location = new System.Drawing.Point(1933, 443);
            this.RobotCompletedLbl.Name = "RobotCompletedLbl";
            this.RobotCompletedLbl.Size = new System.Drawing.Size(187, 52);
            this.RobotCompletedLbl.TabIndex = 135;
            this.RobotCompletedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RunStartedTimeLbl
            // 
            this.RunStartedTimeLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RunStartedTimeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RunStartedTimeLbl.Location = new System.Drawing.Point(1250, 76);
            this.RunStartedTimeLbl.Name = "RunStartedTimeLbl";
            this.RunStartedTimeLbl.Size = new System.Drawing.Size(353, 52);
            this.RunStartedTimeLbl.TabIndex = 134;
            this.RunStartedTimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1042, 79);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(203, 46);
            this.label14.TabIndex = 133;
            this.label14.Text = "Start Time";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RobotConnectBtn
            // 
            this.RobotConnectBtn.BackColor = System.Drawing.Color.Gray;
            this.RobotConnectBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotConnectBtn.ForeColor = System.Drawing.Color.White;
            this.RobotConnectBtn.Location = new System.Drawing.Point(1638, 250);
            this.RobotConnectBtn.Name = "RobotConnectBtn";
            this.RobotConnectBtn.Size = new System.Drawing.Size(275, 125);
            this.RobotConnectBtn.TabIndex = 73;
            this.RobotConnectBtn.Text = "OFF";
            this.RobotConnectBtn.UseVisualStyleBackColor = false;
            this.RobotConnectBtn.Click += new System.EventHandler(this.RobotConnectBtn_Click);
            // 
            // RunElapsedTimeLbl
            // 
            this.RunElapsedTimeLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RunElapsedTimeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RunElapsedTimeLbl.Location = new System.Drawing.Point(1250, 133);
            this.RunElapsedTimeLbl.Name = "RunElapsedTimeLbl";
            this.RunElapsedTimeLbl.Size = new System.Drawing.Size(353, 52);
            this.RunElapsedTimeLbl.TabIndex = 132;
            this.RunElapsedTimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(953, 136);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(292, 46);
            this.label12.TabIndex = 131;
            this.label12.Text = "Total Run Time";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RecipeRTBCopy
            // 
            this.RecipeRTBCopy.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecipeRTBCopy.Location = new System.Drawing.Point(5, 5);
            this.RecipeRTBCopy.Margin = new System.Windows.Forms.Padding(2);
            this.RecipeRTBCopy.Name = "RecipeRTBCopy";
            this.RecipeRTBCopy.ReadOnly = true;
            this.RecipeRTBCopy.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RecipeRTBCopy.Size = new System.Drawing.Size(788, 1143);
            this.RecipeRTBCopy.TabIndex = 129;
            this.RecipeRTBCopy.Text = "";
            this.RecipeRTBCopy.VScroll += new System.EventHandler(this.RecipeRTBCopy_VScroll);
            // 
            // Grind
            // 
            this.Grind.AutoSize = true;
            this.Grind.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grind.Location = new System.Drawing.Point(1368, 640);
            this.Grind.Name = "Grind";
            this.Grind.Size = new System.Drawing.Size(54, 46);
            this.Grind.TabIndex = 128;
            this.Grind.Text = "of";
            this.Grind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GrindNCyclesLbl
            // 
            this.GrindNCyclesLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GrindNCyclesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindNCyclesLbl.Location = new System.Drawing.Point(1422, 638);
            this.GrindNCyclesLbl.Name = "GrindNCyclesLbl";
            this.GrindNCyclesLbl.Size = new System.Drawing.Size(100, 52);
            this.GrindNCyclesLbl.TabIndex = 127;
            this.GrindNCyclesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GrindCycleLbl
            // 
            this.GrindCycleLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GrindCycleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrindCycleLbl.Location = new System.Drawing.Point(1250, 638);
            this.GrindCycleLbl.Name = "GrindCycleLbl";
            this.GrindCycleLbl.Size = new System.Drawing.Size(112, 52);
            this.GrindCycleLbl.TabIndex = 126;
            this.GrindCycleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrentLineLblCopy
            // 
            this.CurrentLineLblCopy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentLineLblCopy.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLineLblCopy.Location = new System.Drawing.Point(817, 290);
            this.CurrentLineLblCopy.Name = "CurrentLineLblCopy";
            this.CurrentLineLblCopy.Size = new System.Drawing.Size(786, 160);
            this.CurrentLineLblCopy.TabIndex = 125;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1022, 640);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(229, 46);
            this.label10.TabIndex = 124;
            this.label10.Text = "Grind Cycle";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RunStateLbl
            // 
            this.RunStateLbl.BackColor = System.Drawing.Color.Gray;
            this.RunStateLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RunStateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RunStateLbl.ForeColor = System.Drawing.Color.White;
            this.RunStateLbl.Location = new System.Drawing.Point(817, 192);
            this.RunStateLbl.Name = "RunStateLbl";
            this.RunStateLbl.Size = new System.Drawing.Size(786, 94);
            this.RunStateLbl.TabIndex = 122;
            this.RunStateLbl.Text = "Current Step";
            this.RunStateLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgramPage
            // 
            this.ProgramPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProgramPage.Controls.Add(this.BigEditBtn);
            this.ProgramPage.Controls.Add(this.RecipeRTB);
            this.ProgramPage.Controls.Add(this.CurrentLineLbl);
            this.ProgramPage.Controls.Add(this.RecipeFilenameLbl);
            this.ProgramPage.Controls.Add(this.MonitorTab);
            this.ProgramPage.Location = new System.Drawing.Point(4, 100);
            this.ProgramPage.Name = "ProgramPage";
            this.ProgramPage.Padding = new System.Windows.Forms.Padding(3);
            this.ProgramPage.Size = new System.Drawing.Size(2132, 1168);
            this.ProgramPage.TabIndex = 1;
            this.ProgramPage.Text = "Program";
            this.ProgramPage.UseVisualStyleBackColor = true;
            // 
            // BigEditBtn
            // 
            this.BigEditBtn.BackColor = System.Drawing.Color.Gray;
            this.BigEditBtn.Enabled = false;
            this.BigEditBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BigEditBtn.ForeColor = System.Drawing.Color.White;
            this.BigEditBtn.Location = new System.Drawing.Point(623, 861);
            this.BigEditBtn.Margin = new System.Windows.Forms.Padding(2);
            this.BigEditBtn.Name = "BigEditBtn";
            this.BigEditBtn.Size = new System.Drawing.Size(138, 111);
            this.BigEditBtn.TabIndex = 95;
            this.BigEditBtn.Text = "Big Edit";
            this.BigEditBtn.UseVisualStyleBackColor = false;
            this.BigEditBtn.Click += new System.EventHandler(this.BigEditBtn_Click);
            // 
            // SetupPage
            // 
            this.SetupPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SetupPage.Controls.Add(this.label19);
            this.SetupPage.Controls.Add(this.RobotPolyscopeVersionLbl);
            this.SetupPage.Controls.Add(this.RobotSerialNumberLbl);
            this.SetupPage.Controls.Add(this.label8);
            this.SetupPage.Controls.Add(this.RobotModelLbl);
            this.SetupPage.Controls.Add(this.label7);
            this.SetupPage.Controls.Add(this.DefaultMoveSetupGrp);
            this.SetupPage.Controls.Add(this.GrindingMoveSetupGrp);
            this.SetupPage.Controls.Add(this.ToolSetupGrp);
            this.SetupPage.Controls.Add(this.GeneralConfigGrp);
            this.SetupPage.Location = new System.Drawing.Point(4, 100);
            this.SetupPage.Name = "SetupPage";
            this.SetupPage.Size = new System.Drawing.Size(2132, 1168);
            this.SetupPage.TabIndex = 2;
            this.SetupPage.Text = "Setup";
            this.SetupPage.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(820, 1125);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(130, 36);
            this.label19.TabIndex = 160;
            this.label19.Text = "Software";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RobotPolyscopeVersionLbl
            // 
            this.RobotPolyscopeVersionLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotPolyscopeVersionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotPolyscopeVersionLbl.Location = new System.Drawing.Point(954, 1125);
            this.RobotPolyscopeVersionLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RobotPolyscopeVersionLbl.Name = "RobotPolyscopeVersionLbl";
            this.RobotPolyscopeVersionLbl.Size = new System.Drawing.Size(718, 36);
            this.RobotPolyscopeVersionLbl.TabIndex = 159;
            this.RobotPolyscopeVersionLbl.Text = "PolyScope";
            this.RobotPolyscopeVersionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RobotSerialNumberLbl
            // 
            this.RobotSerialNumberLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotSerialNumberLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotSerialNumberLbl.Location = new System.Drawing.Point(495, 1125);
            this.RobotSerialNumberLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RobotSerialNumberLbl.Name = "RobotSerialNumberLbl";
            this.RobotSerialNumberLbl.Size = new System.Drawing.Size(257, 36);
            this.RobotSerialNumberLbl.TabIndex = 157;
            this.RobotSerialNumberLbl.Text = "Serial Number";
            this.RobotSerialNumberLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(427, 1125);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 36);
            this.label8.TabIndex = 158;
            this.label8.Text = "S/N";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RobotModelLbl
            // 
            this.RobotModelLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RobotModelLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotModelLbl.Location = new System.Drawing.Point(223, 1125);
            this.RobotModelLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RobotModelLbl.Name = "RobotModelLbl";
            this.RobotModelLbl.Size = new System.Drawing.Size(157, 36);
            this.RobotModelLbl.TabIndex = 153;
            this.RobotModelLbl.Text = "Model";
            this.RobotModelLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(45, 1125);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 36);
            this.label7.TabIndex = 154;
            this.label7.Text = "Robot Model";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DefaultMoveSetupGrp
            // 
            this.DefaultMoveSetupGrp.Controls.Add(this.SetMoveDefaultsBtn);
            this.DefaultMoveSetupGrp.Controls.Add(this.SetLinearAccelBtn);
            this.DefaultMoveSetupGrp.Controls.Add(this.SetBlendRadiusBtn);
            this.DefaultMoveSetupGrp.Controls.Add(this.SetJointSpeedBtn);
            this.DefaultMoveSetupGrp.Controls.Add(this.SetJointAccelBtn);
            this.DefaultMoveSetupGrp.Controls.Add(this.SetLinearSpeedBtn);
            this.DefaultMoveSetupGrp.Location = new System.Drawing.Point(1188, 492);
            this.DefaultMoveSetupGrp.Name = "DefaultMoveSetupGrp";
            this.DefaultMoveSetupGrp.Size = new System.Drawing.Size(927, 320);
            this.DefaultMoveSetupGrp.TabIndex = 118;
            this.DefaultMoveSetupGrp.TabStop = false;
            this.DefaultMoveSetupGrp.Text = "Default (non-Grinding) Motion Parameters";
            // 
            // SetMoveDefaultsBtn
            // 
            this.SetMoveDefaultsBtn.BackColor = System.Drawing.Color.Green;
            this.SetMoveDefaultsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetMoveDefaultsBtn.ForeColor = System.Drawing.Color.White;
            this.SetMoveDefaultsBtn.Location = new System.Drawing.Point(750, 27);
            this.SetMoveDefaultsBtn.Name = "SetMoveDefaultsBtn";
            this.SetMoveDefaultsBtn.Size = new System.Drawing.Size(171, 130);
            this.SetMoveDefaultsBtn.TabIndex = 122;
            this.SetMoveDefaultsBtn.Text = "Restore Defaults";
            this.SetMoveDefaultsBtn.UseVisualStyleBackColor = false;
            this.SetMoveDefaultsBtn.Click += new System.EventHandler(this.SetMoveDefaultsBtn_Click);
            // 
            // SetLinearAccelBtn
            // 
            this.SetLinearAccelBtn.BackColor = System.Drawing.Color.Green;
            this.SetLinearAccelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetLinearAccelBtn.ForeColor = System.Drawing.Color.White;
            this.SetLinearAccelBtn.Location = new System.Drawing.Point(308, 40);
            this.SetLinearAccelBtn.Name = "SetLinearAccelBtn";
            this.SetLinearAccelBtn.Size = new System.Drawing.Size(286, 130);
            this.SetLinearAccelBtn.TabIndex = 110;
            this.SetLinearAccelBtn.Text = "Set Linear Accel";
            this.SetLinearAccelBtn.UseVisualStyleBackColor = false;
            this.SetLinearAccelBtn.Click += new System.EventHandler(this.SetLinearAccelBtn_Click);
            // 
            // SetBlendRadiusBtn
            // 
            this.SetBlendRadiusBtn.BackColor = System.Drawing.Color.Green;
            this.SetBlendRadiusBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetBlendRadiusBtn.ForeColor = System.Drawing.Color.White;
            this.SetBlendRadiusBtn.Location = new System.Drawing.Point(600, 176);
            this.SetBlendRadiusBtn.Name = "SetBlendRadiusBtn";
            this.SetBlendRadiusBtn.Size = new System.Drawing.Size(286, 130);
            this.SetBlendRadiusBtn.TabIndex = 111;
            this.SetBlendRadiusBtn.Text = "Set Blend Radius";
            this.SetBlendRadiusBtn.UseVisualStyleBackColor = false;
            this.SetBlendRadiusBtn.Click += new System.EventHandler(this.SetBlendRadiusBtn_Click);
            // 
            // SetJointSpeedBtn
            // 
            this.SetJointSpeedBtn.BackColor = System.Drawing.Color.Green;
            this.SetJointSpeedBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetJointSpeedBtn.ForeColor = System.Drawing.Color.White;
            this.SetJointSpeedBtn.Location = new System.Drawing.Point(15, 177);
            this.SetJointSpeedBtn.Name = "SetJointSpeedBtn";
            this.SetJointSpeedBtn.Size = new System.Drawing.Size(286, 130);
            this.SetJointSpeedBtn.TabIndex = 112;
            this.SetJointSpeedBtn.Text = "Set Joint Speed";
            this.SetJointSpeedBtn.UseVisualStyleBackColor = false;
            this.SetJointSpeedBtn.Click += new System.EventHandler(this.SetJointSpeedBtn_Click);
            // 
            // SetJointAccelBtn
            // 
            this.SetJointAccelBtn.BackColor = System.Drawing.Color.Green;
            this.SetJointAccelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetJointAccelBtn.ForeColor = System.Drawing.Color.White;
            this.SetJointAccelBtn.Location = new System.Drawing.Point(308, 176);
            this.SetJointAccelBtn.Name = "SetJointAccelBtn";
            this.SetJointAccelBtn.Size = new System.Drawing.Size(286, 130);
            this.SetJointAccelBtn.TabIndex = 113;
            this.SetJointAccelBtn.Text = "Set Joint Accel";
            this.SetJointAccelBtn.UseVisualStyleBackColor = false;
            this.SetJointAccelBtn.Click += new System.EventHandler(this.SetJointAccelBtn_Click);
            // 
            // SetLinearSpeedBtn
            // 
            this.SetLinearSpeedBtn.BackColor = System.Drawing.Color.Green;
            this.SetLinearSpeedBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetLinearSpeedBtn.ForeColor = System.Drawing.Color.White;
            this.SetLinearSpeedBtn.Location = new System.Drawing.Point(15, 43);
            this.SetLinearSpeedBtn.Name = "SetLinearSpeedBtn";
            this.SetLinearSpeedBtn.Size = new System.Drawing.Size(286, 130);
            this.SetLinearSpeedBtn.TabIndex = 109;
            this.SetLinearSpeedBtn.Text = "Set Linear Speed";
            this.SetLinearSpeedBtn.UseVisualStyleBackColor = false;
            this.SetLinearSpeedBtn.Click += new System.EventHandler(this.SetLinearSpeedBtn_Click);
            // 
            // GrindingMoveSetupGrp
            // 
            this.GrindingMoveSetupGrp.Controls.Add(this.SetForceModeDampingBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetForceModeGainScalingBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetGrindJogAccelBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetGrindJogSpeedBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetPointFrequencyBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetGrindDefaultsBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetGrindAccelBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetTrialSpeedBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetMaxGrindBlendRadiusBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetMaxWaitBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetForceDwellBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetTouchSpeedBtn);
            this.GrindingMoveSetupGrp.Controls.Add(this.SetTouchRetractBtn);
            this.GrindingMoveSetupGrp.Location = new System.Drawing.Point(3, 820);
            this.GrindingMoveSetupGrp.Name = "GrindingMoveSetupGrp";
            this.GrindingMoveSetupGrp.Size = new System.Drawing.Size(2106, 299);
            this.GrindingMoveSetupGrp.TabIndex = 117;
            this.GrindingMoveSetupGrp.TabStop = false;
            this.GrindingMoveSetupGrp.Text = "Grinding Motion Parameters";
            // 
            // SetForceModeDampingBtn
            // 
            this.SetForceModeDampingBtn.BackColor = System.Drawing.Color.Green;
            this.SetForceModeDampingBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetForceModeDampingBtn.ForeColor = System.Drawing.Color.White;
            this.SetForceModeDampingBtn.Location = new System.Drawing.Point(535, 169);
            this.SetForceModeDampingBtn.Name = "SetForceModeDampingBtn";
            this.SetForceModeDampingBtn.Size = new System.Drawing.Size(243, 124);
            this.SetForceModeDampingBtn.TabIndex = 126;
            this.SetForceModeDampingBtn.Text = "Set Force Mode Damping";
            this.SetForceModeDampingBtn.UseVisualStyleBackColor = false;
            this.SetForceModeDampingBtn.Click += new System.EventHandler(this.SetForceModeDampingBtn_Click);
            // 
            // SetForceModeGainScalingBtn
            // 
            this.SetForceModeGainScalingBtn.BackColor = System.Drawing.Color.Green;
            this.SetForceModeGainScalingBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetForceModeGainScalingBtn.ForeColor = System.Drawing.Color.White;
            this.SetForceModeGainScalingBtn.Location = new System.Drawing.Point(795, 169);
            this.SetForceModeGainScalingBtn.Name = "SetForceModeGainScalingBtn";
            this.SetForceModeGainScalingBtn.Size = new System.Drawing.Size(243, 124);
            this.SetForceModeGainScalingBtn.TabIndex = 125;
            this.SetForceModeGainScalingBtn.Text = "Set Force Mode Gain Scaling";
            this.SetForceModeGainScalingBtn.UseVisualStyleBackColor = false;
            this.SetForceModeGainScalingBtn.Click += new System.EventHandler(this.SetForceModeGainScalingBtn_Click);
            // 
            // SetGrindJogAccelBtn
            // 
            this.SetGrindJogAccelBtn.BackColor = System.Drawing.Color.Green;
            this.SetGrindJogAccelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetGrindJogAccelBtn.ForeColor = System.Drawing.Color.White;
            this.SetGrindJogAccelBtn.Location = new System.Drawing.Point(275, 169);
            this.SetGrindJogAccelBtn.Name = "SetGrindJogAccelBtn";
            this.SetGrindJogAccelBtn.Size = new System.Drawing.Size(243, 124);
            this.SetGrindJogAccelBtn.TabIndex = 124;
            this.SetGrindJogAccelBtn.Text = "Set Jog Accel";
            this.SetGrindJogAccelBtn.UseVisualStyleBackColor = false;
            this.SetGrindJogAccelBtn.Click += new System.EventHandler(this.SetGrindJogAccel_Click);
            // 
            // SetGrindJogSpeedBtn
            // 
            this.SetGrindJogSpeedBtn.BackColor = System.Drawing.Color.Green;
            this.SetGrindJogSpeedBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetGrindJogSpeedBtn.ForeColor = System.Drawing.Color.White;
            this.SetGrindJogSpeedBtn.Location = new System.Drawing.Point(15, 169);
            this.SetGrindJogSpeedBtn.Name = "SetGrindJogSpeedBtn";
            this.SetGrindJogSpeedBtn.Size = new System.Drawing.Size(243, 124);
            this.SetGrindJogSpeedBtn.TabIndex = 123;
            this.SetGrindJogSpeedBtn.Text = "Set Jog Speed";
            this.SetGrindJogSpeedBtn.UseVisualStyleBackColor = false;
            this.SetGrindJogSpeedBtn.Click += new System.EventHandler(this.SetGrindJogSpeedBtn_Click);
            // 
            // SetPointFrequencyBtn
            // 
            this.SetPointFrequencyBtn.BackColor = System.Drawing.Color.Green;
            this.SetPointFrequencyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetPointFrequencyBtn.ForeColor = System.Drawing.Color.White;
            this.SetPointFrequencyBtn.Location = new System.Drawing.Point(1575, 169);
            this.SetPointFrequencyBtn.Name = "SetPointFrequencyBtn";
            this.SetPointFrequencyBtn.Size = new System.Drawing.Size(243, 124);
            this.SetPointFrequencyBtn.TabIndex = 122;
            this.SetPointFrequencyBtn.Text = "Set Point Frequency";
            this.SetPointFrequencyBtn.UseVisualStyleBackColor = false;
            this.SetPointFrequencyBtn.Click += new System.EventHandler(this.SetPointFrequencyBtn_Click);
            // 
            // SetGrindDefaultsBtn
            // 
            this.SetGrindDefaultsBtn.BackColor = System.Drawing.Color.Green;
            this.SetGrindDefaultsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetGrindDefaultsBtn.ForeColor = System.Drawing.Color.White;
            this.SetGrindDefaultsBtn.Location = new System.Drawing.Point(1935, 39);
            this.SetGrindDefaultsBtn.Name = "SetGrindDefaultsBtn";
            this.SetGrindDefaultsBtn.Size = new System.Drawing.Size(171, 130);
            this.SetGrindDefaultsBtn.TabIndex = 121;
            this.SetGrindDefaultsBtn.Text = "Restore Defaults";
            this.SetGrindDefaultsBtn.UseVisualStyleBackColor = false;
            this.SetGrindDefaultsBtn.Click += new System.EventHandler(this.SetGrindDefaultsBtn_Click);
            // 
            // SetGrindAccelBtn
            // 
            this.SetGrindAccelBtn.BackColor = System.Drawing.Color.Green;
            this.SetGrindAccelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetGrindAccelBtn.ForeColor = System.Drawing.Color.White;
            this.SetGrindAccelBtn.Location = new System.Drawing.Point(275, 39);
            this.SetGrindAccelBtn.Name = "SetGrindAccelBtn";
            this.SetGrindAccelBtn.Size = new System.Drawing.Size(243, 130);
            this.SetGrindAccelBtn.TabIndex = 120;
            this.SetGrindAccelBtn.Text = "Set Acceleration";
            this.SetGrindAccelBtn.UseVisualStyleBackColor = false;
            this.SetGrindAccelBtn.Click += new System.EventHandler(this.SetGrindAccelBtn_Click);
            // 
            // SetTrialSpeedBtn
            // 
            this.SetTrialSpeedBtn.BackColor = System.Drawing.Color.Green;
            this.SetTrialSpeedBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetTrialSpeedBtn.ForeColor = System.Drawing.Color.White;
            this.SetTrialSpeedBtn.Location = new System.Drawing.Point(15, 39);
            this.SetTrialSpeedBtn.Name = "SetTrialSpeedBtn";
            this.SetTrialSpeedBtn.Size = new System.Drawing.Size(243, 130);
            this.SetTrialSpeedBtn.TabIndex = 119;
            this.SetTrialSpeedBtn.Text = "Set Trial Speed";
            this.SetTrialSpeedBtn.UseVisualStyleBackColor = false;
            this.SetTrialSpeedBtn.Click += new System.EventHandler(this.SetTrialSpeedBtn_Click);
            // 
            // SetMaxGrindBlendRadiusBtn
            // 
            this.SetMaxGrindBlendRadiusBtn.BackColor = System.Drawing.Color.Green;
            this.SetMaxGrindBlendRadiusBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetMaxGrindBlendRadiusBtn.ForeColor = System.Drawing.Color.White;
            this.SetMaxGrindBlendRadiusBtn.Location = new System.Drawing.Point(535, 39);
            this.SetMaxGrindBlendRadiusBtn.Name = "SetMaxGrindBlendRadiusBtn";
            this.SetMaxGrindBlendRadiusBtn.Size = new System.Drawing.Size(243, 130);
            this.SetMaxGrindBlendRadiusBtn.TabIndex = 118;
            this.SetMaxGrindBlendRadiusBtn.Text = "Set Max Blend Radius";
            this.SetMaxGrindBlendRadiusBtn.UseVisualStyleBackColor = false;
            this.SetMaxGrindBlendRadiusBtn.Click += new System.EventHandler(this.SetMaxGrindBlendRadiusBtn_Click);
            // 
            // SetMaxWaitBtn
            // 
            this.SetMaxWaitBtn.BackColor = System.Drawing.Color.Green;
            this.SetMaxWaitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetMaxWaitBtn.ForeColor = System.Drawing.Color.White;
            this.SetMaxWaitBtn.Location = new System.Drawing.Point(1575, 39);
            this.SetMaxWaitBtn.Name = "SetMaxWaitBtn";
            this.SetMaxWaitBtn.Size = new System.Drawing.Size(243, 130);
            this.SetMaxWaitBtn.TabIndex = 117;
            this.SetMaxWaitBtn.Text = "Set Max Wait";
            this.SetMaxWaitBtn.UseVisualStyleBackColor = false;
            this.SetMaxWaitBtn.Click += new System.EventHandler(this.SetMaxWaitBtn_Click);
            // 
            // SetForceDwellBtn
            // 
            this.SetForceDwellBtn.BackColor = System.Drawing.Color.Green;
            this.SetForceDwellBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetForceDwellBtn.ForeColor = System.Drawing.Color.White;
            this.SetForceDwellBtn.Location = new System.Drawing.Point(1315, 39);
            this.SetForceDwellBtn.Name = "SetForceDwellBtn";
            this.SetForceDwellBtn.Size = new System.Drawing.Size(243, 130);
            this.SetForceDwellBtn.TabIndex = 116;
            this.SetForceDwellBtn.Text = "Set Force Dwell";
            this.SetForceDwellBtn.UseVisualStyleBackColor = false;
            this.SetForceDwellBtn.Click += new System.EventHandler(this.SetForceDwellBtn_Click);
            // 
            // SetTouchSpeedBtn
            // 
            this.SetTouchSpeedBtn.BackColor = System.Drawing.Color.Green;
            this.SetTouchSpeedBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetTouchSpeedBtn.ForeColor = System.Drawing.Color.White;
            this.SetTouchSpeedBtn.Location = new System.Drawing.Point(795, 39);
            this.SetTouchSpeedBtn.Name = "SetTouchSpeedBtn";
            this.SetTouchSpeedBtn.Size = new System.Drawing.Size(243, 130);
            this.SetTouchSpeedBtn.TabIndex = 115;
            this.SetTouchSpeedBtn.Text = "Set Touch Speed";
            this.SetTouchSpeedBtn.UseVisualStyleBackColor = false;
            this.SetTouchSpeedBtn.Click += new System.EventHandler(this.SetTouchSpeedBtn_Click);
            // 
            // SetTouchRetractBtn
            // 
            this.SetTouchRetractBtn.BackColor = System.Drawing.Color.Green;
            this.SetTouchRetractBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetTouchRetractBtn.ForeColor = System.Drawing.Color.White;
            this.SetTouchRetractBtn.Location = new System.Drawing.Point(1055, 39);
            this.SetTouchRetractBtn.Name = "SetTouchRetractBtn";
            this.SetTouchRetractBtn.Size = new System.Drawing.Size(243, 130);
            this.SetTouchRetractBtn.TabIndex = 114;
            this.SetTouchRetractBtn.Text = "Set Touch Retract";
            this.SetTouchRetractBtn.UseVisualStyleBackColor = false;
            this.SetTouchRetractBtn.Click += new System.EventHandler(this.SetTouchRetractBtn_Click);
            // 
            // ToolSetupGrp
            // 
            this.ToolSetupGrp.Controls.Add(this.CoolantOffBtn);
            this.ToolSetupGrp.Controls.Add(this.CoolantTestBtn);
            this.ToolSetupGrp.Controls.Add(this.ToolOffBtn);
            this.ToolSetupGrp.Controls.Add(this.ToolTestBtn);
            this.ToolSetupGrp.Controls.Add(this.DoorClosedInputLbl);
            this.ToolSetupGrp.Controls.Add(this.FootswitchPressedInputLbl);
            this.ToolSetupGrp.Controls.Add(this.FootswitchPressedInputTxt);
            this.ToolSetupGrp.Controls.Add(this.SetFootswitchPressedInputBtn);
            this.ToolSetupGrp.Controls.Add(this.JointMoveMountBtn);
            this.ToolSetupGrp.Controls.Add(this.JointMoveHomeBtn);
            this.ToolSetupGrp.Controls.Add(this.DoorClosedInputTxt);
            this.ToolSetupGrp.Controls.Add(this.SetDoorClosedInputBtn);
            this.ToolSetupGrp.Controls.Add(this.SelectToolBtn);
            this.ToolSetupGrp.Controls.Add(this.ToolsGrd);
            this.ToolSetupGrp.Controls.Add(this.LoadToolsBtn);
            this.ToolSetupGrp.Controls.Add(this.SaveToolsBtn);
            this.ToolSetupGrp.Controls.Add(this.ClearToolsBtn);
            this.ToolSetupGrp.Location = new System.Drawing.Point(3, 3);
            this.ToolSetupGrp.Name = "ToolSetupGrp";
            this.ToolSetupGrp.Size = new System.Drawing.Size(2112, 475);
            this.ToolSetupGrp.TabIndex = 101;
            this.ToolSetupGrp.TabStop = false;
            this.ToolSetupGrp.Text = "Tools";
            // 
            // CoolantOffBtn
            // 
            this.CoolantOffBtn.BackColor = System.Drawing.Color.Green;
            this.CoolantOffBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CoolantOffBtn.ForeColor = System.Drawing.Color.White;
            this.CoolantOffBtn.Location = new System.Drawing.Point(784, 367);
            this.CoolantOffBtn.Name = "CoolantOffBtn";
            this.CoolantOffBtn.Size = new System.Drawing.Size(82, 96);
            this.CoolantOffBtn.TabIndex = 128;
            this.CoolantOffBtn.Text = "Cool Off";
            this.CoolantOffBtn.UseVisualStyleBackColor = false;
            this.CoolantOffBtn.Click += new System.EventHandler(this.CoolantOffBtn_Click);
            // 
            // CoolantTestBtn
            // 
            this.CoolantTestBtn.BackColor = System.Drawing.Color.Green;
            this.CoolantTestBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CoolantTestBtn.ForeColor = System.Drawing.Color.White;
            this.CoolantTestBtn.Location = new System.Drawing.Point(696, 367);
            this.CoolantTestBtn.Name = "CoolantTestBtn";
            this.CoolantTestBtn.Size = new System.Drawing.Size(82, 96);
            this.CoolantTestBtn.TabIndex = 127;
            this.CoolantTestBtn.Text = "Cool Test";
            this.CoolantTestBtn.UseVisualStyleBackColor = false;
            this.CoolantTestBtn.Click += new System.EventHandler(this.CoolantTestBtn_Click);
            // 
            // ToolOffBtn
            // 
            this.ToolOffBtn.BackColor = System.Drawing.Color.Green;
            this.ToolOffBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolOffBtn.ForeColor = System.Drawing.Color.White;
            this.ToolOffBtn.Location = new System.Drawing.Point(597, 367);
            this.ToolOffBtn.Name = "ToolOffBtn";
            this.ToolOffBtn.Size = new System.Drawing.Size(82, 96);
            this.ToolOffBtn.TabIndex = 126;
            this.ToolOffBtn.Text = "Tool Off";
            this.ToolOffBtn.UseVisualStyleBackColor = false;
            this.ToolOffBtn.Click += new System.EventHandler(this.ToolOffBtn_Click);
            // 
            // ToolTestBtn
            // 
            this.ToolTestBtn.BackColor = System.Drawing.Color.Green;
            this.ToolTestBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolTestBtn.ForeColor = System.Drawing.Color.White;
            this.ToolTestBtn.Location = new System.Drawing.Point(509, 367);
            this.ToolTestBtn.Name = "ToolTestBtn";
            this.ToolTestBtn.Size = new System.Drawing.Size(82, 96);
            this.ToolTestBtn.TabIndex = 125;
            this.ToolTestBtn.Text = "Tool Test";
            this.ToolTestBtn.UseVisualStyleBackColor = false;
            this.ToolTestBtn.Click += new System.EventHandler(this.ToolTestBtn_Click);
            // 
            // DoorClosedInputLbl
            // 
            this.DoorClosedInputLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DoorClosedInputLbl.Location = new System.Drawing.Point(1987, 414);
            this.DoorClosedInputLbl.Name = "DoorClosedInputLbl";
            this.DoorClosedInputLbl.Size = new System.Drawing.Size(97, 49);
            this.DoorClosedInputLbl.TabIndex = 124;
            this.DoorClosedInputLbl.Text = "1,1";
            // 
            // FootswitchPressedInputLbl
            // 
            this.FootswitchPressedInputLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FootswitchPressedInputLbl.Location = new System.Drawing.Point(1650, 414);
            this.FootswitchPressedInputLbl.Name = "FootswitchPressedInputLbl";
            this.FootswitchPressedInputLbl.Size = new System.Drawing.Size(97, 49);
            this.FootswitchPressedInputLbl.TabIndex = 123;
            this.FootswitchPressedInputLbl.Text = "7,1";
            // 
            // FootswitchPressedInputTxt
            // 
            this.FootswitchPressedInputTxt.Location = new System.Drawing.Point(1650, 367);
            this.FootswitchPressedInputTxt.Name = "FootswitchPressedInputTxt";
            this.FootswitchPressedInputTxt.Size = new System.Drawing.Size(97, 44);
            this.FootswitchPressedInputTxt.TabIndex = 122;
            this.FootswitchPressedInputTxt.Text = "7,1";
            // 
            // SetFootswitchPressedInputBtn
            // 
            this.SetFootswitchPressedInputBtn.BackColor = System.Drawing.Color.Green;
            this.SetFootswitchPressedInputBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetFootswitchPressedInputBtn.ForeColor = System.Drawing.Color.White;
            this.SetFootswitchPressedInputBtn.Location = new System.Drawing.Point(1411, 367);
            this.SetFootswitchPressedInputBtn.Name = "SetFootswitchPressedInputBtn";
            this.SetFootswitchPressedInputBtn.Size = new System.Drawing.Size(233, 96);
            this.SetFootswitchPressedInputBtn.TabIndex = 121;
            this.SetFootswitchPressedInputBtn.Text = "Set Footswitch Pressed Input";
            this.SetFootswitchPressedInputBtn.UseVisualStyleBackColor = false;
            this.SetFootswitchPressedInputBtn.Click += new System.EventHandler(this.SetFootswitchInputBtn_Click);
            // 
            // JointMoveMountBtn
            // 
            this.JointMoveMountBtn.BackColor = System.Drawing.Color.Green;
            this.JointMoveMountBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JointMoveMountBtn.ForeColor = System.Drawing.Color.White;
            this.JointMoveMountBtn.Location = new System.Drawing.Point(167, 367);
            this.JointMoveMountBtn.Name = "JointMoveMountBtn";
            this.JointMoveMountBtn.Size = new System.Drawing.Size(155, 96);
            this.JointMoveMountBtn.TabIndex = 120;
            this.JointMoveMountBtn.Text = "Joint Move to Mount";
            this.JointMoveMountBtn.UseVisualStyleBackColor = false;
            this.JointMoveMountBtn.Click += new System.EventHandler(this.JointMoveMountBtn_Click);
            // 
            // JointMoveHomeBtn
            // 
            this.JointMoveHomeBtn.BackColor = System.Drawing.Color.Green;
            this.JointMoveHomeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JointMoveHomeBtn.ForeColor = System.Drawing.Color.White;
            this.JointMoveHomeBtn.Location = new System.Drawing.Point(328, 367);
            this.JointMoveHomeBtn.Name = "JointMoveHomeBtn";
            this.JointMoveHomeBtn.Size = new System.Drawing.Size(155, 96);
            this.JointMoveHomeBtn.TabIndex = 119;
            this.JointMoveHomeBtn.Text = "Joint Move to Home";
            this.JointMoveHomeBtn.UseVisualStyleBackColor = false;
            this.JointMoveHomeBtn.Click += new System.EventHandler(this.JointMoveHomeBtn_Click);
            // 
            // DoorClosedInputTxt
            // 
            this.DoorClosedInputTxt.Location = new System.Drawing.Point(1987, 367);
            this.DoorClosedInputTxt.Name = "DoorClosedInputTxt";
            this.DoorClosedInputTxt.Size = new System.Drawing.Size(97, 44);
            this.DoorClosedInputTxt.TabIndex = 118;
            this.DoorClosedInputTxt.Text = "1,1";
            // 
            // SetDoorClosedInputBtn
            // 
            this.SetDoorClosedInputBtn.BackColor = System.Drawing.Color.Green;
            this.SetDoorClosedInputBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetDoorClosedInputBtn.ForeColor = System.Drawing.Color.White;
            this.SetDoorClosedInputBtn.Location = new System.Drawing.Point(1776, 367);
            this.SetDoorClosedInputBtn.Name = "SetDoorClosedInputBtn";
            this.SetDoorClosedInputBtn.Size = new System.Drawing.Size(205, 96);
            this.SetDoorClosedInputBtn.TabIndex = 96;
            this.SetDoorClosedInputBtn.Text = "Set Door Closed Input";
            this.SetDoorClosedInputBtn.UseVisualStyleBackColor = false;
            this.SetDoorClosedInputBtn.Click += new System.EventHandler(this.SetDoorClosedInputBtn_Click);
            // 
            // SelectToolBtn
            // 
            this.SelectToolBtn.BackColor = System.Drawing.Color.Green;
            this.SelectToolBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectToolBtn.ForeColor = System.Drawing.Color.White;
            this.SelectToolBtn.Location = new System.Drawing.Point(6, 367);
            this.SelectToolBtn.Name = "SelectToolBtn";
            this.SelectToolBtn.Size = new System.Drawing.Size(155, 96);
            this.SelectToolBtn.TabIndex = 95;
            this.SelectToolBtn.Text = "Select";
            this.SelectToolBtn.UseVisualStyleBackColor = false;
            this.SelectToolBtn.Click += new System.EventHandler(this.SelectToolBtn_Click);
            // 
            // ToolsGrd
            // 
            this.ToolsGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.ToolsGrd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ToolsGrd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ToolsGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ToolsGrd.DefaultCellStyle = dataGridViewCellStyle4;
            this.ToolsGrd.Location = new System.Drawing.Point(6, 43);
            this.ToolsGrd.Name = "ToolsGrd";
            this.ToolsGrd.RowTemplate.Height = 34;
            this.ToolsGrd.Size = new System.Drawing.Size(2100, 308);
            this.ToolsGrd.TabIndex = 85;
            // 
            // LoadToolsBtn
            // 
            this.LoadToolsBtn.BackColor = System.Drawing.Color.Green;
            this.LoadToolsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadToolsBtn.ForeColor = System.Drawing.Color.White;
            this.LoadToolsBtn.Location = new System.Drawing.Point(894, 367);
            this.LoadToolsBtn.Name = "LoadToolsBtn";
            this.LoadToolsBtn.Size = new System.Drawing.Size(155, 96);
            this.LoadToolsBtn.TabIndex = 94;
            this.LoadToolsBtn.Text = "Reload";
            this.LoadToolsBtn.UseVisualStyleBackColor = false;
            this.LoadToolsBtn.Click += new System.EventHandler(this.LoadToolsBtn_Click);
            // 
            // SaveToolsBtn
            // 
            this.SaveToolsBtn.BackColor = System.Drawing.Color.Green;
            this.SaveToolsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveToolsBtn.ForeColor = System.Drawing.Color.White;
            this.SaveToolsBtn.Location = new System.Drawing.Point(1055, 367);
            this.SaveToolsBtn.Name = "SaveToolsBtn";
            this.SaveToolsBtn.Size = new System.Drawing.Size(155, 96);
            this.SaveToolsBtn.TabIndex = 93;
            this.SaveToolsBtn.Text = "Save";
            this.SaveToolsBtn.UseVisualStyleBackColor = false;
            this.SaveToolsBtn.Click += new System.EventHandler(this.SaveToolsBtn_Click);
            // 
            // ClearToolsBtn
            // 
            this.ClearToolsBtn.BackColor = System.Drawing.Color.Green;
            this.ClearToolsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearToolsBtn.ForeColor = System.Drawing.Color.White;
            this.ClearToolsBtn.Location = new System.Drawing.Point(1218, 367);
            this.ClearToolsBtn.Name = "ClearToolsBtn";
            this.ClearToolsBtn.Size = new System.Drawing.Size(155, 96);
            this.ClearToolsBtn.TabIndex = 92;
            this.ClearToolsBtn.Text = "Clear";
            this.ClearToolsBtn.UseVisualStyleBackColor = false;
            this.ClearToolsBtn.Click += new System.EventHandler(this.ClearToolsBtn_Click);
            // 
            // GeneralConfigGrp
            // 
            this.GeneralConfigGrp.Controls.Add(this.SaveConfigBtn);
            this.GeneralConfigGrp.Controls.Add(this.AllowRunningOfflineChk);
            this.GeneralConfigGrp.Controls.Add(this.label4);
            this.GeneralConfigGrp.Controls.Add(this.RobotProgramTxt);
            this.GeneralConfigGrp.Controls.Add(this.LoadConfigBtn);
            this.GeneralConfigGrp.Controls.Add(this.DefaultConfigBtn);
            this.GeneralConfigGrp.Controls.Add(this.ServerIpTxt);
            this.GeneralConfigGrp.Controls.Add(this.label2);
            this.GeneralConfigGrp.Controls.Add(this.RobotIpTxt);
            this.GeneralConfigGrp.Controls.Add(this.label3);
            this.GeneralConfigGrp.Controls.Add(this.AutoGrindRootLbl);
            this.GeneralConfigGrp.Controls.Add(this.ChangeRootDirectoryBtn);
            this.GeneralConfigGrp.Controls.Add(this.label1);
            this.GeneralConfigGrp.Location = new System.Drawing.Point(3, 492);
            this.GeneralConfigGrp.Margin = new System.Windows.Forms.Padding(2);
            this.GeneralConfigGrp.Name = "GeneralConfigGrp";
            this.GeneralConfigGrp.Padding = new System.Windows.Forms.Padding(2);
            this.GeneralConfigGrp.Size = new System.Drawing.Size(1180, 320);
            this.GeneralConfigGrp.TabIndex = 96;
            this.GeneralConfigGrp.TabStop = false;
            this.GeneralConfigGrp.Text = "General Configuration";
            // 
            // SaveConfigBtn
            // 
            this.SaveConfigBtn.BackColor = System.Drawing.Color.Green;
            this.SaveConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveConfigBtn.ForeColor = System.Drawing.Color.White;
            this.SaveConfigBtn.Location = new System.Drawing.Point(852, 223);
            this.SaveConfigBtn.Name = "SaveConfigBtn";
            this.SaveConfigBtn.Size = new System.Drawing.Size(157, 86);
            this.SaveConfigBtn.TabIndex = 100;
            this.SaveConfigBtn.Text = "Save";
            this.SaveConfigBtn.UseVisualStyleBackColor = false;
            this.SaveConfigBtn.Click += new System.EventHandler(this.SaveConfigBtn_Click);
            // 
            // AllowRunningOfflineChk
            // 
            this.AllowRunningOfflineChk.Appearance = System.Windows.Forms.Appearance.Button;
            this.AllowRunningOfflineChk.AutoSize = true;
            this.AllowRunningOfflineChk.BackColor = System.Drawing.Color.Gray;
            this.AllowRunningOfflineChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllowRunningOfflineChk.ForeColor = System.Drawing.Color.White;
            this.AllowRunningOfflineChk.Location = new System.Drawing.Point(847, 173);
            this.AllowRunningOfflineChk.Name = "AllowRunningOfflineChk";
            this.AllowRunningOfflineChk.Size = new System.Drawing.Size(325, 43);
            this.AllowRunningOfflineChk.TabIndex = 89;
            this.AllowRunningOfflineChk.Text = "Allow Running Offline";
            this.AllowRunningOfflineChk.UseMnemonic = false;
            this.AllowRunningOfflineChk.UseVisualStyleBackColor = false;
            this.AllowRunningOfflineChk.CheckedChanged += new System.EventHandler(this.AllowRunningOfflineChk_CheckedChanged);
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
            this.RobotProgramTxt.Size = new System.Drawing.Size(606, 44);
            this.RobotProgramTxt.TabIndex = 87;
            // 
            // LoadConfigBtn
            // 
            this.LoadConfigBtn.BackColor = System.Drawing.Color.Green;
            this.LoadConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadConfigBtn.ForeColor = System.Drawing.Color.White;
            this.LoadConfigBtn.Location = new System.Drawing.Point(679, 223);
            this.LoadConfigBtn.Name = "LoadConfigBtn";
            this.LoadConfigBtn.Size = new System.Drawing.Size(157, 86);
            this.LoadConfigBtn.TabIndex = 98;
            this.LoadConfigBtn.Text = "Reload";
            this.LoadConfigBtn.UseVisualStyleBackColor = false;
            this.LoadConfigBtn.Click += new System.EventHandler(this.LoadConfigBtn_Click);
            // 
            // DefaultConfigBtn
            // 
            this.DefaultConfigBtn.BackColor = System.Drawing.Color.Green;
            this.DefaultConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DefaultConfigBtn.ForeColor = System.Drawing.Color.White;
            this.DefaultConfigBtn.Location = new System.Drawing.Point(1015, 223);
            this.DefaultConfigBtn.Name = "DefaultConfigBtn";
            this.DefaultConfigBtn.Size = new System.Drawing.Size(157, 86);
            this.DefaultConfigBtn.TabIndex = 99;
            this.DefaultConfigBtn.Text = "Restore Defaults";
            this.DefaultConfigBtn.UseVisualStyleBackColor = false;
            this.DefaultConfigBtn.Click += new System.EventHandler(this.DefaultConfigBtn_Click);
            // 
            // ServerIpTxt
            // 
            this.ServerIpTxt.Location = new System.Drawing.Point(397, 173);
            this.ServerIpTxt.Name = "ServerIpTxt";
            this.ServerIpTxt.Size = new System.Drawing.Size(267, 44);
            this.ServerIpTxt.TabIndex = 79;
            this.ServerIpTxt.Text = "192.168.0.253";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(173, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 42);
            this.label2.TabIndex = 78;
            this.label2.Text = "UR Robot IP Address";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RobotIpTxt
            // 
            this.RobotIpTxt.Location = new System.Drawing.Point(397, 223);
            this.RobotIpTxt.Name = "RobotIpTxt";
            this.RobotIpTxt.Size = new System.Drawing.Size(267, 44);
            this.RobotIpTxt.TabIndex = 72;
            this.RobotIpTxt.Text = "192.168.0.2";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(73, 174);
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
            this.AutoGrindRootLbl.Size = new System.Drawing.Size(606, 46);
            this.AutoGrindRootLbl.TabIndex = 69;
            this.AutoGrindRootLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChangeRootDirectoryBtn
            // 
            this.ChangeRootDirectoryBtn.Location = new System.Drawing.Point(1009, 57);
            this.ChangeRootDirectoryBtn.Name = "ChangeRootDirectoryBtn";
            this.ChangeRootDirectoryBtn.Size = new System.Drawing.Size(60, 46);
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
            // LogPage
            // 
            this.LogPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LogPage.Controls.Add(this.ClearAllLogRtbBtn);
            this.LogPage.Controls.Add(this.groupBox7);
            this.LogPage.Controls.Add(this.groupBox6);
            this.LogPage.Controls.Add(this.AboutBtn);
            this.LogPage.Controls.Add(this.groupBox3);
            this.LogPage.Controls.Add(this.groupBox5);
            this.LogPage.Controls.Add(this.groupBox10);
            this.LogPage.Controls.Add(this.groupBox4);
            this.LogPage.Location = new System.Drawing.Point(4, 100);
            this.LogPage.Name = "LogPage";
            this.LogPage.Size = new System.Drawing.Size(2132, 1168);
            this.LogPage.TabIndex = 5;
            this.LogPage.Text = "Log";
            this.LogPage.UseVisualStyleBackColor = true;
            // 
            // ClearAllLogRtbBtn
            // 
            this.ClearAllLogRtbBtn.BackColor = System.Drawing.Color.Green;
            this.ClearAllLogRtbBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearAllLogRtbBtn.ForeColor = System.Drawing.Color.White;
            this.ClearAllLogRtbBtn.Location = new System.Drawing.Point(1775, 898);
            this.ClearAllLogRtbBtn.Name = "ClearAllLogRtbBtn";
            this.ClearAllLogRtbBtn.Size = new System.Drawing.Size(161, 116);
            this.ClearAllLogRtbBtn.TabIndex = 92;
            this.ClearAllLogRtbBtn.Text = "Clear All";
            this.ClearAllLogRtbBtn.UseVisualStyleBackColor = false;
            this.ClearAllLogRtbBtn.Click += new System.EventHandler(this.ClearAllLogRtbBtn_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.UrDashboardLogRTB);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(1058, 577);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1067, 309);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Robot Dashboard Server";
            // 
            // AboutBtn
            // 
            this.AboutBtn.BackColor = System.Drawing.Color.Green;
            this.AboutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutBtn.ForeColor = System.Drawing.Color.White;
            this.AboutBtn.Location = new System.Drawing.Point(1978, 898);
            this.AboutBtn.Name = "AboutBtn";
            this.AboutBtn.Size = new System.Drawing.Size(142, 116);
            this.AboutBtn.TabIndex = 81;
            this.AboutBtn.Text = "About";
            this.AboutBtn.UseVisualStyleBackColor = false;
            this.AboutBtn.Click += new System.EventHandler(this.AboutBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LogLevelCombo);
            this.groupBox3.Location = new System.Drawing.Point(1775, 1040);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(331, 110);
            this.groupBox3.TabIndex = 91;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log Level";
            // 
            // LogLevelCombo
            // 
            this.LogLevelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LogLevelCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogLevelCombo.FormattingEnabled = true;
            this.LogLevelCombo.Items.AddRange(new object[] {
            "Error",
            "Warn",
            "Info",
            "Debug",
            "Trace"});
            this.LogLevelCombo.Location = new System.Drawing.Point(23, 43);
            this.LogLevelCombo.Name = "LogLevelCombo";
            this.LogLevelCombo.Size = new System.Drawing.Size(291, 50);
            this.LogLevelCombo.TabIndex = 80;
            this.LogLevelCombo.SelectedIndexChanged += new System.EventHandler(this.DebugLevelCombo_SelectedIndexChanged);
            // 
            // JogRunBtn
            // 
            this.JogRunBtn.BackColor = System.Drawing.Color.Green;
            this.JogRunBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JogRunBtn.ForeColor = System.Drawing.Color.White;
            this.JogRunBtn.Location = new System.Drawing.Point(1572, 9);
            this.JogRunBtn.Margin = new System.Windows.Forms.Padding(2);
            this.JogRunBtn.Name = "JogRunBtn";
            this.JogRunBtn.Size = new System.Drawing.Size(333, 97);
            this.JogRunBtn.TabIndex = 130;
            this.JogRunBtn.Text = "Jog Robot";
            this.JogRunBtn.UseVisualStyleBackColor = false;
            this.JogRunBtn.Click += new System.EventHandler(this.JogRunBtn_Click);
            // 
            // DiameterLbl
            // 
            this.DiameterLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DiameterLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiameterLbl.Location = new System.Drawing.Point(1764, 1291);
            this.DiameterLbl.Name = "DiameterLbl";
            this.DiameterLbl.Size = new System.Drawing.Size(205, 52);
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
            this.PartGeometryBox.Location = new System.Drawing.Point(1453, 1288);
            this.PartGeometryBox.Name = "PartGeometryBox";
            this.PartGeometryBox.Size = new System.Drawing.Size(305, 63);
            this.PartGeometryBox.TabIndex = 110;
            this.PartGeometryBox.SelectedIndexChanged += new System.EventHandler(this.PartGeometryBox_SelectedIndexChanged);
            // 
            // DoorClosedLbl
            // 
            this.DoorClosedLbl.BackColor = System.Drawing.Color.Gray;
            this.DoorClosedLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DoorClosedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoorClosedLbl.ForeColor = System.Drawing.Color.White;
            this.DoorClosedLbl.Location = new System.Drawing.Point(1142, 1359);
            this.DoorClosedLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DoorClosedLbl.Name = "DoorClosedLbl";
            this.DoorClosedLbl.Size = new System.Drawing.Size(305, 69);
            this.DoorClosedLbl.TabIndex = 118;
            this.DoorClosedLbl.Text = "Door Closed?";
            this.DoorClosedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionLbl
            // 
            this.VersionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLbl.Location = new System.Drawing.Point(1765, 1367);
            this.VersionLbl.Name = "VersionLbl";
            this.VersionLbl.Size = new System.Drawing.Size(384, 29);
            this.VersionLbl.TabIndex = 149;
            this.VersionLbl.Text = "VersionLbl";
            this.VersionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FootswitchPressedLbl
            // 
            this.FootswitchPressedLbl.BackColor = System.Drawing.Color.Gray;
            this.FootswitchPressedLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FootswitchPressedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FootswitchPressedLbl.ForeColor = System.Drawing.Color.White;
            this.FootswitchPressedLbl.Location = new System.Drawing.Point(1453, 1360);
            this.FootswitchPressedLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FootswitchPressedLbl.Name = "FootswitchPressedLbl";
            this.FootswitchPressedLbl.Size = new System.Drawing.Size(305, 69);
            this.FootswitchPressedLbl.TabIndex = 150;
            this.FootswitchPressedLbl.Text = "Footswitch Pressed?";
            this.FootswitchPressedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Time2Lbl
            // 
            this.Time2Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Time2Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time2Lbl.Location = new System.Drawing.Point(1765, 1397);
            this.Time2Lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Time2Lbl.Name = "Time2Lbl";
            this.Time2Lbl.Size = new System.Drawing.Size(384, 34);
            this.Time2Lbl.TabIndex = 151;
            this.Time2Lbl.Text = "Time";
            this.Time2Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2160, 1440);
            this.ControlBox = false;
            this.Controls.Add(this.Time2Lbl);
            this.Controls.Add(this.FootswitchPressedLbl);
            this.Controls.Add(this.JogRunBtn);
            this.Controls.Add(this.VersionLbl);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.DoorClosedLbl);
            this.Controls.Add(this.SaveAsRecipeBtn);
            this.Controls.Add(this.NewRecipeBtn);
            this.Controls.Add(this.LoadRecipeBtn);
            this.Controls.Add(this.SaveRecipeBtn);
            this.Controls.Add(this.MainTab);
            this.Controls.Add(this.StepBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.PauseBtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.DiameterLbl);
            this.Controls.Add(this.GrindContactEnabledBtn);
            this.Controls.Add(this.PartGeometryBox);
            this.Controls.Add(this.MountedToolBox);
            this.Controls.Add(this.DiameterDimLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "The Code Sets This Caption";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MonitorTab.ResumeLayout(false);
            this.positionsPage.ResumeLayout(false);
            this.PositionTestButtonGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PositionsGrd)).EndInit();
            this.variablesPage.ResumeLayout(false);
            this.VariableTestButtonGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VariablesGrd)).EndInit();
            this.manualPage.ResumeLayout(false);
            this.revhistPage.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.MainTab.ResumeLayout(false);
            this.RunPage.ResumeLayout(false);
            this.RunPage.PerformLayout();
            this.ProgramPage.ResumeLayout(false);
            this.SetupPage.ResumeLayout(false);
            this.DefaultMoveSetupGrp.ResumeLayout(false);
            this.GrindingMoveSetupGrp.ResumeLayout(false);
            this.ToolSetupGrp.ResumeLayout(false);
            this.ToolSetupGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsGrd)).EndInit();
            this.GeneralConfigGrp.ResumeLayout(false);
            this.GeneralConfigGrp.PerformLayout();
            this.LogPage.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer HeartbeatTmr;
        private System.Windows.Forms.Label TimeLbl;
        private System.Windows.Forms.Timer StartupTmr;
        private System.Windows.Forms.Timer CloseTmr;
        private System.Windows.Forms.Button SaveAsRecipeBtn;
        private System.Windows.Forms.Button NewRecipeBtn;
        private System.Windows.Forms.Button LoadRecipeBtn;
        private System.Windows.Forms.Button SaveRecipeBtn;
        private System.Windows.Forms.Button StepBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.RichTextBox RecipeRTB;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox AllLogRTB;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RichTextBox ErrorLogRTB;
        private System.Windows.Forms.GroupBox groupBox10;
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
        private System.Windows.Forms.ComboBox UserModeBox;
        private System.Windows.Forms.Label RecipeFilenameLbl;
        private System.Windows.Forms.RichTextBox RecipeCommandsRTB;
        private System.Windows.Forms.TabPage manualPage;
        private System.Windows.Forms.Button RobotModeBtn;
        private System.Windows.Forms.Button SafetyStatusBtn;
        private System.Windows.Forms.Button ProgramStateBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label DiameterDimLbl;
        private System.Windows.Forms.TabControl MainTab;
        private System.Windows.Forms.TabPage RunPage;
        private System.Windows.Forms.TabPage ProgramPage;
        private System.Windows.Forms.TabPage SetupPage;
        private System.Windows.Forms.GroupBox ToolSetupGrp;
        private System.Windows.Forms.Button SelectToolBtn;
        private System.Windows.Forms.DataGridView ToolsGrd;
        private System.Windows.Forms.Button LoadToolsBtn;
        private System.Windows.Forms.Button SaveToolsBtn;
        private System.Windows.Forms.Button ClearToolsBtn;
        private System.Windows.Forms.Button DefaultConfigBtn;
        private System.Windows.Forms.Button LoadConfigBtn;
        private System.Windows.Forms.GroupBox GeneralConfigGrp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox RobotProgramTxt;
        private System.Windows.Forms.TextBox ServerIpTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RobotIpTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AutoGrindRootLbl;
        private System.Windows.Forms.Button ChangeRootDirectoryBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage LogPage;
        private System.Windows.Forms.Button RobotConnectBtn;
        private System.Windows.Forms.ComboBox PartGeometryBox;
        private System.Windows.Forms.Label DiameterLbl;
        private System.Windows.Forms.RichTextBox UrDashboardLogRTB;
        private System.Windows.Forms.Label RunStateLbl;
        private System.Windows.Forms.Label Grind;
        private System.Windows.Forms.Label GrindNCyclesLbl;
        private System.Windows.Forms.Label GrindCycleLbl;
        private System.Windows.Forms.Label CurrentLineLblCopy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RichTextBox RecipeRTBCopy;
        private System.Windows.Forms.Button JogRunBtn;
        private System.Windows.Forms.Label RunStartedTimeLbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label RunElapsedTimeLbl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox LogLevelCombo;
        private System.Windows.Forms.TextBox DoorClosedInputTxt;
        private System.Windows.Forms.Button SetDoorClosedInputBtn;
        private System.Windows.Forms.Label DoorClosedLbl;
        private System.Windows.Forms.Label RobotCompletedLbl;
        private System.Windows.Forms.CheckBox AllowRunningOfflineChk;
        private System.Windows.Forms.GroupBox GrindingMoveSetupGrp;
        private System.Windows.Forms.Button SetForceDwellBtn;
        private System.Windows.Forms.Button SetTouchSpeedBtn;
        private System.Windows.Forms.Button SetTouchRetractBtn;
        private System.Windows.Forms.Label GrindProcessStateLbl;
        private System.Windows.Forms.Button JointMoveMountBtn;
        private System.Windows.Forms.Button JointMoveHomeBtn;
        private System.Windows.Forms.Button MoveToolMountBtn;
        private System.Windows.Forms.Button MoveToolHomeBtn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox PositionTestButtonGrp;
        private System.Windows.Forms.Label StepElapsedTimeLbl;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button SetMaxWaitBtn;
        private System.Windows.Forms.Button AboutBtn;
        private System.Windows.Forms.Button SetMaxGrindBlendRadiusBtn;
        private System.Windows.Forms.Button SetTrialSpeedBtn;
        private System.Windows.Forms.GroupBox VariableTestButtonGrp;
        private System.Windows.Forms.Label StepTimeEstimateLbl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label StepTimeRemainingLbl;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label VersionLbl;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label RobotSentLbl;
        private System.Windows.Forms.Button SetGrindAccelBtn;
        private System.Windows.Forms.GroupBox DefaultMoveSetupGrp;
        private System.Windows.Forms.Button SetLinearAccelBtn;
        private System.Windows.Forms.Button SetBlendRadiusBtn;
        private System.Windows.Forms.Button SetJointSpeedBtn;
        private System.Windows.Forms.Button SetJointAccelBtn;
        private System.Windows.Forms.Button SetLinearSpeedBtn;
        private System.Windows.Forms.Button JogBtn;
        private System.Windows.Forms.Button SetMoveDefaultsBtn;
        private System.Windows.Forms.Button SetGrindDefaultsBtn;
        private System.Windows.Forms.Button ClearAllLogRtbBtn;
        private System.Windows.Forms.Button SaveConfigBtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TabPage revhistPage;
        private System.Windows.Forms.RichTextBox RevHistRTB;
        private System.Windows.Forms.Button FullManualBtn;
        private System.Windows.Forms.TextBox FootswitchPressedInputTxt;
        private System.Windows.Forms.Button SetFootswitchPressedInputBtn;
        private System.Windows.Forms.Label FootswitchPressedLbl;
        private System.Windows.Forms.Label DoorClosedInputLbl;
        private System.Windows.Forms.Label FootswitchPressedInputLbl;
        private System.Windows.Forms.Button ToolTestBtn;
        private System.Windows.Forms.Button CoolantOffBtn;
        private System.Windows.Forms.Button CoolantTestBtn;
        private System.Windows.Forms.Button ToolOffBtn;
        private System.Windows.Forms.Label RobotPolyscopeVersionLbl;
        private System.Windows.Forms.Label RobotSerialNumberLbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label RobotModelLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label Time2Lbl;
        private System.Windows.Forms.Button SetPointFrequencyBtn;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label GrindForceReportZLbl;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button BigEditBtn;
        private System.Windows.Forms.Button SetGrindJogSpeedBtn;
        private System.Windows.Forms.Button SetGrindJogAccelBtn;
        private System.Windows.Forms.Button SetForceModeDampingBtn;
        private System.Windows.Forms.Button SetForceModeGainScalingBtn;
    }
}

