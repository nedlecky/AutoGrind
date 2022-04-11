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
    public partial class FileSaveAsDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public string Title { get; set; }
        public string Filter { get; set; }
        public string InitialDirectory { get; set; }
        public string FileName { get; set; }

        private string[] fileList;
        private List<string> directoryList;


        public FileSaveAsDialog()
        {
            InitializeComponent();
        }
        private void LoadFiles(string path, string nameStartsWith = null)
        {
            fileList = Directory.GetFiles(path, Filter);
            FileListBox.Items.Clear();
            foreach (string file in fileList)
            {
                string filename = Path.GetFileName(file);

                if (nameStartsWith == null || filename.StartsWith(nameStartsWith))
                    FileListBox.Items.Add(Path.GetFileName(file));
            }
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

            FileNameTxt.Select();
            FileNameTxt.Text = "";
            LoadFiles(path);
        }

        private void FileSaveAsDialog_Load(object sender, EventArgs e)
        {
            TitleLbl.Text = Title;
            LoadDirectory(InitialDirectory);


            FileNameTxt.Text = Path.GetFileName(FileName);
            FileNameTxt.Select();
            FileNameTxt.SelectAll();
        }
        private void FileListBox_Click(object sender, EventArgs e)
        {
            FileNameTxt.Text = FileListBox.SelectedItem.ToString();

        }
        private void FileListBox_DoubleClick(object sender, EventArgs e)
        {
            FileNameTxt.Text = FileListBox.SelectedItem.ToString();
            SaveBtn_Click(sender, e);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // If nothing selected, try to interpret as a typein?
            if (FileListBox.SelectedIndex < 0)
            {
                string filename = Path.Combine(DirectoryNameLbl.Text, FileNameTxt.Text);
                FileName = Path.ChangeExtension(filename, ".txt");
            }
            else
            {

                //FileName = fileList[FileListBox.SelectedIndex];
                FileName = Path.Combine(DirectoryNameLbl.Text, FileListBox.SelectedItem.ToString());
            }
            log.Debug("SaveBtn_Click(...) Filename={0}", FileName);
            DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            FileName = "";
            log.Debug("CancelBtn_Click(...)");
            DialogResult = DialogResult.Cancel;
        }

        private void DirectoryListBox_DoubleClick(object sender, EventArgs e)
        {
            // Need double click to pop up
            LoadDirectory(directoryList[DirectoryListBox.SelectedIndex]);
        }
    }
}
