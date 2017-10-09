using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using EazyE2E.Element;
using EazyE2E.Enums;
using EazyE2E.HardwareManipulation;
using EazyE2E.Logwatch;
using EazyE2E.Performance;
using EazyE2E.Process;

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            const string calculatorPath = "C:\\Code\\EazyE2E\\TestApplication\\bin\\Debug\\TestApplication.exe";
            using (var process = new EzProcess(calculatorPath, "TestApplication"))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                process.StartProcess();

                var root = new EzRoot(process);
                root.ResizeWindow(10, 10, 100, 100);

                var foo = new EzText(root);
                foo.BackgroundColor.HandleResult(() =>
                {
                    System.Console.WriteLine("Operation was unsupported");
                }, () =>
                {
                    System.Console.WriteLine("Operation yielded a mixed result");
                }, value =>
                {
                    System.Console.WriteLine($"Value: {value}");
                });

                stopwatch.Stop();
                System.Console.WriteLine($"Test took {stopwatch.ElapsedMilliseconds} miliseconds");
                System.Console.ReadLine();
            }
        }
    }
}
