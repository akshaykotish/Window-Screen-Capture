using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing_ScreenCapture
{
    class Capture
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        public void capture(Process proc)
        {
            //Process proc = new Process();
            //proc.StartInfo.FileName = "notepad.exe";
            //proc.Start();
            proc.WaitForInputIdle();
            if (SetForegroundWindow(proc.MainWindowHandle))
            {
                RECT srcRect;
                if (!proc.MainWindowHandle.Equals(IntPtr.Zero))
                {
                    if (GetWindowRect(proc.MainWindowHandle, out srcRect))
                    {
                        int width = srcRect.Right - srcRect.Left;
                        int height = srcRect.Bottom - srcRect.Top;
                        if (width != 0 && height != 0)
                        {
                            Bitmap bmp = new Bitmap(width, height);
                            Graphics screenG = Graphics.FromImage(bmp);

                            try
                            {
                                screenG.CopyFromScreen(srcRect.Left, srcRect.Top,
                                        0, 0, new Size(width, height),
                                        CopyPixelOperation.SourceCopy);

                                bmp.Save(@"C:\Images\" + proc.ProcessName + ".jpg", ImageFormat.Jpeg);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                screenG.Dispose();
                                bmp.Dispose();
                            }
                        }
                    }
                }
            }
        }
    }
}
