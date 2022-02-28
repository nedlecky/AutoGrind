// Olympus Controls AutoGrind Application
// For Tosoh Quartz
//
// Programmer: Ned Lecky
// February 2022


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using NLog;

namespace AutoGrind
{
    public partial class MainForm : Form
    {

        static string AutoGrindRoot = "./";
        private static NLog.Logger log;
        static SplashForm splashForm;
        TcpServer robotServer = null;
        static DataTable variables;
        MessageForm messageForm = null;

        private enum RunState
        {
            INIT,
            IDLE,
            READY,
            RUNNING,
            PAUSED
        }
        RunState runState = RunState.INIT;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string companyName = Application.CompanyName;
            string appName = Application.ProductName;
            string productVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string executable = Application.ExecutablePath;
            string filename = Path.GetFileName(executable);
            string directory = Path.GetDirectoryName(executable);
            string caption = companyName + " " + appName + " " + productVersion;
#if DEBUG
            caption += " RUNNING IN DEBUG MODE";
#endif
            this.Text = caption;

            // Strtup logging system (which also displays messages
            log = NLog.LogManager.GetCurrentClassLogger();

            LoadPersistent();

            for (int i = 0; i < 3; i++)
                log.Info("==============================================================================================");
            log.Info(string.Format("Starting {0} in [{1}]", filename, directory));

            HeartbeatTmr.Interval = 1000;
            HeartbeatTmr.Enabled = true;
            StartupTmr.Interval = 1000;
            StartupTmr.Enabled = true;

            SetRecipeState(RecipeState.NEW);
            SetState(RunState.IDLE);
            GrindBtn_Click(null, null);
        }

        private void StartupTmr_Tick(object sender, EventArgs e)
        {
            splashForm = new SplashForm();
            splashForm.Show();

            log.Info("StartupTmr()...");
            StartupTmr.Enabled = false;

            MessageTmr.Interval = 100;
            MessageTmr.Enabled = true;

            RobotConnectBtn_Click(null, null);

            log.Info("System ready.");
        }
        bool forceClose = false;
        private void CloseTmr_Tick(object sender, EventArgs e)
        {
            // First time this fires, tell all the threads to stop
            //if (++fireCounter == 1)
            //    EndThreads();
            //else
            //{
            // Second time it fires, we can disconnect and shut down!
            CloseTmr.Enabled = false;
            RobotDisconnectBtn_Click(null, null);
            //StopJint();
            MessageTmr_Tick(null, null);
            forceClose = true;
            SaveConfigBtn_Click(null, null);
            NLog.LogManager.Shutdown(); // Flush and close down internal threads and timers
            this.Close();

            //}
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (forceClose) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Do you want to close the application?",
                                    "AutoGrind Confirmation",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                e.Cancel = (result == DialogResult.No);
            }

