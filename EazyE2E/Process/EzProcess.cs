using System;
using System.Diagnostics;

namespace EazyE2E.Process
{
    public class EzProcess
    {
        private readonly string _processName;
        private readonly string _arguments;
        private readonly ProcessWindowStyle _winStyle;

        public EzProcess(string processName, string arguments = "", ProcessWindowStyle winStyle = ProcessWindowStyle.Normal)
        {
            _processName = processName;
            _arguments = arguments;
            _winStyle = winStyle;
        }

        public int StartProcess()
        {
            var start = new ProcessStartInfo
            {
                Arguments = _arguments,
                FileName = _processName,
                WindowStyle = _winStyle
            };
            int exitCode;

            using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(start))
            {
                if (proc == null) throw new NullReferenceException($"Could not find process where ProcessName = {_processName}, and Arguments = {_arguments}");
                proc.Start(); //todo not sure if this is the right method call.  Want to go to the next line once the process is fully launched and ready to begin testing
                exitCode = proc.ExitCode;
            }

            return exitCode;
        }

        public string ProcessName => _processName;
        public string Arguments => _arguments;
    }
}
