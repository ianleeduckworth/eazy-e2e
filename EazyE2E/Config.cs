using System.Diagnostics;

namespace EazyE2E
{
    public class Config
    {
        /// <summary>
        /// TODO: Add summary here.
        /// Default is 500.
        /// </summary>
        public int DoubleClickGap { get; set; } = 500;

        /// <summary>
        /// Number of milliseconds to look for an element.
        /// After the timeout, the search will be aborted.
        /// Default is 1000.
        /// </summary>
        public int FindElementTimeout { get; set; } = 1000;

        /// <summary>
        /// TODO: Add summary here.
        /// Default is 1000.
        /// </summary>
        public int ProcessWaitForExitTimeout { get; set; } = 1000;

        /// <summary>
        /// TODO: Add summary here.
        /// Default is Normal.
        /// </summary>
        public ProcessWindowStyle DefaultWindowStyle { get; set; } = ProcessWindowStyle.Normal;


        /// <summary>
        /// TODO: Add summary here.
        /// TODO: Add default.
        /// TODO: Set correct type.
        /// </summary>
        public int MaximumMemoryProfileTime { get; set; }

        /// <summary>
        /// TODO: The time in milliseconds between mouse events.
        /// TODO: Add default.
        public int TimeBetweenMouseEvents { get; set; }

        /// <summary>
        /// TODO: Add summary here.
        /// Default is true.
        /// </summary>
        public bool AllowSearchingForDescendants { get; set; } = true;

        /// <summary>
        /// TODO: Add summary here.
        /// Default is false.
        /// </summary>
        public bool ExposeBackingWindowsPatterns { get; set; } = false;

        /// <summary>
        /// TODO: Add summary here.
        /// Default is true.
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
