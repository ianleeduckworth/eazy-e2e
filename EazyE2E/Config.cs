using System.Diagnostics;

namespace EazyE2E
{
    public class Config
    {
        /// <summary>
        /// Tells the framework how long to pause inbetween a double click
        /// Default is 500.
        /// </summary>
        public int DoubleClickGap { get; set; } = 500;

        /// <summary>
        /// Number of milliseconds to look for an element.
        /// After the timeout, the search will be aborted.
        /// Default is 1000.
        /// </summary>
        public int FindElementTimeout { get; set; } = 10000;

        /// <summary>
        /// Tells the framework how long to wait for the process to exit before moving on
        /// Default is 1000.
        /// </summary>
        public int ProcessWaitForExitTimeout { get; set; } = 1000;

        /// <summary>
        /// Tells the framework what the default window style of the application should be
        /// Default is Normal.
        /// </summary>
        public ProcessWindowStyle DefaultWindowStyle { get; set; } = ProcessWindowStyle.Normal;


        /// <summary>
        /// Tells the framework the maximum amount of time IN SECONDS to allow a test to profile memory for
        /// Default is Int.MaxValue which means that it will, by default, allow for profiling as long as the test desires
        /// </summary>
        public int MaximumMemoryProfileTime { get; set; } = int.MaxValue;

        /// <summary>
        /// The time in milliseconds between mouse events.
        /// TDefault is 100
        public int TimeBetweenMouseEvents { get; set; } = 100;

        /// <summary>
        /// Tells the framework whether or not it should allow for searching by descendants.  Searching by descendants is generally lazy and inneficient; allows a project administrator to restrict ability to do this
        /// Default is true.
        /// </summary>
        public bool AllowSearchingForDescendants { get; set; } = true;

        /// <summary>
        /// Tells the framework whether or not to expose backing patterns for the developers to use
        /// Default is false.
        /// </summary>
        public bool ExposeBackingWindowsPatterns { get; set; } = false;

        /// <summary>
        /// TTells the framework whether or not to allow caching for the EzText element; this is the only element that caches
        /// Default is false.
        /// </summary>
        public bool AlwaysResetEzText { get; set; } = false;

        /// <summary>
        /// Determines whether or not EazyE2E should kill all existing
        /// instances of an application if it is running when EazyE2E
        /// starts.
        /// Default is false.
        /// </summary>
        public bool TerminateExistingInstance { get; set; } = false;

        public static Config GetDefaultConfiguration()
        {
            // Simply return an instantiated config
            // object since the defaults are set
            // using auto-initialized properties.
            return new Config();
        }
    }
}
