using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoGrind
{
    public partial class FileOpenDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public string Title { get; set; }
        public string Filter { get; set; }
        public string InitialDirectory { get; set; }
        public string FileName { get; set; }

        private string[] fileList;
        private List<string> directoryList;

        public FileOpenDialog()
        {
            InitializeComponent();
        }
        private void LoadFiles(string path)
        {
            fileList = Directory.GetFiles(path, Filter);
            FileListBox.Items.Clear();
            foreach (string file in fileList)
                FileListBox.Items.Add(Path.GetFileName(file));
        }
        private void LoadDirectory(string path)
        {
            DirectoryNameLbl.Text = path;

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

            LoadFiles(path);

        }

        private void DirectoryListBox_Click(object sender, EventArgs e)
        {
            // Single click on directory previews files
            if(DirectoryListBox.SelectedIndex < 1) return;
            LoadFiles(directoryList[DirectoryListBox.SelectedIndex]);
        }

        private void DirectoryListBox_DoubleClick(object sender, EventArgs e)
        {
            // Need double click to pop up
            LoadDirectory(directoryList[DirectoryListBox.SelectedIndex]);
        }

        private void FileOpenForm_Load(object sender, EventArgs e)
        {
            TitleLbl.Text = Title;
            LoadDirectory(InitialDirectory);
        }

        private void FileListBox_DoubleClick(object sender, EventArgs e)
        {
            OpenBtn_Click(null, null);
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            // Do nothing if nothing selected
            if (FileListBox.SelectedIndex < 0) return;

            FileName = fileList[FileListBox.SelectedIndex];
            log.Debug("OpenBtn_Click(...) Filename={0}", FileName);
            DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            log.Debug("CancelBtn_Click(...)");
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
