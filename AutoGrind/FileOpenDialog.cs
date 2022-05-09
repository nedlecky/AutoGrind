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

        private void DirectoryListBox_DoubleClick(object sender, EventArgs e)
        {
            // Need double click to pop up
            if (DirectoryListBox.SelectedIndex >= 0)
                LoadDirectory(directoryList[DirectoryListBox.SelectedIndex]);
        }

        private void FileOpenForm_Load(object sender, EventArgs e)
        {
            TitleLbl.Text = Title;
            LoadDirectory(InitialDirectory);
        }

        private void FileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreviewRTB.LoadFile(Path.Combine(DirectoryNameLbl.Text, FileListBox.Items[FileListBox.SelectedIndex].ToString()), System.Windows.Forms.RichTextBoxStreamType.PlainText);
        }
        private void FileListBox_DoubleClick(object sender, EventArgs e)
        {
            OpenBtn_Click(null, null);
        }


        private void FileNameTxt_TextChanged(object sender, EventArgs e)
        {
            LoadFiles(DirectoryNameLbl.Text, FileNameTxt.Text);
        }
        private void OpenBtn_Click(object sender, EventArgs e)
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
