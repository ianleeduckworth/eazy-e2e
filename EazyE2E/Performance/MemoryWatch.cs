﻿//Copyright 2019 Ian Duckworth

using EazyE2E.Enums;

namespace EazyE2E.Performance
{
	/// <summary>
	/// Houses memory watch information that will set EzPerformanceMonitor know what to search and what the memory amount should be
	/// </summary>
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
