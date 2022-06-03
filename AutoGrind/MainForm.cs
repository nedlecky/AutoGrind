// File: MainForm.cs
// Project: AutoGrind
// Author: Ned Lecky, Olympus Controls
// Purpose: The main code window for the AutoGrind program

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
        TcpServerSupport robotCommandServer = null;
        TcpClientSupport robotDashboardClient = null;
        MessageDialog waitingForOperatorMessageForm = null;
        bool closeOperatorFormOnIndex = false;

        static DataTable variables;
        static DataTable tools;
        static DataTable positions;
        static string[] diameterDefaults = { "0.00", "77.2", "81.9" };

        // App screen design sizes (Zebra L10 Tablet)
        const int screenDesignWidth = 2160;
        const int screenDesignHeight = 1440;

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

        string executionRoot = "";

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
            executionRoot = Path.GetDirectoryName(executable);
            string caption = appName + " Rev " + productVersion;
#if DEBUG
            caption += " DEBUG";
#endif
            this.Text = caption;
            VersionLbl.Text = caption;
            // Startup logging system (which also displays messages)
            log = NLog.LogManager.GetCurrentClassLogger();

            // Check screen dimensions.....
            Rectangle r = Screen.FromControl(this).Bounds;
            log.Info("Screen Dimensions: {0}x{1}", r.Width, r.Height);
            if (r.Width < screenDesignWidth || r.Height < screenDesignHeight)
            {
                DialogResult result = ConfirmMessageBox(String.Format("Screen dimensions for this application must be at least {0} x {1}. Continue anyway?", screenDesignWidth, screenDesignHeight));
                if (result != DialogResult.OK)
                {
                    forceClose = true;
                    Close();
                    return;
                }
            }

            LoadPersistent();
            UserModeBox.SelectedIndex = (int)operatorMode;

            // Set logfile variable in NLog
            LogManager.Configuration.Variables["LogfileName"] = AutoGrindRoot + "/Logs/AutoGrind.log";
            LogManager.ReconfigExistingLoggers();

            // Flag that we're starting
            log.Info("================================================================");
            log.Info(string.Format("Starting {0} in [{1}]", filename, executionRoot));
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
        }
        // Function key shortcut handling (primarily for development testing assistance)
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //log.Trace("MainForm_KeyDown: {0}", e.KeyData);
            switch (e.KeyData)
            {
                case Keys.F5:
                    if (StartBtn.Enabled)
                    {
                        StartBtn_Click(null, null);
                        e.Handled = true;
                    }
                    break;
                case Keys.F6:
                    if (StepBtn.Enabled)
                    {
                        StepBtn_Click(null, null);
                        e.Handled = true;
                    }
                    break;
                case Keys.F7:
                    if (PauseBtn.Enabled)
                    {
                        PauseBtn_Click(null, null);
                        e.Handled = true;
                    }
                    break;
                case Keys.F8:
                    if (StopBtn.Enabled)
                    {
                        StopBtn_Click(null, null);
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void StartupTmr_Tick(object sender, EventArgs e)
        {
            SplashForm splashForm = new SplashForm()
            {
                AutoClose = true,
            };
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
                    SetState(RunState.READY);
                }

            log.Info("System ready.");
        }
        bool forceClose = false;
        private void CloseTmr_Tick(object sender, EventArgs e)
        {
            CloseTmr.Enabled = false;
            RobotDisconnect();
            MessageTmr_Tick(null, null);
            forceClose = true;
            SavePersistent();
            NLog.LogManager.Shutdown(); // Flush and close down internal threads and timers
            this.Close();
        }

        private DialogResult ConfirmMessageBox(string question)
        {
            MessageDialog messageForm = new MessageDialog()
            {
                Title = "System Confirmation",
                Label = question,
                OkText = "&Yes",
                CancelText = "&No"
            };
            DialogResult result = messageForm.ShowDialog();
            return result;
        }
        private DialogResult ErrorMessageBox(string message)
        {
            MessageDialog messageForm = new MessageDialog()
            {
                Title = "System ERROR",
                Label = message,
                OkText = "&OK",
                CancelText = "&Cancel"
            };
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
                if (RecipeWasModified())
                {
                    var result = ConfirmMessageBox(String.Format("Recipe [{0}] has changed.\nSave changes?", LoadRecipeBtn.Text));
                    if (result == DialogResult.OK)
                        SaveRecipeBtn_Click(null, null);
                }

                CloseTmr.Interval = 500;
                CloseTmr.Enabled = true;
                e.Cancel = true; // Cancel this shutdown- we'll let the close out timer shut us down
                log.Info("Shutting down in 500mS...");
            }
        }

        // Something isn't right. If we're running, select PAUSE
        private void EnsureNotRunning()
        {
            if (runState == RunState.RUNNING)
                PauseBtn_Click(null, null);
        }
        // Something isn't right. If we're not stopped, select STOP
        private void EnsureStopped()
        {
            if (runState != RunState.READY)
                StopBtn_Click(null, null);
        }

        static bool robotReady = false;
        static DateTime runStartedTime;   // When did the user hit run?
        static DateTime stepStartedTime;  // When did the current recipe line start executing?
        static DateTime stepEndTimeEstimate;  // When do we think it will end?
        private string TimeSpanFormat(TimeSpan elapsed)
        {
            int hrs = Math.Abs(elapsed.Days * 24 + elapsed.Hours);
            int mins = Math.Abs(elapsed.Minutes);
            int secs = Math.Abs(elapsed.Seconds);
            int msecs = Math.Abs(elapsed.Milliseconds);
            return String.Format("{0:00}h {1:00}m {2:00.0}s", hrs, mins, secs + msecs / 1000.0) + ((elapsed < TimeSpan.Zero && secs > 0.1) ? " OVER" : "");
        }

        private void RecomputeTimes()
        {
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now - runStartedTime;
            RunElapsedTimeLbl.Text = TimeSpanFormat(elapsed);

            TimeSpan stepElapsed = now - stepStartedTime;
            StepElapsedTimeLbl.Text = TimeSpanFormat(stepElapsed);

            TimeSpan timeRemaining = stepEndTimeEstimate - now;
            StepTimeRemainingLbl.Text = TimeSpanFormat(timeRemaining);
        }

        enum ProgramState
        {
            UNKNOWN,
            STOPPED,
            PAUSED,
            PLAYING
        };

        int dashboardCycle = 0;
        int nUnansweredRobotmodeRequests = 0;
        int nUnansweredSafetystatusRequests = 0;
        int nUnansweredProgramstateRequests = 0;

        int iii = 0;
        private void HeartbeatTmr_Tick(object sender, EventArgs e)
        {
            // Update current time
            Time2Lbl.Text = TimeLbl.Text = DateTime.Now.ToString();

            // Logger stress test
            if (StressChk.Checked)
            {
                iii++;
                log.Info("{0} Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", iii);
                log.Info("{0} DASH Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", iii);
                log.Info("{0} EXEC Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", iii);
                log.Info("{0} UR==> Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", iii);
                log.Info("{0} WARN Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", iii);
                log.Info("{0} ERROR Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", iii);
            }

            // Update elapsed time panel
            if (runState == RunState.RUNNING || runState == RunState.PAUSED)
                RecomputeTimes();

            // DASHBOARD Handler: Round-robin sending the Dashboard monitoring commands
            if (robotDashboardClient != null)
                if (!robotDashboardClient.IsClientConnected)
                {
                    RobotConnectBtn.Text = "Dashboard ERROR";
                    RobotConnectBtn.BackColor = Color.Red;
                }
                else
                {
                    // Any responses received?
                    string dashResponse = robotDashboardClient.Receive();
                    log.Trace("DASH received {0}", dashResponse);

                    if (dashResponse != null)
                    {
                        string[] responses = dashResponse.Split('\n');
                        foreach (string response in responses)
                        {
                            log.Trace("DASH parsing {0}", response);
                            if (response.StartsWith("Robotmode: "))
                            {
                                nUnansweredRobotmodeRequests = 0;
                                HandleRobotmodeResponse(response);
                            }
                            else if (response.StartsWith("Safetystatus: "))
                            {
                                nUnansweredSafetystatusRequests = 0;
                                HandleSafetystatusResponse(response);
                            }
                            else
                            {
                                ProgramState programState = IsProgramstateResponse(response);
                                if (programState != ProgramState.UNKNOWN)
                                {
                                    nUnansweredProgramstateRequests = 0;
                                    HandleProgramstateResponse(programState, response);
                                }
                            }
                        }
                    }

                    // Check for unanswered requests
                    if (nUnansweredRobotmodeRequests > 2)
                    {
                        log.Error("Too many sequential missed responses from robotmode");
                        EnsureStopped();
                    }
                    else if (nUnansweredRobotmodeRequests > 0)
                        log.Warn("Missed {0} responses from robotmode", nUnansweredRobotmodeRequests);

                    if (nUnansweredSafetystatusRequests > 2)
                    {
                        log.Error("Too many sequential missed responses from safetystatus");
                        EnsureStopped();
                    }
                    else if (nUnansweredSafetystatusRequests > 0)
                        log.Warn("Missed {0} responses from safetystatus", nUnansweredSafetystatusRequests);

                    if (nUnansweredProgramstateRequests > 2)
                    {
                        log.Error("Too many sequential missed responses from programstate");
                        EnsureStopped();
                    }
                    else if (nUnansweredProgramstateRequests > 0)
                        log.Warn("Missed {0} responses from programstate", nUnansweredProgramstateRequests);


                    switch (dashboardCycle++)
                    {
                        case 0:
                            robotDashboardClient.Send("robotmode");
                            robotDashboardClient.Send("safetystatus");
                            nUnansweredRobotmodeRequests++;
                            nUnansweredSafetystatusRequests++;
                            break;
                        case 1:
                            robotDashboardClient.Send("programstate");
                            nUnansweredProgramstateRequests++;
                            dashboardCycle = 0;
                            break;
                        default:
                            dashboardCycle = 0;
                            break;

                    }
                }

            // When the robot connects, get us ready to go!  Or, if it dosconnects, put us in WAIT
            bool newRobotReady = false;
            if (robotCommandServer == null)
                robotReady = false;
            else
            {
                if (robotCommandServer.IsClientConnected)
                    newRobotReady = true;

                if (newRobotReady != robotReady)
                {
                    robotReady = newRobotReady;
                    if (robotReady)
                    {
                        log.Info("Changing robot connection to READY");

                        // Send persistent values (or defaults) for speeds, accelerations, I/O, etc.
                        ExecuteLine(-1, "grind_contact_enable(0)");  // Set contact enabled = No Touch No Grind
                        ExecuteLine(-1, "grind_retract()");  // Ensure we're not on the part
                        ExecuteLine(-1, string.Format("set_linear_speed({0})", ReadVariable("robot_linear_speed_mmps", "200")));
                        ExecuteLine(-1, string.Format("set_linear_accel({0})", ReadVariable("robot_linear_accel_mmpss", "500")));
                        ExecuteLine(-1, string.Format("set_blend_radius({0})", ReadVariable("robot_blend_radius_mm", "3")));
                        ExecuteLine(-1, string.Format("set_joint_speed({0})", ReadVariable("robot_joint_speed_dps", "45")));
                        ExecuteLine(-1, string.Format("set_joint_accel({0})", ReadVariable("robot_joint_accel_dpss", "180")));
                        ExecuteLine(-1, string.Format("grind_touch_speed({0})", ReadVariable("grind_touch_speed_mmps", "10")));
                        ExecuteLine(-1, string.Format("grind_touch_retract({0})", ReadVariable("grind_touch_retract_mm", "3")));
                        ExecuteLine(-1, string.Format("grind_force_dwell({0})", ReadVariable("grind_force_dwell_ms", "500")));
                        ExecuteLine(-1, string.Format("grind_max_wait({0})", ReadVariable("grind_max_wait_ms", "1500")));
                        ExecuteLine(-1, string.Format("grind_max_blend_radius({0})", ReadVariable("grind_max_blend_radius_mm", "4")));
                        ExecuteLine(-1, string.Format("grind_trial_speed({0})", ReadVariable("grind_trial_speed_mmps", "20")));
                        ExecuteLine(-1, string.Format("grind_accel({0})", ReadVariable("grind_accel_mmpss", "100")));
                        ExecuteLine(-1, string.Format("set_door_closed_input({0})", ReadVariable("robot_door_closed_input", "1,1").Trim(new char[] { '[', ']' })));
                        ExecuteLine(-1, string.Format("set_footswitch_pressed_input({0})", ReadVariable("robot_footswitch_pressed_input", "7,1").Trim(new char[] { '[', ']' })));

                        // Download selected tool and part geometry by acting like a reselect of both
                        MountedToolBox_SelectedIndexChanged(null, null);
                        PartGeometryBox_SelectedIndexChanged(null, null);

                        RobotCommandStatusLbl.BackColor = Color.Green;
                        RobotCommandStatusLbl.Text = "Command Ready";
                        // Restore all button settings with same current state
                        SetState(runState, true);
                    }
                    else
                    {
                        log.Info("Change robot connection to WAIT");
                        RobotCommandStatusLbl.BackColor = Color.Red;
                        RobotCommandStatusLbl.Text = "WAIT";
                        // Restore all button settings with same current state
                        SetState(runState, true);
                        EnsureNotRunning();
                    }
                }

            }
        }

        private void HandleRobotmodeResponse(string robotmodeResponse)
        {
            Color color = Color.Red;
            string buttonText = robotmodeResponse;
            switch (robotmodeResponse)
            {
                case "Robotmode: RUNNING":
                    color = Color.Green;
                    break;
                case "Robotmode: CONFIRM_SAFETY":
                    EnsureStopped();
                    color = Color.Blue;
                    break;
                case "Robotmode: IDLE":
                    EnsureNotRunning();
                    color = Color.Blue;
                    break;
                case "Robotmode: NO_CONTROLLER":
                case "Robotmode: DISCONNECTED":
                case "Robotmode: BACKDRIVE":
                case "Robotmode: POWER_OFF":
                    EnsureStopped();
                    color = Color.Red;
                    break;
                case "Robotmode: POWER_ON":
                    EnsureStopped();
                    color = Color.Blue;
                    break;
                case "Robotmode: BOOTING":
                    EnsureStopped();
                    color = Color.Coral;
                    break;
                default:
                    log.Error("Unknown response to robotmode: {0}", robotmodeResponse);
                    EnsureStopped();
                    buttonText = "Robotmode: ?? " + robotmodeResponse;
                    color = Color.Red;
                    break;
            }
            RobotModeBtn.Text = buttonText;
            RobotModeBtn.BackColor = color;
        }


        private void HandleSafetystatusResponse(string safetystatusResponse)
        {
            Color color = Color.Red;
            string buttonText = safetystatusResponse;
            switch (safetystatusResponse)
            {
                case "Safetystatus: NORMAL":
                    color = Color.Green;
                    break;
                case "Safetystatus: REDUCED":
                    color = Color.Yellow;
                    break;
                case "Safetystatus: PROTECTIVE_STOP":
                case "Safetystatus: RECOVERY":
                case "Safetystatus: SAFEGUARD_STOP":
                case "Safetystatus: SYSTEM_EMERGENCY_STOP":
                case "Safetystatus: ROBOT_EMERGENCY_STOP":
                case "Safetystatus: VIOLATION":
                case "Safetystatus: FAULT":
                case "Safetystatus: AUTOMATIC_MODE_SAFEGUARD_STOP":
                case "Safetystatus: SYSTEM_THREE_POSITION_ENABLING_STOP":
                    EnsureNotRunning();
                    color = Color.Red;
                    break;
                default:
                    log.Error("Unknown response to safetystatus: {0}", safetystatusResponse);
                    EnsureStopped();
                    buttonText = "Safetystatus: ?? " + safetystatusResponse;
                    color = Color.Red;
                    break;
            }
            SafetyStatusBtn.Text = buttonText;
            SafetyStatusBtn.BackColor = color;
        }

        // Is the supplied string a valid response to Dashboard programstate command?
        ProgramState IsProgramstateResponse(string message)
        {
            ProgramState programState = ProgramState.UNKNOWN;

            if (message != null)
            {
                if (message.StartsWith("STOPPED"))
                    programState = ProgramState.STOPPED;
                else if (message.StartsWith("PAUSED"))
                    programState = ProgramState.PAUSED;
                else if (message.StartsWith("PLAYING"))
                    programState = ProgramState.PLAYING;
            }

            return programState;
        }

        private void HandleProgramstateResponse(ProgramState programState, string programstateResponse)
        {
            Color color = Color.Red;
            string buttonText = programstateResponse;
            switch (programState)
            {
                case ProgramState.STOPPED:
                    EnsureStopped();
                    break;
                case ProgramState.PAUSED:
                    EnsureNotRunning();
                    break;
                case ProgramState.PLAYING:
                    color = Color.Green;
                    if (robotCommandServer == null)
                    {
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
                    break;
                default:
                    log.Error("Unknown response to programstate: {0}", programstateResponse);
                    EnsureStopped();
                    buttonText = "Programstate: ?? " + programstateResponse;
                    color = Color.Red;
                    break;
            }
            ProgramStateBtn.Text = buttonText;
            ProgramStateBtn.BackColor = color;
        }


        // ===================================================================
        // START MAIN UI BUTTONS
        // ===================================================================

        private void MainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = MainTab.TabPages[MainTab.SelectedIndex].Text;

            if (tabName == "Log")
            {
                // This forces the log RTBs to all update... otherwise there are artifacts left over from NLog the first time in on program start
                for (int i = 0; i < 2; i++)
                {
                    AllLogRTB.Refresh();
                    ExecLogRTB.Refresh();
                    UrLogRTB.Refresh();
                    UrDashboardLogRTB.Refresh();
                    ErrorLogRTB.Refresh();
                }
            }
        }
        // Prevents selecting (seeing) a tab page that is not enabled
        private void MainTab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex < 0) return;
            e.Cancel = !e.TabPage.Enabled;
        }


        private void SetState(RunState s, bool fForce = false)
        {
            if (fForce || runState != s)
            {
                runState = s;
                log.Info("EXEC SetState({0})", s.ToString());

                EnterRunState();
            }
        }

        private void EnterRunState()
        {
            switch (runState)
            {
                case RunState.IDLE:
                    RunStateLbl.Text = "IDLE";
                    RunStateLbl.BackColor = Color.Gray;

                    // Robot Communication Control
                    RobotConnectBtn.Enabled = true;
                    RobotModeBtn.Enabled = true;
                    SafetyStatusBtn.Enabled = true;
                    ProgramStateBtn.Enabled = true;

                    UserModeBox.Enabled = true;

                    ExitBtn.Enabled = true;
                    JogRunBtn.Enabled = robotReady;
                    JogBtn.Enabled = robotReady;
                    PositionMovePoseBtn.Enabled = robotReady;
                    PositionMoveArmBtn.Enabled = robotReady;
                    PositionSetBtn.Enabled = robotReady;

                    MoveToolMountBtn.Enabled = robotReady;
                    MoveToolHomeBtn.Enabled = robotReady;

                    LoadRecipeBtn.Enabled = true;
                    NewRecipeBtn.Enabled = true;
                    SaveRecipeBtn.Enabled = RecipeWasModified();
                    SaveAsRecipeBtn.Enabled = true;

                    ToolSetupGrp.Enabled = true;
                    GeneralConfigGrp.Enabled = true;
                    DefaultMoveSetupGrp.Enabled = true;
                    GrindingMoveSetupGrp.Enabled = true;

                    StartBtn.Enabled = false;
                    StepBtn.Enabled = false;
                    PauseBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    GrindContactEnabledBtn.Enabled = true;

                    MountedToolBox.Enabled = true;
                    PartGeometryBox.Enabled = true;
                    DiameterLbl.Enabled = true;

                    ExecTmr.Enabled = false;
                    CurrentLineLbl.Text = "";
                    RecipeRTB.Enabled = true;
                    break;
                case RunState.READY:
                    RunStateLbl.Text = "STOPPED";
                    RunStateLbl.BackColor = Color.Red;
                    sleepTimer = null; // Cancels any pending sleep(...)

                    // Robot Communication Control
                    RobotConnectBtn.Enabled = true;
                    RobotModeBtn.Enabled = true;
                    SafetyStatusBtn.Enabled = true;
                    ProgramStateBtn.Enabled = true;

                    UserModeBox.Enabled = true;

                    ExitBtn.Enabled = true;
                    JogRunBtn.Enabled = robotReady;
                    JogBtn.Enabled = robotReady;
                    PositionMovePoseBtn.Enabled = robotReady;
                    PositionMoveArmBtn.Enabled = robotReady;
                    PositionSetBtn.Enabled = robotReady;
                    MoveToolMountBtn.Enabled = robotReady;
                    MoveToolHomeBtn.Enabled = robotReady;

                    LoadRecipeBtn.Enabled = true;
                    NewRecipeBtn.Enabled = true;
                    SaveRecipeBtn.Enabled = RecipeWasModified();
                    SaveAsRecipeBtn.Enabled = true;

                    ToolSetupGrp.Enabled = true;
                    GeneralConfigGrp.Enabled = true;
                    DefaultMoveSetupGrp.Enabled = true;
                    GrindingMoveSetupGrp.Enabled = true;

                    StartBtn.Enabled = true;
                    StepBtn.Enabled = true;
                    PauseBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    GrindContactEnabledBtn.Enabled = true;

                    MountedToolBox.Enabled = true;
                    PartGeometryBox.Enabled = true;
                    DiameterLbl.Enabled = true;

                    ExecTmr.Enabled = false;
                    //CurrentLineLbl.Text = "";
                    RecipeRTB.Enabled = true;
                    break;
                case RunState.RUNNING:
                    RunStateLbl.Text = "RUNNING";
                    RunStateLbl.BackColor = Color.Green;
                    sleepTimer = null; // Cancels any pending sleep(...)

                    // Robot Communication Control
                    RobotConnectBtn.Enabled = false;
                    RobotModeBtn.Enabled = false;
                    SafetyStatusBtn.Enabled = false;
                    ProgramStateBtn.Enabled = false;

                    UserModeBox.Enabled = false;

                    ExitBtn.Enabled = false;
                    JogRunBtn.Enabled = false;
                    JogBtn.Enabled = false;
                    PositionMovePoseBtn.Enabled = false;
                    PositionMoveArmBtn.Enabled = false;
                    PositionSetBtn.Enabled = false;
                    MoveToolMountBtn.Enabled = false;
                    MoveToolHomeBtn.Enabled = false;

                    LoadRecipeBtn.Enabled = false;
                    NewRecipeBtn.Enabled = false;
                    SaveRecipeBtn.Enabled = false;
                    SaveAsRecipeBtn.Enabled = false;

                    ToolSetupGrp.Enabled = false;
                    GeneralConfigGrp.Enabled = false;
                    DefaultMoveSetupGrp.Enabled = false;
                    GrindingMoveSetupGrp.Enabled = false;

                    StartBtn.Enabled = false;
                    StepBtn.Enabled = false;
                    PauseBtn.Enabled = true;
                    PauseBtn.Text = "Pause";
                    StopBtn.Enabled = true;
                    GrindContactEnabledBtn.Enabled = false;

                    MountedToolBox.Enabled = false;
                    PartGeometryBox.Enabled = false;
                    DiameterLbl.Enabled = false;

                    CurrentLineLbl.Text = "";
                    RecipeRTB.Enabled = false;

                    ExecTmr.Interval = 100;
                    ExecTmr.Enabled = true;
                    waitingForOperatorMessageForm = null;

                    break;
                case RunState.PAUSED:
                    RunStateLbl.Text = "PAUSED";
                    RunStateLbl.BackColor = Color.DarkOrange;

                    // Robot Communication Control
                    RobotConnectBtn.Enabled = true;
                    RobotModeBtn.Enabled = true;
                    SafetyStatusBtn.Enabled = true;
                    ProgramStateBtn.Enabled = true;

                    UserModeBox.Enabled = false;

                    ExitBtn.Enabled = false;
                    JogRunBtn.Enabled = false;
                    JogBtn.Enabled = false;
                    PositionMovePoseBtn.Enabled = false;
                    PositionMoveArmBtn.Enabled = false;
                    PositionSetBtn.Enabled = false;
                    MoveToolMountBtn.Enabled = false;
                    MoveToolHomeBtn.Enabled = false;

                    LoadRecipeBtn.Enabled = false;
                    NewRecipeBtn.Enabled = false;
                    SaveRecipeBtn.Enabled = false;
                    SaveAsRecipeBtn.Enabled = false;

                    ToolSetupGrp.Enabled = true;
                    GeneralConfigGrp.Enabled = false;
                    DefaultMoveSetupGrp.Enabled = true;
                    GrindingMoveSetupGrp.Enabled = true;

                    StartBtn.Enabled = false;
                    StepBtn.Enabled = true;
                    PauseBtn.Enabled = true;
                    PauseBtn.Text = "Continue";
                    StopBtn.Enabled = true;
                    GrindContactEnabledBtn.Enabled = true;

                    MountedToolBox.Enabled = false;
                    PartGeometryBox.Enabled = false;
                    DiameterLbl.Enabled = false;

                    RecipeRTB.Enabled = false;

                    ExecTmr.Enabled = false;
                    break;
            }

            ExitBtn.BackColor = ExitBtn.Enabled ? Color.Green : Color.Gray;
            JogRunBtn.BackColor = JogRunBtn.Enabled ? Color.Green : Color.Gray;
            JogBtn.BackColor = JogBtn.Enabled ? Color.Green : Color.Gray;
            PositionMovePoseBtn.BackColor = PositionMovePoseBtn.Enabled ? Color.Green : Color.Gray;
            PositionMoveArmBtn.BackColor = PositionMoveArmBtn.Enabled ? Color.Green : Color.Gray;
            PositionSetBtn.BackColor = PositionSetBtn.Enabled ? Color.Green : Color.Gray;
            MoveToolMountBtn.BackColor = MoveToolMountBtn.Enabled ? Color.Green : Color.Gray;
            MoveToolHomeBtn.BackColor = MoveToolHomeBtn.Enabled ? Color.Green : Color.Gray;

            LoadRecipeBtn.BackColor = LoadRecipeBtn.Enabled ? Color.Green : Color.Gray;
            NewRecipeBtn.BackColor = NewRecipeBtn.Enabled ? Color.Green : Color.Gray;
            SaveRecipeBtn.BackColor = SaveRecipeBtn.Enabled ? Color.Green : Color.Gray;
            SaveAsRecipeBtn.BackColor = SaveAsRecipeBtn.Enabled ? Color.Green : Color.Gray;

            StartBtn.BackColor = StartBtn.Enabled ? Color.Green : Color.Gray;
            PauseBtn.BackColor = PauseBtn.Enabled ? Color.DarkOrange : Color.Gray;
            StepBtn.BackColor = StepBtn.Enabled ? Color.Green : Color.Gray;
            StopBtn.BackColor = StopBtn.Enabled ? Color.Red : Color.Gray;
        }

        bool mountedToolBoxActionDisabled = false;
        private void MountedToolBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            log.Info("MountedToolBox changed to " + MountedToolBox.Text + (mountedToolBoxActionDisabled ? " UPDATE DISABLED" : ""));
            if (mountedToolBoxActionDisabled) return;

            ToolsGrd.ClearSelection();
            if (robotCommandServer != null)
                ExecuteLine(-1, String.Format("select_tool({0})", MountedToolBox.Text));
        }

        private void UpdateGeometryToRobot()
        {
            if (robotCommandServer != null)
            {
                ExecuteLine(-1, String.Format("set_part_geometry_N({0},{1})", PartGeometryBox.SelectedIndex + 1, DiameterLbl.Text));
                WriteVariable("robot_geometry", String.Format("{0},{1}", PartGeometryBox.SelectedItem, DiameterLbl.Text));
            }
        }

        bool partGeometryBoxDisabled = false;
        private void PartGeometryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            log.Info("PartGeometryBox changed to " + PartGeometryBox.Text + (partGeometryBoxDisabled ? " UPDATE DISABLED" : ""));
            if (partGeometryBoxDisabled) return;
            bool isFlat = PartGeometryBox.Text == "FLAT";
            if (isFlat)
            {
                DiameterLbl.Text = "0.0";
                DiameterLbl.Visible = false;
                DiameterDimLbl.Visible = false;
            }
            else
            {
                int index = PartGeometryBox.SelectedIndex;
                DiameterLbl.Text = diameterDefaults[index];
                DiameterLbl.Visible = true;
                DiameterDimLbl.Visible = true;
            }

            UpdateGeometryToRobot();
        }

        public enum ControlSetting
        {
            HIDDEN,
            DISABLED,
            NORMAL
        };
        public class ControlSpec
        {
            public Control control;
            public ControlSetting[] settings = new ControlSetting[3];
            public ControlSpec(Control c, ControlSetting operatorSetting, ControlSetting editorSetting, ControlSetting engineeringSetting)
            {
                control = c;
                settings[0] = operatorSetting;
                settings[1] = editorSetting;
                settings[2] = engineeringSetting;
            }
        }
        static private ControlSpec[] controlSpecs = null;
        private void BuildEnableTable()
        {
            controlSpecs = new ControlSpec[]
            {
                // Position Test Buttons
                new ControlSpec(PositionTestButtonGrp, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),
                new ControlSpec(LoadPositionsBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),
                new ControlSpec(SavePositionsBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),
                new ControlSpec(ClearPositionsBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),
                new ControlSpec(ClearAllPositionsBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),

                // Variable Test Buttons
                new ControlSpec(VariableTestButtonGrp, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),
                new ControlSpec(LoadVariablesBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),
                new ControlSpec(SaveVariablesBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),
                new ControlSpec(ClearVariablesBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),
                new ControlSpec(ClearAllVariablesBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),

                // Set Position Button
                new ControlSpec(PositionSetBtn, ControlSetting.HIDDEN, ControlSetting.DISABLED, ControlSetting.NORMAL),

                // Special Test Controls
                new ControlSpec(StressBtn, ControlSetting.HIDDEN, ControlSetting.HIDDEN, ControlSetting.NORMAL),
                new ControlSpec(StressChk, ControlSetting.HIDDEN, ControlSetting.HIDDEN, ControlSetting.NORMAL),
            };

        }

        private void OperatorModeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (controlSpecs == null) BuildEnableTable();

            OperatorMode origOperatorMode = operatorMode;

            OperatorMode newOperatorMode = (OperatorMode)UserModeBox.SelectedIndex;
            log.Info(string.Format("OperatorMode changing to {0}", newOperatorMode));

