using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1025; i <= 65535; i++)
            {
                try
                {
                    this.sockTCPServer1.Listen(IPAddress.Parse("192.168.10.25"), i, 5000);//即时消息Tcp服务
                    //this.sockTCPServer1.Listen(System.Net.IPAddress.Any, i, 5000);//即时消息Tcp服务
                    //this.WirteLog("服务启动成功");
                }
                catch (Exception ex)
                {
                    // this.WirteLog("服务启动失败:" + ex.Source + ex.Message);
                }
            }
          
            
        }
    }
}
