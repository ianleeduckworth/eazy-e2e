﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using EazyE2E.Element;
using EazyE2E.Process;

namespace EazyE2E.Logwatch
{
    public class EzLogMonitor : IDisposable
    {
        private static HashSet<int> _processRegistry;

        private readonly System.Diagnostics.Process _process;

        private IList<string> _currentWatches;
        private readonly ConcurrentBag<FoundItem> _foundItems;

        private bool _isWatching;

        /// <summary>
        /// Creates an instance of EzLogMonitor based on an EzProcess
        /// </summary>
        /// <param name="process"></param>
        public EzLogMonitor(EzProcess process)
        {
            if (_processRegistry == null) _processRegistry = new HashSet<int>();
            if (_processRegistry.Count > 0)
            {
                if (_processRegistry.Contains(process.ProcessId))
                    throw new InvalidOperationException($"Process with Id {_process.Id} has already been added and cannot be added a second time.");
            }

            _processRegistry.Add(process.ProcessId);

            _process = process.Process;
            _foundItems = new ConcurrentBag<FoundItem>();
            _currentWatches = new List<string>();

            _process.OutputDataReceived += ProcessOnOutputDataReceived;
            _process.ErrorDataReceived += ProcessOnErrorDataReceived;

            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();
        }

        public delegate void IfFailSingular(OutputType type, string watchText, string message, int timeAtOccurance);
        public delegate void IfFailMultiple(IEnumerable<string> watches, int time);
        public delegate void IfFailNonOccurance(string watch, int time);
        public delegate void IfSuccessSingular(OutputType type, string watchText, string message, int timeAtOccurance);
        public delegate void IfSuccessMultiple(IEnumerable<string> watches, int time);
        public delegate void IfSuccessNonOccurance(string watch, int time);

        /// <summary>
        /// Begins a synchronized watch of process' error and standard output for log message.  Calls ifFail if message does not occur and calls ifSuccess if it does
        /// </summary>
        /// <param name="watches">List of strings to be watched for</param>
        /// <param name="timeInSeconds">Total time to listen to the logs</param>
        /// <param name="ifFail">Delegate to be performed in the event of a failure (log message does not occur)</param>
        /// <param name="ifSuccess">Delegate to be performed in the event of success (log message does occur)</param>
        public void SyncWatchForOccurance(IEnumerable<string> watches, int timeInSeconds, IfFailMultiple ifFail, IfSuccessSingular ifSuccess)
        {
            var watchesArray = watches as string[] ?? watches.ToArray();
            SetupWatch(watchesArray);
            var totalMiliseconds = timeInSeconds * 1000;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                if (_foundItems.IsEmpty) continue;

                var foundItem = _foundItems.FirstOrDefault();
                if (foundItem == null) throw new IndexOutOfRangeException("foundItem returned null");

                ifSuccess(foundItem.IsError ? OutputType.Error : OutputType.Standard, foundItem.Watch, foundItem.Message, (int)stopwatch.ElapsedMilliseconds / 1000);
                return;
            } while (stopwatch.ElapsedMilliseconds < totalMiliseconds);

            stopwatch.Stop();
            TeardownWatch();
            ifFail(watchesArray, timeInSeconds);
        }

        /// <summary>
        /// Begins a synchronized watch of process' error and standard output for log message.  Calls ifFail if message does not occur and calls ifSuccess if it does
        /// </summary>
        /// <param name="watchText">String to be watched for</param>
        /// <param name="timeInSeconds">Total time to listen to the logs</param>
        /// <param name="ifFail">Delegate to be performed in the event of a failure (log message does not occur)</param>
        /// <param name="ifSuccess">Delegate to be performed in the event of success (log message does occur)</param>
        public void SyncWatchForOccurance(string watchText, int timeInSeconds, IfFailNonOccurance ifFail, IfSuccessSingular ifSuccess)
        {
            SetupWatch(watchText);
            var totalMiliseconds = timeInSeconds * 1000;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                if (_foundItems.IsEmpty) continue;

                stopwatch.Stop();

                var foundItem = _foundItems.FirstOrDefault();
                if (foundItem == null) throw new IndexOutOfRangeException("foundItem returned null");

                ifSuccess(foundItem.IsError ? OutputType.Error : OutputType.Standard, foundItem.Watch, foundItem.Message, (int)stopwatch.ElapsedMilliseconds / 1000);
                return;
            } while (stopwatch.ElapsedMilliseconds < totalMiliseconds);

