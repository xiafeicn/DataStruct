using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;

namespace WindowsFormsApplication1
{
    public partial class FrmRabbit : Form
    {
        public FrmRabbit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "47.91.211.88",
                UserName = "guest",
                Password = "guest",
            };

            //第一步：创建connection
            var connection = factory.CreateConnection();

            //第二步：创建一个channel
            var channel = connection.CreateModel();

            //第三步：申明交换机【因为rabbitmq已经有了自定义的ampq default exchange】

            //第四步：获取消息
            var result = channel.BasicGet("mytest", true);

            var msg = Encoding.UTF8.GetString(result.Body);
        }
    }
}
