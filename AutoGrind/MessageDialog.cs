// File: MessageDialog.cs
// Project: AutoGrind
// Author: Ned Lecky, Olympus Controls
// Purpose: Two-button dialog with relabelable buttons optimized for use with touch screen

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

        public MessageDialog()
        {
            InitializeComponent();
        }

        private void MessageDialog_Load(object sender, EventArgs e)
        {

            Text = Title;
            label1.Text = Label;
            OkBtn.Text = OkText;
            CancelBtn.Text = CancelText;

            result = DialogResult.None;
            OkBtn.Select();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            result=DialogResult.Cancel;
        }
    }
}
