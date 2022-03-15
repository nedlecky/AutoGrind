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
        MessageForm messageForm = null;

        static DataTable variables;
        static DataTable tools;

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

            // Load the last recipe if there was one loaded in LoadPersistent()
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
        private DialogResult ErrorMessageBox(string message)
        {
            // TODO: Maybe should be a custom dialog like the prompt!
            DialogResult result = MessageBox.Show(message,
                     "AutoGrind Error",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
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
                        RobotStatusLbl.BackColor = Color.Red;
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
                log.Info("EXEC SetState({0})", s.ToString());

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
                    ExitBtn.Enabled = true;
                    JogBtn.Enabled = robotReady;
                    LoadBtn.Enabled = true;
                    StartBtn.Enabled = false;
                    PauseBtn.Enabled = false;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    ExecTmr.Enabled = false;
                    CurrentLineLbl.Text = "";
                    break;
                case RunState.READY:
                    GrindBtn.Enabled = true;
                    EditBtn.Enabled = true;
                    SetupBtn.Enabled = true;
                    ExitBtn.Enabled = true;
                    JogBtn.Enabled = robotReady;
                    LoadBtn.Enabled = true;
                    StartBtn.Enabled = true;
                    PauseBtn.Enabled = false;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    ExecTmr.Enabled = false;
                    CurrentLineLbl.Text = "";
                    break;
                case RunState.RUNNING:
                    GrindBtn.Enabled = false;
                    EditBtn.Enabled = false;
                    SetupBtn.Enabled = false;
                    ExitBtn.Enabled = false;
                    JogBtn.Enabled = false;
                    LoadBtn.Enabled = false;
                    StartBtn.Enabled = false;
                    PauseBtn.Enabled = true;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = true;
                    CurrentLineLbl.Text = "";

                    ExecTmr.Interval = 100;
                    ExecTmr.Enabled = true;

                    break;
                case RunState.PAUSED:
                    GrindBtn.Enabled = false;
                    EditBtn.Enabled = false;
                    SetupBtn.Enabled = false;
                    ExitBtn.Enabled = false;
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
                ExitBtn.BackColor = ExitBtn.Enabled ? Color.Green : Color.Gray;
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

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
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

        private void ClearExecLogRtbBtn_Click(object sender, EventArgs e)
        {
            ExecLogRTB.Clear();
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


        private void GrindContactEnabledBtn_Click(object sender, EventArgs e)
        {
            if (robotServer != null)
                if (robotServer.IsConnected())
                {
                    robotServer.Send(String.Format("(40,1,{0})", GrindContactEnabledBtn.BackColor != Color.Green ? 1 : 0));
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

            SetCurrentLine(-1);
            if (!BuildLabelTable())
                ErrorMessageBox("Error parsing labels from recipe.");
            else
                SetState(RunState.RUNNING);
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            log.Info("PauseBtn_Click(...)");
            robotServer.Send("(10)");  // This will cancel any grind in progress
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
            robotServer.Send("(10)");  // This will cancel any grind in progress
            SetState(RunState.READY);
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

            // Debug Level selection
            DebugLevelCombo.Text = (string)AppNameKey.GetValue("DebugLevelCombo.Text", "INFO");

            // From Grind Tab
            DiameterLbl.Text = (string)AppNameKey.GetValue("DiameterLbl.Text", "25.000");
            AngleLbl.Text = (string)AppNameKey.GetValue("AngleLbl.Text", "0.000");

            // Also load the tools table
            LoadToolsBtn_Click(null, null);

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

            // Debug Level selection
            AppNameKey.SetValue("DebugLevelCombo.Text", DebugLevelCombo.Text);

            // From Grind Tab
            AppNameKey.SetValue("DiameterLbl.Text", DiameterLbl.Text);
            AppNameKey.SetValue("AngleLbl.Text", AngleLbl.Text);

            // Also save the tools table
            SaveToolsBtn_Click(null, null);

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
        private void DebugLevelCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeLogLevel(DebugLevelCombo.Text);
        }

        // ===================================================================
        // END SETUP
        // ===================================================================

        // ===================================================================
        // START EXECUTIVE
        // ===================================================================

        /// <summary>
        /// Is the line a recipe label? TYhis means starting with alpha, followed by 0 or more alphanum, followed by :
        /// </summary>
        /// <param name="line">Input Line</param>
        /// <returns>(bool Success, string Value if matched else null)</returns>
        private (bool Success, string Value) IsLineALabel(string line)
        {
            Regex regex = new Regex("^[A-Za-z][A-Za-z0-9]+:");
            Match match = regex.Match(line);
            if (match.Success)
                return (true, match.Value.Trim(':'));
            else
                return (false, null);
        }

        Dictionary<string, int> labels;
        private bool BuildLabelTable()
        {
            log.Debug("BuildLabelTable()");

            labels = new Dictionary<string, int>();

            int lineNo = 0;
            foreach (string line in RecipeRoRTB.Lines)
            {
                var label = IsLineALabel(line);
                if (label.Success)
                {
                    labels.Add(label.Value, lineNo);
                    log.Debug("Found label {0:000}: {1}", lineNo, label.Value);
                }
                lineNo++;
            }

            return true;
        }

        /// <summary>
        /// Set the lineCurrentlyExecuting to n and highlight it in the RecipRoRTB
        /// </summary>
        /// <param name="n">Line number to start executing</param>
        private void SetCurrentLine(int n)
        {
            lineCurrentlyExecuting = n;

            if (n >= 0 && n < RecipeRTB.Lines.Count())
            {
                int start = RecipeRoRTB.GetFirstCharIndexFromLine(lineCurrentlyExecuting);
                int length = RecipeRoRTB.Lines[lineCurrentlyExecuting].Length;
                //RecipeRoRTB.Select(start,0);
                //RecipeRoRTB.ScrollToCaret();
                RecipeRoRTB.Select(start, length);
            }
        }


        static int lineCurrentlyExecuting = 0;
        private bool StartExecutive()
        {
            return true;
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

        Dictionary<string, string> robotAlias = new Dictionary<string, string>
        {
            // SETTINGS
            {"set_speed",                   "30,1"  },
            {"set_accel",                   "30,2"  },
            {"set_blend",                   "30,3"  },
            {"set_tcp",                     "30,10" },
            {"set_payload",                 "30,11" },
            {"grind_contact_enabled",       "40,1"  },

            // RECTANGLUR GRINDS
            {"grind_rect_flat",             "40,10" },
            {"grind_rect_cylinder",         "40,11" },
            {"grind_rect_sphere",           "40,12" },

            // SERPENTINE GRINDS
            {"grind_serpentine_flat",       "40,20" },
            {"grind_serpentine_cylinder",   "40,21" },

            // CIRCLAR GRINDS
            {"grind_circle_flat",           "40,30" },
            {"grind_circle_sphere",         "40,31" },

            // SPIRAL GRINDS
            {"grind_spiral_flat",           "40,40" },
        };
        private void LogInterpret(string command, int lineNumber, string line)
        {
            log.Info("EXEC {0:0000}: [{1}] {2}", lineNumber, command.ToUpper(), line);
        }
        private bool ExecuteLine(int lineNumber, string line)
        {
            CurrentLineLbl.Text = String.Format("Executing {0:000}: {1}", lineCurrentlyExecuting, line);
            string origLine = line;

            // 1) Ignore comments: drop anything from # onward in the line
            int index = line.IndexOf("#");
            if (index >= 0)
                line = line.Substring(0, index);

            // 2) Cleanup the line: replace all 2 or more whitespace with a single space and drop all leading/trailing whitespace
            string command = Regex.Replace(line, @"\s+", " ").Trim();

            // Skip blank lines or lines that previously had only comments
            if (command.Length < 1)
            {
                log.Info("EXEC {0:0000}: [REM] {1}", lineNumber, origLine);
                return true;
            }

            // =, incr, decr (assignment, ++, --)
            if (command.Contains("=") || command.Contains("++") || command.Contains("--"))
            {
                LogInterpret("assign", lineNumber, command);
                if (!WriteVariable(command))
                    PromptOperator(String.Format("Invalid assignment command: {0}", command));
                return true;
            }

            // Is line a label? If so, we ignore it!
            if (IsLineALabel(command).Success)
            {
                LogInterpret("label", lineNumber, command);
                return true;
            }

            // clear
            if (command == "clear()")
            {
                LogInterpret("clear", lineNumber, command);
                ClearVariablesBtn_Click(null, null);
                return true;
            }

            // import filename?
            if (command.StartsWith("import("))
            {
                LogInterpret("import", lineNumber, command);
                string file = ExtractParameters(command);
                if (file.Length > 1)
                {
                    if (!ImportFile(file))
                        PromptOperator(string.Format("File import error: {0}", command));
                }
                else
                    PromptOperator(String.Format("Invalid import command: {0}", command));

                return true;
            }

            // jump
            if (command.StartsWith("jump("))
            {
                string labelName = ExtractParameters(command);

                int jumpLine;
                if (labels.TryGetValue(labelName, out jumpLine))
                {
                    log.Info("EXEC {0:0000}: [JUMP] {1} --> {2:0000}", lineNumber, command, jumpLine);
                    SetCurrentLine(jumpLine);
                    return true;
                }
                else
                {
                    log.Error("Unknown Label specified in jump Line {0} Exec: {1}", lineNumber, command);
                    PromptOperator("Illegal Jump Command: " + command);
                    return true;
                }
            }

            // jump_gt_zero
            if (command.StartsWith("jump_gt_zero("))
            {
                string[] parameters = ExtractParameters(command).Split(',');
                bool wasSuccessful = false;
                if (parameters.Length != 2)
                {
                    PromptOperator("Expected jump_gt_zero(variable,label):\nNot " + command);
                    return true;
                }
                else
                {
                    string variableName = parameters[0];
                    string labelName = parameters[1];

                    int jumpLine;
                    if (!labels.TryGetValue(labelName, out jumpLine))
                    {
                        PromptOperator("Expected jump_gt_zero(variable,label):\nLabel not found: " + labelName);
                        return true;
                    }
                    else
                    {
                        string value = ReadVariable(variableName);
                        if (value == null)
                        {
                            PromptOperator("Expected jump_gt_zero(variable,label):\nVariable not found: " + variableName);
                            return true;
                        }
                        else
                        {
                            try
                            {
                                double val = Convert.ToDouble(value);
                                if (val > 0.0)
                                {
                                    log.Info("EXEC {0:0000}: [JUMPNOTZERO] {1} --> {2:0000}", lineNumber, command, jumpLine);
                                    SetCurrentLine(jumpLine);
                                }
                                wasSuccessful = true;
                                return true;
                            }
                            catch
                            {
                                PromptOperator(String.Format("Could not convert jump_not_zero variable: {0} = {1}\nFrom: {2}",variableName, value, command));
                                return true;
                            }
                        }
                        
                    }
                }
                if(!wasSuccessful)
                {
                    PromptOperator("Illegal jump_not_zero command: " + command);
                    return true;
                }
            }

            // end
            if (command == "end()")
            {
                LogInterpret("end", lineNumber, command);
                return false;
            }

            // home
            if (command == "home()")
            {
                LogInterpret("home", lineNumber, command);
                GotoHomeBtn_Click(null, null);
                return true;
            }

            // toolchange
            if (command.StartsWith("select_tool("))
            {
                LogInterpret("select_tool", lineNumber, command);
                DataRow row = FindTool(ExtractParameters(command));
                if (row == null)
                {
                    log.Error("Unknown tool specified in Exec: {0.000} {1}", lineNumber, command);
                    PromptOperator("Illegal select_tool command: " + command);
                    return true;
                }
                else
                {
                    GotoToolChangeBtn_Click(null, null);
                    ExecuteLine(-1, String.Format("set_tcp({0},{1},{2},{3},{4},{5})", row["x_m"], row["y_m"], row["z_m"], row["rx_m"], row["ry_m"], row["rz_m"]));
                    ExecuteLine(-1, String.Format("set_payload({0},{1},{2},{3})", row["mass_kg"], row["cogx_m"], row["cogy_m"], row["cogz_m"]));
                    messageForm = new MessageForm("Tool Change Prompt", "Please install tool for: " + command);
                    messageForm.ShowDialog();
                }
                return true;
            }

            // prompt
            if (command.StartsWith("prompt("))
            {
                LogInterpret("prompt", lineNumber, command);
                // This just displays the dialog. ExecTmr will wait for it to close
                PromptOperator(ExtractParameters(command));
                return true;
            }

            // sendrobot
            if (command.StartsWith("sendrobot("))
            {
                LogInterpret("sendrobot", lineNumber, command);
                robotServer.Send("(" + ExtractParameters(command) + ")");
                return true;
            }

            // All of the robot aliases
            // speed(1.1) ==> robotServer.Send("(30,1.1)")
            // grind_contact_enabled(0) ==> robotServer.Send("(40,1,0)")
            // etc.

            int openParenIndex = command.IndexOf("(");
            int closeParenIndex = command.IndexOf(")");
            if (openParenIndex > -1 && closeParenIndex > openParenIndex)
            {
                string commandInRecipe = command.Substring(0, openParenIndex);
                string commandToRobot;
                if (robotAlias.TryGetValue(commandInRecipe, out commandToRobot))
                {
                    LogInterpret(commandInRecipe, lineNumber, command);
                    robotServer.Send("(" + commandToRobot + "," + ExtractParameters(command) + ")");
                    return true;
                }
            }

            log.Error("Unknown Command Line {0} Exec: {1}", lineNumber, command);
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
                if (ReadVariable("robot_ready") != "True")
                {
                    // Only log this one time!
                    if (logFilter != 2)
                        log.Trace("Exec waiting for robot_ready");
                    logFilter = 2;
                }
                else
                {
                    // Resets such that the above log messages will happen
                    logFilter = 3;
                    if (lineCurrentlyExecuting + 1 >= RecipeRoRTB.Lines.Count())
                    {
                        log.Info("EXEC Reached end of file");
                        SetState(RunState.READY);
                    }
                    else
                    {
                        SetCurrentLine(lineCurrentlyExecuting + 1);
                        string line = RecipeRoRTB.Lines[lineCurrentlyExecuting];
                        bool fContinue = ExecuteLine(lineCurrentlyExecuting, line);
                        if (!fContinue)
                        {
                            log.Info("EXEC Aborting execution");
                            SetState(RunState.READY);
                        }
                    }
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

                RobotStatusLbl.BackColor = Color.Red;
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


        private void SetBlendBtn_Click(object sender, EventArgs e)
        {
            if (robotServer != null)
                if (robotServer.IsConnected())
                {
                    robotServer.Send("(32," + BlendTxt.Text + ")");
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


        private Color ColorFromBooleanName(string name)
        {
            switch (name)
            {
                case "True":
                    return Color.Green;
                case "False":
                    return Color.Red;
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
        public bool WriteVariable(string name, string value, bool isSystem = false)
        {
            System.Threading.Monitor.Enter(lockObject);
            string nameTrimmed = name.Trim();
            string valueTrimmed = value.Trim();
            log.Trace("WriteVariable({0}, {1})", nameTrimmed, valueTrimmed);
            if (variables == null)
            {
                log.Error("variables == null!!??");
                return false;
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
                case "robot_ready":
                    RobotReadyLbl.BackColor = ColorFromBooleanName(valueTrimmed);
                    break;
                case "grind_ready":
                    GrindReadyLbl.BackColor = ColorFromBooleanName(valueTrimmed);
                    break;
                case "robot_speed":
                    RobotSpeedLbl.Text = "Speed\n" + valueTrimmed;
                    SpeedTxt.Text = valueTrimmed;
                    break;
                case "robot_accel":
                    RobotAccelLbl.Text = "Accel\n" + valueTrimmed;
                    AccelTxt.Text = valueTrimmed;
                    break;
                case "robot_blend":
                    RobotBlendLbl.Text = "Blend\n" + valueTrimmed;
                    BlendTxt.Text = valueTrimmed;
                    break;
                case "grind_contact_enabled":
                    GrindContactEnabledBtn.BackColor = ColorFromBooleanName(valueTrimmed);
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
            return true;
        }

        /// <summary>
        /// Takes a "name=value" string and set variable "name" equal to "value"  ALSO: will handle name++ and name--
        /// </summary>
        /// <param name="assignment">Variable to be modified</param>
        public bool WriteVariable(string assignment, bool isSystem = false)
        {
            bool wasSuccessful = false;
            string[] s = assignment.Split('=');
            if (s.Length == 2)
            {
                wasSuccessful = WriteVariable(s[0], s[1], isSystem);
            }
            else
            {
                // Not a classic assignment... look for ++ and --
                if (assignment.Length > 2)
                {
                    int incr = 0;
                    if (assignment.EndsWith("++")) incr = 1;
                    if (assignment.EndsWith("--")) incr = -1;
                    if (incr != 0)
                    {
                        try
                        {
                            string name = assignment.Substring(0, assignment.Length - 2);
                            string v = ReadVariable(name);
                            if (v != null)
                            {
                                double x = Convert.ToDouble(v);
                                x += incr;
                                wasSuccessful = WriteVariable(name, x.ToString());
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (!wasSuccessful)
            {
                log.Error("Illegal assignment statement: {0}", assignment);
            }
            return wasSuccessful;
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
            if (DialogResult.Yes == ConfirmMessageBox("This will clear all variables INCLUDING system variables. Proceed?"))
                ClearAndInitializeVariables();
        }

        // ===================================================================
        // END VARIABLE SYSTEM
        // ===================================================================

        // ===================================================================
        // START TOOL SYSTEM
        // ===================================================================

        readonly string toolsFilename = "Tools.var";
        private void ClearAndInitializeTools()
        {
            tools = new DataTable("Tools");
            DataColumn name = tools.Columns.Add("Name", typeof(System.String));
            tools.Columns.Add("x_m", typeof(System.Double));
            tools.Columns.Add("y_m", typeof(System.Double));
            tools.Columns.Add("z_m", typeof(System.Double));
            tools.Columns.Add("rx_m", typeof(System.Double));
            tools.Columns.Add("ry_m", typeof(System.Double));
            tools.Columns.Add("rz_m", typeof(System.Double));
            tools.Columns.Add("mass_kg", typeof(System.Double));
            tools.Columns.Add("cogx_m", typeof(System.Double));
            tools.Columns.Add("cogy_m", typeof(System.Double));
            tools.Columns.Add("cogz_m", typeof(System.Double));
            tools.CaseSensitive = true;
            tools.PrimaryKey = new DataColumn[] { name };

            ToolsGrd.DataSource = tools;
        }
        private void LoadToolsBtn_Click(object sender, EventArgs e)

        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", toolsFilename);
            log.Info("LoadTools from {0}", filename);
            ClearAndInitializeTools();
            try
            {
                tools.ReadXml(filename);

            }
            catch
            { }

            ToolsGrd.DataSource = tools;
            foreach (DataGridViewColumn col in ToolsGrd.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }


        private void SaveToolsBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", toolsFilename);
            log.Info("SaveTools to {0}", filename);
            tools.AcceptChanges();
            tools.WriteXml(filename, XmlWriteMode.WriteSchema, true);
        }

        private void ClearToolsBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == ConfirmMessageBox("This will clear all tools. Proceed?"))
            {

                ClearAndInitializeTools();
                tools.Rows.Add(new object[] { "default", 0, 0, 0.175, 0, -Math.PI, 0, 2.4, 0, 0, 0.080 });
            }
        }

        private DataRow FindTool(string name)
        {
            foreach (DataRow row in tools.Rows)
            {
                if ((string)row["Name"] == name)
                {
                    log.Trace("FindTool({0}) = {1}", row["Name"], row.ToString());
                    return row;
                }
            }
            return null;
        }

        // ===================================================================
        // END TOOL SYSTEM
        // ===================================================================
    }
}
