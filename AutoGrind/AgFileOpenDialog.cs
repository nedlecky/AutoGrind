using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoGrind
{
    public partial class AgFileOpenDialog : Form
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public string Title { get; set; }
        public string Filter { get; set; }
        public string InitialDirectory { get; set; }
        public string FileName { get; set; }

        private string[] fileList;

        public AgFileOpenDialog()
        {
            InitializeComponent();
        }

        private void FileOpenForm_Load(object sender, EventArgs e)
        {
            TitleLbl.Text = Title;
            fileList = Directory.GetFiles(InitialDirectory,Filter);
            foreach (string file in fileList)
                FileListBox.Items.Add(Path.GetFileName(file));
        }

        private void FileListBox_DoubleClick(object sender, EventArgs e)
        {
            OpenBtn_Click(null, null);
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            FileName = fileList[FileListBox.SelectedIndex];
            log.Debug("OpenBtn_Click(...) Filename={0}", FileName);
            DialogResult= DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            log.Debug("CancelBtn_Click(...)");
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
