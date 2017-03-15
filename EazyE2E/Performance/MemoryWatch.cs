using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EazyE2E.Process;

namespace EazyE2E.Performance
{
    public class MemoryWatch
    {
        /// <summary>
        /// Creates an instance of MemoryWatch.  This class is used to tell an instance of EzPerformanceMonitor which memory types to profile for
        /// </summary>
        /// <param name="type">The memory type to be profiled for</param>
        /// <param name="amount">The maximum memory threshold.  If they memory type's memory exceeds this threshold, it will constitute a failure</param>
        public MemoryWatch(MemoryType type, long amount)
        {
            this.Amount = amount;
            this.Type = type;
        }

        /// <summary>
        /// The memory type being profiled for
        /// </summary>
        public MemoryType Type { get; }

        /// <summary>
        /// The maximum memory threshold.  If they memory type's memory exceeds this threshold, it will constitute a failure
        /// </summary>
        public long Amount { get; }
    }
}
