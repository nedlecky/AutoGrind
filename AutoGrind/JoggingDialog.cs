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
        public enum RegisterTouchFlags
        {
            TWF_NONE = 0x00000000,
            TWF_FINETOUCH = 0x00000001, //Specifies that hWnd prefers noncoalesced touch input.
            TWF_WANTPALM = 0x00000002 //Setting this flag disables palm rejection which reduces delays for getting WM_TOUCH messages.
        }
        //[DllImport("user32.dll", SetLastError = true)]
        //static extern bool RegisterTouchWindow(IntPtr hWnd, RegisterTouchFlags flags);

        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        //readonly TcpServerSupport robot;

        public string Prompt { get; set; } = "General Jogging";
        public string Part { get; set; } = "UnknownPart";
        public string Tool { get; set; } = "UnknownTool";

        public bool ShouldSave { get; set; } = false;

        MainForm mainForm;
        public JoggingDialog(MainForm _mainForm)
        {
            InitializeComponent();
            mainForm = _mainForm;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            ShouldSave = false;
            Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ShouldSave = true;
            Close();
        }


        Button[] buttons;

        private void JoggingForm_Load(object sender, EventArgs e)
        {
            buttons = new Button[]
            {
                XplusBtn,
                XminusBtn,
                YplusBtn,
                YminusBtn,
                ZplusBtn,
                ZminusBtn,
                RxPlusBtn,
                RxMinusBtn,
                RyPlusBtn,
                RyMinusBtn,
                RzPlusBtn,
                RzMinusBtn,

                ToolVerticalBtn,
                RxVerticalBtn,
                RyZeroBtn,
                RzZeroBtn
            };

            PurposeLbl.Text = Prompt;
            ToolLbl.Text = "Tool: " + Tool;
            PartLbl.Text = "Part: " + Part;
            SaveBtn.Enabled = ShouldSave;
            SaveBtn.BackColor = ShouldSave ? Color.Green : Color.Gray;

            LoadJogPersistent();

            //RegisterTouchWindow(ZplusBtn.Handle, RegisterTouchFlags.TWF_WANTPALM);
        }
        private void JoggingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveJogPersistent();
        }
        void SaveJogPersistent()
        {
            log.Debug("SaveJogPersistent()");

            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind_jog");

            AppNameKey.SetValue("XyJogDistanceBox.SelectedIndex", XyJogDistanceBox.SelectedIndex);
            AppNameKey.SetValue("ZJogDistanceBox.SelectedIndex", ZJogDistanceBox.SelectedIndex);
            AppNameKey.SetValue("AngleBox.SelectedIndex", AngleBox.SelectedIndex);
            AppNameKey.SetValue("CoordBox.SelectedIndex", CoordBox.SelectedIndex);
        }
        void LoadJogPersistent()
        {
            // Pull setup info from registry.... these are overwritten on exit or with various config save operations
            // Note default values are specified here as well
            log.Debug("LoadJogPersistent()");

            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey AppNameKey = SoftwareKey.CreateSubKey("AutoGrind_jog");

            XyJogDistanceBox.SelectedIndex = (int)AppNameKey.GetValue("XyJogDistanceBox.SelectedIndex", 2);
            ZJogDistanceBox.SelectedIndex = (int)AppNameKey.GetValue("ZJogDistanceBox.SelectedIndex", 1);
            AngleBox.SelectedIndex = (int)AppNameKey.GetValue("AngleBox.SelectedIndex", 2);
            CoordBox.SelectedIndex = (int)AppNameKey.GetValue("CoordBox.SelectedIndex", 0);
        }

        private void ColorAllButtons()
        {
            foreach (Button b in buttons)
                b.BackColor = b.Enabled ? Color.Green : Color.Gray;
        }
        private void EnableAllButtons(bool f)
        {
            foreach (Button b in buttons)
                b.Enabled = f;
        }

        // Enable/Hide buttons based on type of jog
        private void RestrictButtons()
        {
            switch (CoordBox.Text)
            {
                case "BASE":
                case "TOOL":
                    EnableAllButtons(true);
                    ColorAllButtons();
                    break;
                case "PART":
                    log.Info("PART: {0}", Part);
                    if (Part.StartsWith("SPHERE"))
                    {
                        EnableAllButtons(false);
                        ZplusBtn.Enabled = true;
                        ZminusBtn.Enabled = true;
                        RxPlusBtn.Enabled = true;
                        RxMinusBtn.Enabled = true;
                        RyPlusBtn.Enabled = true;
                        RyMinusBtn.Enabled = true;
                        //RxVerticalBtn.Enabled = true;
                        //RyZeroBtn.Enabled = true;
                        ColorAllButtons();
                    }
                    else if (Part.StartsWith("CYLINDER"))
                    {
                        EnableAllButtons(false);
                        XplusBtn.Enabled = true;
                        XminusBtn.Enabled = true;
                        ZplusBtn.Enabled = true;
                        ZminusBtn.Enabled = true;
                        RxPlusBtn.Enabled = true;
                        RxMinusBtn.Enabled = true;
                        //RxVerticalBtn.Enabled = true;
                        ColorAllButtons();
                    }
                    else // FLAT
                    {
                        EnableAllButtons(true);
                        ColorAllButtons();
                    }
                    break;
            }
        }

        private void CoordBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestrictButtons();
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
        Task task = null;
        private void Repeater()
        {
            task = Task.Factory.StartNew(() =>
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

        private void ZplusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            log.Info("ZplusBtn_MouseDown");
            double distance = Convert.ToDouble(ZJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, distance / 1000.0, 0, 0, 0 };
            Jog(p);
            Repeater();
        }

        private void ZminusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            log.Info("ZminusBtn_MouseDown");
            double distance = Convert.ToDouble(ZJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, -distance / 1000.0, 0, 0, 0 };
            Jog(p);
            Repeater();
        }

        private void XplusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double distance = Convert.ToDouble(XyJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { distance / 1000.0, 0, 0, 0, 0, 0 };
            Jog(p);
            Repeater();
        }

        private void XminusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            double distance = Convert.ToDouble(XyJogDistanceBox.SelectedItem.ToString());
            double[] p = new double[] { -distance / 1000.0, 0, 0, 0, 0, 0 };
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

        private void FlipRBtn_Click(object sender, EventArgs e)
        {
            mainForm.RobotSend("16,3," + Deg2Rad(180).ToString());
        }

        private void ZeroPBtn_Click(object sender, EventArgs e)
        {
            mainForm.RobotSend("16,4,0");
        }

        private void ZeroYBtn_Click(object sender, EventArgs e)
        {
            mainForm.RobotSend("16,5,0");
        }
        private void ZeroRpyBtn_Click(object sender, EventArgs e)
        {
            mainForm.RobotSend(string.Format("18,{0},0,0", Deg2Rad(180)));
        }

        // Run through all the controls in the form and enable/disable buttons and comboboxes
        // Don't adjust the FreeDriveButton and keep SaveBtn
        private void ButtonEnable(bool enable)
        {
            foreach (Control c in this.Controls)
            {
                log.Info("type={0}", c.GetType());
                if (c != FreeDriveBtn && (c.GetType().Name == "Button" || c.GetType().Name == "ComboBox"))
                {
                    if (c == SaveBtn)
                    {
                        if (enable)
                            c.Enabled = ShouldSave;
                        else
                            c.Enabled = false;
                    }
                    else
                        c.Enabled = enable;

                    c.BackColor = c.Enabled ? Color.Green : Color.Gray;
                }

            }

        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        private void FreeDriveBtn_Click(object sender, EventArgs e)
        {
            if (FreeDriveBtn.Text.Contains("ON"))
            {
                mainForm.RobotSend("30,19,0");

                FreeDriveBtn.Text = "FreeDrive";
                FreeDriveBtn.BackColor = Color.Green;

                SendMessage(Handle, WM_SETREDRAW, false, 0);
                ButtonEnable(true);
                RestrictButtons();
                SendMessage(Handle, WM_SETREDRAW, true, 0);
                Refresh();
            }
            else
            {
                string freeAxes = "0,0,1,0,0,0";
                mainForm.RobotSend("30,19,1," + freeAxes);
                FreeDriveBtn.Text = "FreeDrive\nON";
                FreeDriveBtn.BackColor = Color.Blue;
                SendMessage(Handle, WM_SETREDRAW, false, 0);
                ButtonEnable(false);
                SendMessage(Handle, WM_SETREDRAW, true, 0);
                Refresh();
            }
        }
    }
}