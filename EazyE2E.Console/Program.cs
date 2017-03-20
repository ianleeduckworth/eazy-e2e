using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Windows.Input;
using EazyE2E.Element;
using EazyE2E.Enums;
using EazyE2E.HardwareManipulation;
using EazyE2E.Logwatch;
using EazyE2E.LongSearch;
using EazyE2E.Performance;
using EazyE2E.Process;

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            var calculatorPath = "C:\\Windows\\System32\\calc.exe";
            using (var process = new EzProcess(calculatorPath, "Calculator"))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                process.StartProcess();

                using (var logMonitor = new EzLogMonitor(process))
                {
                    logMonitor.StartSyncWatch("This is a test", 20, (type, text, message, occurance) =>
                    {
                        System.Console.WriteLine($"Watch text: '{text}' was hit after {occurance} seconds of profiling.  Log message => {type} : '{message}'");
                    }, (text, time) =>
                    {
                        System.Console.WriteLine($"Watch text: '{text}' was not hit after {time} seconds of profiling.");
                    });

//                    var watches = new List<string> {"This is a test", "This is also a test"};
//
//                    logMonitor.StartSyncWatch(watches, 10, (type, text, message, timeAtFailure) =>
//                    {
//                        System.Console.WriteLine($"Watch text: '{text}' was hit after {timeAtFailure} seconds of profiling.  Log message => {type} : '{message}'");
//                    }, (enumerable, time) =>
//                    {
//                        var watchList = enumerable.Aggregate((p, n) => p + ", " + n);
//                        System.Console.WriteLine($"Watches not found after {time} seconds of profiling.  Watch list => '{watchList}'");
//                    });
                }

                stopwatch.Stop();
                System.Console.WriteLine($"Test took {stopwatch.ElapsedMilliseconds} miliseconds");
                System.Console.ReadLine();
            }
        }
    }
}
