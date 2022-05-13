// File: SetValueForm.cs
// Project: AutoGrind
// Author: Ned Lecky, Olympus Controls
// Purpose: A numeric data entry window for use with touch screen (or keyboard)

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

        // These may ne overridden prior to showing the dialog
        public string Value { get; set; } = "0.000";
        public string Label { get; set; } = "Unspecified";
        public int NumberOfDecimals { get; set; } = 3;
        public bool IsPassword { get; set; } = false;
        public double MaxAllowed { get; set; } = 0;
        public double MinAllowed { get; set; } = 0;

        public SetValueForm()
        {
            InitializeComponent();
        }
        private void SetValueForm_Load(object sender, EventArgs e)
        {
            LabelLbl.Text = "Enter\n" + Label;
            ValueTxt.Text = Value;
            if (MinAllowed != 0 || MaxAllowed != 0)
            {
                MinMaxLabel.Text = String.Format("Limit {0} to {1}", MinAllowed, MaxAllowed);
            }
            if (IsPassword) ValueTxt.PasswordChar = '*';

            ValueTxt.Select();
            ValueTxt.SelectAll();
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            if (ValueTxt.Text == "")
            {
                Value = "";
                Close();
                return;
            }
            try
            {
                double x = Convert.ToDouble(ValueTxt.Text);
                bool valueOk = true;
                if (MinAllowed != 0 || MaxAllowed != 0)
                {
                    if (x < MinAllowed)
                    {
                        Value = MinAllowed.ToString();
                        ValueTxt.Text = Value;
                        ValueTxt.Select();
                        ValueTxt.SelectAll();
                        valueOk = false;
                    }
                    else if (x > MaxAllowed)
                    {
                        Value = MaxAllowed.ToString();
                        ValueTxt.Text = Value;
                        ValueTxt.Select();
                        ValueTxt.SelectAll();
                        valueOk = false;
                    }
                }

                if (valueOk)
                {
                    string fmt = "0";
                    if (NumberOfDecimals > 0)
                    {
                        fmt = "0.";
                        for (int i = 0; i < NumberOfDecimals; i++)
                            fmt += "0";
                    }
                    Value = x.ToString(fmt);
                    this.DialogResult = DialogResult.OK;
                    if (IsPassword)
                        log.Info("Setting {0} = ********", Label);
                    else
                        log.Info("Setting {0} = {1}", Label, Value);
                    Close();
                }
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

        // This handles all of the other key clicks (by looking at the Text on the key!)
        void DefaultKeyClick(object sender, EventArgs e)
        {
            ValueTxt.Select();
            string val = ((Button)sender).Text;
            if (val == "<<")
                val = "\b";
            SendKeys.Send(val);
        }
    }
}
