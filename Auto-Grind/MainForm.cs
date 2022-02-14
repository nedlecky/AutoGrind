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

namespace Auto_Grind
{
    public partial class MainForm : Form
    {
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
    }
}
