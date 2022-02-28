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
    public partial class MessageForm : Form
    {
        public DialogResult result = DialogResult.OK;
        public MessageForm(string title, string label)
        {
            InitializeComponent();
            Text = title;
            label1.Text = label;
            result = DialogResult.None;
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
        }

        private void AbortBtn_Click(object sender, EventArgs e)
        {
            result = DialogResult.Abort;
            Close();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            Close();
        }
    }
}
