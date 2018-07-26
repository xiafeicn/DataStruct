using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Aspose.Words;
using GBI.Metrix.Model;
using GBI.Metrix.Model.Entity;
using GBI.Metrix.Service;
using GBI.Metrix.ViewModel.Enum;
using KolTool;

namespace KolTool
{
    /// <summary>
    /// A class to upload a file to a web server using WSE 3.0 web services, with the MTOM standard.
    /// To use this class, drag/drop an instance onto a windows form, and create event handlers for ProgressChanged
    /// and RunWorkerCompleted.  
    /// Make sure to specify the LocalFilePath before you call RunWorkerAsync() to begin the upload
    /// </summary>
    public class KolReportDownload : KolReportBase
    {
        //ZipClass zipFiles = new ZipClass();
        /// <summary>
        /// Start the upload operation synchronously.
        /// The argument must be the start position, usually 0
        /// </summary>
        public void RunWorkerSync(DoWorkEventArgs e)
        {
            OnDoWork(e);
            base.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(e.Result, null, false));
        }

        /// <summary>
        /// This method starts the uplaod process. It supports cancellation, reporting progress, and exception handling.
        /// The argument is the start position, usually 0
        /// </summary>
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            base.OnDoWork(e);

            if (this.CancellationPending)//如果用户申请取消
            {
                e.Cancel = true;
                return;
            }
            try
            {
                DownLoad(KolFile, e); //开始生成文档
            }
            catch (Exception ex)
            {
                //MessageBox.Show(KolFile.hcpId + "导出失败!");
                Application.Restart();
            }

        }

        private void DownLoad(KolFile pamareter, DoWorkEventArgs e)
        {
            this.ReportProgress(0, "开始生成文档...");
            System.Threading.Thread.Sleep(1000);

            template = Path.Combine(Application.StartupPath + string.Format("\\App_Data\\template\\{0}\\template.docx", pamareter.language));
            templateEnd = Path.Combine(Application.StartupPath + string.Format("\\App_Data\\template\\{0}\\templateEnd.docx", pamareter.language));
            templateKOL = Path.Combine(Application.StartupPath + string.Format("\\App_Data\\template\\{0}\\templateKOL.docx", pamareter.language));
            templateDescription = Path.Combine(Application.StartupPath + string.Format("\\App_Data\\template\\{0}\\templateDescription.docx", pamareter.language));


            Document doc = new Document(template);
            // Create a document builder to insert content with into document.
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.MoveToDocumentEnd();
            builder.Document.Range.Replace("$DATE$", DateTime.Now.ToShortDateString(), false, false);
            ////将光标移到目录书签
            //builder.MoveToBookmark("TOC", false, true);

            //设置目录的格式         
            //“目录”两个字居中显示、加粗、搜宋体
            builder.CurrentParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            builder.Font.Bold = true;
            builder.Font.Name = "微软雅黑";
            builder.Font.Size = 16;
            builder.Font.Color = Color.Black;
            builder.Writeln(pamareter.language == "cn" ? "目录" : "Bookmark");

            //清清除所有样式设置
            builder.CurrentParagraph.ParagraphFormat.ClearFormatting();
            //目录居左
            builder.ParagraphFormat.Alignment = (ParagraphAlignment.Left);
            //插入目录，这是固定的
            builder.InsertTableOfContents("\\o \"1-3\" \\h \\z \\u");

            // Insert a dataSet of contents at the beginning of the document.
            //builder.InsertTableOfContents("\\o \"1-3\" \\h \\z \\u");
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
            builder.Writeln(pamareter.language == "cn" ? "KOL 分析报告说明" : "KOL analysis reports description");
            //builder.Writeln("学科领域KOL TOP100");
            //builder.Writeln(pamareter.language == "cn" ? "KOL分析" : "KOL Analysis");
            HcpEntity hcpEntity = HttpService.GetById<BaseEntity<HcpEntity>>(pamareter.hcpId, DataSetEnum.hcp).data.FirstOrDefault();
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading2;
            builder.Writeln(hcpEntity.GetName(pamareter.language));

            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
            //builder.Writeln(pamareter.language == "cn" ? "KOL分析方案" : "KOL Analysis methodology");
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;
            // Call the method below to update the TOC.
            doc.UpdateFields();

            this.ReportProgress(30, "已生成目录...");
            var boooks = doc.Range.Bookmarks;
            foreach (Bookmark bm in boooks)
            {
                if (bm.Text.StartsWith("KOL 分析报告说明") || bm.Text.StartsWith("KOL analysis reports description"))
                {
                    //builder.InsertBreak(BreakType.PageBreak);
                    builder.MoveToBookmark(bm.Name, false, false);                      // you can optionally check the style of the current paragraph and then insert document                 

                    InsertDocument(bm.BookmarkStart.ParentNode, new Document(templateDescription));
                    builder.MoveToBookmark(bm.Name, true, false);
                    builder.InsertBreak(BreakType.PageBreak);
                }

                //if (!pamareter.fileType.Equals("pdf", StringComparison.OrdinalIgnoreCase))
                //{
                //    //只有多个人才会显示KOL分析方案
                //    if (bm.Text.StartsWith("KOL分析方案") || bm.Text.StartsWith("KOL Analysis methodology"))
                //    {
                //        builder.MoveToBookmark(bm.Name, false, false);
                //        Document docEnd = new Document(templateEnd);
                //        InsertDocument(bm.BookmarkStart.ParentNode, docEnd);
                //    }
                //}
               
                //if (bm.Text.StartsWith("学科领域KOL"))
                //{
                //    builder.MoveToBookmark(bm.Name, true, false);
                //    builder.InsertBreak(BreakType.PageBreak);
                //}
                if (bm.Text.Equals(hcpEntity.GetName(pamareter.language),StringComparison.CurrentCultureIgnoreCase))
                {
                    builder.MoveToBookmark(bm.Name, false, false);
                    this.ReportProgress(40, "开始请求报表数据...");
                    // you can optionally check the style of the current paragraph and then insert document                 
                    GeneralKol(bm, builder, hcpEntity, pamareter.language);
                    //builder.InsertBreak(BreakType.PageBreak);
                }
            }

            doc.UpdateFields();
            this.ReportProgress(99, "文档生成成功，正在保存中...");

            doc.Save(Path.Combine(pamareter.savePath, string.Concat(hcpEntity.GetName(pamareter.language), ".", pamareter.fileType == "pdf" ? "pdf" : "docx")));
            this.ReportProgress(100, "文件生成成功...");
            Form2.listFinishedTask.Add(pamareter.hcpId);
            Form2.RemoveUserTask(pamareter.hcpId);
        }

        [DllImport("Kernel32.dll")]
        private static extern bool MoveFileEx(string sourceFile, string destFile, uint i);


    }
}

