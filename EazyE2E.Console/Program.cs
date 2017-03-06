using System;
using System.CodeDom;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Windows.Input;
using EazyE2E.Element;
using EazyE2E.Enums;
using EazyE2E.HardwareManipulation;
using EazyE2E.LongSearch;
using EazyE2E.Process;

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            var calculatorPath = "C:\\Windows\\System32\\calc.exe";
            using (var process = new EzProcess(calculatorPath, "Calculator"))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                process.StartProcess();

                //var root = new EzRoot("Calculator").RootElement;
                var root = new EzRoot(process).RootElement;

                var testSevenBtn2 = LongSearch.LongSearch.PerformSearch(root, new LongSearchItem(PropertyType.AutomationId, "NumberPad"), new LongSearchItem(PropertyType.AutomationId, "num7Button"));

                //find the number we want to click
                var numberPad = root.FindChildByAutomationId("NumberPad");
                var sevenBtn = numberPad.FindChildByAutomationId("num7Button");
                var nineBtn = numberPad.FindChildByAutomationId("num9Button");

                //find the operators that we will need to use
                var stdOperations = root.FindChildByAutomationId("StandardOperators");
                var plusBtn = stdOperations.FindChildByAutomationId("plusButton");
                var equalBtn = stdOperations.FindChildByAutomationId("equalButton");

                //find the display pane for verification
                var displayPane = root.FindChildByAutomationId("CalculatorResults");

                //do the work

                sevenBtn.Click();
                plusBtn.Click();
                nineBtn.Click();
                equalBtn.Click();


                //verify the result
                var result = displayPane.Name == "Display is 16";
                System.Console.WriteLine(result ? $"Test passed.  Result pane should say 16 and it says '{displayPane.Name}'" : $"Error occurred.  Result pane should say 16 but instead it says '{displayPane.Name}'");

                stopwatch.Stop();
                System.Console.WriteLine($"Test took {stopwatch.ElapsedMilliseconds} miliseconds");
                System.Console.ReadLine();
            }
        }

        private static void ExecWrapper(EzElement element, string outputKey)
        {
            element.Click();
            System.Console.WriteLine($"Clicking {outputKey} element");
            Thread.Sleep(1000);
        }
    }
}
