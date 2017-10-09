using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using EazyE2E.Element;
using EazyE2E.Enums;
using EazyE2E.HardwareManipulation;
using EazyE2E.Performance;
using EazyE2E.Process;

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            var calculatorPath = "C:\\Program Files (x86)\\Notepad++\\notepad++.exe";
            using (var process = new EzProcess(calculatorPath, "notepad++"))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                process.StartProcess();
                Thread.Sleep(2000);

                var root = new EzRoot(process);
                var sevenButton = root.RootElement.FindDescendantByAutomationId("num7Button");
                EzMouseFunctions.DoubleClick(sevenButton);

                var results = root.RootElement.FindDescendantByAutomationId("CalculatorResults");

                System.Console.WriteLine(results.Name == "Display is 7" ? "Test passed.  Display said 7" : "Test failed.  Display did not say 7");

                stopwatch.Stop();
                System.Console.WriteLine($"Test took {stopwatch.ElapsedMilliseconds} miliseconds");
                System.Console.ReadLine();
            }
        }
    }
}
