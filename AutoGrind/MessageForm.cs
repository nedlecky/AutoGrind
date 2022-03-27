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
        public MessageForm(string title, string label, string okText = "OK", string cancelText = "Cancel")
        {
            InitializeComponent();

            Text = title;
            label1.Text = label;
            OkBtn.Text = okText;
            CancelBtn.Text = cancelText;

            result = DialogResult.None;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            result=DialogResult.OK;

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            result= DialogResult.Cancel;

        }
    }
}
