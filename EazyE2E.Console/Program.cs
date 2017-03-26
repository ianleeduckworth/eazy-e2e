using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EazyE2E.Enums;
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
