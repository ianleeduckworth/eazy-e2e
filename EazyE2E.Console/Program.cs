using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            const string calculatorPath = "C:\\Windows\\System32\\calc.exe";
            using (var process = new EzProcess(calculatorPath, "Calculator"))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                process.StartProcess();

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
