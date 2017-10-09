using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

                var logMonitor = new EzLogMonitor(process);
                logMonitor.SyncWatchForOccurance(@"Hello World!", 10, (watch, time) =>
                {
                    System.Console.WriteLine($"Message {watch} never occurred after {time} seconds of profiling.");
                }, (type, text, message, occurance) =>
                {
                    System.Console.WriteLine($"Message {text} was found at {occurance} seconds into profiling.  Type: {type}.  Full message: {message}");
                });

                stopwatch.Stop();
                System.Console.WriteLine($"Test took {stopwatch.ElapsedMilliseconds} miliseconds");
                System.Console.ReadLine();
            }
        }
    }
}
