using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinProcessWatcher
{
    public partial class Form1 : Form
    {
        private DateTime startTime=DateTime.MinValue;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10000;
            timer1.Start();

            //string[] files = Directory.GetFiles(dirName);

            //foreach (string file in files)
            //{
            //    FileInfo fi = new FileInfo(file);
            //    if (fi.LastAccessTime < DateTime.Now.AddMonths(-3))
            //        fi.Delete();
            //}
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("chrome");
            if (pname.Length == 0)
            {
                startTime = DateTime.MinValue;
                lblChromeStatus.Text = "stopped" + DateTime.Now.ToString();
                //delete files
                (from f in new DirectoryInfo(@"C:\Users\junaidi\Downloads").GetFiles()
                 where f.CreationTime > startTime
                 select f).ToList()
            .ForEach(f => f.Delete());
                //close form
                Application.Exit();
            }
            else
            {
                if (startTime == DateTime.MinValue)
                {
                    startTime = DateTime.Now;
                    lblStartTime.Text = startTime.ToString();
                }
                lblChromeStatus.Text = "running" + DateTime.Now.ToString();
            }
        }
    }
}
