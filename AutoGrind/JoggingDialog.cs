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
    public partial class JoggingDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        readonly TcpServerSupport robot;
        public bool ShouldSave { get; set; }
        private bool SaveButtonEnabled = false;

        MainForm mainForm;

        public JoggingDialog(TcpServerSupport _robot, MainForm _mainForm, string purpose = "General Jogging", string tool = "UnknownTool", string part = "UnknownPart", bool _SaveButtonEnabled = false)
        {
            InitializeComponent();
            PurposeLbl.Text = purpose;
            ToolLbl.Text = "Tool: " + tool;
            PartLbl.Text = "Part: " + part;
            robot = _robot;
            mainForm = _mainForm;
            ShouldSave = false;
            SaveButtonEnabled = _SaveButtonEnabled;
            SaveBtn.Enabled = SaveButtonEnabled;
            SaveBtn.BackColor = SaveButtonEnabled ? Color.Green : Color.Gray;
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


        private void JoggingForm_Load(object sender, EventArgs e)
        {
            DistanceBox.SelectedIndex = 2;
            AngleBox.SelectedIndex = 2;
            CoordBox.SelectedIndex = 0;
        }

        private double Deg2Rad(double d)
        {
            return d * Math.PI / 180.0;
        }
        private double Rad2Deg(double r)
        {
            return r * 180.0 / Math.PI;
        }
        private void Jog(double[] p)
        {
            string command = "??";
            switch (CoordBox.Text)
            {
                case "BASE":
                    command = "(13";
                    break;
                case "TOOL":
                    command = "(14";
                    break;
                case "PART":
                    command = "(15";
                    break;
            }

            for (int i = 0; i < 6; i++)
                command += "," + p[i].ToString();
            command += ")";
            log.Info("Jog Command: {0}", command);
            robot.Send(command);
        }
        private void XplusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceBox.SelectedItem.ToString());
            double[] p = new double[] { distance / 1000.0, 0, 0, 0, 0, 0 };
            Jog(p);
        }

        private void XminusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceBox.SelectedItem.ToString());
            double[] p = new double[] { -distance / 1000.0, 0, 0, 0, 0, 0 };
            Jog(p);
        }
        private void YplusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, distance / 1000.0, 0, 0, 0, 0 };
            Jog(p);
        }

        private void YminusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, -distance / 1000.0, 0, 0, 0, 0 };
            Jog(p);
        }
        private void ZplusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, distance / 1000.0, 0, 0, 0 };
            Jog(p);
        }

        private void ZminusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, -distance / 1000.0, 0, 0, 0 };
            Jog(p);
        }

        private void RxPlusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, Deg2Rad(angle), 0, 0 };
            Jog(p);
        }

        private void RxMinusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, -Deg2Rad(angle), 0, 0 };
            Jog(p);
        }

        private void RyPlusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, Deg2Rad(angle), 0 };
            Jog(p);
        }

        private void RyMinusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, -Deg2Rad(angle), 0 };
            Jog(p);
        }

        private void RzPlusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, 0, Deg2Rad(angle) };
            Jog(p);
        }

        private void RzMinusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleBox.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, 0, -Deg2Rad(angle) };
            Jog(p);
        }

        private void FlipRBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(16,3," + Deg2Rad(180).ToString() + ")");
        }

        private void ZeroPBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(16,4,0)");
        }

        private void ZeroYBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(16,5,0)");
        }
        private void ZeroRpyBtn_Click(object sender, EventArgs e)
        {
            robot.Send(string.Format("(18,{0},0,0)", Deg2Rad(180)));
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
                            c.Enabled = SaveButtonEnabled;
                        else
                            c.Enabled = false;
                    }
                    else
                        c.Enabled = enable;

                    c.BackColor = c.Enabled ? Color.Green : Color.Gray;
                }

            }

        }
        private void FreeDriveBtn_Click(object sender, EventArgs e)
        {
            if (FreeDriveBtn.Text.Contains("ON"))
            {
                robot.Send("(30,19,0)");

                FreeDriveBtn.Text = "FreeDrive";
                FreeDriveBtn.BackColor = Color.Green;
                ButtonEnable(true);
            }
            else
            {
                robot.Send("(30,19,1)");
                FreeDriveBtn.Text = "FreeDrive\nON";
                FreeDriveBtn.BackColor = Color.Blue;
                ButtonEnable(false);
            }
        }
    }
}