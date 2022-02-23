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

        public JoggingForm(TcpServer _robot)
        {
            InitializeComponent();
            robot = _robot;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void JoggingForm_Load(object sender, EventArgs e)
        {
            DistanceLst.SelectedIndex = 2;
            AngleLst.SelectedIndex = 2;
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
            double[] p = new double[] { 0, 0, 0, angle, 0, 0 };
            Jog(p);
        }

        private void RollminusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, -angle, 0, 0 };
            Jog(p);
        }

        private void PitchplusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, angle, 0 };
            Jog(p);
        }

        private void PitchminusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, -angle, 0 };
            Jog(p);
        }

        private void YawplusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, 0, angle };
            Jog(p);
        }

        private void YawminusBtn_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(AngleLst.SelectedItem.ToString());
            double[] p = new double[] { 0, 0, 0, 0, 0, -angle };
            Jog(p);
        }

        private void ZeroRpyBtn_Click(object sender, EventArgs e)
        {
            robot.Send("(15)");
        }
    }
}
