using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GBI.Metrix.DataAccess;
using GBI.Metrix.Service;
using KolTool;

namespace KolTool
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
            tmrCheckStatus.Interval = 1000 * 10;
            tmrCheckStatus.Start();
        }



        private void btnExport_Click(object sender, EventArgs e)
        {
            FormAccess.KillPhantProcess();
            listAllTask = new List<string>();
            listFinishedTask = new List<string>();
            if (radioButton1.Checked)
            {
                KolReportBase.sourceUrl = "192.168.0.96:8091";
                KolReportBase.domain = "192.168.0.96";
            }
            else
            {
                KolReportBase.sourceUrl = "metrix.gbihealth.com";
                KolReportBase.domain = "metrix.gbihealth.com";
            }

            var users = txtUsers.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var savePath = txtOutPath.Text;
            if (string.IsNullOrWhiteSpace(txtOutPath.Text) || !Directory.Exists(txtOutPath.Text))
            {
                MessageBox.Show("请设置导出路径");
                return;
            }
            string language = rbCN.Checked ? "cn" : "en";
            string type = rbPdf.Checked ? "pdf" : "word";


            FrmTask frmTask = new FrmTask();
            foreach (var user in users)
            {
                UCTaskPanel taskPanel = new UCTaskPanel();

                taskPanel.KolFile = new KolFile()
                {
                    fileType = type,
                    hcpId = user,
                    language = language,
                    savePath = savePath,
                };
                frmTask.listTaskPanel.Add(taskPanel);
                listAllTask.Add(user);
            }
            frmTask.ShowDialog(this);
        }

        private void txtOutPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        public static List<string> listAllTask = new List<string>();
        public static List<string> listFinishedTask = new List<string>();
        public int lastCount = 0;
        public int Counter = 0;
        private void tmrCheckStatus_Tick(object sender, EventArgs e)
        {
            if (lastCount == GetFileCount())
            {
                Counter++;
                //超过5分钟重启
                if (Counter > 30 && listAllTask.Count > listFinishedTask.Count)
                {
                    //var listLeftTask = listAllTask.Except(listFinishedTask);
                    //txtUsers.Text = string.Join(",", listLeftTask);
                    //Counter = 0;
                    Application.Restart();
                }
            }
            else
            {
                lastCount = GetFileCount();
                Counter = 0;
            }
        }

        public int GetFileCount()
        {
            var phantPath = Path.Combine(Application.StartupPath, "temp");
            if (!new DirectoryInfo(phantPath).Exists)
            {
                Directory.CreateDirectory(phantPath);
            }
            return new DirectoryInfo(phantPath).GetFiles().Length;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
