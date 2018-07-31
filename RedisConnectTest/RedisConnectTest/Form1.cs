using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GBI.Core.Cache;

namespace RedisConnectTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ParallelLoopResult result = Parallel.For(1, 10000, i =>
            {
                CacheManager.Get<string>("name");
            });
            //ParallelLoopResult result = Parallel.For(1, 10, i =>
            //{
            //    CacheManager.Get<string>("name");
            //});
        }
    }
}
