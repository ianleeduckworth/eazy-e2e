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

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            const string appPath = "C:\\Code\\EazyE2E\\TestApplication\\bin\\Debug\\TestApplication.exe";
            using (var process = new EzProcess(appPath, "TestApplication"))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                process.StartProcess();

                var logMonitor = new EzLogMonitor(process, new MyComparer());

                logMonitor.SyncWatchForOccurance("hello", 10, (watch, time) =>
                {
                    System.Console.WriteLine($"Did not find text {watch}");
                }, (type, text, message, occurance) =>
                {
                    System.Console.WriteLine($"Message occurred!  Watch text: {text}.  Message: {message}.  Time since watch began: {occurance}.  Type: {type}");
                });

                System.Console.WriteLine("Finished watching.");

                //pause
                System.Console.ReadLine();
            }
        }
    }

    public class MyComparer : ILogMessageComparer
    {
        public bool Compare(string watchText, string logMessage)
        {
            const StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase;
            return logMessage.IndexOf(watchText, stringComparison) >= 0;
        }
    }
}
