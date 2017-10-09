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
            var calculatorPath = "C:\\Program Files (x86)\\Notepad++\\notepad++.exe";
            using (var process = new EzProcess(calculatorPath, "notepad++"))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                process.StartProcess();
                Thread.Sleep(2000);

                var perfMon = new EzPerformanceMonitor(process);

                var memoryWatches = new List<MemoryWatch>
                {
                    new MemoryWatch(MemoryType.PagedMemorySize, 30000000),
                    new MemoryWatch(MemoryType.NonpagedSystemMemorySize, 30000000)
                };

                perfMon.StartSyncWatch(memoryWatches, 10, (type, original, actual, timeAtFailure) =>
                {
                    System.Console.WriteLine($"Test failed.  {type} exceeded the threshold of {original}.  Measured value was {actual} when failure occured at {timeAtFailure} seconds into profile.");
                }, (watches, time) =>
                {
                    var stringifiedTypes = watches.Select(x => x.Type.ToString()).Aggregate((w, n) => w + ", " + n);
                    System.Console.WriteLine($"Test passed.  Memory metrics {stringifiedTypes} were below their thresholds during the entire {time} second profile.");
                });

                stopwatch.Stop();
                System.Console.WriteLine($"Test took {stopwatch.ElapsedMilliseconds} miliseconds");
                System.Console.ReadLine();
            }
        }
    }
}