            if (!e.Cancel)
            {
                if (RecipeRTB.Modified)
                {
                    var result = MessageBox.Show("Current recipe has been modified. Save changes?",
                        "AutoGrind Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        SaveRecipeBtn_Click(null, null);
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                CloseTmr.Interval = 500;
                CloseTmr.Enabled = true;
                e.Cancel = true; // Cancel this shutdown- we'll let the close out timer shut us down
                log.Info("Shutting down in 500mS...");
            }
        }

        bool robotReady = false;
        private void HeartbeatTmr_Tick(object sender, EventArgs e)
        {
            string now = DateTime.Now.ToString("s");
            timeLbl.Text = now;

            bool newRobotReady = false;
            if (robotServer != null)
            {
                if (robotServer.IsClientConnected)
                    newRobotReady = true;
            }

            if (newRobotReady != robotReady)
            {
                robotReady = newRobotReady;
                if (robotReady)
                {
                    log.Info("Change robot connection to READY");
                    RobotStatusLbl.BackColor = Color.Green;
                    RobotStatusLbl.Text = "READY";
                    SetState(RunState.IDLE, true, true);
                }
                else
                {
                    log.Info("Change robot connection to WAIT");
                    RobotStatusLbl.BackColor = Color.Yellow;
                    RobotStatusLbl.Text = "WAIT";
                    SetState(RunState.IDLE, true, true);
                }
            }
        }


        // ========================
        // START MAIN UI BUTTONS
        // ========================

        private void SetState(RunState s, bool fEditing = false, bool fForce = false)
        {
            if (fForce || runState != s)
            {
                runState = s;
                log.Info("SetState({0})", s.ToString());

                EnterRunState(fEditing);
            }
        }

        private void EnterRunState(bool fEditing)
        {
            switch (runState)
            {
                case RunState.IDLE:
                    GrindBtn.Enabled = true;
                    EditBtn.Enabled = true;
                    SetupBtn.Enabled = true;
                    JogBtn.Enabled = robotReady;
                    LoadBtn.Enabled = true;
                    StartBtn.Enabled = false;
                    PauseBtn.Enabled = false;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    ExecTmr.Enabled = false;
                    break;
                case RunState.READY:
                    GrindBtn.Enabled = true;
                    EditBtn.Enabled = true;
                    SetupBtn.Enabled = true;
                    JogBtn.Enabled = robotReady;
                    LoadBtn.Enabled = true;
                    StartBtn.Enabled = true;
                    PauseBtn.Enabled = false;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    ExecTmr.Enabled = false;
                    break;
                case RunState.RUNNING:
                    GrindBtn.Enabled = false;
                    EditBtn.Enabled = false;
                    SetupBtn.Enabled = false;
                    JogBtn.Enabled = false;
                    LoadBtn.Enabled = false;
                    StartBtn.Enabled = false;
                    PauseBtn.Enabled = true;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = true;

                    StartExecutive();
                    ExecTmr.Interval = 100;
                    ExecTmr.Enabled = true;

                    break;
                case RunState.PAUSED:
                    GrindBtn.Enabled = false;
                    EditBtn.Enabled = false;
                    SetupBtn.Enabled = false;
                    JogBtn.Enabled = false;
                    LoadBtn.Enabled = false;
                    StartBtn.Enabled = false;
                    PauseBtn.Enabled = false;
                    ContinueBtn.Enabled = true;
                    StopBtn.Enabled = true;
                    ExecTmr.Enabled = false;
                    break;
            }

            // Set the colors
            if (fEditing)
            {
                //GrindBtn.BackColor = GrindBtn.Enabled ? Color.LightGreen : Color.Gray;
                //EditBtn.BackColor = EditBtn.Enabled ? Color.Green : Color.Gray;
            }
            else
            {
                GrindBtn.BackColor = GrindBtn.Enabled ? Color.Green : Color.Gray;
                EditBtn.BackColor = EditBtn.Enabled ? Color.LightGreen : Color.Gray;
                SetupBtn.BackColor = SetupBtn.Enabled ? Color.LightGreen : Color.Gray;
            }

            JogBtn.BackColor = JogBtn.Enabled ? Color.Green : Color.Gray;
            LoadBtn.BackColor = LoadBtn.Enabled ? Color.Green : Color.Gray;
            StartBtn.BackColor = StartBtn.Enabled ? Color.Green : Color.Gray;
            PauseBtn.BackColor = PauseBtn.Enabled ? Color.DarkOrange : Color.Gray;
            ContinueBtn.BackColor = ContinueBtn.Enabled ? Color.Green : Color.Gray;
            StopBtn.BackColor = StopBtn.Enabled ? Color.Red : Color.Gray;
        }

        private void GrindBtn_Click(object sender, EventArgs e)
        {
            //SetState(RunState.IDLE);
            OperationTab.SelectedTab = OperationTab.TabPages["GrindTab"];
            GrindBtn.BackColor = Color.Green;
            EditBtn.BackColor = Color.LightGreen;
            SetupBtn.BackColor = Color.LightGreen;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            //SetState(RunState.IDLE);
            OperationTab.SelectedTab = OperationTab.TabPages["EditTab"];
            GrindBtn.BackColor = Color.LightGreen;
            EditBtn.BackColor = Color.Green;
            SetupBtn.BackColor = Color.LightGreen;
        }

        private void SetupBtn_Click(object sender, EventArgs e)
        {
            //SetState(RunState.IDLE);
            OperationTab.SelectedTab = OperationTab.TabPages["SetupTab"];
            GrindBtn.BackColor = Color.LightGreen;
            EditBtn.BackColor = Color.LightGreen;
            SetupBtn.BackColor = Color.Green;
        }
        private void KeyboardBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void OperationTab_Selecting(object sender, TabControlCancelEventArgs e)
        // This fires the main Grind, Edit, Setup buttons if the user just changes tabs directly
        {
            log.Info("Selecting {0}", e.TabPage.Text);
            switch (e.TabPage.Text)
            {
                case "Grind":
                    GrindBtn_Click(null, null);
                    break;
                case "Edit":
                    EditBtn_Click(null, null);
                    break;
                case "Setup":
                    SetupBtn_Click(null, null);
                    break;
            }

        }

        // ========================
        // START MAIN UI BUTTONS
        // ========================


        // ========================
        // START GRIND
        // ========================
        private void LoadBtn_Click(object sender, EventArgs e)
        {
            log.Info("LoadBtn_Click(...)");
            LoadRecipeBtn_Click(null, null);
        }


        private void StartBtn_Click(object sender, EventArgs e)
        {
            log.Info("StartBtn_Click(...)");
            SetState(RunState.RUNNING);
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            log.Info("PauseBtn_Click(...)");
            SetState(RunState.PAUSED);
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            log.Info("ContinueBtn_Click(...)");
            SetState(RunState.RUNNING);
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            log.Info("StopBtn_Click(...)");
            SetState(RunState.READY);
        }
        // ========================
        // END GRIND
        // ========================

        // ========================
        // START EDIT
        // ========================
        private enum RecipeState
        {
            INIT,
            NEW,
            LOADED,
            MODIFIED
        }
        RecipeState recipeState = RecipeState.INIT;
        private void SetRecipeState(RecipeState s)
        {
            if (recipeState != s)
            {
                recipeState = s;
                log.Info("SetRecipeState({0})", s.ToString());

                switch (recipeState)
                {
                    case RecipeState.NEW:
                        NewRecipeBtn.Enabled = false;
                        LoadRecipeBtn.Enabled = true;
                        SaveRecipeBtn.Enabled = false;
                        SaveAsRecipeBtn.Enabled = true;
                        break;
                    case RecipeState.LOADED:
                        NewRecipeBtn.Enabled = true;
                        LoadRecipeBtn.Enabled = true;
                        SaveRecipeBtn.Enabled = false;
                        SaveAsRecipeBtn.Enabled = true;
                        break;
                    case RecipeState.MODIFIED:
                        NewRecipeBtn.Enabled = true;
                        LoadRecipeBtn.Enabled = true;
                        SaveRecipeBtn.Enabled = true;
                        SaveAsRecipeBtn.Enabled = true;
                        break;
                }
                NewRecipeBtn.BackColor = NewRecipeBtn.Enabled ? Color.Green : Color.Gray;
                LoadRecipeBtn.BackColor = LoadRecipeBtn.Enabled ? Color.Green : Color.Gray;
                SaveRecipeBtn.BackColor = SaveRecipeBtn.Enabled ? Color.Green : Color.Gray;
                SaveAsRecipeBtn.BackColor = SaveAsRecipeBtn.Enabled ? Color.Green : Color.Gray;
            }
        }

        void LoadRecipeFile(string file)
        {
            log.Info("LoadRecipeFile({0})", file);
            RecipeFilenameLbl.Text = file;
            try
            {
                RecipeRTB.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.PlainText);
                // Copy from the edit window to the runtime window
                RecipeRoRTB.Text = RecipeRTB.Text;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Can't open {0}", file);
            }

            RecipeRTB.Modified = false;
        }

