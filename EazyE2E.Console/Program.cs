using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using EazyE2E.Element;
using EazyE2E.Enums;
using EazyE2E.HardwareManipulation;
using EazyE2E.Logwatch;
using EazyE2E.LongSearch;
using EazyE2E.Performance;
using EazyE2E.Process;

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            //Simple method to launch Windows' built in calculator application and click the 7 button
            const string calculatorPath = "C:\\Windows\\System32\\calc.exe";

            //Get an instance of EzProcess in order to create EzRoot.  Wrap it in a using statement to ensure that .Dispose() is called
            using (EzProcess process = new EzProcess(calculatorPath, "Calculator"))
            {
                process.StartProcess();

                //Get the root element of the application
                EzRoot root = new EzRoot(process);

                //Use the .FindChildByAutomationId to query all children of the root for the first element whose AutomationId property is
                //equal to "NumberPad"
                EzElement numPad = root.RootElement.FindChildByAutomationId("NumberPad");

                //Find the first child of the NumberPad element whose AutomationId property is equal to "num7Button"
                EzElement sevenButton = numPad.FindChildByAutomationId("num7Button");
                EzElement eightButton = numPad.FindChildByAutomationId("num8Button");

                //Click the 7 button.  This will perform a click operation and the calculator's display should now say "7"
                sevenButton.Click();
                eightButton.Click();

                System.Console.ReadLine();
            }
        }
    }
}
