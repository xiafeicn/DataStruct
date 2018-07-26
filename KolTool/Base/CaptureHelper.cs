using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Windows.Forms;
using GBI.Metrix.Service;
using KolTool;

namespace word
{
    public class CaptureHelper
    {
        public CaptureHelper()
        {

        }

        private string phantompath = Path.Combine(Application.StartupPath + "\\App_Data\\phantomjs");
        private string phantomjs = Path.Combine(Application.StartupPath + "\\App_Data\\phantomjs\\phantomjs.exe");
        private string capturejs = Path.Combine(Application.StartupPath + "\\App_Data/phantomjs\\capture.js");
        private string capturenetworkjs = Path.Combine(Application.StartupPath + "\\App_Data\\phantomjs\\captureNetwork.js");



        public string url { get; set; }
        public string urlChart { get; set; }

        private static object locker = new object();
        public List<string> CaptureByNreco(string language)
        {
            lock (locker)
            {
                var tempPath = Path.Combine(Application.StartupPath + "\\temp");
                NReco.PhantomJS.PhantomJS pjs = new NReco.PhantomJS.PhantomJS();
                pjs.ToolPath = phantompath;
                pjs.ExecutionTimeout = new TimeSpan(0, 0, 0, 60);
                string file1 = Path.Combine(tempPath, Guid.NewGuid().ToString() + ".png");
                string file2 = Path.Combine(tempPath, Guid.NewGuid().ToString() + ".png");
                string file3 = Path.Combine(tempPath, Guid.NewGuid().ToString() + ".png");
                string file4 = Path.Combine(tempPath, Guid.NewGuid().ToString() + ".png");
                string file5 = Path.Combine(tempPath, Guid.NewGuid().ToString() + ".png");
                string file6 = Path.Combine(tempPath, Guid.NewGuid().ToString() + ".png");
                string file7 = Path.Combine(tempPath, Guid.NewGuid().ToString() + ".png");

                pjs.Run(capturejs, new string[] { url, file1, file2, file3, file4, file5, file6, file7, language, KolReportBase.domain });
                return new string[] { file1, file2, file3, file4, file5, file6, file7 }.ToList();

            }
        }

        public void KillCatProcess()
        {
            try
            {
                string str = "";
                Process[] processes;
                //Get the list of current active processes.
                processes = System.Diagnostics.Process.GetProcesses();
                //Grab some basic information for each process.
                for (int i = 0; i < processes.Length - 1; i++)
                {
                    if (processes[i].ProcessName.Equals("phantomjs", StringComparison.OrdinalIgnoreCase)/* && processes[i].StartTime > FormAccess.appStarTime*/)
                    {
                        processes[i].Kill();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }

    public class CaptureException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="CaptureException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CaptureException(string message)
            : base(message)
        {

        }
    }
}