        private void NewRecipeBtn_Click(object sender, EventArgs e)
        {
            log.Info("NewRecipeBtn_Click(...)");
            if (RecipeRTB.Modified)
            {
                var result = MessageBox.Show("Current recipe has been modified. Save changes?",
                    "AutoGrind Confirmation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    SaveRecipeBtn_Click(null, null);
            }

            SetRecipeState(RecipeState.NEW);
            SetState(RunState.IDLE, true);
            RecipeFilenameLbl.Text = "Untitled";
            RecipeRTB.Clear();
            RecipeRoRTB.Clear();
        }

        private void LoadRecipeBtn_Click(object sender, EventArgs e)
        {
            log.Info("LoadRecipeBtn_Click(...)");
            if (RecipeRTB.Modified)
            {
                var result = MessageBox.Show("Current recipe has been modified. Save changes?",
                    "AutoGrind Confirmation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    SaveRecipeBtn_Click(null, null);
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open an AutoGrind Recipe";
            dialog.Filter = "AutoGrind Recipe Files|*.agr";
            dialog.InitialDirectory = Path.Combine(AutoGrindRoot, "Recipes");
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadRecipeFile(dialog.FileName);
                SetRecipeState(RecipeState.LOADED);
                SetState(RunState.READY, true);
            }
        }

        private void SaveRecipeBtn_Click(object sender, EventArgs e)
        {
            log.Info("SaveRecipeBtn_Click(...)");
            if (RecipeFilenameLbl.Text == "Untitled" || RecipeFilenameLbl.Text == "")
                SaveAsRecipeBtn_Click(null, null);
            else
            {
                log.Info("Save Recipe program to {0}", RecipeFilenameLbl.Text);
                RecipeRTB.SaveFile(RecipeFilenameLbl.Text, System.Windows.Forms.RichTextBoxStreamType.PlainText);
                RecipeRTB.Modified = false;
                SetRecipeState(RecipeState.LOADED);
                SetState(RunState.READY, true);
                // Copy from the edit window to the runtime window
                RecipeRoRTB.Text = RecipeRTB.Text;
            }
        }

        private void SaveAsRecipeBtn_Click(object sender, EventArgs e)
        {
            log.Info("SaveAsRecipeBtn_Click(...)");
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "AutoGrind Recipe Files|*.agr";
            dialog.Title = "Save an Autogrind Recipe";
            dialog.InitialDirectory = Path.Combine(AutoGrindRoot, "Recipes");
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName != "")
                {
                    RecipeFilenameLbl.Text = dialog.FileName;
                    SaveRecipeBtn_Click(null, null);
                }
            }
        }
        private void RecipeRTB_ModifiedChanged(object sender, EventArgs e)
        {
            SetRecipeState(RecipeState.MODIFIED);
        }


