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
    public partial class SetValueForm : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public string value { get; set; }
        public string label { get; set; }
        private int nDecimals { get; set; }
        private bool isPassword { get; set; }

        public SetValueForm(string val = "0.000", string lab = "??", int nDecimals_ = 3, bool isPassword_ = false)
        {
            InitializeComponent();
            value = val;
            label = lab;
            nDecimals = nDecimals_;
            isPassword = isPassword_;
            LabelLbl.Text = "Enter " + label;
            ValueTxt.Text = value;
            if (isPassword) ValueTxt.PasswordChar = '*';
        }
        private void SetValueForm_Load(object sender, EventArgs e)
        {
            ValueTxt.Select();
            ValueTxt.SelectAll();
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            if (ValueTxt.Text == "")
            {
                value = "";
                Close();
                return;
            }
            try
            {
                double x = Convert.ToDouble(ValueTxt.Text);
                string fmt = "0";
                if (nDecimals > 0)
                {
                    fmt = "0.";
                    for (int i = 0; i < nDecimals; i++)
                        fmt += "0";
                }
                value = x.ToString(fmt);
                this.DialogResult = DialogResult.OK;
                if (isPassword)
                    log.Info("Setting {0} = ********", label);
                else
                    log.Info("Setting {0} = {1}", label, value);
                Close();
            }
            catch
            {
                // User has typed junk into the box... clear it!
                ValueTxt.Text = "";
            }
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ValueTxt.Text = "";

        }
        private void PlusMinusBtn_Click(object sender, EventArgs e)
        {
            string current = ValueTxt.Text;
            if (current.Length == 0) return;

            if (current[0] == '-')
                ValueTxt.Text = current.Substring(1, current.Length - 1);
            else
                ValueTxt.Text = '-' + current;
        }

        void DefaultKeyClick(object sender, EventArgs e)
        {
            ValueTxt.Select();
            string val = ((Button)sender).Text;
            if (val == "<<")
                val = "\b";
            SendKeys.Send(val);
        }

        private void ButtonPeriod_Click(object sender, EventArgs e)
        {
            string current = ValueTxt.Text;
            if (current.Length == 0)
                ValueTxt.Text = "0.";
            else if (!ValueTxt.Text.Contains("."))
                ValueTxt.Text += ".";
        }
    }
}
