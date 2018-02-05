//Copyright 2018 Ian Duckworth

using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using EazyE2E.Configuration;

namespace EazyE2E.Process
{
    /// <summary>
    /// Houses all process related information and functionality.  Can be used to start a process or to query information about the currently running process
    /// </summary>
    public class EzProcess : IDisposable
    {
        private readonly string _processFullPath;
        private readonly string _processName;
        private System.Diagnostics.Process _process;
        private string _arguments;

        /// <summary>
        /// Underlying System.Diagnostics.Process instance
        /// </summary>
        public System.Diagnostics.Process Process => _process;

        /// <summary>
        /// The underlying processId of the application as it would appear in task manager, for example
        /// </summary>
        public int ProcessId => _process.Id;

        /// <summary>
        /// 	The path within the operating's folder structure to the exceutable that was launched to spawn this process
        /// </summary>
        public string ProcessPath => _processFullPath;

        /// <summary>
        /// The name of the current process
        /// </summary>
        public string ProcessName => _processName;

        /// <summary>
        /// Gets the arguments that were used to launch the current process
        /// </summary>
        public string Arguments => _arguments ?? (_arguments = GetArguments());

        /// <summary>
        /// Gets the current style of the window, such as whether it's minimized or maximized
        /// </summary>
        public ProcessWindowStyle WindowStyle => Config.DefaultWindowStyle;

        /// <summary>
        /// Creates an EzProcess based on path and name
        /// </summary>
        /// <param name="processFullPath"></param>
        /// <param name="processName"></param>
        public EzProcess(string processFullPath, string processName)
        {
            _processFullPath = processFullPath;
            _processName = processName;
        }

        /// <summary>
        /// Creates an EzProcess instance based on the System.Diagnostics.Process instance passed in
        /// </summary>
        /// <param name="process"></param>
        public EzProcess(System.Diagnostics.Process process)
        {
            _process = process;
            _processName = process.ProcessName;
            _processFullPath = process.MainModule.FileName;
        }

        /// <summary>
        /// Starts the process based on the path given when instantiating EzProcess
        /// </summary>
        public void StartProcess(string arguments = null)
        {
            if (_arguments == null) _arguments = arguments;

            TerminateExistingInstances();

            var start = new ProcessStartInfo
            {
                FileName = _processFullPath,
                WindowStyle = WindowStyle,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };

            _process = System.Diagnostics.Process.Start(start);

            FindRunningProcess();
        }

        /// <summary>
        /// Attaches to an existing process based on the process name passed in
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static EzProcess AttachToExistingProcess(string processName)
        {
            return new EzProcess(GetProcess(processName));
        }

        private string GetArguments()
        {
            var commandLine = new StringBuilder(_process.MainModule.FileName);
            commandLine.Append(" ");
            using (var searcher = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {_process.Id}"))
            {
                foreach (var result in searcher.Get())
                {
                    commandLine.Append(result["CommandLine"]);
                    commandLine.Append(" ");
                }
            }

            return commandLine.ToString();
        }

        private void TerminateExistingInstances()
        {
            if (!Config.TerminateExistingInstance) return;
            var processes = System.Diagnostics.Process.GetProcessesByName(_processName).ToList();
            processes.ForEach(p => p.Kill());
        }

        private static System.Diagnostics.Process GetProcess(string processName)
        {
            var process = System.Diagnostics.Process.GetProcessesByName(processName);
            if (process.Length > 1) throw new InvalidOperationException($"There are more than one process with the name {processName}.  Please close all running processes with that name.");
            if (process.Length == 0) throw new InvalidOperationException($"A process with the name {processName} could not be found.");
            return process.Single();
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
            _process.WaitForExit(Config.ProcessWaitForExitTimeout);

            // If it hasn't exited, we've probably
            // got the right process.
            if (!_process.HasExited) return;

            // If it has exited, find the currently running process.
            _process = GetProcess(_processName);

            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.UseShellExecute = false;
            _process.Refresh();
        }

        /// <summary>
        /// Kills the process and calls Dispose
        /// </summary>
        public void Dispose()
        {
            if (_process == null) return;
            _process.Kill();
            _process.Dispose();
        }
    }
}
