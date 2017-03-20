using System;
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
        private ConcurrentBag<FoundItem> _foundItems;

        private bool _isWatching;

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
        }

        public delegate void IfFail(OutputType type, string watchText, string message, int timeAtOccurance);
        public delegate void IfSuccessSingular(string watchText, int time);
        public delegate void IfSuccessMultiple(IEnumerable<string> watches, int time);


        public void StartSyncWatch(IEnumerable<string> watches, int timeInSeconds, IfFail ifFail, IfSuccessMultiple ifSuccess)
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

        public void StartSyncWatch(string watchText, int timeInSeconds, IfFail ifFail, IfSuccessSingular ifSuccess)
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
        }

        private void TeardownWatch()
        {
            _currentWatches = null;
            _isWatching = false;
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
