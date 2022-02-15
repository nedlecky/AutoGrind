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
    }
}
