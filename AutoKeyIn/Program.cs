using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoKeyIn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string windowName = Properties.Settings.Default.WindowName;
            int interval = Properties.Settings.Default.ClickInterval;
            string key = Properties.Settings.Default.KeyinKey;

            if (string.IsNullOrEmpty(windowName) )
            {
                Console.WriteLine("Enter window name");
                windowName = Console.ReadLine();
                Console.WriteLine("Enter interval time(s)");
                interval = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter whitch key nead to auto key in");
                key = Console.ReadLine();
                Properties.Settings.Default.WindowName = windowName;
                Properties.Settings.Default.ClickInterval = interval;
                Properties.Settings.Default.KeyinKey = key;
                Properties.Settings.Default.Save();
            }
            
            WindowControl windowControl = new WindowControl();
            CancellationTokenSource ctx = new CancellationTokenSource();
            if (!windowControl.WaitWindowOnline(ctx.Token, windowName))
            {
                Console.WriteLine($"Can't find {windowName} window");
                return;
            }
            Console.WriteLine("Press F5 to Start");
            while (!ctx.IsCancellationRequested && Console.ReadKey().Key != ConsoleKey.F5)
            {
                Console.WriteLine("Press F5 to Start");
            }
            Thread.Sleep(500);
            while (!ctx.IsCancellationRequested)
            {
                Thread.Sleep(interval*1000);
                SendKeys.SendWait("R");
            }
            
        }
    }
}
