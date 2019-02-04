//Copyright 2019 Ian Duckworth

using System;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using EazyE2E.Process;

namespace EazyE2E.Element
{
	/// <summary>
	/// Represents the root element of an application.  When traversing the UI tree, this represents the highest level node
	/// </summary>
    public class EzRoot
    {
        private readonly EzProcess _process;
        private EzElement _rootElement;

        /// <summary>
        /// EzElement representation of the root element of the application
        /// </summary>
        public EzElement RootElement => _rootElement ?? (_rootElement = GetRootElement());

        /// <summary>
        /// Creates an instance of EzRoot based on an EzProcess
        /// </summary>
        /// <param name="process"></param>
        public EzRoot(EzProcess process)
        {
            _process = process;
        }

        /// <summary>
        /// Gets the root element of the process passed in during instantiation
        /// </summary>
        /// <returns></returns>
        public EzElement GetRootElement()
        {
            var propertyCondition = new PropertyCondition(AutomationElement.ProcessIdProperty, _process.ProcessId);
            var element = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, propertyCondition); //todo use TreeScope.Children instead of TreeScope.Subtree
            if (element == null) throw new NullReferenceException($"Root element not found where process name is ${_process.ProcessName}.");
            return new EzElement(element);
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        /// Resizes a window based on parameters inputted
        /// </summary>
        /// <param name="x">X coordinate of the window</param>
        /// <param name="y">Y coordinate of the window</param>
        /// <param name="width">How wide the window will be</param>
        /// <param name="height">How tall the window will be</param>
        public void ResizeWindow(int x, int y, int width, int height)
        {
            MoveWindow(_process.Process.MainWindowHandle, x, y, width, height, true);
        }
    }
}
