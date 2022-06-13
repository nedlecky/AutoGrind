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
    public partial class BigEditDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        // Can override these before showing dialog
        public string Title { get; set; } = "??";
        public int ScreenWidth { get; set; } = 1000;
        public int ScreenHeight { get; set; } = 1000;
        public string Recipe { get; set; } = "";

        public BigEditDialog()
        {
            InitializeComponent();

        }

        private void BigEditDialog_Load(object sender, EventArgs e)
        {
            FilenameLbl.Text = Title;
            Top = 0;
            Left = 0;
            Width = ScreenWidth;
            Height = ScreenHeight;

            DialogResult = DialogResult.None;
            CancelBtn.Select();

            RecipeRTB.Text = Recipe;

            // Load the Recipe Commands for User Inspection
            try
            {
                RecipeCommandsRTB.LoadFile("RecipeCommands.rtf");
            }
            catch (Exception ex)
            {
                log.Error(ex, "Could not load RecipeCommands.rtf!");
            }
        }

        private void KeepBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Recipe=RecipeRTB.Text;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ReloadBtn_Click(object sender, EventArgs e)
        {
            RecipeRTB.Text = Recipe;
        }
    }
}
