using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Words;

namespace WordProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string path = @"C:\Users\fei.xia\Desktop\111111\要处理的备份\cwgl_lx0201\cwgl_lx0202.doc";
        private void button1_Click(object sender, EventArgs e)
        {
            Document doc = new Document(path);
            var nodes = doc.ChildNodes;
        }
    }
}
