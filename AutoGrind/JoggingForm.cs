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
    public partial class JoggingForm : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        TcpServer robot;
        public bool fSave { get; set; }

        public JoggingForm(TcpServer _robot, string purpose = "General Jogging", bool saveable = false)
        {
            InitializeComponent();
            PurposeLbl.Text = purpose;
            robot = _robot;
            fSave = false;
            SaveBtn.Enabled = saveable;
            SaveBtn.BackColor = saveable ? Color.Green : Color.Gray;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            fSave = false;
            Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            fSave = true;
            Close();
        }


        private void JoggingForm_Load(object sender, EventArgs e)
        {
            DistanceLst.SelectedIndex = 2;
            AngleLst.SelectedIndex = 2;
        }

        private double d2r(double d)
        {
            return d * Math.PI / 180.0;
        }
        private double r2d(double r)
        {
            return r * 180.0 / Math.PI;
        }
        private void Jog(double[] p)
        {
            string command = "(13";
            for (int i = 0; i < 6; i++)
                command += "," + p[i].ToString();
            command += ")";
            log.Info("Jog Command: {0}", command);
            robot.Send(command);
        }
        private void XplusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceLst.SelectedItem.ToString());
            double[] p = new double[] { distance, 0, 0, 0, 0, 0 };
            Jog(p);
        }

        private void XminusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceLst.SelectedItem.ToString());
            double[] p = new double[] { -distance, 0, 0, 0, 0, 0 };
            Jog(p);
        }
        private void YplusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceLst.SelectedItem.ToString());
            double[] p = new double[] { 0, distance, 0, 0, 0, 0 };
            Jog(p);
        }

        private void YminusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceLst.SelectedItem.ToString());
            double[] p = new double[] { 0, -distance, 0, 0, 0, 0 };
            Jog(p);
        }
        private void ZplusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, distance, 0, 0, 0 };
            Jog(p);
        }

        private void ZminusBtn_Click(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(DistanceLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, -distance, 0, 0, 0 };
            Jog(p);
        }

        private void RollplusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, d2r(angle), 0, 0 };
            Jog(p);
        }

        private void RollminusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, -d2r(angle), 0, 0 };
            Jog(p);
        }

        private void PitchplusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, d2r(angle), 0 };
            Jog(p);
        }

        private void PitchminusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, -d2r(angle), 0 };
            Jog(p);
        }

        private void YawplusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, 0, d2r(angle) };
            Jog(p);
        }

        private void YawminusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, 0, -d2r(angle) };
            Jog(p);
        }

        private void ZeroRBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(15,3,0)");
        }
        private void FlipRBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(15,3," + d2r(180).ToString() + ")");
        }

        private void ZeroPBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(15,4,0)");
        }

        private void ZeroYBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(15,5,0)");
        }
        private void ZeroRpyBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(18,0,0,0)");
        }

    }
}