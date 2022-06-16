// File: DirectorySelectDialog.cs
// Project: AutoGrind
// Author: Ned Lecky, Lecky Engineering LLC
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

        private void DirectorySelectDialog_Load(object sender, EventArgs e)
        {
            TitleLbl.Text = Title;
            LoadDirectory(SelectedPath);
        }

        // **********************************************************************************************
        // DirectoryListBox Methods
        // **********************************************************************************************

        private void DirectoryListBox_Click(object sender, EventArgs e)
        {
            //DirectoryNameLbl.Text = directoryList[DirectoryListBox.SelectedIndex];
        }

        private void DirectoryListBox_DoubleClick(object sender, EventArgs e)
        {
            if (DirectoryListBox.SelectedIndex >= 0)
            {
                SelectedPath = directoryList[DirectoryListBox.SelectedIndex];
                LoadDirectory(SelectedPath);
            }
        }

        // **********************************************************************************************
        // Button Methods
        // **********************************************************************************************

        private void SelectBtn_Click(object sender, EventArgs e)
        {
            SelectedPath = DirectoryNameLbl.Text;
            log.Debug("SelectBtn_Click(...) SelectedPath={0}", SelectedPath);
            DialogResult = DialogResult.OK;
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            SelectedPath = "";
            log.Debug("CancelBtn_Click(...)");
            DialogResult = DialogResult.Cancel;
        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            log.Debug("DeleteBtn_Click(...)");

            // Delete a directory?
            if (DirectoryListBox.SelectedIndex >= 0)
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
                        Directory.Delete(deleteDirectory, true);
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
            DirectoryNameLbl.Text = path;
        }
    }
}
