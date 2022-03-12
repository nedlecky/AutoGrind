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

            // Startup logging system (which also displays messages)
            log = NLog.LogManager.GetCurrentClassLogger();
            TraceRad.Checked = true; // Set screen RTB logging level (logfile is ALWAYS at Trace)

            // TODO: Must do this first to get AutoGrindRoot prior to logger beginning
            LoadPersistent();

            // Set logfile variable in NLog
            LogManager.Configuration.Variables["LogfileName"] = AutoGrindRoot + "/Logs/AutoGrind.log";
            LogManager.ReconfigExistingLoggers();

            // Flag that we're starting
            log.Info("================================================================");
            log.Info(string.Format("Starting {0} in [{1}]", filename, directory));
            log.Info(caption);
            log.Info("================================================================");

            // 1-second tick
            HeartbeatTmr.Interval = 1000;
            HeartbeatTmr.Enabled = true;

            // Real start of everyone will happen shortly
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

            // Load thge last recipe if there was one loaded in LoadPersistent()
            if (recipeFileToAutoload != "")
                if (LoadRecipeFile(recipeFileToAutoload))
                {
                    SetRecipeState(RecipeState.LOADED);
                    SetState(RunState.READY, true);
                }

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

        /// <summary>
        /// Put up a YesNo modal dialog with heading AutoGrind Confirmation and a "question", then
        /// wait for response and return as DialogResult
        /// </summary>
        /// <param name="question">Question to display in dialog</param>
        /// <returns>DialogResult.Yes, .No, or .Cancel</returns>
        private DialogResult ConfirmMessageBox(string question)
        {
            // TODO: Maybe should be a custom dialog like the prompt!
            DialogResult result = MessageBox.Show(question,
                     "AutoGrind Confirmation",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question);
            return result;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (forceClose) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = ConfirmMessageBox("Do you want to close the application?");
                e.Cancel = (result == DialogResult.No);
            }

            if (!e.Cancel)
            {
                if (RecipeRTB.Modified)
                {
                    var result = ConfirmMessageBox("Current recipe has been modified. Save changes?");
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

                if (newRobotReady != robotReady)
                {
                    robotReady = newRobotReady;
                    if (robotReady)
                    {
                        log.Info("Change robot connection to READY");
                        RobotStatusLbl.BackColor = Color.Green;
                        RobotStatusLbl.Text = "READY";
                        // Restore all button settings with same current state
                        SetState(runState, true, true);
                    }
                    else
                    {
                        log.Info("Change robot connection to WAIT");
                        RobotStatusLbl.BackColor = Color.Yellow;
                        RobotStatusLbl.Text = "WAIT";
                        // Restore all button settings with same current state
                        SetState(runState, true, true);
                    }
                }
            }
        }


        // ===================================================================
        // START MAIN UI BUTTONS
        // ===================================================================

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
        private void ClearAllLogRtbBtn_Click(object sender, EventArgs e)
        {
            AllLogRTB.Clear();
        }

        private void ClearUrLogRtbBtn_Click(object sender, EventArgs e)
        {
            UrLogRTB.Clear();
        }

        private void ClearErrorLogRtbBtn_Click(object sender, EventArgs e)
        {
            ErrorLogRTB.Clear();
        }

        // ===================================================================
        // END MAIN UI BUTTONS
        // ===================================================================


        // ===================================================================
        // START GRIND
        // ===================================================================
        private void LoadBtn_Click(object sender, EventArgs e)
        {
            log.Info("LoadBtn_Click(...)");
            LoadRecipeBtn_Click(null, null);
        }


        private void GrindDryrunBtn_Click(object sender, EventArgs e)
        {
            if (robotServer != null)
                if (robotServer.IsConnected())
                {
                    robotServer.Send(String.Format("(40,1,{0})", GrindDryrunBtn.BackColor != Color.Green ? 0 : 1));
                }
        }


        private void StartBtn_Click(object sender, EventArgs e)
        {
            log.Info("StartBtn_Click(...)");
            if (!robotReady)
            {
                var result = ConfirmMessageBox("Robot not connected. Run anyway?");
                if (result != DialogResult.Yes) return;
            }
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
        private void HaltRobotBtn_Click(object sender, EventArgs e)
        {
            log.Error("UR HaltRobotBtn_Click(...)");
            robotServer.Send("(10)");
        }

        // ===================================================================
        // END GRIND
        // ===================================================================

        // ===================================================================
        // START EDIT
        // ===================================================================
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


        /// <summary>
        /// Load a filename into RecipeRTB and RecibeRoRTB and place the filename in RecipeFilenameLbl.Text
        /// If the file does nbot exost, clear all of the above and return false. Else return true.
        /// </summary>
        /// <param name="file">The file to be loaded.</param>
        /// <returns></returns>
        bool LoadRecipeFile(string file)
        {
            log.Info("LoadRecipeFile({0})", file);
            RecipeFilenameLbl.Text = "";
            RecipeRoRTB.Text = "";
            try
            {
                RecipeRTB.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.PlainText);
                RecipeRTB.Modified = false;
                RecipeFilenameLbl.Text = file;

                // Copy from the edit window to the runtime window
                RecipeRoRTB.Text = RecipeRTB.Text;
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Can't open {0}", file);
                return false;
            }
        }

        private void NewRecipeBtn_Click(object sender, EventArgs e)
        {
            log.Info("NewRecipeBtn_Click(...)");
            if (RecipeRTB.Modified)
            {
                var result = ConfirmMessageBox("Current recipe has been modified. Save changes?");
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
                var result = ConfirmMessageBox("Current recipe has been modified. Save changes?");
                if (result == DialogResult.Yes)
                    SaveRecipeBtn_Click(null, null);
            }

            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Open an AutoGrind Recipe",
                Filter = "AutoGrind Recipe Files|*.agr",
                InitialDirectory = Path.Combine(AutoGrindRoot, "Recipes")
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (LoadRecipeFile(dialog.FileName))
                {
                    SetRecipeState(RecipeState.LOADED);
                    SetState(RunState.READY, true);
                }
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
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "AutoGrind Recipe Files|*.agr",
                Title = "Save an Autogrind Recipe",
                InitialDirectory = Path.Combine(AutoGrindRoot, "Recipes")
            };
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
        // ===================================================================
        // END EDIT
        // ===================================================================

        // ===================================================================
        // START SETUP
        // ===================================================================
        private void DefaultConfigBtn_Click(object sender, EventArgs e)
        {
            log.Info("DefaultConfigBtn_Click(...)");
            AutoGrindRoot = "\\";
            AutoGrindRootLbl.Text = AutoGrindRoot;
            RobotIpPortTxt.Text = "192.168.25.1:30000";
        }
        private string recipeFileToAutoload = "";
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

            // Autoload file is the last loaded recipe
            recipeFileToAutoload = (string)AppNameKey.GetValue("RecipeFilenameLbl.Text", "");
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

            // Save currently loaded recipe
            AppNameKey.SetValue("RecipeFilenameLbl.Text", RecipeFilenameLbl.Text);
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
            FolderBrowserDialog dialog = new FolderBrowserDialog()
            {
                SelectedPath = AutoGrindRoot
            };
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

            if (form.ShouldSave)
            {
                log.Trace(prompt);

                if (robotReady)
                {
                    copyVariableAtWrite = varName + "=actual_joint_positions";
                    isSystemCopyWrite = true;
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
                    string msg = "(21," + ExtractScalars(q) + ')';
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

        private void ChangeLogLevel(string s)
        {
            LogManager.Configuration.Variables["myLevel"] = s;
            LogManager.ReconfigExistingLoggers();
        }
        private void ErrorRad_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLogLevel("Error");
        }

        private void WarnRad_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLogLevel("Warn");
        }

        private void InfoRad_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLogLevel("Info");
        }

        private void DebugRad_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLogLevel("Debug");
        }

        private void TraceRad_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLogLevel("Trace");
        }
        // ===================================================================
        // END SETUP
        // ===================================================================

        // ===================================================================
        // START EXECUTIVE
        // ===================================================================
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
        /// <param name="filename">File to import- assumed to reside in AutoGrindRoot/Recipes</param>
        /// <returns>true if file import completed successfully</returns>
        private bool ImportFile(string filename)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(Path.Combine(AutoGrindRoot, "Recipes", filename));

                foreach (string line in lines)
                {
                    log.Info("Import Line: {0}", line);
                    if (line.Contains("="))
                        WriteVariable(line);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex, "ImportFile({0}) failed", filename);
                return false;
            }
        }

        /// <summary>
        /// Put up MessageForm dialog. Execution will pause until the operator handles the response.
        /// </summary>
        /// <param name="message">This is the message to be displayed</param>
        private void PromptOperator(string message, string heading = "AutoGrind Prompt")
        {
            log.Info("Prompting Operator: heading={0} message={1}", heading, message);
            messageForm = new MessageForm(heading, message);
            messageForm.ShowDialog();
        }

        /// <summary>
        /// Return the characters enclosed in the first set of matching ( ) in a string
        /// Example: "speed (13.0)" returns 13.0 
        /// </summary>
        /// <param name="s">input string</param>
        /// <returns>Characters enclosed in (...) or ""</returns>
        string ExtractParameters(string s)
        {
            try
            {
                return s.Split('(', ')')[1];
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Return the characters enclosed in the first set of matching [ ] in a string
        /// Example: "q[2,3,4,5,6,7]" returns 2,3,4,5,6,7 
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>Characters enclosed in [...] or ""</returns>
        string ExtractScalars(string input)
        {
            try
            {
                return input.Split('[', ']')[1];
            }
            catch
            {
                return "";
            }
        }
        private bool ExecuteLine(string line)
        {
            // Cleanup the line: replace all 2 or more whitespace with a single space and drop all leading/trailing whitespace
            string command = Regex.Replace(line, @"\s+", " ").Trim();

            // Blank line
            if (command.Length < 1)
            {
                log.Info("Line {0} BLANK: {1}", currentLine, command);
                return true;
            }

            // # (comment)
            if (command[0] == '#')
            {
                log.Info("Line {0} COMMENT: {1}", currentLine, command);
                return true;
            }

            // = (assignment)
            if (command.Contains("="))
            {
                log.Info("Line {0} ASSIGNMENT: {1}", currentLine, command);
                WriteVariable(command);
                return true;
            }

            // clear
            if (command.StartsWith("clear()"))
            {
                log.Info("{0} CLEAR: {1}", currentLine, command);
                ClearVariablesBtn_Click(null, null);
                return true;
            }

            // import filename?
            if (command.StartsWith("import("))
            {
                log.Info("{0} IMPORT: {1}", currentLine, command);
                string file = ExtractParameters(command);
                if (file.Length > 1)
                {
                    if (!ImportFile(file))
                        PromptOperator(string.Format("File import error: {0}", command));
                }
                else
                    PromptOperator("Invalid import command: {0}", command);

                return true;
            }

            // end
            if (command.StartsWith("end"))
            {
                log.Info("{0} END: {1}", currentLine, command);
                return false;
            }

            // home
            if (command.StartsWith("home()"))
            {
                log.Info("{0} HOME: {1}", currentLine, command);
                GotoHomeBtn_Click(null, null);
                return true;
            }

            // toolchange
            if (command.StartsWith("toolchange("))
            {
                log.Info("{0} TOOLCHANGE: {1}", currentLine, command);
                GotoToolChangeBtn_Click(null, null);
                messageForm = new MessageForm("Tool Change Prompt", "Please install tool for: " + command);
                messageForm.ShowDialog();
                return true;
            }

            // sendrobot
            if (command.StartsWith("sendrobot("))
            {
                log.Info("{0} SENDROBOT: {1}", currentLine, command);
                robotServer.Send("(" + ExtractParameters(command) + ")");
                return true;
            }

            // prompt
            if (command.StartsWith("prompt("))
            {
                log.Info("{0} PROMPT: {1}", currentLine, command);
                // This just displays the dialog. ExecTmr will wait for it to close
                PromptOperator(ExtractParameters(command));
                return true;
            }

            // speed
            if (command.StartsWith("speed("))
            {
                log.Info("{0} speed: {1}", currentLine, command);
                string param = ExtractParameters(command);
                robotServer.Send("(30," + param + ")");
                SpeedTxt.Text = param;
                return true;
            }

            // accel
            if (command.StartsWith("accel("))
            {
                log.Info("{0} accel: {1}", currentLine, command);
                string param = ExtractParameters(command);
                robotServer.Send("(31," + param + ")");
                AccelTxt.Text = param;
                return true;
            }

            // grind_dryrun
            if (command.StartsWith("grind_dryrun("))
            {
                log.Info("{0} grind_dryrun: {1}", currentLine, command);
                robotServer.Send("(40,1," + ExtractParameters(command) + ")");
                return true;
            }

            // grind_flat_rect
            // TODO These 30, 31, 40,10 things should be in a lookup
            if (command.StartsWith("grind_flat_rect("))
            {
                log.Info("{0} grind_flat_rect: {1}", currentLine, command);
                robotServer.Send("(40,10," + ExtractParameters(command) + ")");
                return true;
            }

            // grind_cyl_rect
            if (command.StartsWith("grind_cyl_rect("))
            {
                log.Info("{0} grind_cyl_rect: {1}", currentLine, command);
                robotServer.Send("(40,20," + ExtractParameters(command) + ")");
                return true;
            }

            // grind_sphere_rect
            if (command.StartsWith("grind_sphere_rect("))
            {
                log.Info("{0} grind_sphere_rect: {1}", currentLine, command);
                robotServer.Send("(40,25," + ExtractParameters(command) + ")");
                return true;
            }

            // grind_flat_serp
            if (command.StartsWith("grind_flat_serp("))
            {
                log.Info("{0} grind_flat_serp: {1}", currentLine, command);
                robotServer.Send("(40,30," + ExtractParameters(command) + ")");
                return true;
            }

            // grind_cyl_serp
            if (command.StartsWith("grind_cyl_serp("))
            {
                log.Info("{0} grind_cyl_serp: {1}", currentLine, command);
                robotServer.Send("(40,40," + ExtractParameters(command) + ")");
                return true;
            }
            // grind_flat_circle
            if (command.StartsWith("grind_flat_circle("))
            {
                log.Info("{0} grind_flat_circle: {1}", currentLine, command);
                robotServer.Send("(40,50," + ExtractParameters(command) + ")");
                return true;
            }
            // grind_flat_spiral
            if (command.StartsWith("grind_flat_spiral("))
            {
                log.Info("{0} grind_flat_spiral: {1}", currentLine, command);
                robotServer.Send("(40,60," + ExtractParameters(command) + ")");
                return true;
            }

            log.Error("Unknown Command Line {0} Exec: {1}", currentLine, command);
            messageForm = new MessageForm("Illegal Recipe Command", "Illegal Recipe line: " + command);
            messageForm.ShowDialog();
            return true;
        }

        int logFilter = 0;
        private void ExecTmr_Tick(object sender, EventArgs e)
        {
            //log.Info("ExecTmr(...) curLine={0}", currentLine);
            // Wait for any operator prompt to be cleared
            if (messageForm != null)
            {
                switch (messageForm.result)
                {
                    case DialogResult.None:
                        return;
                    case DialogResult.Abort:
                        log.Error("Operator selected \"Abort\" in MessageForm");
                        SetState(RunState.READY);
                        messageForm = null;
                        return;
                    case DialogResult.OK:
                        log.Info("Operator selected \"Continue\" in MessageForm");
                        messageForm = null;
                        break;
                }
            }

            if (!robotReady)
            {
                // Only log this one time!
                if (logFilter != 1)
                    log.Trace("Exec waiting for robotReady");
                logFilter = 1;
            }
            else
            {
                if (ReadVariable("robot_busy") == "True")
                {
                    // Only log this one time!
                    if (logFilter != 2)
                        log.Trace("Exec waiting for !robot_busy");
                    logFilter = 2;
                }
                else
                {
                    // Resets such that the above log messages will happen
                    logFilter = 3;
                    string line = RecipeRoRTB.Lines[currentLine];
                    bool fContinue = ExecuteLine(line);
                    currentLine++;

                    if (!fContinue || currentLine >= nLines)
                        SetState(RunState.READY);
                }
            }
        }
        // ===================================================================
        // END EXECUTIVE
        // ===================================================================

        // ===================================================================
        // START ROBOT INTERFACE
        // ===================================================================
        private void RobotConnectBtn_Click(object sender, EventArgs e)
        {
            RobotDisconnectBtn_Click(null, null);

            robotServer = new TcpServer()
            {
                ReceiveCallback = GeneralCallBack
            };
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

        private void MessageTmr_Tick(object sender, EventArgs e)
        {
            if (robotServer != null)
                if (robotServer.IsConnected())
                {
                    robotServer.Receive();
                }
        }

        // ===================================================================
        // END ROBOT INTERFACE
        // ===================================================================

        // ===================================================================
        // START VARIABLE SYSTEM
        // ===================================================================

        readonly string variablesFilename = "Variables.var";

        private void ReadVariableBtn_Click(object sender, EventArgs e)
        {
            string name = VariableNameTxt.Text.Trim();
            string value = ReadVariable(name);
            log.Info("ReadVariableBtn_Click(...) returns {0}={1}", name, value);
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
            //log.Error("ReadVariable({0}) Not Found", name);
            return null;
        }


        private Color BusyColorFromBooleanName(string name)
        {
            switch (name)
            {
                case "True":
                    return Color.Red;
                case "False":
                    return Color.Green;
                default:
                    return Color.Yellow;
            }
        }
        static readonly object lockObject = new object();
        static string alsoWriteVariableAs = null;
        static string copyVariableAtWrite = null;
        static bool isSystemAlsoWrite = false;
        static bool isSystemCopyWrite = false;
        /// <summary>
        /// Update variable 'name' with 'value' if it exists otherwise add it. Also set system flag
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isSystem"></param>
        public void WriteVariable(string name, string value, bool isSystem = false)
        {
            System.Threading.Monitor.Enter(lockObject);
            string nameTrimmed = name.Trim();
            string valueTrimmed = value.Trim();
            log.Trace("WriteVariable({0}, {1})", nameTrimmed, valueTrimmed);
            if (variables == null)
            {
                log.Error("variables == null!!??");
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
                    row["Value"] = valueTrimmed;
                    row["IsNew"] = true;
                    row["TimeStamp"] = datetime;
                    row["IsSystem"] = isSystem;
                    foundVariable = true;
                    break;
                }
            }

            if (!foundVariable)
                variables.Rows.Add(new object[] { nameTrimmed, valueTrimmed, true, datetime, isSystem });

            // Update real-time annunciators
            switch (nameTrimmed)
            {
                case "robot_busy":
                    RobotBusyLbl.BackColor = BusyColorFromBooleanName(valueTrimmed);
                    break;
                case "grind_busy":
                    GrindBusyLbl.BackColor = BusyColorFromBooleanName(valueTrimmed);
                    break;
                case "robot_speed":
                    RobotSpeedLbl.Text = "Speed\n" + valueTrimmed;
                    break;
                case "robot_accel":
                    RobotAccelLbl.Text = "Accel\n" + valueTrimmed;
                    break;
                case "grind_dryrun":
                    GrindDryrunBtn.BackColor = BusyColorFromBooleanName(valueTrimmed);
                    break;
            }

            variables.AcceptChanges();
            Monitor.Exit(lockObject);

            // This is a special capability that is not necessarily the best way to handle this!
            // If you set alsoWriteVariableAs=name, the next WriteVariable will write the same value to name
            if (alsoWriteVariableAs != null)
            {
                string dupName = alsoWriteVariableAs;
                alsoWriteVariableAs = null; // Let's avoid infinite recursion :)
                WriteVariable(dupName, valueTrimmed, isSystemAlsoWrite);
                isSystemAlsoWrite = false;
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
                        copyVariableAtWrite = null; // Let's avoid infinite recursion :)
                        WriteVariable(strings[0], valueTrimmed, isSystemCopyWrite);
                        isSystemCopyWrite = false;
                    }
                }
            }
        }

        /// <summary>
        /// Takes a "mname=value" string and set variable "name" equal to "value"
        /// </summary>
        /// <param name="assignment"></param>
        public void WriteVariable(string assignment, bool isSystem = false)
        {
            string[] s = assignment.Split('=');
            if (s.Length != 2)
            {
                log.Error("WriteVariable({0} is invalid", assignment);
            }
            else
            {
                WriteVariable(s[0], s[1], isSystem);
            }

        }
        private void WriteStringValueBtn_Click(object sender, EventArgs e)
        {
            string name = VariableNameTxt.Text;
            string value = WriteStringValueTxt.Text;
            bool fSystem = IsSystemChk.Checked;
            WriteVariable(name, value, fSystem);
        }

        private void LoadVariablesBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", variablesFilename);
            log.Info("LoadVariables from {0}", filename);
            ClearAndInitializeVariables();
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

        private bool DeleteNonSystemVariable()
        {
            foreach (DataRow row in variables.Rows)
            {
                if (row["IsSystem"].ToString() != "True")
                {
                    log.Debug("Delete {0}", row["Name"]);
                    row.Delete();
                    variables.AcceptChanges();
                    return true;
                }
            }
            return false;
        }
        private void ClearVariablesBtn_Click(object sender, EventArgs e)
        {
            while (DeleteNonSystemVariable()) ;
            variables.AcceptChanges();
        }

        private void ClearAndInitializeVariables()
        {
            variables = new DataTable("Variables");
            DataColumn name = variables.Columns.Add("Name", typeof(System.String));
            variables.Columns.Add("Value", typeof(System.String));
            variables.Columns.Add("IsNew", typeof(System.Boolean));
            variables.Columns.Add("TimeStamp", typeof(System.String));
            variables.Columns.Add("IsSystem", typeof(System.Boolean));
            variables.CaseSensitive = true;
            variables.PrimaryKey = new DataColumn[] { name };
            VariablesGrd.DataSource = variables;
        }

        private void ClearAllVariablesBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == ConfirmMessageBox("This will clear all variables INLUDING system variables. Proceed?"))
                ClearAndInitializeVariables();
        }

        // ===================================================================
        // END VARIABLE SYSTEM
        // ===================================================================
    }
}
