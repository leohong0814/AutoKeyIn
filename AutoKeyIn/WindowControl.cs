using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoKeyIn
{
    public class WindowControl
    {
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public bool WaitWindowOnline(CancellationToken cancelByUser, string windowName, int retryTimes = -1)
        {
            IntPtr calculatorHandle = IntPtr.Zero;
            int retry = 0;
            while (calculatorHandle == IntPtr.Zero)
            {
                cancelByUser.ThrowIfCancellationRequested();
                calculatorHandle = FindWindow(null, windowName);
                if (retry == retryTimes)
                {
                    return false;
                }
                Thread.Sleep(500);
                retry++;
            }
            SetForegroundWindow(calculatorHandle);
            return true;
        }
    }
}
