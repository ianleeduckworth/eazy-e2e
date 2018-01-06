using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Drawing;
using System.Windows.Automation;
using EazyE2E.Element;
using EazyE2E.HardwareManipulation;
using EazyE2E.Logwatch;
using EazyE2E.Performance;
using EazyE2E.Process;
using System.Linq;
using EazyE2E.Enums;

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            const string appPath = "C:\\Windows\\System32\\calc.exe";
            using (var process = new EzProcess(appPath, "Calculator"))
            {
                process.StartProcess();

                var root = new EzRoot(process);
                var numberPad = root.RootElement.FindChildByAutomationId("NumberPad");
                var sevenButton = numberPad.FindChildByAutomationId("num7Button");

                sevenButton.Click();

                //pause
                System.Console.ReadLine();
            }
        }
    }
}