        // ========================
        // END EDIT
        // ========================


        // ========================
        // START SETUP
        // ========================

        private void DefaultConfigBtn_Click(object sender, EventArgs e)
        {
            log.Info("DefaultConfigBtn_Click(...)");
            AutoGrindRoot = "\\";
            AutoGrindRootLbl.Text = AutoGrindRoot;
            RobotIpPortTxt.Text = "192.168.25.1:30000";
        }
        void LoadPersistent()
        {
            // Pull setup info from registry.... these are overwritten on exit or with various config save operations
            // Note default values are specified here as well
            log.Info("LoadPersistent()");

            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");

            // Window State
            Left = (Int32)AppNameKey.GetValue("Left", 0);
            Top = (Int32)AppNameKey.GetValue("Top", 0);

            // From Setup Tab
            AutoGrindRoot = (string)AppNameKey.GetValue("AutoGrindRoot", "\\");
            AutoGrindRootLbl.Text = AutoGrindRoot;
            RobotIpPortTxt.Text = (string)AppNameKey.GetValue("RobotIpPortTxt.Text", "192.168.25.1:30000");
            UtcTimeChk.Checked = Convert.ToBoolean(AppNameKey.GetValue("UtcTimeChk.Checked", "True"));


            // From Grind Tab
            DiameterLbl.Text = (string)AppNameKey.GetValue("DiameterLbl.Text", "25.000");
            AngleLbl.Text = (string)AppNameKey.GetValue("AngleLbl.Text", "0.000");

            // Also load the variables table
            LoadVariablesBtn_Click(null, null);
        }

        void SavePersistent()
        {
            log.Info("SavePersistent()");

            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");

            // Window State
            AppNameKey.SetValue("Left", Left);
            AppNameKey.SetValue("Top", Top);

            // From Setup Tab
            AppNameKey.SetValue("AutoGrindRoot", AutoGrindRoot);
            AppNameKey.SetValue("RobotIpPortTxt.Text", RobotIpPortTxt.Text);
            AppNameKey.SetValue("UtcTimeChk.Checked", UtcTimeChk.Checked);

            // From Grind Tab
            AppNameKey.SetValue("DiameterLbl.Text", DiameterLbl.Text);
            AppNameKey.SetValue("AngleLbl.Text", AngleLbl.Text);

            // Also save the variables table
            SaveVariablesBtn_Click(null, null);
        }

