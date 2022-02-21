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
        public MessageForm(string title, string label)
        {
            InitializeComponent();
            Text = title;
            label1.Text = label;

        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            //Left = 0;
            //Top = 500;
        }
    }
}
