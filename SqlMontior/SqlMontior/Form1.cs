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

namespace SqlMontior
{
    public partial class Form1 : Form
    {
        public static readonly object lockObj = new object();

        public static string LogFile = Path.Combine(Application.StartupPath, "log.txt");
        public Form1()
        {
            InitializeComponent();
            SQLHelper.PublicIpConnectionString = textBox1.Text.Trim();
            SQLHelper.LocalIpConnectionString = textBox2.Text.Trim();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (lockObj)
            {
                try
                {
                    SQLHelper.ExecuteSqlToDataTable("select top 1 * from users", SQLHelper.PublicIpConnectionString);
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.ToString());
                    File.AppendAllText(LogFile,string.Format("{0} {1} \r\n {2}", DateTime.Now, SQLHelper.PublicIpConnectionString,ex.ToString()));
                }

                try
                {
                    SQLHelper.ExecuteSqlToDataTable("select top 1 * from users", SQLHelper.LocalIpConnectionString);
                }
                catch (Exception ex)
                {
                    listBox2.Items.Add(ex.ToString());
                    File.AppendAllText(LogFile, string.Format("{0} {1} \r\n {2}", DateTime.Now, SQLHelper.LocalIpConnectionString, ex.ToString()));
                }
            }
        }
    }
}