        private void LoadConfigBtn_Click(object sender, EventArgs e)
        {
            log.Info("LoadConfigBtn_Click(...)");
            LoadPersistent();
        }

        private void SaveConfigBtn_Click(object sender, EventArgs e)
        {
            log.Info("SaveConfigBtn_Click(...)");
            SavePersistent();
        }

        private void ChangeLEonardRootBtn_Click(object sender, EventArgs e)
        {
            log.Info("ChangeLEonardRootBtn_Click(...)");
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = AutoGrindRoot;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                log.Info("New AutoGrindRoot={0}", dialog.SelectedPath);
                AutoGrindRoot = dialog.SelectedPath;
                AutoGrindRootLbl.Text = AutoGrindRoot;

                // Make standard subdirectories (if they don't exist)
                System.IO.Directory.CreateDirectory(Path.Combine(AutoGrindRoot, "Logs"));
                System.IO.Directory.CreateDirectory(Path.Combine(AutoGrindRoot, "Recipes"));
            }
        }

        private void JogBtn_Click(object sender, EventArgs e)
        {
            JoggingForm form = new JoggingForm(robotServer, "Jog to Defect");

            form.ShowDialog(this);
        }

        private void DiameterLbl_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(DiameterLbl.Text, "DIAMETER");

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                DiameterLbl.Text = form.value;
            }
        }

        private void AngleLbl_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(AngleLbl.Text, "ANGLE");

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                AngleLbl.Text = form.value;
            }
        }

        private void RecordPosition(string prompt, string varName)
        {
            JoggingForm form = new JoggingForm(robotServer, prompt, true);

            form.ShowDialog(this);

            if (form.fSave)
            {
                log.Trace(prompt);

                if (robotReady)
                {
                    copyVariableAtWrite = varName + "=actual_joint_positions";
                    robotServer.Send("(20)");
                }

            }
        }
        private void GotoPosition(string varName)
        {
            log.Trace("GotoPosition({0})", varName);
            if (robotReady)
            {
                string q = ReadVariable(varName);
                if (q != null)
                {
                    string msg = "(21," + q.Trim('(');
                    log.Trace("Sending {0}", msg);
                    robotServer.Send(msg);
                }
            }
        }

        private void SetLeftBtn_Click(object sender, EventArgs e)
        {
            RecordPosition("Set Left End of Cylinder", "left_cylinder_end_q");
        }

        private void SetRightBtn_Click(object sender, EventArgs e)
        {
            RecordPosition("Set Right End of Cylinder", "right_cylinder_end_q");
        }

        private void SetDomeBtn_Click(object sender, EventArgs e)
        {
            RecordPosition("Set Dome Top", "dome_top_q");
        }
        private void SetHomeBtn_Click(object sender, EventArgs e)
        {
            RecordPosition("Set Home Position", "home_q");
        }

        private void SetToolChangeBtn_Click(object sender, EventArgs e)
        {
            RecordPosition("Set Tool Change Position", "tool_change_q");
        }

        private void GotoLeftBtn_Click(object sender, EventArgs e)
        {
            GotoPosition("left_cylinder_end_q");
        }

        private void GotoRightBtn_Click(object sender, EventArgs e)
        {
            GotoPosition("right_cylinder_end_q");
        }

        private void GotoDomeBtn_Click(object sender, EventArgs e)
        {
            GotoPosition("dome_top_q");
        }

        private void GotoHomeBtn_Click(object sender, EventArgs e)
        {
            GotoPosition("home_q");
        }

        private void GotoToolChangeBtn_Click(object sender, EventArgs e)
        {
            GotoPosition("tool_change_q");
        }



        // ========================
        // END SETUP
        // ========================


        // ========================
        // START EXECUTIVE
        // ========================

        static int currentLine = 0;
        static int nLines = 0;
        private void StartExecutive()
        {
            nLines = RecipeRoRTB.Lines.Count();
            currentLine = 0;
            log.Info("StartExecutive() nLines={0}", nLines);
        }
        /// <summary>
        /// Read file looking for lines of the form "name=value" and pass then to the variable write function
        /// </summary>
        /// <param name="filename">The filename of interest</param>
        private void ImportFile(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(AutoGrindRoot, filename));

            foreach (string line in lines)
            {
                log.Trace("Import Line: {0}", line);
                if (line.Contains("="))
                    WriteVariable(line);
            }
        }
        private bool ExecuteLine(string line)
        {
            // Fetch the line and replace all 1 or more whitespace with a single space and drop all leading/trailing whitespace
            string command = Regex.Replace(line, @"\s+", " ").Trim();

            // Blank?
            if (command.Length < 1)
            {
                log.Trace("Line {0} BLANK: {1}", currentLine, command);
                return true;
            }

            // Comment?
            if (command[0] == '#')
            {
                log.Trace("Line {0} COMMENT: {1}", currentLine, command);
                return true;
            }

            // Assignment?
            if (command.Contains("="))
            {
                log.Trace("Line {0} ASSIGNMENT: {1}", currentLine, command);
                WriteVariable(command);
                return true;
            }

            // Clear?
            if (command.StartsWith("clear"))
            {
                log.Trace("{0} CLEAR: {1}", currentLine, command);
                ClearVariablesBtn_Click(null, null);
                return true;
            }

            // Import?
            if (command.StartsWith("import "))
            {
                log.Trace("{0} IMPORT: {1}", currentLine, command);
                string[] words = command.Split(' ');
                if (words.Length > 1)
                    ImportFile(words[1]);

                return true;
            }

            // End?
            if (command.StartsWith("end"))
            {
                log.Trace("{0} END: {1}", currentLine, command);
                return false;
            }

            // Home
            if (command.StartsWith("home"))
            {
                log.Trace("{0} HOME: {1}", currentLine, command);
                GotoHomeBtn_Click(null, null);
                return true;
            }

            // toolchange
            if (command.StartsWith("toolchange"))
            {
                log.Trace("{0} TOOLCHANGE: {1}", currentLine, command);
                GotoToolChangeBtn_Click(null, null);
                messageForm = new MessageForm("Tool Change Prompt","Please install tool for: " + command);
                messageForm.ShowDialog();
                return true;
            }

            // sendrobot
            if (command.StartsWith("sendrobot"))
            {
                log.Trace("{0} SENDROBOT: {1}", currentLine, command);
                robotServer.Send(command.Substring(9));
                return true;
            }

            // prompt
            if (command.StartsWith("prompt"))
            {
                log.Trace("{0} PROMPT: {1}", currentLine, command);
                messageForm = new MessageForm("General Prompt", command.Substring(7));
                messageForm.ShowDialog();
                return true;
            }

            // speed
            if (command.StartsWith("speed"))
            {
                log.Trace("{0} speed: {1}", currentLine, command);
                robotServer.Send("(30," + command.Substring(6) + ")");
                return true;
            }

            // accel
            if (command.StartsWith("accel"))
            {
                log.Trace("{0} accel: {1}", currentLine, command);
                robotServer.Send("(31," + command.Substring(6) + ")");
                return true;
            }

            // Grindcircle

            log.Trace("Unknown Command Line {0} Exec: {1}", currentLine, command);
            return true;
        }
        private void ExecTmr_Tick(object sender, EventArgs e)
        {
            //log.Info("ExecTmr(...) curLine={0}", currentLine);
            // Wait for any operator prompt to be cleared
            if(messageForm!=null)
            {
                switch (messageForm.result)
                {
                    case DialogResult.None:
                        return;
                    case DialogResult.Abort:
                        log.Error("Operator requested abort from prompt");
                        SetState(RunState.READY);
                        messageForm = null;
                        return;
                        break;
                    case DialogResult.OK:
                        log.Info("Operator says proceed from prompt");
                        messageForm = null;
                        break;
                }
            }

            if (!robotReady)
            {
                log.Trace("Exec waiting for robotReady");
            }
            else
            {
                if (ReadVariable("robot_running") == "True")
                {
                    log.Trace("Exec waiting for !robot_running");
                }
                else
                {
                    string line = RecipeRoRTB.Lines[currentLine];
                    bool fContinue = ExecuteLine(line);
                    currentLine++;

                    if (!fContinue || currentLine >= nLines)
                        SetState(RunState.READY);
                }
            }
        }

        // ========================
        // END EXECUTIVE
        // ========================

        // ========================
        // START ROBOT INTERFACE
        // ========================

        private void RobotConnectBtn_Click(object sender, EventArgs e)
        {
            RobotDisconnectBtn_Click(null, null);

            robotServer = new TcpServer();
            robotServer.receiveCallback = GeneralCallBack;
            if (robotServer.Connect(RobotIpPortTxt.Text) > 0)
            {
                log.Error("Robot server initialization failure");
            }
            else
            {
                log.Info("Robot connection ready");

                RobotStatusLbl.BackColor = Color.Yellow;
                RobotStatusLbl.Text = "WAIT";
            }
        }

        private void RobotDisconnectBtn_Click(object sender, EventArgs e)
        {
            if (robotServer != null)
            {
                if (robotServer.IsConnected())
                {
                    robotServer.Send("(98)");
                    robotServer.Disconnect();
                }
                robotServer = null;
            }
            RobotStatusLbl.BackColor = Color.Red;
            RobotStatusLbl.Text = "OFF";
        }

        private void RobotSendBtn_Click(object sender, EventArgs e)
        {
            if (robotServer != null)
                if (robotServer.IsConnected())
                {
                    robotServer.Send(RobotMessageTxt.Text);
                }
        }
        private void SetSpeedBtn_Click(object sender, EventArgs e)
        {
            if (robotServer != null)
                if (robotServer.IsConnected())
                {
                    robotServer.Send("(30," + SpeedTxt.Text + ")");
                }
        }

        private void SetAccelBtn_Click(object sender, EventArgs e)
        {
            if (robotServer != null)
                if (robotServer.IsConnected())
                {
                    robotServer.Send("(31," + AccelTxt.Text + ")");
                }
        }



        /// <summary>
        /// TODO: Clean these up, use consistent string formatting ideas, and the variables
        /// TODO All of these... the variable name should be prefixed with the unique device name not the message prefix
        /// Currently expect 0 or more #-separated name=value sequences
        /// Examples:
        /// return1=abc
        /// return1=abc#return2=xyz
        /// SET name value
        /// </summary>
        /// <param name="message"></param>
        void GeneralCallBack(string message)
        {
            //log.Info("GCB<==({0},{1})", message, prefix);

            string[] requests = message.Split('#');
            foreach (string request in requests)
            {
                // {script.....}
                if (false)//request.StartsWith("{") && request.EndsWith("}"))
                    ;// ExecuteJavaScript(request.Substring(1, request.Length - 2));
                else
                {
                    // name=value
                    // TODO not clear what happens if you have
                    //      name = value
                    //      name = this is a test
                    //      name = "this is a test"
                    if (request.Contains("="))
                        WriteVariable(request);
                    else
                    {
                        // SET name value
                        if (request.StartsWith("SET "))
                        {
                            string[] s = request.Split(' ');
                            if (s.Length == 3)
                                WriteVariable(s[1], s[2]);
                            else
                                log.Error("Illegal SET statement: {0}", request);
                        }
                        else
                            log.Error("Illegal GCB command: {0}", request);
                    }
                }
            }
        }

        private void MessageTmr_Tick(object sender, EventArgs e)
        {
            if (robotServer != null)
                if (robotServer.IsConnected())
                {
                    robotServer.Receive();
                }
        }

        // ========================
        // END ROBOT INTERFACE
        // ========================

        // ========================
        // START VARIABLE SYSTEM
        // ========================

        readonly string variablesFilename = "Variables.var";

        private void ReadVariableBtn_Click(object sender, EventArgs e)
        {
            string name = VariableNameTxt.Text.Trim();
            string v = ReadVariable(name);
        }

        private string ReadVariable(string name)
        {
            foreach (DataRow row in variables.Rows)
            {
                if ((string)row["Name"] == name)
                {
                    log.Trace("ReadVariable({0}) = {1}", row["Name"], row["Value"]);
                    row["IsNew"] = false;
                    return row["Value"].ToString();
                }
            }
            log.Error("ReadVariable({0}) Not Found", name);
            return null;
        }

        static readonly object lockObject = new object();
        static string alsoWriteVariableAs = null;
        static string copyVariableAtWrite = null;
        /// <summary>
        /// Update variable 'name' with 'value' if it exists otherwise add it
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void WriteVariable(string name, string value)
        {
            System.Threading.Monitor.Enter(lockObject);
            string nameTrimmed = name.Trim();
            log.Info("WriteVariable({0}, {1})", nameTrimmed, value);
            if (variables == null)
            {
                log.Error("variables=null!");
                return;
            }
            string datetime;
            if (UtcTimeChk.Checked)
                datetime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            else
                datetime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");

            bool foundVariable = false;
            foreach (DataRow row in variables.Rows)
            {
                if ((string)row["Name"] == nameTrimmed)
                {
                    // TODO: This is where it breaks prior to Thread Safety work
                    row["Value"] = value;
                    row["IsNew"] = true;
                    row["TimeStamp"] = datetime;
                    foundVariable = true;
                    break;
                }
            }

            if (!foundVariable)
                variables.Rows.Add(new object[] { nameTrimmed, value, true, datetime });

            variables.AcceptChanges();
            Monitor.Exit(lockObject);

            // This is a special capability that is not necessarily the best way to handle this!
            // If you set alsoWriteVariableAs=name, the next WriteVariable will write the same value to name
            if (alsoWriteVariableAs != null)
            {
                string dupName = alsoWriteVariableAs;
                alsoWriteVariableAs = null; // Let's avoid infinite recursion :)
                WriteVariable(dupName, value);
            }

            // Another experiment
            // Set copyVariableAtWrite to "name1=name2" and when name2 gets written it will also be written to name1
            if (copyVariableAtWrite != null)
            {
                string[] strings = copyVariableAtWrite.Split('=');
                if (strings.Length > 1)
                {
                    if (strings[1] == nameTrimmed)
                    {
                        string dupName = copyVariableAtWrite;
                        copyVariableAtWrite = null; // Let's avoid infinite recursion :)
                        WriteVariable(strings[0], value);
                    }
                }
            }
        }

        /// <summary>
        /// Takes a "mname=value" string and set variable "name" equal to "value"
        /// </summary>
        /// <param name="assignment"></param>
        public void WriteVariable(string assignment)
        {
            string[] s = assignment.Split('=');
            if (s.Length != 2)
            {
                log.Error("WriteVariable({0} is invalid", assignment);
            }
            else
            {
                WriteVariable(s[0], s[1]);
            }

        }
        private void WriteStringValueBtn_Click(object sender, EventArgs e)
        {
            string name = VariableNameTxt.Text;
            string value = WriteStringValueTxt.Text;
            WriteVariable(name, value);
        }

        private void LoadVariablesBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", variablesFilename);
            log.Info("LoadVariables from {0}", filename);
            ClearVariablesBtn_Click(null, null);
            try
            {
                variables.ReadXml(filename);

            }
            catch
            { }

            VariablesGrd.DataSource = variables;
            foreach (DataGridViewColumn col in VariablesGrd.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            foreach (DataRow row in variables.Rows)
            {
                row["IsNew"] = false;
            }
        }

        private void SaveVariablesBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", variablesFilename);
            log.Info("SaveVariables to {0}", filename);
            variables.AcceptChanges();
            variables.WriteXml(filename, XmlWriteMode.WriteSchema, true);
        }

        private void ClearVariablesBtn_Click(object sender, EventArgs e)
        {
            variables = new DataTable("Variables");
            DataColumn name = variables.Columns.Add("Name", typeof(System.String));
            variables.Columns.Add("Value", typeof(System.String));
            variables.Columns.Add("IsNew", typeof(System.Boolean));
            variables.Columns.Add("TimeStamp", typeof(System.String));
            variables.CaseSensitive = true;
            variables.PrimaryKey = new DataColumn[] { name };
            VariablesGrd.DataSource = variables;
        }

 
        // ========================
        // END VARIABLE SYSTEM
        // ========================

    }
}
