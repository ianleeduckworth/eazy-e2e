using System;
using EazyE2E.Element;
using EazyE2E.Process;

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            const string appPath = "C:\\Windows\\System32\\calc.exe";
            using (var process = new EzProcess(appPath, "Calculator"))
            {
                process.StartProcess();

                var root = new EzRoot(process);
                var numberPad = root.RootElement.FindChildByAutomationId("NumberPad");
                var sevenButton = numberPad.FindChildByAutomationId("num7Button");

                sevenButton.Click();

                //pause
                System.Console.ReadLine();
            }
        }
    }
}
