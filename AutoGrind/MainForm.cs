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
        TcpServerSupport robotCommandServer = null;
        TcpClientSupport robotDashboardClient = null;
        MessageForm messageForm = null;

        static DataTable variables;
        static DataTable tools;
        static DataTable positions;

        private enum RunState
        {
            INIT,
            IDLE,
            READY,
            RUNNING,
            PAUSED
        }
        RunState runState = RunState.INIT;

        private enum OperatorMode
        {
            OPERATOR,
            EDITOR,
            ENGINEERING
        }
        OperatorMode operatorMode = OperatorMode.OPERATOR;

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
            OperatorModeBox.SelectedIndex = (int)operatorMode;

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

        private DialogResult ConfirmMessageBox(string question)
        {
            messageForm = new MessageForm("AutoGrind Confirmation", question, "Yes", "No");
            DialogResult result = messageForm.ShowDialog();
            return result;
        }
        private DialogResult ErrorMessageBox(string message)
        {
            messageForm = new MessageForm("AutoGrind Error", message, "OK", "Cancel");
            DialogResult result = messageForm.ShowDialog();
            return result;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (forceClose) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = ConfirmMessageBox("Do you want to close the application?");
                e.Cancel = (result != DialogResult.OK);
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
        int nDashboard = 0;
        bool pollDashboardStateNow = false;
        private void HeartbeatTmr_Tick(object sender, EventArgs e)
        {
            string now = DateTime.Now.ToString("s");
            timeLbl.Text = now;

            // Ping the dashboard every 5th second
            if (robotDashboardClient != null && ((nDashboard++ % 5) == 0 || pollDashboardStateNow))
                if (robotDashboardClient.IsClientConnected)
                {
                    pollDashboardStateNow = false;
                    // Poll and interpret robotmode
                    string robotmodeResponse = robotDashboardClient.InquiryResponse("robotmode");
                    Color color = Color.Red;
                    string buttonText = robotmodeResponse;
                    switch (robotmodeResponse)
                    {
                        case "Robotmode: RUNNING":
                            color = Color.Green;
                            break;
                        case "Robotmode: IDLE":
                            color = Color.Blue;
                            break;
                        case "Robotmode: POWER_OFF":
                            color = Color.Red;
                            break;
                        case "Robotmode: POWER_ON":
                            color = Color.Blue;
                            break;
                        case "Robotmode: BOOTING":
                            color = Color.Coral;
                            break;
                        default:
                            buttonText = "?? " + robotmodeResponse;
                            color = Color.Red;
                            break;
                    }
                    RobotModeBtn.Text = buttonText;
                    RobotModeBtn.BackColor = color;

                    // Poll and interpret safetystatus
                    robotmodeResponse = robotDashboardClient.InquiryResponse("safetystatus");
                    color = Color.Red;
                    buttonText = robotmodeResponse;
                    switch (robotmodeResponse)
                    {
                        case "Safetystatus: NORMAL":
                            color = Color.Green;
                            break;
                        case "Safetystatus: PROTECTIVE_STOP":
                            color = Color.Red;
                            break;
                        default:
                            buttonText = "?? " + robotmodeResponse;
                            color = Color.Red;
                            break;
                    }
                    SafetyStatusBtn.Text = buttonText;
                    SafetyStatusBtn.BackColor = color;

                    // Poll and interpret programstate
                    robotmodeResponse = robotDashboardClient.InquiryResponse("programstate",100);
                    color = Color.Red;
                    if(robotmodeResponse!=null)
                        if (robotmodeResponse.StartsWith("PLAYING"))
                            color = Color.Green;
                    ProgramStateBtn.Text = robotmodeResponse;
                    ProgramStateBtn.BackColor = color;
                }

            // Manage whether robot command is connected and init when it does
            bool newRobotReady = false;
            if (robotCommandServer != null)
            {
                if (robotCommandServer.IsClientConnected)
                    newRobotReady = true;

                if (newRobotReady != robotReady)
                {
                    robotReady = newRobotReady;
                    if (robotReady)
                    {
                        log.Info("Changing robot connection to READY");

                        // Send defaults speeds and accelerations
                        ExecuteLine(-1, string.Format("set_linear_speed({0})", ReadVariable("robot_speed", "200")));
                        ExecuteLine(-1, string.Format("set_linear_accel({0})", ReadVariable("robot_accel", "500")));
                        ExecuteLine(-1, string.Format("set_blend_radius({0})", ReadVariable("robot_blend", "3")));
                        ExecuteLine(-1, string.Format("set_joint_speed({0})", ReadVariable("robot_joint_speed", "90")));
                        ExecuteLine(-1, string.Format("set_joint_accel({0})", ReadVariable("robot_joint_accel", "180")));
                        ExecuteLine(-1, "grind_contact_enabled(0)");  // Set contact enabled = False

                        // Download selected tool and part geometry by acting like a reselect of both
                        MountedToolBox_SelectedIndexChanged(null, null);
                        PartGeometryBox_SelectedIndexChanged(null, null);

                        RobotCommandStatusLbl.BackColor = Color.Green;
                        RobotCommandStatusLbl.Text = "Command Ready";
                        // Restore all button settings with same current state
                        SetState(runState, true, true);
                    }
                    else
                    {
                        log.Info("Change robot connection to WAIT");
                        RobotCommandStatusLbl.BackColor = Color.Red;
                        RobotCommandStatusLbl.Text = "WAIT";
                        // Restore all button settings with same current state
                        SetState(runState, true, true);
                    }
                }

            }
        }


        // ===================================================================
        // START MAIN UI BUTTONS
        // ===================================================================

        // This forces the log RTBs to all update... otherwise there are artifacts left over from NLog the first time in on program start
        private void MainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = MainTab.TabPages[MainTab.SelectedIndex].Text;
            log.Info("Main Tab changed to " + tabName);

            if (tabName == "Log")
            {
                for (int i = 0; i < 2; i++)
                {
                    AllLogRTB.Refresh();
                    ExecLogRTB.Refresh();
                    UrLogRTB.Refresh();
                    ErrorLogRTB.Refresh();
                }
            }
        }


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
                    //GrindBtn.Enabled = true;
                    //EditBtn.Enabled = true;
                    //SetupBtn.Enabled = true;
                    ExitBtn.Enabled = true;
                    JogBtn.Enabled = robotReady;
                    //LoadBtn.Enabled = true;
                    StartBtn.Enabled = false;
                    PauseBtn.Enabled = false;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    ExecTmr.Enabled = false;
                    CurrentLineLbl.Text = "";
                    break;
                case RunState.READY:
                    //GrindBtn.Enabled = true;
                    //EditBtn.Enabled = true;
                    //SetupBtn.Enabled = true;
                    ExitBtn.Enabled = true;
                    JogBtn.Enabled = robotReady;
                    //LoadBtn.Enabled = true;
                    StartBtn.Enabled = true;
                    PauseBtn.Enabled = false;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    ExecTmr.Enabled = false;
                    CurrentLineLbl.Text = "";
                    break;
                case RunState.RUNNING:
                    //GrindBtn.Enabled = false;
                    //EditBtn.Enabled = false;
                    //SetupBtn.Enabled = false;
                    ExitBtn.Enabled = false;
                    JogBtn.Enabled = false;
                    //LoadBtn.Enabled = false;
                    StartBtn.Enabled = false;
                    PauseBtn.Enabled = true;
                    ContinueBtn.Enabled = false;
                    StopBtn.Enabled = true;
                    CurrentLineLbl.Text = "";

                    ExecTmr.Interval = 100;
                    ExecTmr.Enabled = true;

                    break;
                case RunState.PAUSED:
                    //GrindBtn.Enabled = false;
                    //EditBtn.Enabled = false;
                    //SetupBtn.Enabled = false;
                    ExitBtn.Enabled = false;
                    JogBtn.Enabled = false;
                    //LoadBtn.Enabled = false;
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
                //GrindBtn.BackColor = GrindBtn.Enabled ? Color.Green : Color.Gray;
                //EditBtn.BackColor = EditBtn.Enabled ? Color.LightGreen : Color.Gray;
                //SetupBtn.BackColor = SetupBtn.Enabled ? Color.LightGreen : Color.Gray;
                ExitBtn.BackColor = ExitBtn.Enabled ? Color.Green : Color.Gray;
            }

            JogBtn.BackColor = JogBtn.Enabled ? Color.Green : Color.Gray;
            //LoadBtn.BackColor = LoadBtn.Enabled ? Color.Green : Color.Gray;
            StartBtn.BackColor = StartBtn.Enabled ? Color.Green : Color.Gray;
            PauseBtn.BackColor = PauseBtn.Enabled ? Color.DarkOrange : Color.Gray;
            ContinueBtn.BackColor = ContinueBtn.Enabled ? Color.Green : Color.Gray;
            StopBtn.BackColor = StopBtn.Enabled ? Color.Red : Color.Gray;
        }
        private void MountedToolBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            log.Info("Operator changing tool to " + MountedToolBox.Text);

            ToolsGrd.ClearSelection();
            if (robotCommandServer != null)
                ExecuteLine(-1, String.Format("select_tool({0})", MountedToolBox.Text));
            PartGeometryBox.Text = "FLAT";
        }

        private void UpdateGeometryToRobot()
        {
            if (robotCommandServer != null)
                ExecuteLine(-1, String.Format("set_part_geometry_N({0},{1})", PartGeometryBox.SelectedIndex + 1, PartGeometryBox.SelectedIndex == 0 ? "0" : DiameterLbl.Text));
        }

        private void PartGeometryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            log.Info("Operator changing geometry to " + PartGeometryBox.Text);
            bool isFlat = PartGeometryBox.Text == "FLAT";
            if (isFlat)
            {
                DiameterLbl.Visible = false;
                DiameterDimLbl.Visible = false;
            }
            else
            {
                DiameterLbl.Visible = true;
                DiameterDimLbl.Visible = true;
            }

            UpdateGeometryToRobot();
        }

        //const int standardWidth = 1050;
        const int fullWidth = 1920;
        private void OperatorModeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperatorMode origOperatorMode = operatorMode;

            OperatorMode newOperatorMode = (OperatorMode)OperatorModeBox.SelectedIndex;
            log.Info(string.Format("OperatorMode changing to {0}", newOperatorMode));
            SetValueForm form;
            switch (newOperatorMode)
            {
                case OperatorMode.OPERATOR:
                    //Width = standardWidth;
                    break;
                case OperatorMode.EDITOR:
                    form = new SetValueForm("", "Please enter passcode for EDITOR", 0, true);
                    if (form.ShowDialog(this) != DialogResult.OK || form.value != "9")
                    {
                        OperatorModeBox.SelectedIndex = 0;
                        return;
                    }
                    break;
                case OperatorMode.ENGINEERING:
                    // TEMPORARILY DON"T NEED PASSWORD
                    // form = new SetValueForm("", "Please enter passcode for ENGINEERING",0,true);
                    /*
                    if (form.ShowDialog(this) != DialogResult.OK || form.value != "99")
                    {
                        OperatorModeBox.SelectedIndex = 0;
                        return;
                    }
                    */
                    break;
            }

            operatorMode = newOperatorMode;
            switch (operatorMode)
            {
                case OperatorMode.OPERATOR:
                    //Width = standardWidth;
                    //EditBtn.Visible = false;
                    //SetupBtn.Visible = false;
                    //OperationTab.TabPages["EditTab"].Visible = false;
                    //OperationTab.TabPages["SetupTab"].Visible = false;
                    //MonitorTab.Visible = false;


                    break;
                case OperatorMode.EDITOR:
                    //Width = standardWidth;
                    //EditBtn.Visible = true;
                    //SetupBtn.Visible = false;
                    //OperationTab.TabPages["EditTab"].Visible = true;
                    //OperationTab.TabPages["SetupTab"].Visible = false;
                    //MonitorTab.Visible = true;
                    break;
                case OperatorMode.ENGINEERING:
                    //Width = fullWidth;
                    //EditBtn.Visible = true;
                    //SetupBtn.Visible = true;
                    //OperationTab.TabPages["EditTab"].Visible = true;
                    //OperationTab.TabPages["SetupTab"].Visible = true;
                    //MonitorTab.Visible = true;
                    break;
            }
        }


        private void GrindBtn_Click(object sender, EventArgs e)
        {
            //SetState(RunState.IDLE);
            //OperationTab.SelectedTab = OperationTab.TabPages["GrindTab"];
            //GrindBtn.BackColor = Color.Green;
            //EditBtn.BackColor = Color.LightGreen;
            //SetupBtn.BackColor = Color.LightGreen;

            // Get latest from edit control
            RecipeRTB.Text = RecipeRTB.Text;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            //SetState(RunState.IDLE);
            //OperationTab.SelectedTab = OperationTab.TabPages["EditTab"];
            //GrindBtn.BackColor = Color.LightGreen;
            //EditBtn.BackColor = Color.Green;
            //SetupBtn.BackColor = Color.LightGreen;
        }

        private void SetupBtn_Click(object sender, EventArgs e)
        {
            //SetState(RunState.IDLE);
            //OperationTab.SelectedTab = OperationTab.TabPages["SetupTab"];
            //GrindBtn.BackColor = Color.LightGreen;
            //EditBtn.BackColor = Color.LightGreen;
            //SetupBtn.BackColor = Color.Green;
        }

        private void RobotModeBtn_Click(object sender, EventArgs e)
        {
            switch (RobotModeBtn.Text)
            {
                case "Robotmode: RUNNING":
                    robotDashboardClient?.InquiryResponse("power off");
                    break;
                case "Robotmode: IDLE":
                    robotDashboardClient?.InquiryResponse("brake release");
                    break;
                case "Robotmode: POWER_OFF":
                    robotDashboardClient?.InquiryResponse("power on");
                    break;
                default:
                    log.Error("Unknown robot mode button state! {0}", RobotModeBtn.Text);
                    break;
            }
            pollDashboardStateNow = true;

        }

        private void SafetyStatusBtn_Click(object sender, EventArgs e)
        {
            switch (SafetyStatusBtn.Text)
            {
                case "Safetystatus: NORMAL":
                    robotDashboardClient?.InquiryResponse("power off");
                    break;
                case "Safetystatus: PROTECTIVE_STOP":
                    robotDashboardClient?.InquiryResponse("unlock protective stop");
                    break;
                default:
                    log.Error("Unknown robot mode button state! {0}", RobotModeBtn.Text);
                    break;
            }
            pollDashboardStateNow = true;
        }

        private void ProgramStateBtn_Click(object sender, EventArgs e)
        {
            if (ProgramStateBtn.Text.StartsWith("PLAYING"))
            {
                robotCommandServer?.Send("(99)");
                robotDashboardClient?.InquiryResponse("stop", 500);
                RobotCommandStatusLbl.BackColor = Color.Red;
                RobotCommandStatusLbl.Text = "OFF";

            }
            else
                robotDashboardClient?.InquiryResponse("play", 500);

            pollDashboardStateNow = true;
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
                    if (operatorMode == OperatorMode.OPERATOR)
                        GrindBtn_Click(null, null);
                    else
                        EditBtn_Click(null, null);
                    break;
                case "Setup":
                    if (operatorMode == OperatorMode.OPERATOR || operatorMode == OperatorMode.EDITOR)
                        GrindBtn_Click(null, null);
                    else
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

        private void GrindContactEnabledBtn_Click(object sender, EventArgs e)
        {
            if (robotCommandServer != null)
                if (robotCommandServer.IsConnected())
                {
                    robotCommandServer.Send(String.Format("(40,1,{0})", GrindContactEnabledBtn.BackColor != Color.Green ? 1 : 0));
                }
        }


        private void StartBtn_Click(object sender, EventArgs e)
        {
            log.Info("StartBtn_Click(...)");
            if (!robotReady)
            {
                var result = ConfirmMessageBox("Robot not connected. Run anyway?");
                if (result != DialogResult.OK) return;
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
            robotCommandServer.Send("(10)");  // This will cancel any grind in progress
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
            robotCommandServer.Send("(10)");  // This will cancel any grind in progress
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
            RecipeRTB.Text = "";
            try
            {
                RecipeRTB.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.PlainText);
                RecipeRTB.Modified = false;
                RecipeFilenameLbl.Text = file;
                RecipeFilenameOnlyLbl.Text = Path.GetFileName(file);

                // Copy from the edit window to the runtime window
                RecipeRTB.Text = RecipeRTB.Text;
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
                if (result == DialogResult.OK)
                    SaveRecipeBtn_Click(null, null);
            }

            SetRecipeState(RecipeState.NEW);
            SetState(RunState.IDLE, true);
            RecipeFilenameLbl.Text = "Untitled";
            RecipeFilenameOnlyLbl.Text = "Untitled";
            RecipeRTB.Clear();
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
            RobotIpTxt.Text = "192.168.25.1:30000";
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
            Left = 0;// (Int32)AppNameKey.GetValue("Left", 0);
            Top = 0;// (Int32)AppNameKey.GetValue("Top", 0);
            Width = 2160;// (Int32)AppNameKey.GetValue("Width", 1920);
            Height = 1440;

            // From Setup Tab
            AutoGrindRoot = (string)AppNameKey.GetValue("AutoGrindRoot", "\\");
            AutoGrindRootLbl.Text = AutoGrindRoot;
            RobotProgramTxt.Text = (string)AppNameKey.GetValue("RobotProgramTxt.Text", "AutoGrind/AutoGrind01.urp");
            RobotIpTxt.Text = (string)AppNameKey.GetValue("RobotIpTxt.Text", "192.168.0.2");
            ServerIpTxt.Text = (string)AppNameKey.GetValue("ServerIpTxt.Text", "192.168.0.252");
            UtcTimeChk.Checked = Convert.ToBoolean(AppNameKey.GetValue("UtcTimeChk.Checked", "True"));

            // Operator Mode
            operatorMode = (OperatorMode)(Int32)AppNameKey.GetValue("operatorMode", 0);
            OperatorModeBox.SelectedIndex = (int)operatorMode;

            // Debug Level selection
            DebugLevelCombo.Text = (string)AppNameKey.GetValue("DebugLevelCombo.Text", "INFO");

            // Load the tools table
            LoadToolsBtn_Click(null, null);

            // Load the positions table
            LoadPositionsBtn_Click(null, null);

            // Load the variables table
            LoadVariablesBtn_Click(null, null);

            // Load the User's Manual
            LoadManualBtn_Click(null, null);

            // Autoload file is the last loaded recipe
            recipeFileToAutoload = (string)AppNameKey.GetValue("RecipeFilenameLbl.Text", "");

            // Retrieve currently mounted tool
            MountedToolBox.Text = (string)AppNameKey.GetValue("MountedToolBox.Text", "");

            // Retrieve current part geometry
            //    TODO Forcing this to FLAT, 250.0 since we don't trust the stored diameter
            PartGeometryBox.Text = "FLAT"; // (string)AppNameKey.GetValue("PartGeometryBox.Text", "FLAT");
            DiameterLbl.Text = (string)AppNameKey.GetValue("DiameterLbl.Text", "250.0");
        }

        void SavePersistent()
        {
            log.Info("SavePersistent()");

            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");

            // Window State
            AppNameKey.SetValue("Left", Left);
            AppNameKey.SetValue("Top", Top);
            AppNameKey.SetValue("Width", Width);
            AppNameKey.SetValue("Height", Width);

            // From Setup Tab
            AppNameKey.SetValue("AutoGrindRoot", AutoGrindRoot);
            AppNameKey.SetValue("RobotProgramTxt.Text", RobotProgramTxt.Text);
            AppNameKey.SetValue("RobotIpTxt.Text", RobotIpTxt.Text);
            AppNameKey.SetValue("ServerIpTxt.Text", ServerIpTxt.Text);
            AppNameKey.SetValue("UtcTimeChk.Checked", UtcTimeChk.Checked);

            // Operator Mode
            AppNameKey.SetValue("operatorMode", (Int32)operatorMode);

            // Debug Level selection
            AppNameKey.SetValue("DebugLevelCombo.Text", DebugLevelCombo.Text);

            // Save currently mounted tool and tools table
            AppNameKey.SetValue("MountedToolBox.Text", MountedToolBox.Text);
            SaveToolsBtn_Click(null, null);

            // Save the positions table
            SavePositionsBtn_Click(null, null);

            // Save the variables table
            SaveVariablesBtn_Click(null, null);

            // Save currently loaded recipe
            AppNameKey.SetValue("RecipeFilenameLbl.Text", RecipeFilenameLbl.Text);


            // Save current part geometry tool
            AppNameKey.SetValue("PartGeometryBox.Text", PartGeometryBox.Text);
            AppNameKey.SetValue("DiameterLbl.Text", DiameterLbl.Text);
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

        private void ChangeRootDirectoryBtn_Click(object sender, EventArgs e)
        {
            log.Info("ChangeRootDirectoryBtn_Click(...)");
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
            string partName = PartGeometryBox.Text;
            if (partName != "FLAT")
                partName += " " + DiameterLbl.Text + " mm DIA";

            JoggingForm form = new JoggingForm(robotCommandServer, this, "Jog to Defect", ReadVariable("robot_tool"), partName);

            form.ShowDialog(this);
        }

        private void DiameterLbl_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(DiameterLbl.Text, PartGeometryBox.Text + " DIAM, MM", 1);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                DiameterLbl.Text = form.value;
            }

            UpdateGeometryToRobot();
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
            foreach (string line in RecipeRTB.Lines)
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
                int start = RecipeRTB.GetFirstCharIndexFromLine(lineCurrentlyExecuting);
                int length = RecipeRTB.Lines[lineCurrentlyExecuting].Length;
                RecipeRTB.Select(start, length);
            }
        }


        static int lineCurrentlyExecuting = 0;

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
        string ExtractParameters(string s, int nParams = 0)
        {
            try
            {
                string parameters = s.Split('(', ')')[1];

                //if nParams is specified, verify we have the right number!
                if (nParams > 0)
                {
                    string[] p = parameters.Split(',');
                    if (p.Length != nParams)
                        return "";
                }
                return parameters;
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

        // Specifies number of expected parameters and prefix in SendRobot for each function
        public struct CommandSpec
        {
            public int nParams;
            public string prefix;
        };

        Dictionary<string, CommandSpec> robotAlias = new Dictionary<string, CommandSpec>
        {
            // SETTINGS
            {"set_linear_speed",        new CommandSpec(){nParams=1, prefix="30,1" } },
            {"set_linear_accel",        new CommandSpec(){nParams=1, prefix="30,2" } },
            {"set_blend_radius",        new CommandSpec(){nParams=1, prefix="30,3" } },
            {"set_joint_speed",         new CommandSpec(){nParams=1, prefix="30,4" } },
            {"set_joint_accel",         new CommandSpec(){nParams=1, prefix="30,5" } },
            {"set_part_geometry_N",     new CommandSpec(){nParams=2, prefix="30,6" } },
            {"set_tcp",                 new CommandSpec(){nParams=6, prefix="30,10" } },
            {"set_payload",             new CommandSpec(){nParams=4, prefix="30,11" } },
            {"grind_contact_enabled",   new CommandSpec(){nParams=1, prefix="40,1" } },

            {"grind_line",              new CommandSpec(){nParams=5, prefix="40,10" }  },
            {"grind_rect",              new CommandSpec(){nParams=5, prefix="40,20" }  },
            {"grind_serpentine",        new CommandSpec(){nParams=7, prefix="40,30" }  },
            {"grind_circle",            new CommandSpec(){nParams=4, prefix="40,40" }  },
            {"grind_spiral",            new CommandSpec(){nParams=6, prefix="40,50" }  },
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
            if (command == "clear()" || command == "clear")
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
                //bool wasSuccessful = false;
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
                                    log.Info("EXEC {0:0000}: [JUMPGTZERO] {1} --> {2:0000}", lineNumber, command, jumpLine);
                                    SetCurrentLine(jumpLine);
                                }
                                //wasSuccessful = true;
                                return true;
                            }
                            catch
                            {
                                PromptOperator(String.Format("Could not convert jump_not_zero variable: {0} = {1}\nFrom: {2}", variableName, value, command));
                                return true;
                            }
                        }

                    }
                }
            }

            // movejoint
            if (command.StartsWith("movejoint("))
            {
                string positionName = ExtractParameters(command);
                LogInterpret("movejoint", lineNumber, command);

                if (!GotoPositionJoint(positionName))
                {
                    log.Error("Unknown position name specified in movejoint Line {0} Exec: {1}", lineNumber, command);
                    PromptOperator("Illegal position name: " + command);
                }
                return true;
            }

            // movepose
            if (command.StartsWith("movepose("))
            {
                string positionName = ExtractParameters(command);
                LogInterpret("movepose", lineNumber, command);

                if (!GotoPositionPose(positionName))
                {
                    log.Error("Unknown position name specified in movepose Line {0} Exec: {1}", lineNumber, command);
                    PromptOperator("Illegal position name: " + command);
                }
                return true;
            }

            // end
            if (command == "end()" || command == "end")
            {
                LogInterpret("end", lineNumber, command);
                return false;
            }

            // select_tool  (Assumes operator has already installed it somehow!!)
            if (command.StartsWith("select_tool("))
            {
                LogInterpret("select_tool", lineNumber, command);
                DataRow row = FindTool(ExtractParameters(command, 1));
                if (row == null)
                {
                    log.Error("Unknown tool specified in Exec: {0.000} {1}", lineNumber, command);
                    PromptOperator("Illegal select_tool command: " + command);
                    return true;
                }
                else
                {
                    ExecuteLine(-1, String.Format("set_tcp({0},{1},{2},{3},{4},{5})", row["x_m"], row["y_m"], row["z_m"], row["rx_rad"], row["ry_rad"], row["rz_rad"]));
                    ExecuteLine(-1, String.Format("set_payload({0},{1},{2},{3})", row["mass_kg"], row["cogx_m"], row["cogy_m"], row["cogz_m"]));
                    WriteVariable("robot_tool=" + row["Name"]);
                    MountedToolBox.Text = (string)row["Name"];
                }
                return true;
            }

            // set_part_geometry
            if (command.StartsWith("set_part_geometry("))
            {
                LogInterpret("set_part_geometry", lineNumber, command);

                string parameters = ExtractParameters(command, 2);
                if (parameters.Length == 0)
                {
                    log.Error("Illegal parameters for set_part_geometry Exec: {0.000} {1}", lineNumber, command);
                    PromptOperator("Illegal set_part_geometry command:\n" + command);
                    return true;
                }
                string[] paramList = parameters.Split(',');
                if (paramList.Length != 2)
                {
                    log.Error("Illegal parameters for set_part_geometry Exec: {0.000} {1}", lineNumber, command);
                    PromptOperator("Illegal set_part_geometry command:\n" + command);
                    return true;
                }

                int geomIndex = 0;
                switch (paramList[0])
                {
                    case "FLAT":
                        geomIndex = 1;
                        break;
                    case "CYLINDER":
                        geomIndex = 2;
                        DiameterLbl.Text = paramList[1];
                        break;
                    case "SPHERE":
                        geomIndex = 3;
                        DiameterLbl.Text = paramList[1];
                        break;
                    default:
                        log.Error("First argument to must be FLAT, CYLINDER, or SPHERE Exec: {0.000} {1}", lineNumber, command);
                        PromptOperator("First argument to must be FLAT, CYLINDER, or SPHERE:\n" + command);
                        return true;
                }

                ExecuteLine(-1, String.Format("set_part_geometry_N({0},{1})", geomIndex, paramList[1]));
                PartGeometryBox.Text = paramList[0];
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
                robotCommandServer.Send("(" + ExtractParameters(command) + ")");
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
                CommandSpec commandSpec;
                if (robotAlias.TryGetValue(commandInRecipe, out commandSpec))
                {
                    LogInterpret(commandInRecipe, lineNumber, command);
                    string parameters = ExtractParameters(command, commandSpec.nParams);
                    if (parameters.Length > 0 || commandSpec.nParams == 0)
                        robotCommandServer.Send("(" + commandSpec.prefix + "," + parameters + ")");
                    else
                        PromptOperator(string.Format("Wrong number of operands. Expected {0}:\n{1}", commandSpec.nParams, command));
                    return true;
                }
            }

            log.Error("Unknown Command Line {0} Exec: {1}", lineNumber, command);
            PromptOperator("Illegal recipe line:\n" + command);
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
                    log.Info("EXEC Waiting for robotReady");
                logFilter = 1;
            }
            else
            {
                if (ReadVariable("robot_ready") != "True")
                {
                    // Only log this one time!
                    if (logFilter != 2)
                        log.Info("EXEC Waiting for robot_ready");
                    logFilter = 2;
                }
                else
                {
                    // Resets such that the above log messages will happen
                    logFilter = 3;
                    if (lineCurrentlyExecuting + 1 >= RecipeRTB.Lines.Count())
                    {
                        log.Info("EXEC Reached end of file");
                        SetState(RunState.READY);
                    }
                    else
                    {
                        SetCurrentLine(lineCurrentlyExecuting + 1);
                        string line = RecipeRTB.Lines[lineCurrentlyExecuting];
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

            // Connect client to the UR dashboard
            robotDashboardClient = new TcpClientSupport("DASH")
            {
                ReceiveCallback = DashboardCallback
            };
            if (robotDashboardClient.Connect(RobotIpTxt.Text, "29999") > 0)
            {
                log.Error("Robot dashboard client initialization failure");
                RobotDashboardStatusLbl.BackColor = Color.Red;
                RobotDashboardStatusLbl.Text = "Dashboard Error";
                return;
            }
            else
            {
                log.Info("Robot dashboard connection ready");

                RobotModelLbl.Text = robotDashboardClient.InquiryResponse("get robot model");
                RobotSerialNumberLbl.Text = robotDashboardClient.InquiryResponse("get serial number");
                robotDashboardClient.InquiryResponse("stop");

                pollDashboardStateNow = true;


                string loadedProgramResponse = robotDashboardClient.InquiryResponse("load " + RobotProgramTxt.Text, 1000);
                if (loadedProgramResponse == null)
                {
                    log.Error("Failed to load {0}. No response.", RobotProgramTxt.Text);
                    ErrorMessageBox(String.Format("Failed to load {0}. No response.", RobotProgramTxt.Text));
                    return;
                }
                if (loadedProgramResponse.StartsWith("File not found"))
                {
                    log.Error("Failed to load {0}. Response was \"{1}\"", RobotProgramTxt.Text, loadedProgramResponse);
                    ErrorMessageBox(String.Format("Failed to load {0}. Response was \"{1}\"", RobotProgramTxt.Text, loadedProgramResponse));
                    return;
                }

                string getLoadedProgramResponse = robotDashboardClient.InquiryResponse("get loaded program", 1000);
                if (getLoadedProgramResponse == null)
                {
                    log.Error("Failed to verify loading {0}. No response.", RobotProgramTxt.Text);
                    ErrorMessageBox(String.Format("Failed to verify loading {0}. No response", RobotProgramTxt.Text));
                    return;
                }

                if (!getLoadedProgramResponse.Contains(RobotProgramTxt.Text))
                {
                    log.Error("Failed to verify loading {0}. Response was \"{1}\"", RobotProgramTxt.Text, getLoadedProgramResponse);
                    ErrorMessageBox(String.Format("Failed to verify loading {0}. Response was \"{1}\"", RobotProgramTxt.Text, getLoadedProgramResponse));
                    return;
                }

                string playResponse = robotDashboardClient.InquiryResponse("play", 1000);
                if (!playResponse.StartsWith("Starting program"))
                {
                    log.Error("Failed to start program playing. Response was \"{0}\"", playResponse);
                    ErrorMessageBox(String.Format("Failed to start program playing. Response was \"{0}\"", playResponse));
                    return;
                }

                RobotDashboardStatusLbl.BackColor = Color.Green;
                RobotDashboardStatusLbl.Text = "Dashboard Ready";
            }

            // Setup a server for the UR to connect to
            robotCommandServer = new TcpServerSupport()
            {
                ReceiveCallback = CommandCallback
            };
            if (robotCommandServer.Connect(ServerIpTxt.Text, "30000") > 0)
            {
                log.Error("Robot command server initialization failure");
                RobotCommandStatusLbl.BackColor = Color.Red;
                RobotCommandStatusLbl.Text = "Command Error";
            }
            else
            {
                log.Info("Robot command connection ready");

                RobotCommandStatusLbl.BackColor = Color.Red;
                RobotCommandStatusLbl.Text = "Command Waiting";
            }
        }

        private void RobotDisconnectBtn_Click(object sender, EventArgs e)
        {
            // Disconnect client from dashboard
            if (robotDashboardClient != null)
            {
                if (robotDashboardClient.IsClientConnected)
                {
                    robotDashboardClient.Send("stop");
                    robotDashboardClient.Send("quit");
                    robotDashboardClient.Disconnect();
                }
                robotDashboardClient = null;
            }
            RobotDashboardStatusLbl.BackColor = Color.Red;
            RobotDashboardStatusLbl.Text = "OFF";

            // Close command server
            if (robotCommandServer != null)
            {
                if (robotCommandServer.IsConnected())
                {
                    robotCommandServer.Send("(98)");
                    robotCommandServer.Disconnect();
                }
                robotCommandServer = null;
            }
            RobotCommandStatusLbl.BackColor = Color.Red;
            RobotCommandStatusLbl.Text = "OFF";
        }

        private void RobotSendBtn_Click(object sender, EventArgs e)
        {
            if (robotCommandServer != null)
                if (robotCommandServer.IsConnected())
                {
                    robotCommandServer.Send(RobotMessageTxt.Text);
                }
        }
        private void SetLinearSpeedBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(ReadVariable("robot_linear_speed_mmps"), "default robot SPEED, mm/s", 3);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_linear_speed({0})", form.value));
            }
        }

        private void SetLinearAccelBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(ReadVariable("robot_linear_accel_mmpss"), "default robot ACCELERATION, mm/s^2", 3);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_linear_accel({0})", form.value));
            }
        }

        private void SetBlendRadiusBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(ReadVariable("robot_blend_radius_mm"), "default robot BLEND RADIUS, mm", 3);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_blend_radius({0})", form.value));
            }
        }
        private void SetJointSpeedBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(ReadVariable("robot_joint_speed_dps"), "default robot JOINT SPEED, deg/s", 3);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_joint_speed({0})", form.value));
            }

        }

        private void SetJointAccelBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(ReadVariable("robot_joint_accel_dpss"), "default robot JOINT ACCELERATION, deg/s^2", 3);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_joint_accel({0})", form.value));
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
        void CommandCallback(string message)
        {
            //log.Info("UR<== {0}", message);

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
                        log.Error("Illegal callback command: {0}", request);
                }
            }
        }
        void DashboardCallback(string message)
        {
            log.Info("DASH<== {0}", message);
        }

        private void MessageTmr_Tick(object sender, EventArgs e)
        {
            //if (robotDashboardClient != null)
            //    if (robotDashboardClient.IsClientConnected)
            //        robotDashboardClient.Receive();

            if (robotCommandServer != null)
                if (robotCommandServer.IsConnected())
                {
                    robotCommandServer.Receive();
                    return;
                }
            RobotReadyLbl.BackColor = Color.Red;
            GrindReadyLbl.BackColor = Color.Red;
        }

        // ===================================================================
        // END ROBOT INTERFACE
        // ===================================================================

        // ===================================================================
        // START VARIABLE SYSTEM
        // ===================================================================

        readonly string variablesFilename = "Variables.xml";

        private string ReadVariable(string name, string defaultValue = null)
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
            return defaultValue;
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
        static string copyPositionAtWrite = null;
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

            // Automatically consider and variables with name starting in robot_ to be system variables
            if (nameTrimmed.StartsWith("robot_")) isSystem = true;

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
                case "robot_tool":
                    RobotReadyLbl.BackColor = ColorFromBooleanName(valueTrimmed);
                    break;
                case "grind_ready":
                    GrindReadyLbl.BackColor = ColorFromBooleanName(valueTrimmed);
                    break;
                case "robot_linear_speed_mmps":
                    SetLinearSpeedBtn.Text = "Linear Speed\n" + valueTrimmed + " mm/s";
                    break;
                case "robot_linear_accel_mmpss":
                    SetLinearAccelBtn.Text = "Linear Acceleration\n" + valueTrimmed + " mm/s^2";
                    break;
                case "robot_blend_radius_mm":
                    SetBlendRadiusBtn.Text = "Blend Radius\n" + valueTrimmed + " mm";
                    break;
                case "robot_joint_speed_dps":
                    SetJointSpeedBtn.Text = "Joint Speed\n" + valueTrimmed + " deg/s";
                    break;
                case "robot_joint_accel_dpss":
                    SetJointAccelBtn.Text = "Joint Acceleration\n" + valueTrimmed + " deg/s^2";
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

            // Set copyPositionAtWrite to "name" and when position_p or position_q gets written it will also be written to Position:name
            if (copyPositionAtWrite != null)
            {
                if (name == "position_q")
                {
                    WritePosition(copyPositionAtWrite, valueTrimmed, "", isSystemCopyWrite);
                }
                if (name == "position_p")
                {
                    WritePosition(copyPositionAtWrite, "", valueTrimmed, isSystemCopyWrite);
                    copyPositionAtWrite = null;
                    isSystemCopyWrite = false;
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

        private bool DeleteFirstNonSystemVariable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["IsSystem"].ToString() != "True")
                {
                    log.Debug("Delete {0}", row["Name"]);
                    row.Delete();
                    table.AcceptChanges();
                    return true;
                }
            }
            return false;
        }
        private void ClearVariablesBtn_Click(object sender, EventArgs e)
        {
            while (DeleteFirstNonSystemVariable(variables)) ;
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
            if (DialogResult.OK == ConfirmMessageBox("This will clear all variables INCLUDING system variables. Proceed?"))
                ClearAndInitializeVariables();
        }

        // ===================================================================
        // END VARIABLE SYSTEM
        // ===================================================================

        // ===================================================================
        // START TOOL SYSTEM
        // ===================================================================
        private void SelectToolBtn_Click(object sender, EventArgs e)
        {
            if (ToolsGrd.SelectedRows.Count == 1)
            {
                DataRow row = ((DataRowView)ToolsGrd.SelectedRows[0].DataBoundItem).Row;
                string name = row["Name"].ToString();

                log.Info("Selecting tool {0}", name);

                ExecuteLine(-1, string.Format("select_tool({0})", name));
            }
        }

        readonly string toolsFilename = "Tools.xml";
        private void ClearAndInitializeTools()
        {
            tools = new DataTable("Tools");
            DataColumn name = tools.Columns.Add("Name", typeof(System.String));
            tools.Columns.Add("x_m", typeof(System.Double));
            tools.Columns.Add("y_m", typeof(System.Double));
            tools.Columns.Add("z_m", typeof(System.Double));
            tools.Columns.Add("rx_rad", typeof(System.Double));
            tools.Columns.Add("ry_rad", typeof(System.Double));
            tools.Columns.Add("rz_rad", typeof(System.Double));
            tools.Columns.Add("mass_kg", typeof(System.Double));
            tools.Columns.Add("cogx_m", typeof(System.Double));
            tools.Columns.Add("cogy_m", typeof(System.Double));
            tools.Columns.Add("cogz_m", typeof(System.Double));
            tools.CaseSensitive = true;
            tools.PrimaryKey = new DataColumn[] { name };

            ToolsGrd.DataSource = tools;
        }
        private void RefreshMountedToolBox()
        {
            MountedToolBox.Items.Clear();
            foreach (DataRow row in tools.Rows)
            {
                MountedToolBox.Items.Add(row["Name"]);
            }
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

            RefreshMountedToolBox();
        }


        private void SaveToolsBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", toolsFilename);
            log.Info("SaveTools to {0}", filename);
            tools.AcceptChanges();
            tools.WriteXml(filename, XmlWriteMode.WriteSchema, true);
            RefreshMountedToolBox();
        }

        private void ClearToolsBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == ConfirmMessageBox("This will clear all tools. Proceed?"))
            {

                ClearAndInitializeTools();
                tools.Rows.Add(new object[] { "faceplate", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                tools.Rows.Add(new object[] { "2F85", 0, 0, 0.175, 0, 0, 0, 1.0, 0, 0, 0.050 });
                tools.Rows.Add(new object[] { "offset", 0, 0.1, 0.1, 0, 0, 0, 1.0, 0, 0, 0.050 });
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

        // ===================================================================
        // START POSITIONS SYSTEM
        // ===================================================================

        readonly string positionsFilename = "Positions.xml";

        private string ReadPositionJoint(string name)
        {
            foreach (DataRow row in positions.Rows)
            {
                if ((string)row["Name"] == name)
                {
                    log.Trace("ReadPositionJoint({0}) = {1}", row["Name"], row["Joints"]);
                    return row["Joints"].ToString();
                }
            }
            log.Error("ReadPositionJoint({0}) Not Found", name);
            return null;
        }
        private string ReadPositionPose(string name)
        {
            foreach (DataRow row in positions.Rows)
            {
                if ((string)row["Name"] == name)
                {
                    log.Trace("ReadPositionPose({0}) = {1}", row["Name"], row["Pose"]);
                    return row["Pose"].ToString();
                }
            }
            log.Error("ReadPositionPose({0}) Not Found", name);
            return null;
        }


        public bool WritePosition(string name, string joints = "", string pose = "", bool isSystem = false)
        {
            System.Threading.Monitor.Enter(lockObject);

            log.Trace("WritePosition({0}, {1}, {2}, {3})", name, joints, pose, isSystem);
            if (positions == null)
            {
                log.Error("positions == null!!??");
                return false;
            }

            bool foundVariable = false;
            foreach (DataRow row in positions.Rows)
            {
                if ((string)row["Name"] == name)
                {
                    if (joints != "") row["Joints"] = joints;
                    if (pose != "") row["Pose"] = pose;
                    row["IsSystem"] = isSystem;
                    foundVariable = true;
                    break;
                }
            }

            if (!foundVariable)
                positions.Rows.Add(new object[] { name, joints, pose, isSystem });

            positions.AcceptChanges();
            Monitor.Exit(lockObject);
            return true;
        }


        private void LoadPositionsBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", positionsFilename);
            log.Info("LoadPositions from {0}", filename);
            ClearAndInitializePositions();
            try
            {
                positions.ReadXml(filename);
            }
            catch
            { }

            PositionsGrd.DataSource = positions;
            foreach (DataGridViewColumn col in PositionsGrd.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void SavePositionsBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", positionsFilename);
            log.Info("SavePositions to {0}", filename);
            positions.AcceptChanges();
            positions.WriteXml(filename, XmlWriteMode.WriteSchema, true);
        }

        private void ClearPositionsBtn_Click(object sender, EventArgs e)
        {
            while (DeleteFirstNonSystemVariable(positions)) ;
        }

        private void ClearAndInitializePositions()
        {
            positions = new DataTable("Positions");
            DataColumn name = positions.Columns.Add("Name", typeof(System.String));
            positions.Columns.Add("Joints", typeof(System.String));
            positions.Columns.Add("Pose", typeof(System.String));
            positions.Columns.Add("IsSystem", typeof(System.Boolean));
            positions.CaseSensitive = true;
            positions.PrimaryKey = new DataColumn[] { name };
            PositionsGrd.DataSource = positions;
        }

        private void ClearAllPositionsBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == ConfirmMessageBox("This will clear all positions INCLUDING system positions. Proceed?"))
                ClearAndInitializePositions();
        }

        private void RecordPosition(string prompt, string varName)
        {
            JoggingForm form = new JoggingForm(robotCommandServer, this, prompt, ReadVariable("robot_tool"), "Teaching Position Only", true);

            form.ShowDialog(this);

            if (form.ShouldSave)
            {
                log.Trace(prompt);

                if (robotReady)
                {
                    copyPositionAtWrite = varName;
                    robotCommandServer.Send("(25)");
                }

            }
        }
        private bool GotoPositionJoint(string varName)
        {
            log.Trace("GotoPositionJoint({0})", varName);
            if (robotReady)
            {
                string q = ReadPositionJoint(varName);
                if (q != null)
                {
                    string msg = "(21," + ExtractScalars(q) + ')';
                    log.Trace("Sending {0}", msg);
                    robotCommandServer.Send(msg);
                    return true;
                }
            }
            return false;
        }
        private bool GotoPositionPose(string varName)
        {
            log.Trace("GotoPositionPose({0})", varName);
            if (robotReady)
            {
                string q = ReadPositionPose(varName);
                if (q != null)
                {
                    string msg = "(22," + ExtractScalars(q) + ')';
                    log.Trace("Sending {0}", msg);
                    robotCommandServer.Send(msg);
                    return true;
                }
            }
            return false;
        }

        private void PositionSetBtn_Click(object sender, EventArgs e)
        {
            if (PositionsGrd.SelectedRows.Count != 1)
            {
                ErrorMessageBox("Select a row in the Positions table to overwrite!");
                return;
            }
            DataRow row = ((DataRowView)PositionsGrd.SelectedRows[0].DataBoundItem).Row;
            string name = row["Name"].ToString();

            log.Info("Setting Position {0}", name);

            RecordPosition("Please teach position: " + name, name);
        }

        private void PositionMovePoseBtn_Click(object sender, EventArgs e)
        {
            if (PositionsGrd.SelectedRows.Count != 1)
            {
                ErrorMessageBox("Select a row in the Positions table to move to!");
                return;
            }
            DataRow row = ((DataRowView)PositionsGrd.SelectedRows[0].DataBoundItem).Row;
            string name = row["Name"].ToString();
            GotoPositionPose(name);
        }

        private void PositionMoveArmBtn_Click(object sender, EventArgs e)
        {
            if (PositionsGrd.SelectedRows.Count != 1)
            {
                ErrorMessageBox("Select a row in the Positions table to move to!");
                return;
            }
            DataRow row = ((DataRowView)PositionsGrd.SelectedRows[0].DataBoundItem).Row;
            string name = row["Name"].ToString();
            GotoPositionJoint(name);
        }

        private void AskSafetyStatusBtn_Click(object sender, EventArgs e)
        {
            robotDashboardClient?.InquiryResponse("safetystatus");
        }

        private void UnlockProtectiveStopBtn_Click(object sender, EventArgs e)
        {
            robotDashboardClient?.InquiryResponse("unlock protective stop");
        }

        private void DashboardSendBtn_Click(object sender, EventArgs e)
        {
            robotDashboardClient?.InquiryResponse(DashboardMessageTxt.Text);
        }
        // ===================================================================
        // END POSITIONS SYSTEM
        // ===================================================================

        // ===================================================================
        // START MANUAL SYSTEM
        // ===================================================================

        private void LoadManualBtn_Click(object sender, EventArgs e)
        {
            try
            {
                InstructionsRTB.LoadFile("Instructions.RTF");
            }
            catch (Exception ex)
            {
                log.Error("Could not load instruction sheet!");
            }
        }

        private void SaveManualBtn_Click(object sender, EventArgs e)
        {
            InstructionsRTB.SaveFile("Instructions.RTF");
        }

        // ===================================================================
        // END MANUAL SYSTEM
        // ===================================================================
    }
}
