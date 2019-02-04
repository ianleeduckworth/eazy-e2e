//Copyright 2019 Ian Duckworth

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EazyE2E.Configuration;
using EazyE2E.Enums;
using EazyE2E.Process;

namespace EazyE2E.Performance
{
	/// <summary>
	/// Allows for an application to be profiled and monitored based on its memory metrics
	/// </summary>
    public class EzPerformanceMonitor
    {
        private readonly EzProcess _process;

		/// <summary>
		/// Creates a new instance of EzPerformanceMonitor based on EzProcess
		/// </summary>
		/// <param name="process"></param>
        public EzPerformanceMonitor(EzProcess process)
        {
            _process = process;
        }

		/// <summary>
		/// Based on System.Process NonpagedSystemMemorySize64
		/// </summary>
        public long NonpagedSystemMemorySize
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.NonpagedSystemMemorySize64;
            }
        }

		/// <summary>
		/// Based on System.Process PagedMemorySize64
		/// </summary>
		public long PagedMemorySize
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.PagedMemorySize64;
            }
        }

		/// <summary>
		/// Based on System.Process PagedSystemMemorySize64
		/// </summary>
		public long PagedSystemMemorySize
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.PagedSystemMemorySize64;
            }
        }

		/// <summary>
		/// Based on System.Process PeakPagedMemorySize64
		/// </summary>
		public long PeakPagedMemorySize
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.PeakPagedMemorySize64;
            }  
        }

		/// <summary>
		/// Based on System.Process PeakVirtualMemorySize64
		/// </summary>
		public long PeakVirtualMemorySize
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.PeakVirtualMemorySize64;
            }  
        }

		/// <summary>
		/// Based on System.Process PeakWorkingSet64
		/// </summary>
		public long PeakWorkingSet
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.PeakWorkingSet64;
            }  
        }

		/// <summary>
		/// Based on System.Process PrivateMemorySize64
		/// </summary>
		public long PrivateMemorySize
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.PrivateMemorySize64;
            }
        }

		/// <summary>
		/// Based on System.Process VirtualMemorySize64
		/// </summary>
		public long VirtualMemorySize
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.VirtualMemorySize64;
            }
        }

		/// <summary>
		/// Based on System.Process WorkingSet64
		/// </summary>
		public long WorkingSet
        {
            get
            {
                _process.Process.Refresh();
                return _process.Process.WorkingSet64;
            }
        } 

        /// <summary>
        /// Begins a synchronous watch of a particular memory metric for a particular amount of time.  
        /// If the memory metric selected goes above the amount passed in, the ifFail method will be called.  
        /// If, after the time has passed, the memory has not exceeded the threshhold, the ifSuccess method will be called
        /// </summary>
        /// <param name="type">The particular memory metric to be profiled</param>
        /// <param name="timeInSeconds">The amount of time IN SECONDS (not miliseconds) to profile for</param>
        /// <param name="amount">The threshold of failure; if the memory metric goes higher than this amount, the ifFail method will be called</param>
        /// <param name="ifFail">Action to do if a failure occurs.  Returns the memory type being profiled for, the original failure threshold, and the amount that caused the failure</param>
        /// <param name="ifSuccess">Action to do if the profile succeeds.  Returns the original memory type being profiled and the failure threshold</param>
        public void StartSyncWatch(MemoryType type, long amount, int timeInSeconds, IfFail ifFail, SingularSuccess ifSuccess)
        {
            if (timeInSeconds > Config.MaximumMemoryProfileTime) timeInSeconds = Config.MaximumMemoryProfileTime;

            var timeInMiliseconds = timeInSeconds*1000;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                if (CheckMemory(type, amount))
                {
                    ifFail(type, amount, GetMemoryFromType(type), (int)stopwatch.ElapsedMilliseconds / 1000);
                    return;
                }
            } while (stopwatch.ElapsedMilliseconds < timeInMiliseconds);
            stopwatch.Stop();

            ifSuccess(type, amount, timeInSeconds);
        }

        /// <summary>
        /// Begins a synchronous watch of a particular memory metric for a particular amount of time.  
        /// If any of the memory metrics selected go above the amount passed in, the ifFail method will be called.  
        /// If, after the time has passed, all of the memory metrics have not exceeded the threshhold, the ifSuccess method will be called
        /// </summary>
        /// <param name="timeInSeconds">The amount of time IN SECONDS (not miliseconds) to profile for</param>
        /// <param name="ifFail">Action to do if a failure occurs.  Returns the memory type being profiled for, the original failure threshold, and the amount that caused the failure</param>
        /// <param name="ifSuccess">Action to do if the profile succeeds.  Returns the original array of MemoryWatches being profiled for.  Each MemoryWatch contains a MemoryType and a failure threshold</param>
        /// <param name="watches">A list of items to watch.  Each item is composed of a memory type an an amount to watch for</param>
        public void StartSyncWatch(IEnumerable<MemoryWatch> watches, int timeInSeconds, IfFail ifFail, MultiSuccess ifSuccess)
        {
            if (timeInSeconds > Config.MaximumMemoryProfileTime) timeInSeconds = Config.MaximumMemoryProfileTime;

            var timeInMiliseconds = timeInSeconds * 1000;
            var stopwatch = new Stopwatch();
            var memoryWatches = watches as MemoryWatch[] ?? watches.ToArray();
            stopwatch.Start();

            do
            {
                var failure = memoryWatches.FirstOrDefault(x => CheckMemory(x.Type, x.Amount));
                if (failure != null)
                {
                    ifFail(failure.Type, failure.Amount, GetMemoryFromType(failure.Type), (int)stopwatch.ElapsedMilliseconds / 1000);
                    return;
                }
            } while (stopwatch.ElapsedMilliseconds < timeInMiliseconds);
            stopwatch.Stop();

            ifSuccess(memoryWatches, timeInSeconds);
        }

        /// <summary>
		/// Delegate to be run if a failure occurs
		/// </summary>
		/// <param name="type"></param>
		/// <param name="original"></param>
		/// <param name="actual"></param>
		/// <param name="timeAtFailure"></param>
        public delegate void IfFail(MemoryType type, long original, long actual, int timeAtFailure);

		/// <summary>
		/// Delegate to run if a singular success occurs
		/// </summary>
		/// <param name="type"></param>
		/// <param name="original"></param>
		/// <param name="time"></param>
        public delegate void SingularSuccess(MemoryType type, long original, int time);

		/// <summary>
		/// Delegate to run if there are multiple successes
		/// </summary>
		/// <param name="watches"></param>
		/// <param name="time"></param>
        public delegate void MultiSuccess(IEnumerable<MemoryWatch> watches, int time);

        // Note that for this method, true = a failure,  false = a success for the memory type checked.
        private bool CheckMemory(MemoryType type, long amount)
        {
            switch (type)
            {
                case MemoryType.NonpagedSystemMemorySize:
                    if (this.NonpagedSystemMemorySize > amount) return true;
                    break;
                case MemoryType.PagedMemorySize:
                    if (this.PagedMemorySize > amount) return true;
                    break;
                case MemoryType.PagedSystemMemorySize:
                    if (this.PagedSystemMemorySize > amount) return true;
                    break;
                case MemoryType.PeakPagedMemorySize:
                    if (this.PeakPagedMemorySize > amount) return true;
                    break;
                case MemoryType.PeakVirtualMemorySize:
                    if (this.PeakVirtualMemorySize > amount) return true;
                    break;
                case MemoryType.PeakWorkingSet:
                    if (this.PeakWorkingSet > amount) return true;
                    break;
                case MemoryType.PrivateMemorySize:
                    if (this.PrivateMemorySize > amount) return true;
                    break;
                case MemoryType.VirtualMemorySize:
                    if (this.VirtualMemorySize > amount) return true;
                    break;
                case MemoryType.WorkingSet:
                    if (this.WorkingSet > amount) return true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return false;
        }

        private long GetMemoryFromType(MemoryType type)
        {
            switch (type)
            {
                case MemoryType.NonpagedSystemMemorySize:
                    return this.NonpagedSystemMemorySize;
                case MemoryType.PagedMemorySize:
                    return this.PagedMemorySize;
                case MemoryType.PagedSystemMemorySize:
                    return this.PagedSystemMemorySize;
                case MemoryType.PeakPagedMemorySize:
                    return this.PeakPagedMemorySize;
                case MemoryType.PeakVirtualMemorySize:
                    return this.PeakVirtualMemorySize;
                case MemoryType.PeakWorkingSet:
                    return this.PeakWorkingSet;
                case MemoryType.PrivateMemorySize:
                    return this.PrivateMemorySize;
                case MemoryType.VirtualMemorySize:
                    return this.VirtualMemorySize;
                case MemoryType.WorkingSet:
                    return this.WorkingSet;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
