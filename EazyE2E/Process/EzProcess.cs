using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace EazyE2E.Process
{
    public class EzProcess : IDisposable
    {
        private readonly string _processFullPath;
        private readonly string _processName;
        private System.Diagnostics.Process _process;

        public int ProcessId => _process.Id;
        public string Arguments { get; set; } = string.Empty;
        public ProcessWindowStyle WindowStyle { get; set; } = ProcessWindowStyle.Normal;

        public EzProcess(string processFullPath, string processName)
        {
            _processFullPath = processFullPath;
            _processName = processName;
        }


        /// <summary>
        /// Starts the process based on the path given when instantiating EzProcess
        /// </summary>
        public void StartProcess()
        {
            var start = new ProcessStartInfo
            {
                FileName = _processFullPath,
                WindowStyle = WindowStyle,
                Arguments = Arguments
            };

            _process = System.Diagnostics.Process.Start(start);

            FindRunningProcess();
        }

        /// <summary>
        /// If a process is already started when calling Process.Start,
        /// and the process looks for an existing process, sometimes
        /// it will forward the request to the existing process and then
        /// exit.  This method handles that scenario and gets a reference
        /// to the correct process.
        /// </summary>
        private void FindRunningProcess()
        {
            // Give the process 1 second to start or forward the start request.
            _process.WaitForExit(1000);

            // If it hasn't exited, we've probably
            // got the right process.
            if (!_process.HasExited) return;

            // If it has exited, find the currently running process.
            var process = System.Diagnostics.Process.GetProcessesByName(_processName);
            if (process.Length > 1) throw new InvalidOperationException($"There are more than one process with the name {_processName}.  Please close all running processes with that name.");
            if (process.Length == 0) throw new InvalidOperationException($"A process with the name {_processName} could not be found.");
            _process = process.Single();
        }

        public void Dispose()
        {
            _process.Kill();
            _process.Dispose();
        }
    }
}
