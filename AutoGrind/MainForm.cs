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

                CloseTmr.Interval = 500;
                CloseTmr.Enabled = true;
                e.Cancel = true; // Cancel this shutdown- we'll let the close out timer shut us down
                log.Info("Shutting down in 500mS...");
            }
        }

        private void GrindBtn_Click(object sender, EventArgs e)
        {
            OperationTab.SelectedTab = OperationTab.TabPages["GrindTab"];

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            OperationTab.SelectedTab = OperationTab.TabPages["EditTab"];

        }

        private void SetupBtn_Click(object sender, EventArgs e)
        {
            OperationTab.SelectedTab = OperationTab.TabPages["SetupTab"];

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
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            log.Info("PauseBtn_Click(...)");
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            log.Info("ContinueBtn_Click(...)");
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            log.Info("StopBtn_Click(...)");
        }
        // ========================
        // END GRIND
        // ========================

        // ========================
        // START EDIT
        // ========================
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
            RecipeFilenameLbl.Text = "Untitled";
            RecipeRTB.Clear();
        }

        private void LoadRecipeBtn_Click(object sender, EventArgs e)
        {
            log.Info("LoadRecipeBtn_Click(...)");
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open an AutoHGrind Recipe";
            dialog.Filter = "AutoGrind Recipe Files|*.agr";
            dialog.InitialDirectory = Path.Combine(AutoGrindRoot, "Recipes");
            if (dialog.ShowDialog() == DialogResult.OK)
                LoadRecipeFile(dialog.FileName);

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

            //AutoConnectOnLoadChk.Checked = Convert.ToBoolean(AppNameKey.GetValue("AutoConnectOnLoadChk.Checked", "False"));
        }

        void SavePersistent()
        {
            log.Info("SavePersistent()");

            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");

            AppNameKey.SetValue("AutoGrindRoot", AutoGrindRoot);

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
        // ========================
        // END CONFIG
        // ========================


    }
}
