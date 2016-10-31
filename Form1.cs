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
        private DateTime startTime = DateTime.MinValue;
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
           
            if (IsChromeRunning())
            {
                
                if (startTime == DateTime.MinValue)
                {
                    startTime = DateTime.Now;
                    lblStartTime.Text = startTime.ToString();
                }
                lblChromeStatus.Text = "running" + DateTime.Now.ToString();
            }
            else
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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChromeRunning())

            {
                var window = MessageBox.Show("Chrome is still running. Are you sure you want to close?", "caption", buttons: MessageBoxButtons.YesNo);
                if (window == DialogResult.No) e.Cancel = true;
                else e.Cancel = false;
            }

            else
            {
              
            }


        }

        private bool IsChromeRunning()
        {
            Process[] pname = Process.GetProcessesByName("chrome");
            if (pname.Length == 0)
            {
                return false;
            }
            else return true;
        }
    }
}

