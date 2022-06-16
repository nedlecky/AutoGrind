// File: JoggingDialog.cs
// Project: AutoGrind
// Author: Ned Lecky, Lecky Engineering LLC
// Purpose: Jogging and Freedrive manual robot movement

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoGrind
{

    public partial class JoggingDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public string Prompt { get; set; } = "General Jogging";
        public string Part { get; set; } = "UnknownPart";
        public string Tool { get; set; } = "UnknownTool";

        public bool ShouldSave { get; set; } = false;
        private bool freedriveMode = false;

        MainForm mainForm;
        public JoggingDialog(MainForm _mainForm)
        {
            InitializeComponent();
            mainForm = _mainForm;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            FreedriveOff();
            ShouldSave = false;
            Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            FreedriveOff();
            ShouldSave = true;
            Close();
        }

        private void JoggingForm_Load(object sender, EventArgs e)
        {
            PurposeLbl.Text = Prompt;
            ToolLbl.Text = "Tool: " + Tool;
            PartLbl.Text = "Part: " + Part;
            SaveBtn.Enabled = ShouldSave;
            SaveBtn.BackColor = ShouldSave ? Color.Green : Color.Gray;

            FreedriveGrp.Enabled = false;
            ClickJogGrp.Enabled = true;

            LoadPersistent();
        }
        private void JoggingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePersistent();
        }
        private RegistryKey MyRegistryKey()
        {
            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind");
            RegistryKey FormNameKey = AppNameKey.CreateSubKey("JoggingDialog");
            return FormNameKey;
        }

        void SavePersistent()
        {
            RegistryKey FormNameKey = MyRegistryKey();

            FormNameKey.SetValue("Left", Left);
            FormNameKey.SetValue("Top", Top);
            FormNameKey.SetValue("Width", Width);
            FormNameKey.SetValue("Height", Width);

            FormNameKey.SetValue("XyJogDistanceBox.SelectedIndex", XyJogDistanceBox.SelectedIndex);
            FormNameKey.SetValue("ZJogDistanceBox.SelectedIndex", ZJogDistanceBox.SelectedIndex);
            FormNameKey.SetValue("AngleBox.SelectedIndex", AngleBox.SelectedIndex);
            FormNameKey.SetValue("CoordBox.SelectedIndex", CoordBox.SelectedIndex);
        }
        void LoadPersistent()
        {
            RegistryKey FormNameKey = MyRegistryKey();

            Left = (Int32)FormNameKey.GetValue("Left", (MainForm.screenDesignWidth - Width) / 2);
            Top = (Int32)FormNameKey.GetValue("Top", (MainForm.screenDesignHeight - Height) / 2);

            XyJogDistanceBox.SelectedIndex = (int)FormNameKey.GetValue("XyJogDistanceBox.SelectedIndex", 2);
            ZJogDistanceBox.SelectedIndex = (int)FormNameKey.GetValue("ZJogDistanceBox.SelectedIndex", 1);
            AngleBox.SelectedIndex = (int)FormNameKey.GetValue("AngleBox.SelectedIndex", 2);
            CoordBox.SelectedIndex = (int)FormNameKey.GetValue("CoordBox.SelectedIndex", 0);
        }

        // Enable/Hide buttons based on type of jog
        // This may be non-useful! Will see during installation
        private void RestrictButtons()
        {
            switch (CoordBox.Text)
            {
                case "BASE":
                    FreeXChk.Checked = true;
                    FreeYChk.Checked = true;
                    FreeZChk.Checked = true;
                    FreeRxChk.Checked = true;
                    FreeRyChk.Checked = true;
                    FreeRzChk.Checked = true;
                    ZplusBtn.Text = "DOWN";
                    ZminusBtn.Text = "UP";
                    YplusBtn.Text = "LEFT";
                    YminusBtn.Text = "RIGHT";
                    XplusBtn.Text = "FORE";
                    XminusBtn.Text = "BACK";
                    break;
                case "TOOL":
                    FreeXChk.Checked = true;
                    FreeYChk.Checked = true;
                    FreeZChk.Checked = true;
                    FreeRxChk.Checked = true;
                    FreeRyChk.Checked = true;
                    FreeRzChk.Checked = true;
                    ZplusBtn.Text = "IN";
                    ZminusBtn.Text = "OUT";
                    YplusBtn.Text = "LEFT";
                    YminusBtn.Text = "RIGHT";
                    XplusBtn.Text = "FORE";
                    XminusBtn.Text = "BACK";
                    break;
                case "PART":
                    log.Info("PART: {0}", Part);
                    ZplusBtn.Text = "IN";
                    ZminusBtn.Text = "OUT";
                    YplusBtn.Text = "LEFT";
                    YminusBtn.Text = "RIGHT";
                    XplusBtn.Text = "FORE";
                    XminusBtn.Text = "BACK";
                    if (Part.StartsWith("SPHERE"))
                    {
                        FreeXChk.Checked = false;
                        FreeYChk.Checked = false;
                        FreeZChk.Checked = true;
                        FreeRxChk.Checked = true;
                        FreeRyChk.Checked = true;
                        FreeRzChk.Checked = false;
                    }
                    else if (Part.StartsWith("CYLINDER"))
                    {
                        FreeXChk.Checked = true;
                        FreeYChk.Checked = false;
                        FreeZChk.Checked = true;
                        FreeRxChk.Checked = true;
                        FreeRyChk.Checked = false;
                        FreeRzChk.Checked = false;
                    }
                    else // FLAT
                    {
                        FreeXChk.Checked = true;
                        FreeYChk.Checked = true;
                        FreeZChk.Checked = true;
                        FreeRxChk.Checked = true;
                        FreeRyChk.Checked = true;
                        FreeRzChk.Checked = true;
                    }
                    break;
            }
        }

        private void CoordBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestrictButtons();
            if (FreedriveBtn.Text.Contains("ON"))
                EnableFreedrive();
        }

        private double Deg2Rad(double d)
        {
            return d * Math.PI / 180.0;
        }
        private double Rad2Deg(double r)
        {
            return r * 180.0 / Math.PI;
        }
        string lastJogCommand;
        private void Jog(double[] p)
        {
            lastJogCommand = "??";
            switch (CoordBox.Text)
            {
                case "BASE":
                    lastJogCommand = "13";
                    break;
                case "TOOL":
                    lastJogCommand = "14";
                    break;
                case "PART":
                    lastJogCommand = "15";
                    break;
            }

            for (int i = 0; i < 6; i++)
                lastJogCommand += "," + p[i].ToString();
            log.Info("Jog Command: {0}", lastJogCommand);
            mainForm.RobotSend(lastJogCommand);
        }

        static bool continueTask;
        private void Repeater()
        {
            Task.Factory.StartNew(() =>
            {
                continueTask = true;
                while (continueTask)
                {
                    System.Threading.Thread.Sleep(25);
                    if (continueTask && mainForm.RobotCompletedCaughtUp())
                    {
                        mainForm.RobotSend(lastJogCommand);
                    }
                }
            });
        }
        private void BtnMouseUp(object sender, MouseEventArgs e)
        {
            log.Info("MouseUp");
            continueTask = false;
        }
        // Return 1 for Base Coordinates Selected, else -1
        private double BaseSign()
        {
            return CoordBox.Text == "BASE" ? 1 : -1;
        }

        private void ZplusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            log.Info("ZplusBtn_MouseDown");
            double distance = Convert.ToDouble(ZJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, distance / 1000.0 * -BaseSign(), 0, 0, 0 };
            Jog(p);
            Repeater();
        }

        private void ZminusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            log.Info("ZminusBtn_MouseDown");
            double distance = Convert.ToDouble(ZJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, -distance / 1000.0 * -BaseSign(), 0, 0, 0 };
            Jog(p);
            Repeater();
        }

        private void XplusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double distance = Convert.ToDouble(XyJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { distance / 1000.0 * -BaseSign(), 0, 0, 0, 0, 0 };
            Jog(p);
            Repeater();
        }

        private void XminusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double distance = Convert.ToDouble(XyJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { -distance / 1000.0 * -BaseSign(), 0, 0, 0, 0, 0 };
            Jog(p);
            Repeater();
        }
        private void YplusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double distance = Convert.ToDouble(XyJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, distance / 1000.0, 0, 0, 0, 0 };
            Jog(p);
            Repeater();
        }

        private void YminusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double distance = Convert.ToDouble(XyJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, -distance / 1000.0, 0, 0, 0, 0 };
            Jog(p);
            Repeater();
        }

        private void RxPlusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, Deg2Rad(angle), 0, 0 };
            Jog(p);
            Repeater();
        }

        private void RxMinusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, -Deg2Rad(angle), 0, 0 };
            Jog(p);
            Repeater();
        }

        private void RyPlusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, Deg2Rad(angle), 0 };
            Jog(p);
            Repeater();
        }

        private void RyMinusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, -Deg2Rad(angle), 0 };
            Jog(p);
            Repeater();
        }

        private void RzPlusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, 0, Deg2Rad(angle) };
            Jog(p);
            Repeater();
        }

        private void RzMinusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, 0, -Deg2Rad(angle) };
            Jog(p);
            Repeater();
        }

        private void ALignButton_Click(object sender, EventArgs e)
        {
            //mainForm.RobotSend(string.Format("18,0,{0},0", Deg2Rad(180)));
            mainForm.RobotSend(string.Format("18,0,{0},0", Deg2Rad(180)));
            //mainForm.RobotSend("16,3,0");
            //mainForm.RobotSend($"16,4,{Deg2Rad(180)}");
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        private void EnableFreedrive()
        {
            string freeAxes = "";
            freeAxes += (FreeXChk.Checked ? "1" : "0");
            freeAxes += "," + (FreeYChk.Checked ? "1" : "0");
            freeAxes += "," + (FreeZChk.Checked ? "1" : "0");
            freeAxes += "," + (FreeRxChk.Checked ? "1" : "0");
            freeAxes += "," + (FreeRyChk.Checked ? "1" : "0");
            freeAxes += "," + (FreeRzChk.Checked ? "1" : "0");
            mainForm.RobotSend("30,19,1," + CoordBox.SelectedIndex.ToString() + "," + freeAxes);
        }

        private void FreedriveOn()
        {

            if (freedriveMode) return;

            freedriveMode = true;
            FreedriveGrp.Enabled = true;
            ClickJogGrp.Enabled = false;

            EnableFreedrive();

            FreedriveBtn.Text = "Freedrive\nON";
            FreedriveBtn.BackColor = Color.Blue;
            SendMessage(Handle, WM_SETREDRAW, false, 0);
            SendMessage(Handle, WM_SETREDRAW, true, 0);
            Refresh();
        }
        private void FreedriveOff()
        {
            if (!freedriveMode) return;

            freedriveMode = false;
            FreedriveGrp.Enabled = false;
            ClickJogGrp.Enabled = true;

            mainForm.RobotSend("30,19,0");

            FreedriveBtn.Text = "Freedrive";
            FreedriveBtn.BackColor = Color.Green;

            SendMessage(Handle, WM_SETREDRAW, false, 0);
            RestrictButtons();
            SendMessage(Handle, WM_SETREDRAW, true, 0);
            Refresh();
        }
        private void FreeDriveBtn_Click(object sender, EventArgs e)
        {
            if (freedriveMode)
                FreedriveOff();
            else
                FreedriveOn();
        }
        private void FreeChk_CheckedChanged(object sender, EventArgs e)
        {
            if (freedriveMode)
                EnableFreedrive();
        }

        private void FreedriveAllBtn_Click(object sender, EventArgs e)
        {
            FreeXChk.Checked = true;
            FreeYChk.Checked = true;
            FreeZChk.Checked = true;
            FreeRxChk.Checked = true;
            FreeRyChk.Checked = true;
            FreeRzChk.Checked = true;
        }

        private void FreedriveTransBtn_Click(object sender, EventArgs e)
        {
            FreeXChk.Checked = true;
            FreeYChk.Checked = true;
            FreeZChk.Checked = true;
            FreeRxChk.Checked = false;
            FreeRyChk.Checked = false;
            FreeRzChk.Checked = false;
        }

        private void FreedrivePlaneBtn_Click(object sender, EventArgs e)
        {
            FreeXChk.Checked = true;
            FreeYChk.Checked = true;
            FreeZChk.Checked = false;
            FreeRxChk.Checked = false;
            FreeRyChk.Checked = false;
            FreeRzChk.Checked = false;
        }

        private void FreedriveRotBtn_Click(object sender, EventArgs e)
        {
            FreeXChk.Checked = false;
            FreeYChk.Checked = false;
            FreeZChk.Checked = false;
            FreeRxChk.Checked = true;
            FreeRyChk.Checked = true;
            FreeRzChk.Checked = true;
        }

    }
}