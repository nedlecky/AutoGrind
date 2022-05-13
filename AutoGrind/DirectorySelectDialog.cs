// File: DirectorySelectDialog.cs
// Project: AutoGrind
// Author: Ned Lecky, Olympus Controls
// Purpose: Custom Directory Select dialog with large buttons for use with touch screen

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
    public partial class DirectorySelectDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public string SelectedPath { get; set; }
        public string Title { get; set; }

        List<string> directoryList;
        public DirectorySelectDialog()
        {
            InitializeComponent();
        }

        private void LoadDirectory(string path)
        {
            string[] subDirectoryList;
            try
            {
                subDirectoryList = Directory.GetDirectories(path);
            }
            catch
            {
                return;
            }

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
