using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing_ScreenCapture
{
    public partial class Form1 : Form
    {
        Capture capture = new Capture();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var image = ScreenCapture.CaptureDesktop();
            image.Save(@"C:\Images\s1.jpg", ImageFormat.Jpeg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process[] procs = Process.GetProcesses();
            IntPtr hWnd;
            int i = 1;
            foreach (Process proc in procs)
            {
                if ((hWnd = proc.MainWindowHandle) != IntPtr.Zero)
                {
                    Console.WriteLine("{0} : {1}", proc.ProcessName, hWnd);
                    capture.capture(proc);
                    //var img = ScreenCapture.CaptureWindow(proc.MainWindowHandle);
                    //if(img != null)
                    //{
                    //    img.Save(@"C:\Images\" + proc.ProcessName + ".jpg", ImageFormat.Jpeg);
                    //}

                    //i++;
                }
            }
        }
    }
}
