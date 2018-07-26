namespace KolTool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rbCN = new System.Windows.Forms.RadioButton();
            this.rbEN = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtUsers = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbPdf = new System.Windows.Forms.RadioButton();
            this.rbWord = new System.Windows.Forms.RadioButton();
            this.txtOutPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tmrCheckStatus = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbCN
            // 
            this.rbCN.AutoSize = true;
            this.rbCN.Checked = true;
            this.rbCN.Location = new System.Drawing.Point(21, 14);
            this.rbCN.Name = "rbCN";
            this.rbCN.Size = new System.Drawing.Size(47, 16);
            this.rbCN.TabIndex = 0;
            this.rbCN.TabStop = true;
            this.rbCN.Text = "中文";
            this.rbCN.UseVisualStyleBackColor = true;
            // 
            // rbEN
            // 
            this.rbEN.AutoSize = true;
            this.rbEN.Location = new System.Drawing.Point(74, 14);
            this.rbEN.Name = "rbEN";
            this.rbEN.Size = new System.Drawing.Size(47, 16);
            this.rbEN.TabIndex = 0;
            this.rbEN.TabStop = true;
            this.rbEN.Text = "英文";
            this.rbEN.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbCN);
            this.panel1.Controls.Add(this.rbEN);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 45);
            this.panel1.TabIndex = 1;
            // 
            // txtUsers
            // 
            this.txtUsers.Location = new System.Drawing.Point(13, 99);
            this.txtUsers.Multiline = true;
            this.txtUsers.Name = "txtUsers";
            this.txtUsers.Size = new System.Drawing.Size(649, 127);
            this.txtUsers.TabIndex = 2;
            this.txtUsers.Text = "108558,2818,792638,60957,794674,26390,460964,792464,4944462,5275";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(587, 246);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbPdf);
            this.panel2.Controls.Add(this.rbWord);
            this.panel2.Location = new System.Drawing.Point(240, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 45);
            this.panel2.TabIndex = 1;
            // 
            // rbPdf
            // 
            this.rbPdf.AutoSize = true;
            this.rbPdf.Checked = true;
            this.rbPdf.Location = new System.Drawing.Point(21, 14);
            this.rbPdf.Name = "rbPdf";
            this.rbPdf.Size = new System.Drawing.Size(41, 16);
            this.rbPdf.TabIndex = 0;
            this.rbPdf.TabStop = true;
            this.rbPdf.Text = "pdf";
            this.rbPdf.UseVisualStyleBackColor = true;
            // 
            // rbWord
            // 
            this.rbWord.AutoSize = true;
            this.rbWord.Location = new System.Drawing.Point(74, 14);
            this.rbWord.Name = "rbWord";
            this.rbWord.Size = new System.Drawing.Size(47, 16);
            this.rbWord.TabIndex = 0;
            this.rbWord.TabStop = true;
            this.rbWord.Text = "word";
            this.rbWord.UseVisualStyleBackColor = true;
            // 
            // txtOutPath
            // 
            this.txtOutPath.Location = new System.Drawing.Point(95, 63);
            this.txtOutPath.Name = "txtOutPath";
            this.txtOutPath.Size = new System.Drawing.Size(548, 21);
            this.txtOutPath.TabIndex = 4;
            this.txtOutPath.Text = "D:\\\\";
            this.txtOutPath.Click += new System.EventHandler(this.txtOutPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "设置导出目录";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioButton1);
            this.panel3.Controls.Add(this.radioButton2);
            this.panel3.Location = new System.Drawing.Point(443, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(185, 45);
            this.panel3.TabIndex = 1;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(21, 14);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "线下";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(74, 14);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "线上";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // tmrCheckStatus
            // 
            this.tmrCheckStatus.Interval = 10000;
            this.tmrCheckStatus.Tick += new System.EventHandler(this.tmrCheckStatus_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 281);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOutPath);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtUsers);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量导出KOL报表";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbCN;
        private System.Windows.Forms.RadioButton rbEN;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtUsers;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbPdf;
        private System.Windows.Forms.RadioButton rbWord;
        private System.Windows.Forms.TextBox txtOutPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Timer tmrCheckStatus;
    }
}