            stopwatch.Stop();
            TeardownWatch();
            ifFail(watchText, timeInSeconds);
        }

        /// <summary>
        /// Begins a synchronized watch of process' error and standard output for log message.  Calls ifFail if message does occur and calls ifSuccess if it does not
        /// </summary>
        /// <param name="watches">List of strings to be watched for</param>
        /// <param name="timeInSeconds">Total time to listen to the logs</param>
        /// <param name="ifFail">Delegate to be performed in the event of a failure (log message does occur)</param>
        /// <param name="ifSuccess">Delegate to be performed in the event of success (log message does not occur)</param>
        public void SyncWatchForNonOccurance(IEnumerable<string> watches, int timeInSeconds, IfFailSingular ifFail, IfSuccessMultiple ifSuccess)
        {
            var watchesArray = watches as string[] ?? watches.ToArray();
            SetupWatch(watchesArray);
            var totalMiliseconds = timeInSeconds * 1000;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                if (_foundItems.IsEmpty) continue;

                var foundItem = _foundItems.FirstOrDefault();
                if (foundItem == null) throw new IndexOutOfRangeException("foundItem returned null");

                ifFail(foundItem.IsError ? OutputType.Error : OutputType.Standard, foundItem.Watch, foundItem.Message, (int)stopwatch.ElapsedMilliseconds / 1000);

                return;
            } while (stopwatch.ElapsedMilliseconds < totalMiliseconds);

            stopwatch.Stop();
            TeardownWatch();
            ifSuccess(watchesArray, timeInSeconds);
        }

        /// <summary>
        /// Begins a synchronized watch of process' error and standard output for log message.  Calls ifFail if message does occur and calls ifSuccess if it does not
        /// </summary>
        /// <param name="watchText">String to be watched for</param>
        /// <param name="timeInSeconds">Total time to listen to the logs</param>
        /// <param name="ifFail">Delegate to be performed in the event of a failure (log message does occur)</param>
        /// <param name="ifSuccess">Delegate to be performed in the event of success (log message does not occur)</param>
        public void SyncWatchForNonOccurance(string watchText, int timeInSeconds, IfFailSingular ifFail, IfSuccessNonOccurance ifSuccess)
        {
            SetupWatch(watchText);
            var totalMiliseconds = timeInSeconds * 1000;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                if (_foundItems.IsEmpty) continue;

                var foundItem = _foundItems.FirstOrDefault();
                if (foundItem == null) throw new IndexOutOfRangeException("foundItem returned null");

                ifFail(foundItem.IsError ? OutputType.Error : OutputType.Standard, foundItem.Watch, foundItem.Message, (int)stopwatch.ElapsedMilliseconds / 1000);
                return;
            } while (stopwatch.ElapsedMilliseconds < totalMiliseconds);

            stopwatch.Stop();
            TeardownWatch();
            ifSuccess(watchText, timeInSeconds);
        }

        private void SetupWatch(IEnumerable<string> msgs)
        {
            foreach (var msg in msgs)
            {
                _currentWatches.Add(msg);
            }

            _isWatching = true;
        }

        private void SetupWatch(string msg)
        {
            _currentWatches.Add(msg);
            _isWatching = true;
        }

        private void TeardownWatch()
        {
            _currentWatches = null;
            _isWatching = false;
            _process.CancelOutputRead();
            _process.CancelErrorRead();
        }

        private void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if (!_isWatching) return;
            DoWork(true, dataReceivedEventArgs);
        }

        private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if (!_isWatching) return;
            DoWork(false, dataReceivedEventArgs);
        }

        private void DoWork(bool isError, DataReceivedEventArgs eventArgs)
        {
            foreach (var watch in _currentWatches.Where(watch => eventArgs.Data.Contains(watch)))
            {
                _foundItems.Add(new FoundItem(isError, eventArgs.Data, watch));
            }
        }

        public void Dispose()
        {
            _isWatching = false;
            _process.OutputDataReceived -= ProcessOnOutputDataReceived;
            _process.ErrorDataReceived -= ProcessOnErrorDataReceived;
            _processRegistry.Remove(_process.Id);
        }

        public class FoundItem
        {
            public FoundItem(bool isError, string message, string watch)
            {
                this.IsError = isError;
                this.Message = message;
                this.Watch = watch;
            }

            public string Watch { get; }
            public bool IsError { get; }
            public string Message { get; }
        }
    }
}
