using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace X509
{
    internal class Program
    {
        private const int SwpShowWindow = 0x0040;
        private static IntPtr Handle => GetConsoleWindow();

        [DllImport("kernel32")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
                                                int x, int y, int cx, int cy, int uFlags);

        private static void Main(string[] args)
        {
            try
            {
                SetWindowPosition();
                new CertificateAnalyzer().Analyze();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine("########## Press Enter to close the console ##########");
            Console.ReadLine();
        }

        private static void SetWindowPosition()
        {
            var screen = Screen.PrimaryScreen.Bounds;
            SetWindowPos(Handle, IntPtr.Zero,
                         screen.Width*2/3, 0, screen.Width/3, screen.Height,
                         SwpShowWindow);
        }
    }
}