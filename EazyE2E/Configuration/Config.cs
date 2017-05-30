using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;

namespace EazyE2E.Configuration
{
    public static class Config
    {
        /// <summary>
        /// Tells the framework how long to pause inbetween a double click
        /// Default is 500.
        /// </summary>
        public static int DoubleClickGap
        {
            get
            {
                var val = GetConfigFileValue("DoubleClickGap");
                int returnVal;
                return int.TryParse(val, out returnVal) ? returnVal : 500;
            }
        }

        /// <summary>
        /// Number of milliseconds to look for an element.
        /// After the timeout, the search will be aborted and null will be returned.
        /// Default is 10000.
        /// </summary>
        public static int FindElementTimeout //TODO NOT SURE HOW I CAN USE THIS; not sure if it's even possible to set a timeout 
        {
            get
            {
                var val = GetConfigFileValue("FindElementTimeout");
                int returnVal;
                return int.TryParse(val, out returnVal) ? returnVal : 10000;
            }
        }

        /// <summary>
        /// Tells the framework how long to wait for the process to exit before moving on
        /// Default is 1000.
        /// </summary>
        public static int ProcessWaitForExitTimeout
        {
            get
            {
                var val = GetConfigFileValue("ProcessWaitForExitTimeout");
                int returnVal;
                return int.TryParse(val, out returnVal) ? returnVal : 1000;
            }
        }

        /// <summary>
        /// Tells the framework what the default window style of the application should be
        /// Default is Normal.
        /// </summary>
        public static ProcessWindowStyle DefaultWindowStyle
        {
            get
            {
                var val = GetConfigFileValue("DefaultWindowStyle");
                ProcessWindowStyle returnVal;
                return Enum.TryParse(val, out returnVal) ? returnVal : ProcessWindowStyle.Normal;
            }
        }


        /// <summary>
        /// Tells the framework the maximum amount of time IN SECONDS to allow a test to profile memory for
        /// Default is Int.MaxValue which means that it will, by default, allow for profiling as long as the test desires
        /// </summary>
        public static int MaximumMemoryProfileTime
        {
            get
            {
                var val = GetConfigFileValue("MaximumMemoryProfileTime");
                int returnVal;
                return int.TryParse(val, out returnVal) ? returnVal : int.MaxValue;
            }
        }

        /// <summary>
        /// The time in milliseconds between mouse events.
        /// TDefault is 100
        public static int TimeBetweenMouseEvents
        {
            get
            {
                var val = GetConfigFileValue("TimeBetweenMouseEvents");
                int returnVal;
                return int.TryParse(val, out returnVal) ? returnVal : 100;
            }
        }

        /// <summary>
        /// Tells the framework whether or not it should allow for searching by descendants.  Searching by descendants is generally lazy and inneficient; allows a project administrator to restrict ability to do this
        /// Default is true.
        /// </summary>
        public static bool AllowSearchingForDescendants
        {
            get
            {
                var val = GetConfigFileValue("AllowSearchingForDescendants");
                bool returnVal;
                return bool.TryParse(val, out returnVal) ? returnVal : true;
            }
        }

        /// <summary>
        /// Tells the framework whether or not to expose backing patterns for the developers to use
        /// Default is false.
        /// </summary>
        public static bool ExposeBackingWindowsPatterns
        {
            get
            {
                var val = GetConfigFileValue("ExposeBackingWindowsPatterns");
                bool returnVal;
                return bool.TryParse(val, out returnVal) ? returnVal : false;
            }
        }

        /// <summary>
        /// TTells the framework whether or not to allow caching for the EzText element; this is the only element that caches
        /// Default is false.
        /// </summary>
        public static bool AlwaysResetEzText
        {
            get
            {
                var val = GetConfigFileValue("AlwaysResetEzText");
                bool returnVal;
                return bool.TryParse(val, out returnVal) ? returnVal : false;
            }
        }

        /// <summary>
        /// Determines whether or not EazyE2E should kill all existing
        /// instances of an application if it is running when EazyE2E
        /// starts.
        /// Default is false.
        /// </summary>
        public static bool TerminateExistingInstance
        {
            get
            {
                var val = GetConfigFileValue("TerminateExistingInstance");
                bool returnVal;
                return bool.TryParse(val, out returnVal) ? returnVal : false;
            }
        }

        private const string SECTION_NAME = "eazye2eSettings";

        private static string GetConfigFileValue(string name)
        {
            //var keyValues = (NameValueCollection) ConfigurationManager.GetSection(SECTION_NAME);
            //var value = keyValues.Get(name);
            //return value;
            return null;
        }
    }
}
