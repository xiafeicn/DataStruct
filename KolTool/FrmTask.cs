using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KolTool;

namespace KolTool
{
    public partial class FrmTask : Form
    {
        public List<UCTaskPanel> listTaskPanel = new List<UCTaskPanel>();
        public FrmTask()
        {
            InitializeComponent();
        }

        private void FrmTask_Load(object sender, EventArgs e)
        {
            tabControlPanel1.Controls.Clear();

            ThreadPool.SetMinThreads(2, 2); // set min thread to 5
            ThreadPool.SetMaxThreads(20, 20); // set max thread to 12

            foreach (var item in listTaskPanel)
            {
                item.Dock = DockStyle.Top;
                tabControlPanel1.Controls.Add(item);
            }
        }
    }
}
