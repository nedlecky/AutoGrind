using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoGrind
{
    public partial class AgDirectorySelectDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public string SelectedPath { get; set; }
        public string Title { get; set; }

        List<string> directoryList;
        public AgDirectorySelectDialog()
        {
            InitializeComponent();
        }

        private void LoadDirectory(string path)
        {
            string[] subDirectoryList = Directory.GetDirectories(path);

            directoryList = new List<string>();
            DirectoryListBox.Items.Clear();

            DirectoryInfo parent = Directory.GetParent(path);
            if (parent != null)
            {

                DirectoryListBox.Items.Add("..");
                directoryList.Add(Directory.GetParent(path).FullName);
            }

            foreach (string directory in subDirectoryList)
            {
                DirectoryListBox.Items.Add(Path.GetFileName(directory));
                directoryList.Add(directory);
            }

            SelectedDirectoryLbl.Text = path;
        }
        private void AgDirectorySelectDialog_Load(object sender, EventArgs e)
        {
            TitleLbl.Text = Title;
            LoadDirectory(SelectedPath);
        }
        private void DirectoryListBox_Click(object sender, EventArgs e)
        {
            SelectedDirectoryLbl.Text = directoryList[DirectoryListBox.SelectedIndex];
        }

        private void DirectoryListBox_DoubleClick(object sender, EventArgs e)
        {
            SelectedPath = directoryList[DirectoryListBox.SelectedIndex];
            LoadDirectory(SelectedPath);
        }
        private void SelectBtn_Click(object sender, EventArgs e)
        {
            SelectedPath = SelectedDirectoryLbl.Text;
            log.Debug("SelectBtn_Click(...) SelectedPath={0}", SelectedPath);
            DialogResult = DialogResult.OK;

        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            SelectedPath = "";
            log.Debug("CancelBtn_Click(...)");
            DialogResult = DialogResult.Cancel;
        }

    }
}
