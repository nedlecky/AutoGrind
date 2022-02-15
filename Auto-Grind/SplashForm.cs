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
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            CloseTmr.Interval = 2000;
            CloseTmr.Enabled = true;
        }
        private void CloseTmr_Tick(object sender, EventArgs e)
        {
            Close();
        }

    }
}
