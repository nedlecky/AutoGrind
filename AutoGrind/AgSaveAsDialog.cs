using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoGrind
{
    public partial class AgSaveAsDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public string Title { get; set; }
        public string Filter { get; set; }
        public string InitialDirectory { get; set; }
        public string FileName { get; set; }

        private string[] fileList;

        public AgSaveAsDialog()
        {
            InitializeComponent();
        }
        private void AgSaveAsDialog_Load(object sender, EventArgs e)
        {
            TitleLbl.Text = Title;
            fileList = Directory.GetFiles(InitialDirectory, Filter);
            foreach (string file in fileList)
                FileListBox.Items.Add(Path.GetFileName(file));
        }
        private void FileListBox_Click(object sender, EventArgs e)
        {
            FileNameTxt.Text = FileListBox.SelectedItem.ToString();

        }
        private void FileListBox_DoubleClick(object sender, EventArgs e)
        {
            FileNameTxt.Text = FileListBox.SelectedItem.ToString();
            SaveBtn_Click(sender, e);

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            FileName = Path.Combine(InitialDirectory, FileNameTxt.Text);
            log.Debug("SaveBtn_Click(...) Filename={0}", FileName);
            DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            FileName = "";
            log.Debug("CancelBtn_Click(...)");
            DialogResult = DialogResult.Cancel;
        }
    }
}
