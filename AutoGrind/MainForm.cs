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

            log.Info("Sample message for UR");
            log.Info("UR: Another sample message");
            log.Error("Test ERROR message");
            log.Info("==> Sending something");
            log.Info("<== Receiving something");


            HeartbeatTmr.Interval = 1000;
            HeartbeatTmr.Enabled = true;
            StartupTmr.Interval = 1000;
            StartupTmr.Enabled = true;

            SetRecipeState(RecipeState.NEW);
            SetState(RunState.IDLE);
            GrindBtn_Click(null, null);
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
            //DisconnectAllDevicesBtn_Click(null, null);
            //StopJint();
            //MessageTmr_Tick(null, null);
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
                // Check on dirty JavaScript program
                /*
                 * if (JavaScriptCodeRTB.Modified)
                {
                    var result = MessageBox.Show("JavaScript modified. Save before closing?",
                                        "LEonard Confirmation",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        SaveJavaProgramBtn_Click(null, null);
                    }
                }
                */
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

        private void HeartbeatTmr_Tick(object sender, EventArgs e)
        {
            string now = DateTime.Now.ToString("s");
            timeLbl.Text = now;
        }

        private void StartupTmr_Tick(object sender, EventArgs e)
        {
            splashForm = new SplashForm();
            splashForm.Show();

            log.Info("StartupTmr()...");
            StartupTmr.Enabled = false;


            log.Info("System ready.");
        }

        // ========================
        // START MAIN UI BUTTONS
        // ========================

        private void SetState(RunState s, bool fEditing=false)
        {
            if (runState != s)
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
                    JogBtn.Enabled = true;
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
                    JogBtn.Enabled = true;
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
                GrindBtn.BackColor = GrindBtn.Enabled ? Color.LightGreen : Color.Gray;
                EditBtn.BackColor = EditBtn.Enabled ? Color.Green : Color.Gray;
            }
            else
            {
                GrindBtn.BackColor = GrindBtn.Enabled ? Color.Green : Color.Gray;
                EditBtn.BackColor = EditBtn.Enabled ? Color.LightGreen : Color.Gray;
            }
            SetupBtn.BackColor = SetupBtn.Enabled ? Color.LightGreen : Color.Gray;

            JogBtn.BackColor = LoadBtn.Enabled ? Color.Green : Color.Gray;
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
            switch(e.TabPage.Text)
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
            SetState(RunState.IDLE,true);
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
            dialog.Title = "Open an AutoHGrind Recipe";
            dialog.Filter = "AutoGrind Recipe Files|*.agr";
            dialog.InitialDirectory = Path.Combine(AutoGrindRoot, "Recipes");
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadRecipeFile(dialog.FileName);
                SetRecipeState(RecipeState.LOADED);
                SetState(RunState.READY,true);
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
        // START CONFIG
        // ========================

        private void DefaultConfigBtn_Click(object sender, EventArgs e)
        {
            log.Info("DefaultConfigBtn_Click(...)");
            AutoGrindRoot = "\\";
            AutoGrindRootLbl.Text = AutoGrindRoot;
        }
        void LoadPersistent()
        {
            // Pull setup info from registry.... these are overwritten on exit or with various config save operations
            // Note default values are specified here as well
            log.Info("LoadPersistent()");

            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");

            AutoGrindRoot = (string)AppNameKey.GetValue("AutoGrindRoot", "\\");
            AutoGrindRootLbl.Text = AutoGrindRoot;
            DiameterLbl.Text = (string)AppNameKey.GetValue("DiameterLbl.Text", "25.000");
            AngleLbl.Text = (string)AppNameKey.GetValue("AngleLbl.Text", "0.000");

            //AutoConnectOnLoadChk.Checked = Convert.ToBoolean(AppNameKey.GetValue("AutoConnectOnLoadChk.Checked", "False"));
        }

        void SavePersistent()
        {
            log.Info("SavePersistent()");

            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");

            AppNameKey.SetValue("AutoGrindRoot", AutoGrindRoot);
            AppNameKey.SetValue("DiameterLbl.Text", DiameterLbl.Text);
            AppNameKey.SetValue("AngleLbl.Text", AngleLbl.Text);

            //AppNameKey.SetValue("AutoConnectOnLoadChk.Checked", AutoConnectOnLoadChk.Checked);
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
            JoggingForm form = new JoggingForm();

            form.ShowDialog(this);
        }

        private void DiameterLbl_Click(object sender, EventArgs e)
        {
            SetValueForm form = new SetValueForm(DiameterLbl.Text, "DIAMETER");

            if (form.ShowDialog(this)==DialogResult.OK)
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


        // ========================
        // END CONFIG
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

        private bool ExecuteLine(string line)
        {
            // Fetch the line and replace all 1 or more whitespace with a single space and drop all leading/trainling whitespace
            string command = Regex.Replace(line, @"\s+", " ").Trim();

            // Blank?
            if (command.Length < 1)
            {
                log.Trace("Line {0} BLANK: {1}", currentLine, command);
                currentLine++;
                return true;
            }

            // Comment?
            if (command[0]=='#')
            {
                log.Trace("Line {0} COMMENT: {1}", currentLine, command);
                currentLine++;
                return true;
            }

            // End??
            if (command.StartsWith("end"))
            {
                log.Trace("{0} END: {1}", currentLine, command);
                currentLine++;
                return false;
            }

            // Home
            // Grindcircle
            // Toolchange

            log.Trace("Line {0} Exec: {1}", currentLine, command);
            currentLine++;
            return true;

        }
        private void ExecTmr_Tick(object sender, EventArgs e)
        {
            //log.Info("ExecTmr(...) curLine={0}", currentLine);
            string line = RecipeRoRTB.Lines[currentLine];
            bool fContinue = ExecuteLine(line);

            if (!fContinue || currentLine >= nLines)
                SetState(RunState.READY);

        }

        // ========================
        // END EXECUTIVE
        // ========================


    }
}