#if !DEBUG
            // Enforce any password requirements (unless we're in DEBUG for convenience)

            switch (newOperatorMode)
            {
                case OperatorMode.OPERATOR:
                    break;
                case OperatorMode.EDITOR:
                    SetValueForm form = new SetValueForm()
                    {
                        Value = "",
                        Label = "Passcode for EDITOR",
                        NumberOfDecimals = 0,
                        MaxAllowed = 999999,
                        MinAllowed = 0,
                        IsPassword = true,
                    };
                    if (form.ShowDialog(this) != DialogResult.OK || form.Value != "9")
                    {
                        UserModeBox.SelectedIndex = 0;
                        return;
                    }
                    break;
                case OperatorMode.ENGINEERING:
                    form = new SetValueForm()
                    {
                        Value = "",
                        Label = "Passcode for ENGINEERING",
                        NumberOfDecimals = 0,
                        MaxAllowed = 999999,
                        MinAllowed = 0,
                        IsPassword = true,
                    };
                    if (form.ShowDialog(this) != DialogResult.OK || form.Value != "99")
                    {
                        UserModeBox.SelectedIndex = 0;
                        return;
                    }
                    break;
            }
#endif
            operatorMode = newOperatorMode;

            const int RunPage = 0;
            const int ProgramPage = 1;
            const int SetupPage = 2;
            const int LogPage = 3;
            if (MainTab.TabPages[1] != null)  // Helps during program load before instantiation!
            {
                log.Info("Setting Operator Mode {0}", operatorMode);
                switch (operatorMode)
                {
                    case OperatorMode.OPERATOR:
                        MainTab.TabPages[RunPage].Enabled = true;
                        MainTab.TabPages[ProgramPage].Enabled = false;
                        MainTab.TabPages[SetupPage].Enabled = false;
                        MainTab.TabPages[LogPage].Enabled = true;
                        MainTab.SelectedIndex = 0;
                        break;
                    case OperatorMode.EDITOR:
                        MainTab.TabPages[RunPage].Enabled = true;
                        MainTab.TabPages[ProgramPage].Enabled = true;
                        MainTab.TabPages[SetupPage].Enabled = false;
                        MainTab.TabPages[LogPage].Enabled = true;
                        MainTab.SelectedIndex = 1;
                        break;
                    case OperatorMode.ENGINEERING:
                        MainTab.TabPages[RunPage].Enabled = true;
                        MainTab.TabPages[ProgramPage].Enabled = true;
                        MainTab.TabPages[SetupPage].Enabled = true;
                        MainTab.TabPages[LogPage].Enabled = true;
                        break;
                }
            }

            foreach (ControlSpec spec in controlSpecs)
            {
                Control c = spec.control;
                switch (spec.settings[(int)operatorMode])
                {
                    case ControlSetting.HIDDEN:
                        c.Enabled = false;
                        c.Visible = false;
                        break;
                    case ControlSetting.DISABLED:
                        c.Enabled = false;
                        c.Visible = true;
                        if (c.GetType().Name == "Button") c.BackColor = Color.Gray;
                        break;
                    case ControlSetting.NORMAL:
                        c.Enabled = true;
                        c.Visible = true;
                        if (c.GetType().Name == "Button") c.BackColor = Color.Green;
                        break;

                }
            }

            // Reenable other buttons and tabs as dictated by currect runState
            SetState(runState, true);
        }

        private void RobotModeBtn_Click(object sender, EventArgs e)
        {
            CloseSafetyPopup();
            switch (RobotModeBtn.Text)
            {
                case "Robotmode: RUNNING":
                    robotDashboardClient?.Send("power off");
                    break;
                case "Robotmode: IDLE":
                    robotDashboardClient?.Send("brake release");
                    break;
                case "Robotmode: POWER_OFF":
                    robotDashboardClient?.Send("power on");
                    break;
                default:
                    log.Error("Unknown robot mode button state! {0}", RobotModeBtn.Text);
                    ErrorMessageBox(String.Format("Unsure how to recover from {0}", RobotModeBtn.Text));
                    break;
            }
        }

        private void SafetyStatusBtn_Click(object sender, EventArgs e)
        {
            CloseSafetyPopup();
            switch (SafetyStatusBtn.Text)
            {
                case "Safetystatus: NORMAL":
                    robotDashboardClient?.Send("power off");
                    break;
                case "Safetystatus: PROTECTIVE_STOP":
                    robotDashboardClient?.InquiryResponse("unlock protective stop", 200);
                    robotDashboardClient?.InquiryResponse("close safety popup", 200);

                    break;
                case "Safetystatus: ROBOT_EMERGENCY_STOP":
                    ErrorMessageBox("Release Robot E-Stop");
                    robotDashboardClient?.InquiryResponse("close safety popup", 200);
                    break;
                default:
                    log.Error("Unknown safety status button state! {0}", SafetyStatusBtn.Text);
                    ErrorMessageBox(String.Format("Unsure how to recover from {0}", SafetyStatusBtn.Text));
                    break;
            }
        }

        private void ProgramStateBtn_Click(object sender, EventArgs e)
        {
            CloseSafetyPopup();
            if (ProgramStateBtn.Text.StartsWith("PLAYING"))
            {
                RobotSend("99");
                robotDashboardClient?.Send("stop");
                RobotCommandStatusLbl.BackColor = Color.Red;
                RobotCommandStatusLbl.Text = "OFF";
                RobotReadyLbl.BackColor = Color.Red;
                GrindReadyLbl.BackColor = Color.Red;
                GrindProcessStateLbl.BackColor = Color.Red;
                CloseCommandServer();
            }
            else
            {
                robotDashboardClient?.Send("play");
                // If we're starting back in the middle of something, this will abort it
                RobotSend("10");
            }
        }

        private void KeyboardBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void AllLogRTB_DoubleClick(object sender, EventArgs e)
        {
            AllLogRTB.Clear();
        }

        private void ExecLogRTB_DoubleClick(object sender, EventArgs e)
        {
            ExecLogRTB.Clear();
        }

        private void UrLogRTB_DoubleClick(object sender, EventArgs e)
        {
            UrLogRTB.Clear();
        }

        private void UrDashboardLogRTB_DoubleClick(object sender, EventArgs e)
        {
            UrDashboardLogRTB.Clear();
        }

        private void ErrorLogRTB_DoubleClick(object sender, EventArgs e)
        {
            ErrorLogRTB.Clear();
        }

        private void AboutBtn_Click(object sender, EventArgs e)
        {
            SplashForm splashForm = new SplashForm()
            {
                AutoClose = false
            };
            splashForm.ShowDialog();
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
                    string var = ReadVariable("grind_contact_enable", "0");
                    // Increment current setting to cycle through 0, 1, 2
                    int val = Convert.ToInt32(var);
                    val++;
                    val %= 3;
                    RobotSend(String.Format("35,1,{0}", val));
                }
        }

        private bool PrepareToRun()
        {
            // Mark and display the start time, set counters to 0
            runStartedTime = DateTime.Now;
            RunStartedTimeLbl.Text = runStartedTime.ToString();
            GrindCycleLbl.Text = "";
            GrindNCyclesLbl.Text = "";
            StepTimeEstimateLbl.Text = "";


            // This allows offline dry runs but makes sure you know!
            if (!robotReady)
            {
                if (AllowRunningOfflineChk.Checked)
                {
                    var result = ConfirmMessageBox("Robot not connected.\nRun anyway?");
                    if (result != DialogResult.OK) return false;
                }
                else
                {
                    ErrorMessageBox("Robot not connected.\nRunning not allowed per Setup checkbox.");
                    return false;
                }
            }

            SetCurrentLine(0);
            bool goodLabels = BuildLabelTable();
            return goodLabels;
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            log.Info("StartBtn_Click(...)");

            if (ReadVariable("robot_door_closed", "0") != "1")
            {
                ErrorMessageBox("Cannot Start. Door Open!");
                return;
            }
            if (ReadVariable("robot_footswitch_pressed", "0") == "1")
            {
                ErrorMessageBox("Cannot Start. Footswitch Pressed!");
                return;
            }

            if (PrepareToRun())
            {
                isSingleStep = false;
                SetRecipeState(RecipeState.RUNNING);
                RobotSend("10");  // This makes sure we're synced!
                SetState(RunState.RUNNING);
            }
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            log.Info("PauseBtn{0}_Click(...)", PauseBtn.Text);
            switch (runState)
            {
                case RunState.RUNNING:
                    // Perform PAUSE function
                    RobotSend("10");  // This will cancel any grind in progress
                    SetState(RunState.PAUSED);
                    break;
                case RunState.PAUSED:
                    // Perform CONTINUE function
                    if (ReadVariable("robot_door_closed", "0") != "1")
                    {
                        ErrorMessageBox("Cannot Continue. Door Open!");
                        return;
                    }
                    if (ReadVariable("robot_footswitch_pressed", "0") == "1")
                    {
                        ErrorMessageBox("Cannot Continue. Footswitch Pressed!");
                        return;
                    }

                    MessageDialog messageForm = new MessageDialog()
                    {
                        Title = "System Question",
                        Label = "Repeat highlighted line or move on ?",
                        OkText = "&Repeat",
                        CancelText = "&Move On"
                    };
                    DialogResult result = messageForm.ShowDialog();
                    if (result == DialogResult.OK) lineCurrentlyExecuting--;
                    RobotSend("10");
                    SetState(RunState.RUNNING);
                    break;
            }
        }

        private void StepBtn_Click(object sender, EventArgs e)
        {
            log.Info("StepBtn_Click(...) STATE IS {0}", runState);
            if (ReadVariable("robot_door_closed", "0") != "1")
            {
                ErrorMessageBox("Cannot Step. Door Open!");
                return;
            }
            if (ReadVariable("robot_footswitch_pressed", "0") == "1")
            {
                ErrorMessageBox("Cannot Step. Footswitch Pressed!");
                return;
            }

            switch (runState)
            {
                case RunState.READY:
                    // This is like hitting "Start" except we also set isSingleStep so we'll halt after line 1
                    if (PrepareToRun())
                    {
                        isSingleStep = true;
                        SetRecipeState(RecipeState.RUNNING);
                        SetState(RunState.RUNNING);
                    }
                    break;
                case RunState.PAUSED:
                    // Perform STEP function
                    MessageDialog messageForm = new MessageDialog()
                    {
                        Title = "System Question",
                        Label = "Repeat highlighted line or move on to next?",
                        OkText = "&Repeat",
                        CancelText = "&Move On"
                    };
                    DialogResult result = messageForm.ShowDialog();
                    if (result == DialogResult.OK) lineCurrentlyExecuting--;
                    RobotSend("10");
                    isSingleStep = true;
                    SetState(RunState.RUNNING);
                    break;
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            log.Info("StopBtn_Click(...)");
            RobotSend("10");  // This will cancel any grind in progress

            // Make sure we are off the part
            ExecuteLine(-1, "grind_retract()");

            UnboldRecipe();
            SetState(RunState.READY);
            SetRecipeState(recipeStateAtRun);
        }


        // ===================================================================
        // END GRIND
        // ===================================================================

        // ===================================================================
        // START EDIT
        // ===================================================================

        // Drop any highlighted lines!
        private enum RecipeState
        {
            INIT,
            NEW,
            LOADED,
            MODIFIED,
            RUNNING
        }
        RecipeState recipeState = RecipeState.INIT;
        RecipeState recipeStateAtRun = RecipeState.INIT;
        private void SetRecipeState(RecipeState s)
        {
            if (recipeState != s)
            {
                log.Info("SetRecipeState({0})", s.ToString());

                RecipeState oldRecipeState = recipeState;
                recipeState = s;

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
                    case RecipeState.RUNNING:
                        recipeStateAtRun = oldRecipeState;
                        NewRecipeBtn.Enabled = false;
                        LoadRecipeBtn.Enabled = false;
                        SaveRecipeBtn.Enabled = false;
                        SaveAsRecipeBtn.Enabled = false;
                        break;
                }
                NewRecipeBtn.BackColor = NewRecipeBtn.Enabled ? Color.Green : Color.Gray;
                LoadRecipeBtn.BackColor = LoadRecipeBtn.Enabled ? Color.Green : Color.Gray;
                SaveRecipeBtn.BackColor = SaveRecipeBtn.Enabled ? Color.Green : Color.Gray;
                SaveAsRecipeBtn.BackColor = SaveAsRecipeBtn.Enabled ? Color.Green : Color.Gray;
            }
        }


        /// <summary>
        /// Load a filename into RecipeRTB and place the filename in RecipeFilenameLbl.Text
        /// If the file does nbot exist, clear all of the above and return false. Else return true.
        /// </summary>
        /// <param name="file">The file to be loaded.</param>
        /// <returns></returns>
        private string recipeAsLoaded = "";  // As it was when loaded so we can test for actual mods
        private bool RecipeWasModified()
        {
            return recipeAsLoaded != RecipeRTB.Text;
        }
        bool LoadRecipeFile(string file)
        {
            log.Info("LoadRecipeFile({0})", file);
            RecipeFilenameLbl.Text = "";
            RecipeRTB.Text = "";
            try
            {
                RecipeRTB.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.PlainText);
                RecipeFilenameLbl.Text = file;
                recipeAsLoaded = RecipeRTB.Text;
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
            if (RecipeWasModified())
            {
                var result = ConfirmMessageBox(String.Format("Recipe [{0}] has changed.\nSave changes?", LoadRecipeBtn.Text));
                if (result == DialogResult.OK)
                    SaveRecipeBtn_Click(null, null);
            }

            SetRecipeState(RecipeState.NEW);
            SetState(RunState.IDLE);
            RecipeFilenameLbl.Text = "Untitled";
            RecipeRTB.Clear();
            recipeAsLoaded = "";
            MainTab.SelectedIndex = 1; // = "Program";
        }

        private void LoadRecipeBtn_Click(object sender, EventArgs e)
        {
            log.Info("LoadRecipeBtn_Click(...)");
            if (RecipeWasModified())
            {
                var result = ConfirmMessageBox(String.Format("Recipe [{0}] has changed.\nSave changes?", LoadRecipeBtn.Text));
                if (result == DialogResult.OK)
                    SaveRecipeBtn_Click(null, null);
            }

            string initialDirectory;
            if (RecipeFilenameLbl.Text != "Untitled" && RecipeFilenameLbl.Text.Length > 0)
                initialDirectory = Path.GetDirectoryName(RecipeFilenameLbl.Text);
            else
                initialDirectory = Path.Combine(AutoGrindRoot, "Recipes");
            FileOpenDialog dialog = new FileOpenDialog()
            {
                Title = "Open an AutoGrind Recipe",
                Filter = "*.txt",
                InitialDirectory = initialDirectory
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (LoadRecipeFile(dialog.FileName))
                {
                    SetRecipeState(RecipeState.LOADED);
                    SetState(RunState.READY);
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
                recipeAsLoaded = RecipeRTB.Text;
                SetRecipeState(RecipeState.LOADED);
                SetState(RunState.READY);
            }
        }

        private void SaveAsRecipeBtn_Click(object sender, EventArgs e)
        {
            log.Info("SaveAsRecipeBtn_Click(...)");
            FileSaveAsDialog dialog = new FileSaveAsDialog()
            {
                Title = "Save an Autogrind Recipe As...",
                Filter = "*.txt",
                InitialDirectory = Path.Combine(AutoGrindRoot, "Recipes"),
                FileName = RecipeFilenameLbl.Text,
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName != "")
                {
                    string filename = dialog.FileName;
                    if (!filename.EndsWith(".txt")) filename += ".txt";
                    bool okToSave = true;
                    if (File.Exists(filename))
                    {
                        if (DialogResult.OK != ConfirmMessageBox(string.Format("File {0} already exists. Overwrite?", filename)))
                            okToSave = false;
                    }
                    if (okToSave)
                    {
                        RecipeFilenameLbl.Text = filename;
                        SaveRecipeBtn_Click(null, null);
                    }
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
            if (DialogResult.OK != ConfirmMessageBox("This will reset the General Configuration settings. Proceed?"))
                return;

            AutoGrindRoot = "C:\\AutoGrind";
            AutoGrindRootLbl.Text = AutoGrindRoot;
            AutoGrindRootLbl.Text = "AutoGrind/AutoGrind01.urp";
            ServerIpTxt.Text = "192.168.0.252";
            RobotIpTxt.Text = "192.168.0.2";
            AllowRunningOfflineChk.Checked = false;
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

        private void AllowRunningOfflineChk_CheckedChanged(object sender, EventArgs e)
        {
            if (AllowRunningOfflineChk.Checked)
                AllowRunningOfflineChk.BackColor = Color.Green;
            else
                AllowRunningOfflineChk.BackColor = Color.Gray;
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
            // Zebra L10 Tablet runs best at 2160x1440 100% mag
            Left = 0;// (Int32)AppNameKey.GetValue("Left", 0);
            Top = 0;// (Int32)AppNameKey.GetValue("Top", 0);
            Width = screenDesignWidth;// (Int32)AppNameKey.GetValue("Width", 1920);
            Height = screenDesignHeight;

            // From Setup Tab
            AutoGrindRoot = (string)AppNameKey.GetValue("AutoGrindRoot", "\\AutoGrind");
            if (!Directory.Exists(AutoGrindRoot))
            {
                DialogResult result = ConfirmMessageBox(String.Format("Root Directory [{0}] does not exist. Create it?", AutoGrindRoot));
                if (result == DialogResult.OK)
                {
                    System.IO.Directory.CreateDirectory(AutoGrindRoot);
                }
                else
                {
                    forceClose = true;
                    Close();
                    return;
                }
            }

            // Make standard subdirectories (if they don't exist)
            System.IO.Directory.CreateDirectory(Path.Combine(AutoGrindRoot, "Logs"));
            System.IO.Directory.CreateDirectory(Path.Combine(AutoGrindRoot, "Recipes"));

            AutoGrindRootLbl.Text = AutoGrindRoot;
            RobotProgramTxt.Text = (string)AppNameKey.GetValue("RobotProgramTxt.Text", "AutoGrind/AutoGrind01.urp");
            RobotIpTxt.Text = (string)AppNameKey.GetValue("RobotIpTxt.Text", "192.168.0.2");
            ServerIpTxt.Text = (string)AppNameKey.GetValue("ServerIpTxt.Text", "192.168.0.252");
            AllowRunningOfflineChk.Checked = Convert.ToBoolean(AppNameKey.GetValue("AllowRunningOfflineChk.Checked", "False"));

            // Operator Mode
            // Ignore persistence here: if we're running in debug it will kindly start us in Engineering mode else Operator
#if DEBUG
            operatorMode = OperatorMode.ENGINEERING;
#else
            operatorMode = OperatorMode.OPERATOR; // (OperatorMode)(Int32)AppNameKey.GetValue("operatorMode", 0);
#endif
            UserModeBox.SelectedIndex = (int)operatorMode;

            // Debug Level selection (forced to INFO now)
            // DebugLevelCombo.Text = (string)AppNameKey.GetValue("DebugLevelCombo.Text", "Info");
            LogLevelCombo.Text = "Info";

            // Load the tools table
            LoadToolsBtn_Click(null, null);

            // Load the positions table
            LoadPositionsBtn_Click(null, null);

            // Load the variables table
            LoadVariablesBtn_Click(null, null);

            // Load the Recipe Commands for User Inspection
            try
            {
                RecipeCommandsRTB.LoadFile("RecipeCommands.rtf");
            }
            catch (Exception ex)
            {
                log.Error(ex, "Could not load RecipeCommands.rtf!");
            }

            // Load Revision Hstory for User Inspection
            try
            {
                RevHistRTB.LoadFile("RevisionHistory.rtf");
            }
            catch (Exception ex)
            {
                log.Error(ex, "Could not load RevisionHistory.rtf!");
            }

            // Autoload file is the last loaded recipe
            recipeFileToAutoload = (string)AppNameKey.GetValue("RecipeFilenameLbl.Text", "");

            // Retrieve currently mounted tool
            MountedToolBox.Text = (string)AppNameKey.GetValue("MountedToolBox.Text", "");

            // Retrieve current part geometry
            for (int i = 0; i < 3; i++)
                diameterDefaults[i] = (string)AppNameKey.GetValue(String.Format("Diameter[{0}].Text", i), i == 0 ? "0.0" : "100.0");
            PartGeometryBox.Text = (string)AppNameKey.GetValue("PartGeometryBox.Text", "FLAT");
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
            AppNameKey.SetValue("AllowRunningOfflineChk.Checked", AllowRunningOfflineChk.Checked);

            // Operator Mode
            AppNameKey.SetValue("operatorMode", (Int32)operatorMode);

            // Debug Level selection
            AppNameKey.SetValue("DebugLevelCombo.Text", LogLevelCombo.Text);

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
            for (int i = 0; i < 3; i++)
                AppNameKey.SetValue(String.Format("Diameter[{0}].Text", i), diameterDefaults[i]);
        }

        private void ChangeRootDirectoryBtn_Click(object sender, EventArgs e)
        {
            log.Info("ChangeRootDirectoryBtn_Click(...)");
            DirectorySelectDialog dialog = new DirectorySelectDialog()
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

            JoggingDialog form = new JoggingDialog(this)
            {
                Prompt = "Jog to Defect",
                Tool = ReadVariable("robot_tool"),
                Part = partName
            };

            form.ShowDialog(this);
        }
        private void JogRunBtn_Click(object sender, EventArgs e)
        {
            JogBtn_Click(sender, e);
        }


        private void DiameterLbl_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = DiameterLbl.Text,
                Label = PartGeometryBox.Text + " DIAM, MM",
                NumberOfDecimals = 0,
                MaxAllowed = 3000,
                MinAllowed = 75,
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                DiameterLbl.Text = form.Value;
            }

            // Save value in slot associated with this geometry
            int index = PartGeometryBox.SelectedIndex;
            diameterDefaults[index] = DiameterLbl.Text;

            UpdateGeometryToRobot();
        }

        private void ChangeLogLevel(string s)
        {
            LogManager.Configuration.Variables["myLevel"] = s;
            LogManager.ReconfigExistingLoggers();
        }
        private void DebugLevelCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeLogLevel(LogLevelCombo.Text);
        }

        // ===================================================================
        // END SETUP
        // ===================================================================

        // ===================================================================
        // START EXECUTIVE
        // ===================================================================

        /// <summary>
        /// Is the line a recipe label? This means starting with alpha, followed by 0 or more alphanum, followed by :
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
            log.Debug("EXEC BuildLabelTable()");

            labels = new Dictionary<string, int>();

            int lineNo = 1;
            foreach (string line in RecipeRTB.Lines)
            {
                var label = IsLineALabel(line);
                if (label.Success)
                {
                    try
                    {
                        labels.Add(label.Value, lineNo);
                    }
                    catch
                    {
                        ErrorMessageBox(String.Format("Label Problem\nRepeated label \"{0}\" on line {1}", label.Value, lineNo));
                        return false;
                    }
                    log.Debug("EXEC Found label {0:000}: {1}", lineNo, label.Value);
                }
                lineNo++;
            }

            return true;
        }

        // 1-index line curently executing in recipe (1 is first line)
        static int lineCurrentlyExecuting = 0;
        /// <summary>
        /// Set the lineCurrentlyExecuting to n and highlight it in the RecipeRTB
        /// </summary>
        /// <param name="n">Line number to start executing</param>
        private string SetCurrentLine(int n)
        {
            lineCurrentlyExecuting = n;

            if (n >= 1 && n <= RecipeRTB.Lines.Count())
            {
                (int start, int length) = RecipeRTB.GetLineExtents(lineCurrentlyExecuting - 1);

                RecipeRTB.SelectAll();
                RecipeRTB.SelectionFont = new Font(RecipeRTB.Font, FontStyle.Regular);

                RecipeRTB.Select(start, length);
                RecipeRTB.SelectionFont = new Font(RecipeRTB.Font, FontStyle.Bold);
                RecipeRTB.ScrollToCaret();
                RecipeRTB.ScrollToCaret();

                RecipeRTBCopy.Select(start, length);
                RecipeRTBCopy.SelectionFont = new Font(RecipeRTBCopy.Font, FontStyle.Bold);
                RecipeRTBCopy.ScrollToCaret();
                RecipeRTBCopy.ScrollToCaret();
                return RecipeRTB.Lines[lineCurrentlyExecuting - 1];
            }
            return null;
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
                        UpdateVariable(line);
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
        private void PromptOperator(string message, bool closeOnReady = false, string heading = "AutoGrind Prompt")
        {
            log.Info("Prompting Operator: heading={0} message={1}", heading, message);
            waitingForOperatorMessageForm = new MessageDialog()
            {
                Title = heading,
                Label = message,
                OkText = "&Continue Execution",
                CancelText = "&Abort"
            };
            closeOperatorFormOnIndex = closeOnReady;
            waitingForOperatorMessageForm.ShowDialog();
        }

        /// <summary>
        /// Return the characters enclosed in the first set of matching ( ) in a string
        /// Example: "speed (13.0)" returns 13.0 
        /// </summary>
        /// <param name="s">input string</param>
        /// <returns>Characters enclosed in (...) or ""</returns>
        string ExtractParameters(string s, int nParams = -1, bool cutSpaces = true)
        {
            try
            {
                // Get what is enclosed between the first set of parentheses
                string parameters = "";
                parameters = Regex.Match(s, @"\(([^)]*)\)").Groups[1].Value;
                /* \(           # Starts with a '(' character"
                       (        # Parentheses in a regex mean "put (capture) the stuff in between into the Groups array
                          [^)]  # Any character that is not a ')' character
                          *     # Zero or more occurrences of the aforementioned "non ')' char
                       )        # Close the capturing group
                   \)           # Ends with a ')' character  */
                log.Trace("EXEC params=\"{0}\"", parameters);

                // Drop spaces if requested!
                if (cutSpaces)
                    parameters = Regex.Replace(parameters, @"\s+", "");


                // If nParams is specified (> -1), verify we have the right number!
                if (nParams > -1)
                {
                    if (nParams == 0)
                    {
                        if (parameters.Length != 0)
                        {
                            log.Trace("EXEC sees params={0} where none are expected", parameters);
                            return s;  // Nothing expected, we'll return what was there hoping to trigger a failure!
                        }
                    }
                    else
                    {
                        int commaCount = parameters.Count(f => (f == ','));
                        if (commaCount != nParams - 1)
                            return "";
                    }
                }
                return parameters;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Recipe line parameter error: {0} {1}", s, ex);
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

        // Specifies number of expected parameters and prefix in RobotSend for each function
        public struct CommandSpec
        {
            public int nParams;
            public string prefix;
        };

        // These recipe commands will be converted to send_robot(prefix,[nParams additional parameters])
        readonly Dictionary<string, CommandSpec> robotAlias = new Dictionary<string, CommandSpec>
        {
            // The main "send anything" command
            {"send_robot",              new CommandSpec(){nParams=-1, prefix="" } },

            {"set_linear_speed",        new CommandSpec(){nParams=1,  prefix="30,1," } },
            {"set_linear_accel",        new CommandSpec(){nParams=1,  prefix="30,2," } },
            {"set_blend_radius",        new CommandSpec(){nParams=1,  prefix="30,3," } },
            {"set_joint_speed",         new CommandSpec(){nParams=1,  prefix="30,4," } },
            {"set_joint_accel",         new CommandSpec(){nParams=1,  prefix="30,5," } },
            {"set_part_geometry_N",     new CommandSpec(){nParams=2,  prefix="30,6," } },
            {"set_door_closed_input",   new CommandSpec(){nParams=2,  prefix="30,10," } },
            {"set_tool_on_outputs",     new CommandSpec(){nParams=-1, prefix="30,11," } },
            {"set_tool_off_outputs",    new CommandSpec(){nParams=-1, prefix="30,12," } },
            {"set_coolant_on_outputs",  new CommandSpec(){nParams=-1, prefix="30,13," } },
            {"set_coolant_off_outputs", new CommandSpec(){nParams=-1, prefix="30,14," } },
            {"tool_on",                 new CommandSpec(){nParams=0,  prefix="30,15" } },
            {"tool_off",                new CommandSpec(){nParams=0,  prefix="30,16" } },
            {"coolant_on",              new CommandSpec(){nParams=0,  prefix="30,17" } },
            {"coolant_off",             new CommandSpec(){nParams=0,  prefix="30,18" } },
            {"free_drive",              new CommandSpec(){nParams=1,  prefix="30,19," } },
            {"set_tcp",                 new CommandSpec(){nParams=6,  prefix="30,20," } },
            {"set_payload",             new CommandSpec(){nParams=4,  prefix="30,21," } },
            {"set_footswitch_pressed_input", new CommandSpec(){nParams=2,  prefix="30,22," } },

            {"set_output",              new CommandSpec(){nParams=2,  prefix="30,30," } },

            {"grind_contact_enable",    new CommandSpec(){nParams=1,  prefix="35,1," } },
            {"grind_touch_retract",     new CommandSpec(){nParams=1,  prefix="35,2," } },
            {"grind_touch_speed",       new CommandSpec(){nParams=1,  prefix="35,3," } },
            {"grind_force_dwell",       new CommandSpec(){nParams=1,  prefix="35,4," } },
            {"grind_max_wait",          new CommandSpec(){nParams=1,  prefix="35,5," } },
            {"grind_max_blend_radius",  new CommandSpec(){nParams=1,  prefix="35,6," } },
            {"grind_trial_speed",       new CommandSpec(){nParams=1,  prefix="35,7," } },
            {"grind_accel",             new CommandSpec(){nParams=1,  prefix="35,8," } },

            {"grind_line",              new CommandSpec(){nParams=6,  prefix="40,10," }  },
            {"grind_rect",              new CommandSpec(){nParams=6,  prefix="40,20," }  },
            {"grind_serp",              new CommandSpec(){nParams=8,  prefix="40,30," }  },
            {"grind_circle",            new CommandSpec(){nParams=5,  prefix="40,40," }  },
            {"grind_spiral",            new CommandSpec(){nParams=7,  prefix="40,50," }  },
            {"grind_retract",           new CommandSpec(){nParams=0,  prefix="40,99" } },
        };
        private void LogInterpret(string command, int lineNumber, string line)
        {
            if (lineNumber < 1)
                log.Info("EXEC [{0}] {1}", command.ToUpper(), line);
            else
                log.Info("EXEC {0:0000}: [{1}] {2}", lineNumber, command.ToUpper(), line);
        }
        /// <summary>
        /// Return true iff string 'str' represents a number between lowLim and hiLim
        /// </summary>
        /// <param name="str">String to be checked</param>
        /// <param name="lowLim">Lowest allowable int</param>
        /// <param name="hiLim">Highest allowable int</param>
        /// <returns></returns>
        private bool ValidNumericString(string s, double lowLim, double hiLim)
        {
            try
            {
                double x = Convert.ToDouble(s);
                if (x < lowLim || x > hiLim)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void ExecError(string message)
        {
            log.Error("EXEC " + message.Replace('\n', ' '));
            PromptOperator("ERROR:\n" + message);
        }

        private bool ExecuteLine(int lineNumber, string line)
        {
            // Step is starting now
            stepStartedTime = DateTime.Now;

            // Default time estimate to complete step is 0
            stepEndTimeEstimate = stepStartedTime;

            // Any variables to substitute {varName}
            string origLine = line;
            line = Regex.Replace(line, @"\{([^}]*)\}", m => ReadVariable(m.Groups[1].Value, "var_not_found"));
            /* {            # Bracket, means "starts with a '{' character"
                   (        # Parentheses in a regex mean "put (capture) the stuff in between into the Groups array
                      [^}]  # Any character that is not a '}' character
                      *     # Zero or more occurrences of the aforementioned "non '}' char
                   )        # Close the capturing group
               }            # Ends with a '}' character  */
            if (line != origLine)
                log.Info("EXEC {0:0000}: \"{1}\" from \"{2}\"", lineNumber, line, origLine);

            // Line gets shown on screen with variables substututed and time estimate (unless we're making system calls)
            if (lineNumber > 0)
            {
                CurrentLineLbl.Text = String.Format("{0:000}: {1}", lineNumber, line);
                StepTimeEstimateLbl.Text = TimeSpanFormat(new TimeSpan());
            }


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

            // import filename
            if (command.StartsWith("import("))
            {
                LogInterpret("import", lineNumber, command);
                string file = ExtractParameters(command);
                if (file.Length > 1)
                {
                    if (!ImportFile(file))
                        ExecError(string.Format("File import error: {0} file would not import", command));
                }
                else
                    ExecError(String.Format("Invalid import command: {0}", command));

                return true;
            }

            // sleep
            if (command.StartsWith("sleep("))
            {
                LogInterpret("sleep", lineNumber, command);
                double sleepSeconds = Convert.ToDouble(ExtractParameters(command, 1).ToString());
                sleepMs = sleepSeconds * 1000.0;
                sleepTimer = new Stopwatch();
                sleepTimer.Start();

                double sec = Math.Truncate(sleepSeconds);
                double msec = (sleepSeconds - sec) * 1000.0;
                log.Info("Looks like {0} sec  {1} msec", sec, msec);

                TimeSpan ts = new TimeSpan(0, 0, 0, (int)sec, (int)msec);
                StepTimeEstimateLbl.Text = TimeSpanFormat(ts);
                stepEndTimeEstimate = DateTime.Now.AddMilliseconds(sleepMs);
                return true;
            }

            // assert
            if (command.StartsWith("assert("))
            {
                LogInterpret("assert", lineNumber, command);
                string[] parameters = ExtractParameters(command, 2).Split(',');
                if (parameters.Length != 2)
                {
                    ExecError(String.Format("Unknown assert command Line {0}: {1}", lineNumber, origLine));
                    return true;
                }
                string value = ReadVariable(parameters[0], null);
                if (value == null)
                {
                    ExecError(String.Format("Unknown variable in assert command Line {0}: {1}", lineNumber, origLine));
                    return true;
                }
                if (value != parameters[1])
                {
                    ExecError(String.Format("Assertion FAILS Line {0}: {1}", lineNumber, origLine));
                    return true;
                }
                return true;
            }

            // jump
            if (command.StartsWith("jump("))
            {
                string labelName = ExtractParameters(command);

                if (labels.TryGetValue(labelName, out int jumpLine))
                {
                    log.Info("EXEC {0:0000}: [JUMP] {1} --> {2:0000}", lineNumber, origLine, jumpLine);
                    SetCurrentLine(jumpLine);
                    return true;
                }
                else
                {
                    ExecError(String.Format("Unknown label specified in jump Line {0}: {1}", lineNumber, origLine));
                    return true;
                }
            }

            // jump_gt_zero
            if (command.StartsWith("jump_gt_zero("))
            {
                string[] parameters = ExtractParameters(command).Split(',');
                if (parameters.Length != 2)
                {
                    ExecError("Expected jump_gt_zero(variable,label):\nNot " + command);
                    return true;
                }
                else
                {
                    string variableName = parameters[0];
                    string labelName = parameters[1];

                    if (!labels.TryGetValue(labelName, out int jumpLine))
                    {
                        ExecError(String.Format("Expected jump_gt_zero(variable,label): {0} Label not found: {1}", origLine, labelName));
                        return true;
                    }
                    else
                    {
                        string value = ReadVariable(variableName);
                        if (value == null)
                        {
                            ExecError(String.Format("Expected jump_gt_zero(variable,label): {0} Variable not found: {1}", origLine, variableName));
                            return true;
                        }
                        else
                        {
                            try
                            {
                                double val = Convert.ToDouble(value);
                                if (val > 0.0)
                                {
                                    log.Info("EXEC {0:0000}: [JUMPGTZERO] {1} --> {2:0000}", lineNumber, origLine, jumpLine);
                                    SetCurrentLine(jumpLine);
                                }
                                return true;
                            }
                            catch
                            {
                                ExecError(String.Format("Could not convert jump_not_zero variable: {0} = {1} From: {2}", variableName, value, command));
                                return true;
                            }
                        }

                    }
                }
            }

            // move_joint
            if (command.StartsWith("move_joint("))
            {
                LogInterpret("move_joint", lineNumber, command);
                string positionName = ExtractParameters(command);

                if (!GotoPositionJoint(positionName))
                    ExecError(string.Format("Joint move failed line {0}: {1}", lineNumber, origLine));
                return true;
            }

            // move_linear
            if (command.StartsWith("move_linear("))
            {
                LogInterpret("move_linear", lineNumber, origLine);
                string positionName = ExtractParameters(command);

                if (!GotoPositionPose(positionName))
                    ExecError(string.Format("Linear move failed line {0}: {1}", lineNumber, origLine));
                return true;
            }

            // move_relative
            if (command.StartsWith("move_relative("))
            {
                LogInterpret("move_relative", lineNumber, origLine);
                string xy = ExtractParameters(command, 2);

                if (xy == "")
                    ExecError(string.Format("Relative move no parameters x,y\nline {0}: {1}", lineNumber, origLine));
                else
                {
                    try
                    {
                        string[] p = xy.Split(',');
                        double x = Convert.ToDouble(p[0]) / 1000.0;
                        double y = Convert.ToDouble(p[1]) / 1000.0;
                        if (Math.Abs(x) > 0.010 || Math.Abs(y) > 0.010)
                            ExecError(string.Format("X and Y must be no more than +/10mm\nline {0}: {1}", lineNumber, origLine));
                        else
                            // This is a move relative to part
                            switch (PartGeometryBox.Text)
                            {
                                case "FLAT":
                                    RobotSend(String.Format("15,{0},{1},0,0,0,0", x, y));
                                    break;
                                case "CYLINDER":
                                    // Convert y to radians!
                                    double diam = Convert.ToDouble(DiameterLbl.Text);
                                    RobotSend(String.Format("15,{0},0,0,{1},0,0", x, 2000 * y / diam));
                                    break;
                                case "SPHERE":
                                    // Convert x and y to radians!
                                    diam = Convert.ToDouble(DiameterLbl.Text);
                                    RobotSend(String.Format("15,0,0,0,{0},{1},0", 2000 * x / diam, 2000 * y / diam));
                                    break;
                                default:
                                    ExecError(string.Format("move_relative: Unknown geometry \"{0}\"\nline {1}: {2}", PartGeometryBox.Text, lineNumber, origLine));
                                    break;
                            }
                    }
                    catch
                    {
                        ExecError(string.Format("Relative move bad paramters x,y\nline {0}: {1}", lineNumber, origLine));
                    }
                }

                return true;
            }

            // save_position
            if (command.StartsWith("save_position("))
            {
                LogInterpret("save_position", lineNumber, origLine);
                string positionName = ExtractParameters(command);
                if (positionName.Length < 1)
                {
                    ExecError(string.Format("No position name specified\nline {0}: {1}", lineNumber, origLine));
                    return true;
                }
                copyPositionAtWrite = positionName;
                RobotSend("25");
                return true;
            }

            // move_tool_home
            if (command.StartsWith("move_tool_home()"))
            {
                LogInterpret("move_tool_home", lineNumber, origLine);
                MoveToolHomeBtn_Click(null, null);
                return true;
            }

            // move_tool_mount
            if (command.StartsWith("move_tool_mount()"))
            {
                LogInterpret("move_tool_mount", lineNumber, origLine);
                MoveToolMountBtn_Click(null, null);
                return true;
            }

            // end
            if (command == "end()" || command == "end")
            {
                LogInterpret("end", lineNumber, origLine);
                return false;
            }

            // select_tool  (Assumes operator has already installed it somehow!!)
            if (command.StartsWith("select_tool("))
            {
                LogInterpret("select_tool", lineNumber, origLine);
                DataRow row = FindTool(ExtractParameters(command, 1));
                if (row == null)
                {
                    log.Error("Unknown tool specified in EXEC: {0.000} {1}", lineNumber, command);
                    PromptOperator("Unrecognized select_tool command: " + command);
                    return true;
                }
                else
                {
                    // Kind of like a subroutine that calls all the pieces needed to effect a tool change
                    // Just in case... make sure we disable current tool
                    ExecuteLine(-1, String.Format("set_tcp({0},{1},{2},{3},{4},{5})", row["x_m"], row["y_m"], row["z_m"], row["rx_rad"], row["ry_rad"], row["rz_rad"]));
                    ExecuteLine(-1, String.Format("set_payload({0},{1},{2},{3})", row["mass_kg"], row["cogx_m"], row["cogy_m"], row["cogz_m"]));

                    ExecuteLine(-1, String.Format("tool_off()"));
                    ExecuteLine(-1, String.Format("coolant_off()"));
                    ExecuteLine(-1, String.Format("set_tool_on_outputs({0})", row["ToolOnOuts"]));
                    ExecuteLine(-1, String.Format("set_tool_off_outputs({0})", row["ToolOffOuts"]));
                    ExecuteLine(-1, String.Format("set_coolant_on_outputs({0})", row["CoolantOnOuts"]));
                    ExecuteLine(-1, String.Format("set_coolant_off_outputs({0})", row["CoolantOffOuts"]));
                    ExecuteLine(-1, String.Format("tool_off()"));
                    ExecuteLine(-1, String.Format("coolant_off()"));
                    WriteVariable("robot_tool", row["Name"].ToString());

                    // Set Move buttons to go to tool change and home locations
                    MoveToolMountBtn.Text = row["MountPosition"].ToString();
                    MoveToolHomeBtn.Text = row["HomePosition"].ToString();

                    // Update the UI selector but don't trigger another set of commands to the robot!
                    mountedToolBoxActionDisabled = true;
                    MountedToolBox.Text = (string)row["Name"];
                    mountedToolBoxActionDisabled = false;
                    // Give the UI some time to process all of those command returns!!!
                    Thread.Sleep(1000);
                }
                return true;
            }

            // set_part_geometry
            if (command.StartsWith("set_part_geometry("))
            {
                LogInterpret("set_part_geometry", lineNumber, origLine);

                string parameters = ExtractParameters(command, 2);
                if (parameters.Length == 0)
                {
                    log.Error("Illegal parameters for set_part_geometry EXEC: {0.000} {1}", lineNumber, command);
                    PromptOperator("Illegal set_part_geometry command:\n" + command);
                    return true;
                }
                string[] paramList = parameters.Split(',');
                if (paramList.Length != 2)
                {
                    log.Error("Illegal parameters for set_part_geometry EXEC: {0.000} {1}", lineNumber, command);
                    PromptOperator("Illegal set_part_geometry command:\n" + command);
                    return true;
                }

                switch (paramList[0])
                {
                    case "FLAT":
                        DiameterLbl.Text = "0.0";
                        DiameterLbl.Visible = false;
                        DiameterDimLbl.Visible = false;
                        break;
                    case "CYLINDER":
                        if (!ValidNumericString(paramList[1], 75, 3000))
                        {
                            log.Error("Diameter must be between 75 and 3000 EXEC: {0.000} {1}", lineNumber, command);
                            PromptOperator("Diameter be between 75 and 3000:\n" + command);
                            return true;
                        }
                        DiameterLbl.Text = paramList[1];
                        DiameterLbl.Visible = true;
                        DiameterDimLbl.Visible = true;
                        diameterDefaults[1] = paramList[1];
                        break;
                    case "SPHERE":
                        if (!ValidNumericString(paramList[1], 75, 3000))
                        {
                            log.Error("Diameter must be between 75 and 3000 EXEC: {0.000} {1}", lineNumber, command);
                            PromptOperator("Diameter be between 75 and 3000:\n" + command);
                            return true;
                        }
                        DiameterLbl.Text = paramList[1];
                        DiameterLbl.Visible = true;
                        DiameterDimLbl.Visible = true;
                        diameterDefaults[2] = paramList[1];
                        break;
                    default:
                        log.Error("First argument to must be FLAT, CYLINDER, or SPHERE EXEC: {0.000} {1}", lineNumber, command);
                        PromptOperator("First argument to must be FLAT, CYLINDER, or SPHERE:\n" + command);
                        return true;
                }

                // Update the UI control but don't have it trigger commands to robot, which is done explicitly below
                partGeometryBoxDisabled = true;
                PartGeometryBox.Text = paramList[0];
                partGeometryBoxDisabled = false;

                UpdateGeometryToRobot();
                return true;
            }

            // prompt
            if (command.StartsWith("prompt("))
            {
                LogInterpret("prompt", lineNumber, origLine);
                // This just displays the dialog. ExecTmr will wait for it to close
                PromptOperator(ExtractParameters(command, -1, false));
                return true;
            }

            // Handle all of the other robot commands (which just use send_robot, some prefix params, and any other specified params)
            // Example:
            // set_linear_speed(1.1) ==> RobotSend("30,1.1")
            // grind_rect(30,30,5,20,10) ==> RobotSend("40,20,30,30,5,20,10")
            // etc.

            // Find the commandName from commandName(parameters)
            int openParenIndex = command.IndexOf("(");
            int closeParenIndex = command.IndexOf(")");
            if (openParenIndex > -1 && closeParenIndex > openParenIndex)
            {
                string commandInRecipe = command.Substring(0, openParenIndex);
                if (robotAlias.TryGetValue(commandInRecipe, out CommandSpec commandSpec))
                {
                    LogInterpret(commandInRecipe, lineNumber, origLine);
                    string parameters = ExtractParameters(command, commandSpec.nParams);
                    // Must be all numeric: Really, all (nnn,nnn,nnn)
                    if (!Regex.IsMatch(parameters, @"^[()+-.,0-9]*$"))
                        ExecError(string.Format("Illegal parameters line {0}: {1}", lineNumber, origLine));
                    else
                    {
                        if (commandSpec.nParams > 0 && parameters.Length > 0 ||      // Got some parameters and must have been the right number
                           (commandSpec.nParams == 0 && parameters.Length == 0) ||   // Expected 0 parameters and got nothing
                           (commandSpec.nParams == -1 && parameters.Length > 0)      // Willing to accept whatever you have (as long as there's something!)
                           )
                            RobotSend(commandSpec.prefix + parameters);
                        else
                            ExecError(string.Format("Line {0}: Wrong number of operands. Expected {1} {2}", lineCurrentlyExecuting, commandSpec.nParams, origLine));
                    }
                    return true;
                }
            }

            // Matched nothing above... could be an assignment operator =, -=, +=, ++, --
            if (UpdateVariable(command))
            {
                LogInterpret("assign", lineNumber, origLine);
                return true;
            }

            ExecError(string.Format("Cannot interpret line {0}: {1}", lineNumber, origLine));
            return true;
        }


        private void UnboldRecipe()
        {
            RecipeRTB.SelectAll();
            RecipeRTB.SelectionFont = new Font(RecipeRTB.Font, FontStyle.Regular);
            RecipeRTB.DeselectAll();

            RecipeRTBCopy.SelectAll();
            RecipeRTBCopy.SelectionFont = new Font(RecipeRTB.Font, FontStyle.Regular);
            RecipeRTBCopy.DeselectAll();
        }

        bool isSingleStep = false;
        int logFilter = 0;
        Stopwatch sleepTimer = null;
        double sleepMs = 0;
        private void ReportStepTimeStats()
        {
            if (stepEndTimeEstimate != stepStartedTime)
            {
                // Redo this at the very end- normally is only called at 1Hz by HeartbeatTmr
                RecomputeTimes();
                log.Info("EXEC Estimated={0} Actual={1}", StepTimeEstimateLbl.Text, StepElapsedTimeLbl.Text);
            }
        }
        public bool RobotCompletedCaughtUp()
        {
            return ReadVariable("robot_completed") == robotSendIndex.ToString();
        }
        private void ExecTmr_Tick(object sender, EventArgs e)
        {
            // Wait for any operator prompt to be cleared
            if (waitingForOperatorMessageForm != null)
            {
                switch (waitingForOperatorMessageForm.result)
                {
                    case DialogResult.None:
                        return;
                    case DialogResult.Cancel:
                        log.Error("Operator selected \"Abort\" in MessageForm");
                        SetState(RunState.READY);
                        waitingForOperatorMessageForm = null;
                        return;
                    case DialogResult.OK:
                        log.Info("Operator selected \"Continue\" in MessageForm");
                        waitingForOperatorMessageForm = null;
                        break;
                }
            }

            // Stopwatch
            if (sleepTimer != null)
            {
                if (sleepTimer.ElapsedMilliseconds > sleepMs)
                    sleepTimer = null;
                else
                    return;
            }

            // Waiting on robotReady or will cook along if AllowRunningOffline
            if (!(robotReady || AllowRunningOfflineChk.Checked))
            {
                // Only log this one time!
                if (logFilter != 1)
                    log.Info("EXEC Waiting for robotReady");
                logFilter = 1;
            }
            else
            {
                if (!RobotCompletedCaughtUp())
                {
                    // Only log this one time!
                    if (logFilter != 2)
                        log.Info("Waiting for robot operation complete");
                    logFilter = 2;
                }
                else
                {
                    // Resets such that the above log messages will happen
                    logFilter = 3;
                    if (lineCurrentlyExecuting >= RecipeRTB.Lines.Count())
                    {
                        log.Info("EXEC Reached end of file");

                        ReportStepTimeStats();


                        // Make sure we're retracted
                        ExecuteLine(-1, "grind_retract()");

                        UnboldRecipe();
                        SetRecipeState(recipeStateAtRun);
                        SetState(RunState.READY);
                    }
                    else
                    {
                        ReportStepTimeStats();
                        string line = SetCurrentLine(lineCurrentlyExecuting + 1);
                        bool fContinue = ExecuteLine(lineCurrentlyExecuting, line);
                        if (isSingleStep)
                        {
                            isSingleStep = false;
                            SetState(RunState.PAUSED);
                        }
                        if (!fContinue)
                        {
                            log.Info("EXEC Aborting execution");
                            UnboldRecipe();
                            SetRecipeState(recipeStateAtRun);
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
        private void CloseDashboardClient()
        {
            // Disconnect client from dashboard
            if (robotDashboardClient != null)
            {
                if (robotDashboardClient.IsClientConnected)
                {
                    robotDashboardClient.InquiryResponse("stop");
                    robotDashboardClient.InquiryResponse("quit");
                    robotDashboardClient.Disconnect();
                }
                robotDashboardClient = null;
            }
            RobotConnectBtn.BackColor = Color.Red;
            RobotConnectBtn.Text = "OFF";
            RobotModeBtn.BackColor = Color.Red;
            SafetyStatusBtn.BackColor = Color.Red;
            ProgramStateBtn.BackColor = Color.Red;
        }
        private void CloseCommandServer()
        {
            // Stop us if we're running!
            if (runState == RunState.RUNNING || runState == RunState.PAUSED)
            {
                SetState(RunState.READY);
            }

            if (robotCommandServer != null)
            {
                if (robotCommandServer.IsConnected())
                {
                    RobotSend("98");
                    robotCommandServer.Disconnect();
                }
                robotCommandServer = null;
            }
            RobotCommandStatusLbl.BackColor = Color.Red;
            RobotCommandStatusLbl.Text = "OFF";
        }
        private void RobotDisconnect()
        {
            CloseCommandServer();
            CloseDashboardClient();
        }

        private void CloseSafetyPopup()
        {
            log.Info("close popup = {0}", robotDashboardClient?.InquiryResponse("close popup"), 200);
            log.Info("close safety popup = {0}", robotDashboardClient?.InquiryResponse("close safety popup"), 200);
        }
        private void RobotConnectBtn_Click(object sender, EventArgs e)
        {
            bool fReconnect = RobotConnectBtn.Text == "OFF";
            RobotDisconnect();

            if (!fReconnect) return;

            // Connect client to the UR dashboard
            robotDashboardClient = new TcpClientSupport("DASH")
            {
                ReceiveCallback = DashboardCallback
            };
            if (robotDashboardClient.Connect(RobotIpTxt.Text, "29999") > 0)
            {
                log.Error("Dashboard client initialization failure");
                RobotConnectBtn.BackColor = Color.Red;
                RobotConnectBtn.Text = "Dashboard ERROR";
                return;
            }
            log.Info("Dashboard connection ready");
            Thread.Sleep(50);
            string response = robotDashboardClient.Receive();
            if (response != "Connected: Universal Robots Dashboard Server")
            {
                log.Error("Dashboard connection returned {0}", response);

                // This would be unexpected but you might still be OK!
            }
            RobotConnectBtn.BackColor = Color.Green;
            RobotConnectBtn.Text = "Dashboard OK";

            // Start querying the bot
            RobotModelLbl.Text = robotDashboardClient.InquiryResponse("get robot model", 200);
            RobotSerialNumberLbl.Text = robotDashboardClient.InquiryResponse("get serial number", 200);
            RobotPolyscopeVersionLbl.Text = robotDashboardClient.InquiryResponse("PolyscopeVersion", 200);
            robotDashboardClient.InquiryResponse("stop", 200);
            CloseSafetyPopup();

            string closeSafetyPopupResponse = robotDashboardClient.InquiryResponse("close safety popup", 1000);
            string isInRemoteControlResponse = robotDashboardClient.InquiryResponse("is in remote control", 1000);
            if (isInRemoteControlResponse == null)
            {
                log.Error("Failed to be able to check remote control");
                ErrorMessageBox(String.Format("Failed to check reomte control mode. No response."));
                return;
            }
            if (isInRemoteControlResponse != "true")
            {
                log.Error("Robot not in remote control mode");
                ErrorMessageBox(String.Format("Robot not in remote control mode!"));
                return;
            }
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
        }

        int robotSendIndex = 100;
        // Command is a 0-n element comma-separated list "x,y,z" of doubles
        // We send (index,x,y,z)
        public bool RobotSend(string command)
        {
            if (robotCommandServer != null)
                if (robotCommandServer.IsConnected())
                {
                    ++robotSendIndex;
                    if (robotSendIndex > 999) robotSendIndex = 100;
                    try  // This fails if the jog thread is calling it!
                    {
                        RobotSentLbl.Text = robotSendIndex.ToString();
                        RobotSentLbl.Refresh();
                        RobotCompletedLbl.BackColor = Color.Red;
                        RobotCompletedLbl.Refresh();
                    }
                    catch { }

                    int checkValue = 1000 - robotSendIndex;
                    string sendMessage = string.Format("({0},{1},{2})", robotSendIndex, checkValue, command);
                    robotCommandServer.Send(sendMessage);
                    return true;
                }
            return false;
        }
        private void SetLinearSpeedBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("robot_linear_speed_mmps"),
                Label = "default Robot LINEAR SPEED, mm/s",
                NumberOfDecimals = 1,
                MinAllowed = 10,
                MaxAllowed = 500
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_linear_speed({0})", form.Value));
            }
        }

        private void SetLinearAccelBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("robot_linear_accel_mmpss"),
                Label = "default Robot LINEAR ACCELERATION, mm/s^2",
                NumberOfDecimals = 1,
                MinAllowed = 10,
                MaxAllowed = 2000
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_linear_accel({0})", form.Value));
            }
        }

        private void SetBlendRadiusBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("robot_blend_radius_mm"),
                Label = "default Robot BLEND RADIUS, mm",
                NumberOfDecimals = 1,
                MinAllowed = 0,
                MaxAllowed = 10
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_blend_radius({0})", form.Value));
            }
        }
        private void SetJointSpeedBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("robot_joint_speed_dps"),
                Label = "default Robot JOINT SPEED, deg/s",
                NumberOfDecimals = 1,
                MinAllowed = 2,
                MaxAllowed = 45
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_joint_speed({0})", form.Value));
            }
        }

        private void SetJointAccelBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("robot_joint_accel_dpss"),
                Label = "default Robot JOINT ACCELERATION, deg/s^2",
                NumberOfDecimals = 1,
                MinAllowed = 2,
                MaxAllowed = 180
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("set_joint_accel({0})", form.Value));
            }
        }
        private void SetMoveDefaultsBtn_Click(object sender, EventArgs e)
        {
            log.Info("SetMoveDefaultsBtn_Click(...)");
            if (DialogResult.OK != ConfirmMessageBox("This will reset the Default Motion Parameters. Proceed?"))
                return;

            ExecuteLine(-1, String.Format("set_linear_speed({0})", 200));
            ExecuteLine(-1, String.Format("set_linear_accel({0})", 500));
            ExecuteLine(-1, String.Format("set_blend_radius({0})", 3));
            ExecuteLine(-1, String.Format("set_joint_speed({0})", 20));
            ExecuteLine(-1, String.Format("set_joint_accel({0})", 30));
        }

        private void SetTouchSpeedBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("grind_touch_speed_mmps"),
                Label = "Grind TOUCH SPEED, mm/s",
                NumberOfDecimals = 1,
                MinAllowed = 1,
                MaxAllowed = 15
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("grind_touch_speed({0})", form.Value));
            }
        }

        private void SetTouchRetractBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("grind_touch_retract_mm"),
                Label = "Grind TOUCH RETRACT DISTANCE, mm",
                NumberOfDecimals = 1,
                MinAllowed = 0,
                MaxAllowed = 10
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("grind_touch_retract({0})", form.Value));
            }
        }
        private void SetForceDwellBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("grind_force_dwell_ms"),
                Label = "Grind FORCE DWELL TIME, mS",
                NumberOfDecimals = 0,
                MinAllowed = 0,
                MaxAllowed = 2000
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("grind_force_dwell({0})", form.Value));
            }
        }

        private void SetMaxWaitBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("grind_max_wait_ms"),
                Label = "Grind MAX WAIT TIME, mS",
                NumberOfDecimals = 0,
                MinAllowed = 0,
                MaxAllowed = 3000
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("grind_max_wait({0})", form.Value));
            }
        }
        private void SetMaxGrindBlendRadiusBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("grind_max_blend_radius_mm"),
                Label = "Grind MAX BLEND RADIUS, mm",
                NumberOfDecimals = 1,
                MinAllowed = 0,
                MaxAllowed = 10,
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("grind_max_blend_radius({0})", form.Value));
            }
        }
        private void SetTrialSpeedBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("grind_trial_speed_mmps"),
                Label = "Grind TRIAL SPEED, mm/s",
                NumberOfDecimals = 1,
                MinAllowed = 1,
                MaxAllowed = 200
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("grind_trial_speed({0})", form.Value));
            }
        }

        private void SetGrindAccelBtn_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm()
            {
                Value = ReadVariable("grind_accel_mmpss"),
                Label = "Grind ACCELERATION, mm/s^2",
                NumberOfDecimals = 1,
                MinAllowed = 10,
                MaxAllowed = 500
            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ExecuteLine(-1, String.Format("grind_accel({0})", form.Value));
            }
        }
        private void SetGrindDefaultsBtn_Click(object sender, EventArgs e)
        {
            log.Info("SetGrindDefaultsBtn_Click(...)");
            if (DialogResult.OK != ConfirmMessageBox("This will reset the Grinding Motion Parameters. Proceed?"))
                return;

            ExecuteLine(-1, String.Format("grind_trial_speed({0})", 20));
            ExecuteLine(-1, String.Format("grind_accel({0})", 100));
            ExecuteLine(-1, String.Format("grind_max_blend_radius({0})", 1.5));
            ExecuteLine(-1, String.Format("grind_touch_speed({0})", 10));
            ExecuteLine(-1, String.Format("grind_touch_retract({0})", 3));
            ExecuteLine(-1, String.Format("grind_force_dwell({0})", 500));
            ExecuteLine(-1, String.Format("grind_max_wait({0})", 1500));
        }



        /// <summary>
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
                if (request.Contains("="))
                    UpdateVariable(request);
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
            if (robotCommandServer != null)
                if (robotCommandServer.IsConnected())
                {
                    robotCommandServer.Receive();
                    return;
                }
            RobotReadyLbl.BackColor = Color.Red;
            GrindReadyLbl.BackColor = Color.Red;
            GrindProcessStateLbl.BackColor = Color.Red;
        }

        // ===================================================================
        // END ROBOT INTERFACE
        // ===================================================================

        // ===================================================================
        // START VARIABLE SYSTEM
        // ===================================================================

        readonly string variablesFilename = "Variables.xml";

        public string ReadVariable(string name, string defaultValue = null)
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


        private Color ColorFromBooleanName(string name, bool invert = false)
        {
            switch (name)
            {
                case "True":
                    return invert ? Color.Red : Color.Green;
                case "False":
                    return invert ? Color.Green : Color.Red;
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

            // Automatically consider and variables with name starting in robot_ or grind_to be system variables
            if (nameTrimmed.StartsWith("robot_") || nameTrimmed.StartsWith("grind_")) isSystem = true;

            log.Trace("WriteVariable({0}, {1})", nameTrimmed, valueTrimmed);
            if (variables == null)
            {
                log.Error("variables == null!!??");
                return false;
            }
            string datetime;
            //datetime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");  // If you prefer UTC time
            datetime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");

            bool foundVariable = false;
            foreach (DataRow row in variables.Rows)
            {
                if ((string)row["Name"] == nameTrimmed)
                {
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
                    RobotReadyLbl.Refresh();
                    break;
                case "robot_starting":
                    // This gets sent to us by command_validate on the UR. It means command valueTrimmed is going to start executing
                    log.Info("UR<== EXEC {0} STARTING", valueTrimmed);
                    break;
                case "robot_completed":
                    // This gets sent to us by PolyScope on the UR after command valueTrimmed has finished executing
                    RobotCompletedLbl.Text = valueTrimmed;
                    log.Info("UR<== EXEC {0} COMPLETED", valueTrimmed);

                    // Color us green if we're caught up!
                    if (RobotSentLbl.Text == RobotCompletedLbl.Text)
                        RobotCompletedLbl.BackColor = Color.Green;
                    RobotCompletedLbl.Refresh();

                    // Close operator "wait for robot" form if we're caught up
                    if (waitingForOperatorMessageForm != null && closeOperatorFormOnIndex && RobotSentLbl.Text == RobotCompletedLbl.Text)
                    {
                        waitingForOperatorMessageForm.Close();
                        waitingForOperatorMessageForm = null;
                        closeOperatorFormOnIndex = false;
                    }
                    break;
                case "grind_ready":
                    GrindReadyLbl.BackColor = ColorFromBooleanName(valueTrimmed);
                    break;
                case "grind_process_state":
                    GrindProcessStateLbl.BackColor = ColorFromBooleanName(valueTrimmed, true);
                    break;
                case "grind_n_cycles":
                    GrindNCyclesLbl.Text = valueTrimmed;
                    break;
                case "grind_cycle":
                    if (valueTrimmed == "0") valueTrimmed = "-";  // Gets set to 0 initially, will go to 1 when 1st actual cycle starts
                    GrindCycleLbl.Text = valueTrimmed;
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
                case "robot_door_closed_input":
                    DoorClosedInputLbl.Text = DoorClosedInputTxt.Text = valueTrimmed.Trim(new char[] { '[', ']' });
                    break;
                case "robot_footswitch_pressed_input":
                    FootswitchPressedInputLbl.Text = FootswitchPressedInputTxt.Text = valueTrimmed.Trim(new char[] { '[', ']' });
                    break;
                case "robot_step_time_estimate_ms":
                    double ms = Convert.ToDouble(valueTrimmed);
                    stepEndTimeEstimate = stepStartedTime.AddMilliseconds(ms);
                    TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)ms);
                    StepTimeEstimateLbl.Text = TimeSpanFormat(ts);
                    break;
                case "robot_door_closed":
                    switch (valueTrimmed)
                    {
                        case "0":
                            DoorClosedLbl.Text = "DOOR OPEN";
                            DoorClosedLbl.BackColor = Color.Red;
                            if (runState == RunState.RUNNING)
                                PauseBtn_Click(null, null);
                            break;
                        case "1":
                            DoorClosedLbl.Text = "DOOR CLOSED";
                            DoorClosedLbl.BackColor = Color.Green;
                            break;
                        default:
                            DoorClosedLbl.Text = "DOOR ????";
                            DoorClosedLbl.BackColor = Color.Yellow;
                            break;
                    }
                    break;
                case "robot_footswitch_pressed":
                    switch (valueTrimmed)
                    {
                        case "0":
                            FootswitchPressedLbl.Text = "FOOTSWITCH\nNot Pressed";
                            FootswitchPressedLbl.BackColor = Color.Green;
                            if (runState != RunState.RUNNING)
                                // Disable freedrive mode
                                RobotSend("30,19,0");
                            break;
                        case "1":
                            FootswitchPressedLbl.Text = "FOOTSWITCH PRESSED";
                            FootswitchPressedLbl.BackColor = Color.Red;
                            if (runState != RunState.RUNNING)
                                // Enable freedrive in base coords with all axes on
                                RobotSend("30,19,1,0,1,1,1,1,1,1");
                            break;
                        default:
                            FootswitchPressedLbl.Text = "FOOTSWITCH ????";
                            FootswitchPressedLbl.BackColor = Color.Yellow;
                            break;
                    }
                    break;
                case "grind_touch_speed_mmps":
                    SetTouchSpeedBtn.Text = "Grind Touch Speed\n" + valueTrimmed + " mm/s";
                    break;
                case "grind_touch_retract_mm":
                    SetTouchRetractBtn.Text = "Grind Touch Retract\n" + valueTrimmed + " mm";
                    break;
                case "grind_force_dwell_ms":
                    SetForceDwellBtn.Text = "Grind Force Dwell Time\n" + valueTrimmed + " ms";
                    break;
                case "grind_max_wait_ms":
                    SetMaxWaitBtn.Text = "Grind Max Wait Time\n" + valueTrimmed + " ms";
                    break;
                case "grind_max_blend_radius_mm":
                    SetMaxGrindBlendRadiusBtn.Text = "Grind Max Blend Radius\n" + valueTrimmed + " mm";
                    break;
                case "grind_trial_speed_mmps":
                    SetTrialSpeedBtn.Text = "Grind Trial Speed\n" + valueTrimmed + " mm/s";
                    break;
                case "grind_accel_mmpss":
                    SetGrindAccelBtn.Text = "Grind Acceleration\n" + valueTrimmed + " mm/s^2";
                    break;
                case "grind_contact_enable":
                    switch (valueTrimmed)
                    {
                        case "0":
                            GrindContactEnabledBtn.Text = "Touch OFF\nGrind OFF";
                            GrindContactEnabledBtn.BackColor = Color.Red;
                            break;
                        case "1":
                            GrindContactEnabledBtn.Text = "Touch ON\nGrind OFF";
                            GrindContactEnabledBtn.BackColor = Color.Blue;
                            break;
                        case "2":
                            GrindContactEnabledBtn.Text = "Touch ON\n Grind ON";
                            GrindContactEnabledBtn.BackColor = Color.Green;
                            break;
                        default:
                            GrindContactEnabledBtn.Text = "????";
                            GrindContactEnabledBtn.BackColor = Color.Red;
                            break;
                    }
                    break;
            }

            //variables.AcceptChanges();
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


        // Regex to look for varname = value expressions (value can be any string, numeric or not)
        // @"^\s*                           Start of line, ignore leading whitespace
        // (?<name>[A-Za-z][A-Za-z0-9_]*)   Group "name" is one alpha followed by 0 or more alphanum and underscore
        // \s*                              Ignore whitespace
        // =                                Equals
        // \s*                              Ignore whitespace
        // (?<value>[A-Za-z0-9 _]+)         Group "value" is one or morealphanum space or underscore
        // \s*$"                            Ignore whitespace to EOL
        static readonly Regex directAssignmentRegex = new Regex(@"^\s*(?<name>[A-Za-z][A-Za-z0-9_]*)\s*=\s*(?<value>[\S ]+)$", RegexOptions.ExplicitCapture & RegexOptions.Compiled);

        // Regex to look for varname += or -= number expressions
        // @"^\s*                           Start of line, ignore leading whitespace
        // (?<name>[A-Za-z][A-Za-z0-9_]*)   Group "name" is one alpha followed by 0 or more alphanum and underscore
        // \s*                              Ignore whitespace
        // (?<operator>(\+=|\-=))           Group "operator" can be += or -=
        // \s*                              Ignore whitespace
        // (?<value>[\-+]?[0-9.]+)          Group "value" can be optional (+ or -) followed by one or more digits and decimal
        // \s*$"                            Ignore whitespace to EOL
        static readonly Regex plusMinusEqualsRegex = new Regex(@"^\s*(?<name>[A-Za-z][A-Za-z0-9_]*)\s*(?<operator>(\+=|\-=))\s*(?<value>[\-+]?[0-9.]+)$", RegexOptions.ExplicitCapture & RegexOptions.Compiled);

        // Regex to look for varname += or -= number expressions
        // @"^\s*                           Start of line, ignore leading whitespace
        // (?<name>[A-Za-z][A-Za-z0-9_]*)   Group "name" is one alpha followed by 0 or more alphanum and underscore
        // (?<operator>(\+\+|\-\-))         Group "operator" can be ++ or --
        // \s*$"                            Ignore whitespace to EOL
        static readonly Regex plusPlusMinusMinusRegex = new Regex(@"^\s*(?<name>[A-Za-z][A-Za-z0-9_]*)(?<operator>(\+\+|\-\-))\s*$", RegexOptions.ExplicitCapture & RegexOptions.Compiled);

        /// <summary>
        /// Takes a "name=value" string and set variable "name" equal to "value"
        /// ALSO: will handle name++ and name--
        /// ALSO: will handle name+=value and name-=value
        /// </summary>
        /// <param name="assignment">Variable to be modified</param>
        public bool UpdateVariable(string assignment, bool isSystem = false)
        {
            bool wasSuccessful = false;

            Match m = directAssignmentRegex.Match(assignment);
            if (m.Success)
            {
                log.Trace("DirectAssignment {0}={1}", m.Groups["name"].Value, m.Groups["value"].Value);
                wasSuccessful = WriteVariable(m.Groups["name"].Value, m.Groups["value"].Value, isSystem);
            }
            else
            {
                m = plusMinusEqualsRegex.Match(assignment);
                if (m.Success)
                {
                    log.Trace("PlusMinusEqualsAssignment {0}{1}{2}", m.Groups["name"].Value, m.Groups["operator"].Value, m.Groups["value"].Value);
                    string v = ReadVariable(m.Groups["name"].Value);
                    if (v != null)
                    {
                        try
                        {
                            double x = Convert.ToDouble(v);
                            double y = Convert.ToDouble(m.Groups["value"].Value);
                            x = x + ((m.Groups["operator"].Value == "+=") ? y : -y);

                            wasSuccessful = WriteVariable(m.Groups["name"].Value, x.ToString());
                        }
                        catch { }
                    }
                }
                else
                {
                    m = plusPlusMinusMinusRegex.Match(assignment);
                    if (m.Success)
                    {
                        log.Trace("IncrAssignment {0}{1}", m.Groups["name"].Value, m.Groups["operator"].Value);
                        string v = ReadVariable(m.Groups["name"].Value);
                        if (v != null)
                        {
                            try
                            {
                                double x = Convert.ToDouble(v);
                                x = x + ((m.Groups["operator"].Value == "++") ? 1.0 : -1.0);
                                wasSuccessful = WriteVariable(m.Groups["name"].Value, x.ToString());
                            }
                            catch { }
                        }
                    }
                }
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

            // Clear the IsNew flags
            foreach (DataRow row in variables.Rows)
                row["IsNew"] = false;
        }

        private void SaveVariablesBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", variablesFilename);
            log.Info("SaveVariables to {0}", filename);
            variables.AcceptChanges();
            variables.WriteXml(filename, XmlWriteMode.WriteSchema, true);
        }

        private bool DeleteFirstNonSystemEntry(DataTable table)
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
            while (DeleteFirstNonSystemEntry(variables)) ;
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
        private DataRow SelectedRow(DataGridView dg)
        {
            if (dg.SelectedCells.Count < 1) return null;
            var cell = dg.SelectedCells[0];
            if (cell.ColumnIndex != 0) return null;
            if (cell.Value == null) return null;
            return (DataRow)((DataRowView)dg.Rows[cell.RowIndex].DataBoundItem).Row;
        }
        private string SelectedName(DataGridView dg)
        {
            if (dg.SelectedCells.Count < 1) return null;
            var cell = dg.SelectedCells[0];
            if (cell.ColumnIndex != 0) return null;
            if (cell.Value == null) return null;
            return cell.Value.ToString();
        }
        private void SelectToolBtn_Click(object sender, EventArgs e)
        {
            string name = SelectedName(ToolsGrd);
            if (name == null)
                ErrorMessageBox("Please select a tool in the tool table.");
            else
            {
                log.Info("Selecting tool {0}", name);
                ExecuteLine(-1, string.Format("select_tool({0})", name));
            }
        }

        private void JointMoveMountBtn_Click(object sender, EventArgs e)
        {
            string name = SelectedName(ToolsGrd);
            if (name == null)
                ErrorMessageBox("Please select a tool in the tool table.");
            else
            {
                string position = SelectedRow(ToolsGrd)["MountPosition"].ToString();
                log.Info("Joint Move Tool Mount to {0}", position.ToString());
                GotoPositionJoint(position);
                PromptOperator("Wait for move to tool mount position complete", true);
            }
        }

        private void JointMoveHomeBtn_Click(object sender, EventArgs e)
        {
            string name = SelectedName(ToolsGrd);
            if (name == null)
                ErrorMessageBox("Please select a tool in the tool table.");
            else
            {
                string position = SelectedRow(ToolsGrd)["HomePosition"].ToString();
                log.Info("Joint Move Tool Home to {0}", position);
                GotoPositionJoint(position);
                PromptOperator("Wait for move to tool home complete", true);
            }
        }


        private void ToolTestBtn_Click(object sender, EventArgs e)
        {
            ExecuteLine(-1, "tool_on()");
        }

        private void ToolOffBtn_Click(object sender, EventArgs e)
        {
            ExecuteLine(-1, "tool_off()");
        }

        private void CoolantTestBtn_Click(object sender, EventArgs e)
        {
            ExecuteLine(-1, "coolant_on()");
        }

        private void CoolantOffBtn_Click(object sender, EventArgs e)
        {
            ExecuteLine(-1, "coolant_off()");
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
            tools.Columns.Add("ToolOnOuts", typeof(System.String));
            tools.Columns.Add("ToolOffOuts", typeof(System.String));
            tools.Columns.Add("CoolantOnOuts", typeof(System.String));
            tools.Columns.Add("CoolantOffOuts", typeof(System.String));
            tools.Columns.Add("MountPosition", typeof(System.String));
            tools.Columns.Add("HomePosition", typeof(System.String));
            tools.CaseSensitive = true;
            tools.PrimaryKey = new DataColumn[] { name };

            ToolsGrd.DataSource = tools;
        }
        private void RefreshMountedToolBox(int currentToolIndex = -1)
        {
            log.Info("RefreshMountedToolBox({0})", currentToolIndex);
            MountedToolBox.Items.Clear();
            foreach (DataRow row in tools.Rows)
            {
                MountedToolBox.Items.Add(row["Name"]);
            }

            if (currentToolIndex>=0)
            {
                log.Info("Selecting {0}", currentToolIndex);
                MountedToolBox.SelectedIndex = currentToolIndex;
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
            {
                DialogResult result = ConfirmMessageBox("Could not load tools. Create some defaults?");
                if (result == DialogResult.OK)
                {
                    CreateDefaultTools();
                }
            }

            ToolsGrd.DataSource = tools;
            RefreshMountedToolBox();
        }


        private void SaveToolsBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(AutoGrindRoot, "Recipes", toolsFilename);
            log.Info("SaveTools to {0}", filename);
            tools.AcceptChanges();
            tools.WriteXml(filename, XmlWriteMode.WriteSchema, true);
            RefreshMountedToolBox(MountedToolBox.SelectedIndex);
        }

        private void CreateDefaultTools()
        {
            tools.Rows.Add(new object[] { "sander", 0, 0, 0.186, 0, 0, 0, 2.99, -0.011, 0.019, 0.067, "2,1", "2,0", "3,1", "3,0", "sander_mount", "sander_home" });
            tools.Rows.Add(new object[] { "spindle", 0, -0.165, 0.09, 0, 2.2214, -2.2214, 2.61, -0.004, -0.015, 0.049, "5,1", "5,0", "3,1", "3,0", "spindle_mount", "spindle_home" });
            tools.Rows.Add(new object[] { "pen", 0, -0.08, 0.075, 0, 2.2214, -2.2214, 1.0, -0.004, -0.015, 0.049, "2,0,5,0", "2,0,5,0", "3,0", "3,0", "spindle_mount", "spindle_home" });
            tools.Rows.Add(new object[] { "pen_SA", 0, -0.072, 0.103, 0, 2.2214, -2.2214, 0.98, 0, 0.002, 0.048, "2,0,5,0", "2,0,5,0", "3,0", "3,0", "spindle_mount", "spindle_home" });
            tools.Rows.Add(new object[] { "2F85", 0, 0, 0.175, 0, 0, 0, 1.0, 0, 0, 0.050, "2,1,5,1", "2,0,5,0", "3,1", "3,0", "sander_mount", "sander_home" });
            tools.Rows.Add(new object[] { "none", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "sander_mount", "sander_home" });
        }

        /*
          From TQ 5/18/2022
          <Tools>
            <Name>sander</Name>
            <x_m>0</x_m>
            <y_m>0</y_m>
            <z_m>0.186</z_m>
            <rx_rad>0</rx_rad>
            <ry_rad>0</ry_rad>
            <rz_rad>0</rz_rad>
            <mass_kg>2.99</mass_kg>
            <cogx_m>-0.011</cogx_m>
            <cogy_m>0.019</cogy_m>
            <cogz_m>0.067</cogz_m>
            <ToolOnOuts>2,1</ToolOnOuts>
            <ToolOffOuts>2,0</ToolOffOuts>
            <CoolantOnOuts>3,1</CoolantOnOuts>
            <CoolantOffOuts>3,0</CoolantOffOuts>
            <MountPosition>sander_mount</MountPosition>
            <HomePosition>sander_home</HomePosition>
          </Tools>
          <Tools>
            <Name>spindle</Name>
            <x_m>0</x_m>
            <y_m>-0.165</y_m>
            <z_m>0.09</z_m>
            <rx_rad>0</rx_rad>
            <ry_rad>2.2214</ry_rad>
            <rz_rad>-2.2214</rz_rad>
            <mass_kg>2.61</mass_kg>
            <cogx_m>-0.004</cogx_m>
            <cogy_m>-0.015</cogy_m>
            <cogz_m>0.049</cogz_m>
            <ToolOnOuts>5,1</ToolOnOuts>
            <ToolOffOuts>5,0</ToolOffOuts>
            <CoolantOnOuts>3,1</CoolantOnOuts>
            <CoolantOffOuts>3,0</CoolantOffOuts>
            <MountPosition>spindle_mount</MountPosition>
            <HomePosition>spindle_home</HomePosition>
          </Tools>
        */
        private void ClearToolsBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == ConfirmMessageBox("This will clear all existing tools. Proceed?"))
                ClearAndInitializeTools();
            if (DialogResult.OK == ConfirmMessageBox("Would you like to create the default tools?"))
                CreateDefaultTools();
        }

        private void MoveToolMountBtn_Click(object sender, EventArgs e)
        {
            GotoPositionJoint(MoveToolMountBtn.Text);
            PromptOperator("Wait for move to tool mount position complete", true);
        }

        private void MoveToolHomeBtn_Click(object sender, EventArgs e)
        {
            GotoPositionJoint(MoveToolHomeBtn.Text);
            PromptOperator("Wait for move to tool home complete", true);
        }

        private void SetDoorClosedInputBtn_Click(object sender, EventArgs e)
        {
            ExecuteLine(-1, string.Format("set_door_closed_input({0})", DoorClosedInputTxt.Text));
        }

        private void SetFootswitchInputBtn_Click(object sender, EventArgs e)
        {
            ExecuteLine(-1, string.Format("set_footswitch_pressed_input({0})", FootswitchPressedInputTxt.Text));
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
            if (DialogResult.OK == ConfirmMessageBox("This will clear all non-system positions. Proceed?"))
                while (DeleteFirstNonSystemEntry(positions)) ;
        }

        private void CreateDefaultPositions()
        {
            positions.Rows.Add(new object[] { "spindle_mount", "[-2.68179,-1.90227,-1.42486,-2.95848,-1.70261,0.000928376]", "p[-0.928515, -0.296863, 0.369036, 1.47493, 2.77222, 0.00280416]" });
            positions.Rows.Add(new object[] { "spindle_home", "[-2.71839,-0.892528,-2.14111,-3.27621,-1.68817,-0.019554]", "p[-0.410055, -0.0168446, 0.429258, -1.54452, -2.73116, -0.0509774]" });
            positions.Rows.Add(new object[] { "sander_mount", "[-2.53006,-2.15599,-1.18223,-1.37402,1.57131,0.124]", "p[-0.933321, -0.442727, 0.284064, 1.61808, 2.6928, 0.000150004]" });
            positions.Rows.Add(new object[] { "sander_home", "[-2.57091,-0.82644,-2.14277,-1.743,1.57367,-0.999559]", "p[-0.319246, 0.00105911, 0.464005, -5.0997e-05, 3.14151, 3.32468e-05]" });
            positions.Rows.Add(new object[] { "grind1", "[-0.964841,-1.56224,-2.25801,-2.46721,-0.975704,0.0351043]", "p[0.115668, -0.664968, 0.149296, -0.0209003, 3.11011, 0.00405717]" });
            positions.Rows.Add(new object[] { "grind2", "[-1.19025,-1.54723,-2.28053,-2.45891,-1.20106,0.0341677]", "p[0.00572967, -0.666445, 0.145823, -0.0208504, 3.11009, 0.004073]" });
            positions.Rows.Add(new object[] { "grind3", "[-1.41341,-1.57357,-2.26161,-2.45085,-1.42422,0.0333479]", "p[-0.0942147, -0.667831, 0.142729, -0.0208677, 3.1101, 0.00394188]" });
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

        /*
          TQ Positions 5/18/2022
          <Positions>
            <Name>spindle_mount</Name>
            <Joints>[-2.68179,-1.90227,-1.42486,-2.95848,-1.70261,0.000928376]</Joints>
            <Pose>p[-0.928515, -0.296863, 0.369036, 1.47493, 2.77222, 0.00280416]</Pose>
            <IsSystem>false</IsSystem>
          </Positions>
          <Positions>
            <Name>spindle_home</Name>
            <Joints>[-2.71839,-0.892528,-2.14111,-3.27621,-1.68817,-0.019554]</Joints>
            <Pose>p[-0.410055, -0.0168446, 0.429258, -1.54452, -2.73116, -0.0509774]</Pose>
            <IsSystem>false</IsSystem>
          </Positions>
          <Positions>
            <Name>sander_mount</Name>
            <Joints>[-2.53006,-2.15599,-1.18223,-1.37402,1.57131,0.124]</Joints>
            <Pose>p[-0.933321, -0.442727, 0.284064, 1.61808, 2.6928, 0.000150004]</Pose>
            <IsSystem>false</IsSystem>
          </Positions>
          <Positions>
            <Name>sander_home</Name>
            <Joints>[-2.57091,-0.82644,-2.14277,-1.743,1.57367,-0.999559]</Joints>
            <Pose>p[-0.319246, 0.00105911, 0.464005, -5.0997e-05, 3.14151, 3.32468e-05]</Pose>
            <IsSystem>false</IsSystem>
          </Positions>
          <Positions>
            <Name>grind1</Name>
            <Joints>[-0.964841,-1.56224,-2.25801,-2.46721,-0.975704,0.0351043]</Joints>
            <Pose>p[0.115668, -0.664968, 0.149296, -0.0209003, 3.11011, 0.00405717]</Pose>
            <IsSystem>false</IsSystem>
          </Positions>
          <Positions>
            <Name>grind2</Name>
            <Joints>[-1.19025,-1.54723,-2.28053,-2.45891,-1.20106,0.0341677]</Joints>
            <Pose>p[0.00572967, -0.666445, 0.145823, -0.0208504, 3.11009, 0.004073]</Pose>
            <IsSystem>false</IsSystem>
          </Positions>
          <Positions>
            <Name>grind3</Name>
            <Joints>[-1.41341,-1.57357,-2.26161,-2.45085,-1.42422,0.0333479]</Joints>
            <Pose>p[-0.0942147, -0.667831, 0.142729, -0.0208677, 3.1101, 0.00394188]</Pose>
            <IsSystem>false</IsSystem>
          </Positions>
        */

        private void ClearAllPositionsBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == ConfirmMessageBox("This will clear all positions INCLUDING system positions. Proceed?"))
                ClearAndInitializePositions();
            if (DialogResult.OK == ConfirmMessageBox("Would you like to create the default positions?"))
                CreateDefaultPositions();
        }

        private void RecordPosition(string prompt, string varName)
        {
            JoggingDialog form = new JoggingDialog(this)
            {
                Prompt = prompt,
                Tool = ReadVariable("robot_tool"),
                Part = "Teaching Position Only",
                ShouldSave = true
            };

            form.ShowDialog(this);

            if (form.ShouldSave)
            {
                log.Trace(prompt);

                if (robotReady)
                {
                    copyPositionAtWrite = varName;
                    RobotSend("25");
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
                    string msg = "21," + ExtractScalars(q);
                    log.Trace("Sending {0}", msg);
                    RobotSend(msg);
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
                    string msg = "22," + ExtractScalars(q);
                    log.Trace("Sending {0}", msg);
                    RobotSend(msg);
                    return true;
                }
            }
            return false;
        }

        private void PositionSetBtn_Click(object sender, EventArgs e)
        {
            string name = SelectedName(PositionsGrd);
            if (name == null)
                ErrorMessageBox("Please select a position in the table to teach.");
            else
            {
                log.Info("Setting Position {0}", name);
                RecordPosition("Please teach position: " + name, name);
            }
        }

        private void PositionMovePoseBtn_Click(object sender, EventArgs e)
        {
            string name = SelectedName(PositionsGrd);
            if (name == null)
                ErrorMessageBox("Please select a target position in the table.");
            else
            {
                GotoPositionPose(name);
                PromptOperator(String.Format("Wait for robot linear move to {0} complete", name), true);
            }
        }

        private void PositionMoveArmBtn_Click(object sender, EventArgs e)
        {
            string name = SelectedName(PositionsGrd);
            if (name == null)
                ErrorMessageBox("Please select a target position in the table.");
            else
            {
                GotoPositionJoint(name);
                PromptOperator(String.Format("Wait for robot joint move to {0} complete", name), true);
            }
        }

        private void AskSafetyStatusBtn_Click(object sender, EventArgs e)
        {
            robotDashboardClient?.InquiryResponse("safetystatus");
        }

        private void UnlockProtectiveStopBtn_Click(object sender, EventArgs e)
        {
            robotDashboardClient?.InquiryResponse("unlock protective stop");
        }

        // ===================================================================
        // END POSITIONS SYSTEM
        // ===================================================================

        private void CurrentLineLbl_TextChanged(object sender, EventArgs e)
        {
            CurrentLineLblCopy.Text = CurrentLineLbl.Text;
        }

        private void RecipeFilenameLbl_TextChanged(object sender, EventArgs e)
        {
            LoadRecipeBtn.Text = Path.GetFileNameWithoutExtension(RecipeFilenameLbl.Text);
        }

        private void RecipeRTB_TextChanged(object sender, EventArgs e)
        {
            if (runState != RunState.RUNNING)
            {
                SetRecipeState(RecipeState.MODIFIED);
            }
            RecipeRTBCopy.Text = RecipeRTB.Text;
        }

        private void StressBtn_Click(object sender, EventArgs e)
        {
            Task taskA = new Task(() =>
            {
                for (int i = 1; i <= 87; i++)
                {
                    log.Info("{0} Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", i);
                    log.Info("{0} DASH Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", i);
                    log.Info("{0} EXEC Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", i);
                    log.Info("{0} UR==> Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", i);
                    log.Warn("{0} WARN Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", i);
                    log.Error("{0} ERROR Some extra long lines of text to stress the loggers and UI scrolling capability, not to mention the logfile", i);
                }
            });

            // Start the task.
            taskA.Start();
        }

        private void FullManualBtn_Click(object sender, EventArgs e)
        {
            string ExecutionRoot = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            startInfo.Arguments = String.Format("file:\\{0}\\AutoGrind%20User%20Manual.pdf", executionRoot);
            process.StartInfo = startInfo;
            process.Start();
        }

    }
    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// Dependable replacement for RTB.GetFirstCharIndexFromLine. Actuall adds up the previous lines plus terminator.
        /// When you don't do this, you get odd behavior with line wrapping and if you toggle it off, you get flashing of the control.
        /// </summary>
        /// <param name="n">0-indexed line number to examine</param>
        /// <returns></returns>
        public static (int start, int length) GetLineExtents(this System.Windows.Forms.RichTextBox richTextBox, int n)
        {
            int start = 0;
            for (int i = 0; i < n; i++)
                start += richTextBox.Lines[i].Length + 1;

            return (start, richTextBox.Lines[n].Length);
        }
    }
}
