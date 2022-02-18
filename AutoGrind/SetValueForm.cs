﻿using System;
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
        private static NLog.Logger log;

        public string value { get; set; }
        public string label { get; set; }

        public SetValueForm(string val = "0.000", string lab="??")
        {
            InitializeComponent();
            value = val;
            label = lab;
            ValueLbl.Text = value;
            LabelLbl.Text = "Enter " + label;
        }
        private void SetValueForm_Load(object sender, EventArgs e)
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                double x = Convert.ToDouble(ValueLbl.Text);
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
            ValueLbl.Text = "";

        }
        private void ButtonBackspace_Click(object sender, EventArgs e)
        {
            string current = ValueLbl.Text;
            if (current.Length > 0)
                ValueLbl.Text = current.Substring(0, current.Length - 1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "1";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "2";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "3";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "4";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "5";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "6";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "7";
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "8";
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "9";
        }

        private void ButtonPeriod_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += ".";
        }

        private void Button0_Click(object sender, EventArgs e)
        {
            ValueLbl.Text += "0";
        }

    }
}