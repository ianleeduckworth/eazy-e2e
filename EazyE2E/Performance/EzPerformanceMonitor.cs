using System;
using System.Threading;
using EazyE2E.Process;

namespace EazyE2E.Performance
{
    public class EzPerformanceMonitor
    {
        private readonly EzProcess _process;

        public EzPerformanceMonitor(EzProcess process)
        {
            _process = process;
        }

        public long NonpagedSystemMemorySize => _process.Process.NonpagedSystemMemorySize64;
        public long PagedMemorySize => _process.Process.PagedMemorySize64;
        public long PagedSystemMemorySize => _process.Process.PagedSystemMemorySize64;
        public long PeakPagedMemorySize => _process.Process.PeakPagedMemorySize64;
        public long PeakVirtualMemorySize => _process.Process.PeakVirtualMemorySize64;
        public long PeakWorkingSet => _process.Process.PeakWorkingSet64;
        public long PrivateMemorySize => _process.Process.PrivateMemorySize64;
        public long VirtualMemorySize => _process.Process.VirtualMemorySize64;
        public long WorkingSet => _process.Process.WorkingSet64;

        /// <summary>
        /// Begins a synchronous watch of a particular memory metric for a particular amount of time.  
        /// If the memory metric selected goes above the amount passed in, the ifFail method will be called.  
        /// If, after the time has passed, the memory has not exceeded the threshhold, the ifSuccess method will be called
        /// </summary>
        /// <param name="type">The particular memory metric to be profiled</param>
        /// <param name="timeInSeconds">The amount of time IN SECONDS (not miliseconds) to profile for</param>
        /// <param name="amount">The threshold of failure; if the memory metric goes higher than this amount, the ifFail method will be called</param>
        /// <param name="ifFail">Action to do if a failure occurs.  Returns the memory type being profiled for, the original failure threshold, and the amount that caused the failure</param>
        /// <param name="ifSuccess">Action to do if the profile succeeds.  Returns the memory type being profiled for and the original failure threshold</param>
        public void StartSyncWatch(MemoryType type, int timeInSeconds, long amount, Action<MemoryType, long, long> ifFail, Action<MemoryType, long> ifSuccess)
        {
            for (var i = 0; i < timeInSeconds; i++)
            {
                if (CheckMemory(type, amount))
                {
                    ifFail(type, amount, GetMemoryFromType(type));
                    return;
                }

                Thread.Sleep(1000); //todo I don't really like Thread.sleep...
            }

            ifSuccess(type, amount);
        }

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
