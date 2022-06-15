// File: MessageDialog.cs
// Project: AutoGrind
// Author: Ned Lecky, Lecky Engineering LLC
// Purpose: Two-button dialog with relabelable buttons optimized for use with touch screen

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoGrind
{
    public partial class MessageDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public DialogResult result = DialogResult.OK;

        // Can override these before showing dialog
        public string Title { get; set; } = "??";
        public string Label { get; set; } = "???";
        public string OkText { get; set; } = "OK";
        public string CancelText { get; set; } = "Cancel";

        public bool IsTypeIn { get; set; } = false;
        public string TypeInLabel { get; set; } = "";
        public string TypeInText { get; set; }

        public bool IsMotionWait { get; set; } = false;



        MainForm mainForm;
        public MessageDialog(MainForm _mainForm = null)
        {
            InitializeComponent();
            mainForm = _mainForm;
        }

        private void MessageDialog_Load(object sender, EventArgs e)
        {
            Text = Title;
            label1.Text = Label;
            OkBtn.Text = OkText;
            CancelBtn.Text = CancelText;
            result = DialogResult.None;
            OkBtn.Select();
            LoadPersistent();

            // Setup for type in
            if (IsTypeIn)
            {
                TypeInLbl.Text = TypeInLabel;
                TypeInTxt.Text = TypeInText;
                TypeInLbl.Visible = true;
                TypeInTxt.Visible = true;
                TypeInTxt.Select();
            }
            else
            {
                TypeInLbl.Text = "??";
                TypeInTxt.Text = "??";
                TypeInLbl.Visible = false;
                TypeInTxt.Visible = false;
            }

            // Setup for Motion Wait
            if (IsMotionWait)
            {
                OkBtn.BackColor = Color.Red;
                CancelBtn.BackColor = Color.Red;
            }
        }

        private void MessageDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePersistent();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            TypeInText = TypeInTxt.Text;

            if (IsMotionWait)
            {
                // Not only halt the bot but also stop any running program
                mainForm?.RobotSendHalt();
                result = DialogResult.Cancel;
            }
            else
                result = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (IsMotionWait)
            {
                // Not only halt the bot but also stop any running program
                mainForm?.RobotSendHalt();
                result = DialogResult.Cancel;
            }
            else
                result = DialogResult.Cancel;
        }

        private void SavePersistent()
        {
            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");
            RegistryKey FormNameKey = AppNameKey.CreateSubKey("MessageDialog");

            FormNameKey.SetValue("Left", Left);
            FormNameKey.SetValue("Top", Top);
            FormNameKey.SetValue("Width", Width);
            FormNameKey.SetValue("Height", Width);
        }
        private void LoadPersistent()
        {
            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");
            RegistryKey FormNameKey = AppNameKey.CreateSubKey("MessageDialog");

            Left = (Int32)FormNameKey.GetValue("Left", 0);
            Top = (Int32)FormNameKey.GetValue("Top", 0);
        }

    }
}
