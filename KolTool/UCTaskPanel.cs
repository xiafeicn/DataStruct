using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KolTool
{
    public partial class UCTaskPanel : UserControl
    {
        public KolFile KolFile { get; set; }

        public bool isUploading { get; set; }

        public UCTaskPanel()
        {
            InitializeComponent();
            isUploading = true;
            pictureBox1.Image = FormAccess.GetFileIcon(".pdf").ToBitmap();

        }

        private void FileTaskPanel_Load(object sender, EventArgs e)
        {
            lblFileName.Text = KolFile.hcpId;
            ThreadPool.QueueUserWorkItem(new WaitCallback(UploadFile), KolFile);

        }
        KolReportDownload ftu = new KolReportDownload();
        /// <summary>
        /// This method is used as a thread start function.
        /// The parameter is a Triplet because the ThreadStart can only take one object parameter
        /// </summary>
        /// <param name="triplet">First=guid; Second=path; Third=offset</param>
        public void UploadFile(object o)
        {
            long offset = 0;
            //ftu.manualReset.Reset();
            ftu.KolFile = o as KolFile;
            ftu.ProgressChanged += new ProgressChangedEventHandler(ft_ProgressChanged);
            ftu.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ft_RunWorkerCompleted);
            ftu.RunWorkerSync(new DoWorkEventArgs(offset));
        }

        delegate void ProgressCompleteDelegate(object sender, RunWorkerCompletedEventArgs e);
        void ft_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new ProgressCompleteDelegate(this.ft_RunWorkerCompleted), new object[] { sender, e });
                return;
            }
            if (!e.Cancelled)
            {
                lblSpeed.Text = "文件生成成功!";
                isUploading = false;
                progressBarX1.Value = progressBarX1.Maximum;
                btnStart.Visible = false;
            }
        }

        delegate void ProgressChangedDelegate(object sender, ProgressChangedEventArgs e);
        void ft_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new ProgressChangedDelegate(this.ft_ProgressChanged), new object[] { sender, e });
                return;
            }

            lblSpeed.Text = e.UserState.ToString();
            if (e.ProgressPercentage > 0 && e.ProgressPercentage < progressBarX1.Maximum)
            {
                progressBarX1.Value = e.ProgressPercentage;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "暂停")
            {
                ftu.manualReset.Reset();
                btnStart.Text = "继续";
            }
            else
            {
                ftu.manualReset.Set();
                btnStart.Text = "暂停";
            }
        }
    }

    public class KolFile
    {
        public string hcpId { get; set; }

        public string language { get; set; }

        public string fileType { get; set; }

        public string savePath { get; set; }
    }
}
