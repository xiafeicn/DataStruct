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

namespace ScanPort
{
    public partial class Form1 : Form
    {
        private IPAddress LocalIpAddress;

        string ips = "101.132.34.100";// "117.80.237.42";

        public Form1()
        {
            InitializeComponent();

            System.Net.IPAddress[] ip = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());//获得本机局域网地址

            LocalIpAddress = ip[ip.Length - 1];//获得本机局域网地址
        }
        public IPAddress LocalIP = IPAddress.Parse("127.0.0.1");
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1024; i <= 65535; i++)
            {
                //如果用户自己与对方其中一方使用TCP登录，则需要使用TCP代理服务器中转数据传输
                this.sockTCPClient1.InitSocket(this.LocalIpAddress, i);//邦定本机TCP随机端口
                this.sockTCPClient1.Connect(System.Net.IPAddress.Parse(ips), 86);//TCP检测联接服务器
            }

        }
    }
}
