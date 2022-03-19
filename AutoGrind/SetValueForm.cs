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

        public SetValueForm(string val = "0.000", string lab = "??")
        {
            InitializeComponent();
            value = val;
            label = lab;
            LabelLbl.Text = "Enter " + label;
            ValueTxt.Text = value;
        }
        private void SetValueForm_Load(object sender, EventArgs e)
        {
            ValueTxt.Select();
            ValueTxt.SelectAll();
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                double x = Convert.ToDouble(ValueTxt.Text);
                value = x.ToString("0.000");
                this.DialogResult = DialogResult.OK;
                log.Info("Setting {0} = {1}", label, value);
                Close();
            }
            catch (Exception ex)
            {
                log.Error("{0} is not a number", value, ex);
                // TODO: Some sort of explanatory dialog here
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
