using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace EazyE2E.Configuration
{
    /// <summary>
    /// Houses all configuration data used through the framework
    /// </summary>
    public static class Config
    {
        private static int? _doubleClickGap;
        private static int? _findElementTimeout;
        private static int? _processWaitForExitTimeout;
        private static ProcessWindowStyle? _defaultWindowStyle;
        private static int? _maximumMemoryProfileTime;
        private static int? _timeBetweenMouseEvents;
        private static int? _timeBetweenKeyboardEvents;
        private static bool? _allowSearchingForDescendants;
        private static bool? _exposeBackingWindowsPatterns;
        private static bool? _alwaysResetEzText;
        private static bool? _terminateExistingInstance;

        /// <summary>
        /// Tells the framework how long to pause inbetween a double click
        /// Default is 500.
        /// </summary>
        public static int DoubleClickGap
        {
            get
            {
                if (_doubleClickGap == null)
                {
                    var val = GetConfigFileValue("DoubleClickGap");
                    int returnVal;
                    _doubleClickGap = int.TryParse(val, out returnVal) ? returnVal : 500;
                }

                return _doubleClickGap.Value;
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
                if (_findElementTimeout == null)
                {
                    var val = GetConfigFileValue("FindElementTimeout");
                    int returnVal;
                    _findElementTimeout = int.TryParse(val, out returnVal) ? returnVal : 10000;
                }

                return _findElementTimeout.Value;
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
                if (_processWaitForExitTimeout == null)
                {
                    var val = GetConfigFileValue("ProcessWaitForExitTimeout");
                    int returnVal;
                    _processWaitForExitTimeout = int.TryParse(val, out returnVal) ? returnVal : 1000;
                }

                return _processWaitForExitTimeout.Value;
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
                if (_defaultWindowStyle == null)
                {
                    var val = GetConfigFileValue("DefaultWindowStyle");
                    ProcessWindowStyle returnVal;
                    _defaultWindowStyle = Enum.TryParse(val, out returnVal) ? returnVal : ProcessWindowStyle.Normal;
                }

                return _defaultWindowStyle.Value;
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
                if (_maximumMemoryProfileTime == null)
                {
                    var val = GetConfigFileValue("MaximumMemoryProfileTime");
                    int returnVal;
                    _maximumMemoryProfileTime = int.TryParse(val, out returnVal) ? returnVal : int.MaxValue;
                }

                return _maximumMemoryProfileTime.Value;
            }
        }

        /// <summary>
        /// The time in milliseconds between mouse events.
        /// Default is 100
        /// </summary>
        public static int TimeBetweenMouseEvents
        {
            get
            {
                if (_timeBetweenMouseEvents == null)
                {
                    var val = GetConfigFileValue("TimeBetweenMouseEvents");
                    int returnVal;
                    _timeBetweenMouseEvents = int.TryParse(val, out returnVal) ? returnVal : 100;
                }

                return _timeBetweenMouseEvents.Value;
            }
        }

        /// <summary>
        /// The time in milliseconds between keyboard events.
        /// Default is 100
        /// </summary>
        public static int TimeBetweenKeyboardEvents
        {
            get
            {
                if (_timeBetweenKeyboardEvents == null)
                {
                    var val = GetConfigFileValue("TimeBetweenKeyboardEvents");
                    int returnVal;
                    _timeBetweenKeyboardEvents = int.TryParse(val, out returnVal) ? returnVal : 100;
                }

                return _timeBetweenKeyboardEvents.Value;
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
                if (_allowSearchingForDescendants == null)
                {
                    var val = GetConfigFileValue("AllowSearchingForDescendants");
                    bool returnVal;
                    _allowSearchingForDescendants = !bool.TryParse(val, out returnVal) || returnVal;
                }

                return _allowSearchingForDescendants.Value;
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
                if (_exposeBackingWindowsPatterns == null)
                {
                    var val = GetConfigFileValue("ExposeBackingWindowsPatterns");
                    bool returnVal;
                    _exposeBackingWindowsPatterns = bool.TryParse(val, out returnVal) && returnVal;
                }

                return _exposeBackingWindowsPatterns.Value;
            }
        }

        /// <summary>
        /// Tells the framework whether or not to allow caching for the EzText element; this is the only element that caches
        /// Default is false.
        /// </summary>
        public static bool AlwaysResetEzText
        {
            get
            {
                if (_alwaysResetEzText == null)
                {
                    var val = GetConfigFileValue("AlwaysResetEzText");
                    bool returnVal;
                    _alwaysResetEzText = bool.TryParse(val, out returnVal) && returnVal;
                }

                return _alwaysResetEzText.Value;
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
                if (_terminateExistingInstance == null)
                {
                    var val = GetConfigFileValue("TerminateExistingInstance");
                    bool returnVal;
                    _terminateExistingInstance = bool.TryParse(val, out returnVal) && returnVal;
                }

                return _terminateExistingInstance.Value;
            }
        }

        private static string GetConfigFileValue(string name)
        {
            var keyValues = ConfigurationManager.AppSettings;
            var value = keyValues.Get(name);
            return value;
        }
    }
}
