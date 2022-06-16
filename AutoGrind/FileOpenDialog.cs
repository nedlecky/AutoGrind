// File: FileOpenDialog.cs
// Project: AutoGrind
// Author: Ned Lecky, Lecky Engineering LLC
// Purpose: Custom File Open dialog with large buttons for use with touch screen

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

        private void FileOpenForm_Load(object sender, EventArgs e)
        {
            TitleLbl.Text = Title;
            LoadDirectory(InitialDirectory);
        }


        // **********************************************************************************************
        // FileNameTxt Methods
        // **********************************************************************************************

        private void FileNameTxt_TextChanged(object sender, EventArgs e)
        {
            LoadFiles(DirectoryNameLbl.Text, FileNameTxt.Text);
        }

        // **********************************************************************************************
        // DirectoryListBox Methods
        // **********************************************************************************************

        // When you select (highlight) a directory, file is unhighlighted
        private void DirectoryListBox_Click(object sender, EventArgs e)
        {
            FileListBox.SelectedIndex = -1;
        }

        private void DirectoryListBox_DoubleClick(object sender, EventArgs e)
        {
            // Need double click to pop up
            if (DirectoryListBox.SelectedIndex >= 0)
                LoadDirectory(directoryList[DirectoryListBox.SelectedIndex]);
        }

        // **********************************************************************************************
        // FileListBox Methods
        // **********************************************************************************************

        // When you select (highlight) a file, directory is unhighlighted
        private void FileListBox_Click(object sender, EventArgs e)
        {
            DirectoryListBox.SelectedIndex = -1;
        }
        private void FileListBox_DoubleClick(object sender, EventArgs e)
        {
            OpenBtn_Click(null, null);
        }
        private void FileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PreviewRTB.LoadFile(Path.Combine(DirectoryNameLbl.Text, FileListBox.Items[FileListBox.SelectedIndex].ToString()), System.Windows.Forms.RichTextBoxStreamType.PlainText);
            }
            catch { }
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            // If nothing selected, try to interpret as a typein?
            if (FileListBox.SelectedIndex < 0)
            {
                if (FileNameTxt.Text.Length == 0) return;
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

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            log.Debug("DeleteBtn_Click(...)");

            // Delete a file?
            if (FileListBox.SelectedIndex >= 0)
            {

                string deleteFilename = Path.Combine(DirectoryNameLbl.Text, FileListBox.SelectedItem.ToString());
                MessageDialog messageForm = new MessageDialog()
                {
                    Title = "System Confirmation",
                    Label = $"Delete FILE {deleteFilename}\n ARE YOU SURE?",
                    OkText = "&Yes",
                    CancelText = "&No"
                };
                DialogResult result = messageForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    File.Delete(deleteFilename);
                    log.Info("Deleted {0}", deleteFilename);
                    LoadFiles(DirectoryNameLbl.Text);
                }
            }
            // Delete a directory?
            else if (DirectoryListBox.SelectedIndex >= 0)
            {
                if (DirectoryListBox.SelectedItem.ToString() != "..") // Don't delete your parent directory!
                {
                    string deleteDirectory = Path.Combine(DirectoryNameLbl.Text, DirectoryListBox.SelectedItem.ToString());
                    MessageDialog messageForm = new MessageDialog()
                    {
                        Title = "System Confirmation",
                        Label = $"Delete DIRECTORY AND CONTENTS\n{deleteDirectory}",
                        OkText = "&Yes",
                        CancelText = "&No"
                    };
                    DialogResult result = messageForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Directory.Delete(deleteDirectory,true);
                        log.Info("Deleted directory {0}", deleteDirectory);
                        LoadDirectory(DirectoryNameLbl.Text);
                    }
                }
            }
        }

        private void NewFolderBtn_Click(object sender, EventArgs e)
        {
            MessageDialog messageForm = new MessageDialog()
            {
                Title = "System Confirmation",
                Label = $"Create new folder in\n{DirectoryNameLbl.Text}?",
                OkText = "&Yes",
                CancelText = "&No",
                IsTypeIn = true,
                TypeInLabel = "New Folder:",
                TypeInText = ""
            };
            DialogResult result = messageForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string createDirectory = Path.Combine(DirectoryNameLbl.Text, messageForm.TypeInText);
                Directory.CreateDirectory(createDirectory);
                log.Info("Folder Created: {0}", createDirectory);
                LoadDirectory(DirectoryNameLbl.Text);
            }
        }

        // **********************************************************************************************
        // Support Functions
        // **********************************************************************************************

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

    }
}
