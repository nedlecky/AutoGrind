// File: SetValueForm.cs
// Project: AutoGrind
// Author: Ned Lecky, Lecky Engineering LLC
// Purpose: A numeric data entry window for use with touch screen (or keyboard)

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
    public partial class SetValueForm : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        // These may be overridden prior to showing the dialog
        public double Value { get; set; } = 0;
        public string ValueOutString{ get; set; } = "??";
        public string Label { get; set; } = "Unspecified";
        public int NumberOfDecimals { get; set; } = 3;
        public bool IsPassword { get; set; } = false;
        public double MaxAllowed { get; set; } = 0;
        public double MinAllowed { get; set; } = 0;
        public double Default { get; set; } = -999;

        public SetValueForm()
        {
            InitializeComponent();
        }
        private void SetValueForm_Load(object sender, EventArgs e)
        {
            LoadPersistent();
            LabelLbl.Text = "ENTER\n" + Label;
            MakeValueOutString();
            ValueTxt.Text = ValueOutString;

            string minmaxString = "";
            if (MinAllowed != 0 || MaxAllowed != 0)
            {
                string formatter = String.Format("Limit {{0:F{0}}} to {{1:F{0}}}",  NumberOfDecimals);
                minmaxString += String.Format(formatter, MinAllowed, MaxAllowed);
            }
            if (Default != -999)
            {
                string formatter = String.Format("\nDefault={{0:F{0}}}", NumberOfDecimals);
                minmaxString += String.Format(formatter, Default);
            }
            MinMaxLabel.Text = minmaxString;

            if (IsPassword) ValueTxt.PasswordChar = '*';

            ValueTxt.Select();
            ValueTxt.SelectAll();
        }
        private void SetValueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePersistent();
        }

        private void MakeValueOutString()
        {
            string formatter = String.Format("{{0:F{0}}}", NumberOfDecimals);
            ValueOutString = String.Format(formatter, Value);
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            if (ValueTxt.Text == "")
            {
                Value = 0;
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
                        Value = MinAllowed;
                        MakeValueOutString();
                        ValueTxt.Text = ValueOutString;
                        ValueTxt.Select();
                        ValueTxt.SelectAll();
                        valueOk = false;
                    }
                    else if (x > MaxAllowed)
                    {
                        Value = MaxAllowed;
                        MakeValueOutString();
                        ValueTxt.Text = ValueOutString;
                        ValueTxt.Select();
                        ValueTxt.SelectAll();
                        valueOk = false;
                    }
                }

                if (valueOk)
                {
                    Value = x;
                    MakeValueOutString();
                    this.DialogResult = DialogResult.OK;
                    if (IsPassword)
                        log.Debug("SetValueForm: Setting {0} = ********", Label);
                    else
                        log.Debug("SetValueForm: Setting {0} = {1}", Label, Value);
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
        private void SavePersistent()
        {
            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");
            RegistryKey FormNameKey = AppNameKey.CreateSubKey("SetValueForm");

            FormNameKey.SetValue("Left", Left);
            FormNameKey.SetValue("Top", Top);
            FormNameKey.SetValue("Width", Width);
            FormNameKey.SetValue("Height", Width);
        }
        private void LoadPersistent()
        {
            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");
            RegistryKey FormNameKey = AppNameKey.CreateSubKey("SetValueForm");

            Left = (Int32)FormNameKey.GetValue("Left", (MainForm.screenDesignWidth - Width) / 2);
            Top = (Int32)FormNameKey.GetValue("Top", (MainForm.screenDesignHeight - Height) / 2);
        }
    }
}
