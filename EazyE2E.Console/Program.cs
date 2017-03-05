using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Windows.Input;
using EazyE2E.Element;
using EazyE2E.HardwareManipulation;
using EazyE2E.Process;

namespace EazyE2E.Console
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            string calculatorPath = "C:\\Windows\\System32\\calc.exe";
            using (var process = new EzProcess(calculatorPath, "Calculator"))
            {
                process.StartProcess();

                //var root = new EzRoot("Calculator").RootElement;
                var root = new EzRoot(process).RootElement;
                var displayPane = root.FindDescendantByAutomationId("CalculatorResults");

                //get the clear button
                var clear = root.FindDescendantByAutomationId("clearButton");

                //get parent elements for numbers and operators respectfully
                var numbers = root.FindDescendantByAutomationId("NumberPad");
                var standardOperators = root.FindDescendantByAutomationId("StandardOperators");

                clear.Click();
                EzKeyboardFunctions.PressKey(displayPane, Key.NumPad7);
                var result = displayPane.Name.Split(' ')[2].Trim() == "7";
                System.Console.WriteLine(result
                    ? "Display pane is currently displaying 7"
                    : $"There was an error.  Display pane is currenly displaying: ${displayPane.Name}");
                Thread.Sleep(1000);
                EzKeyboardFunctions.PressKey(displayPane, Key.Back);
                result = displayPane.Name.Split(' ')[2].Trim() == "0";
                System.Console.WriteLine(result
                    ? "Display pane is currently displaying nothing, which is correct."
                    : $"There ws an error.  Display pane is currently displaying: ${displayPane.Name}");

                //get numbers buttons based on numbers object
                //var oneButton = numbers.FindChildByAutomationId("num1Button");
                //var twoButton = numbers.FindChildByAutomationId("num2Button");
                //var fiveButton = numbers.FindChildByAutomationId("num5Button");
                //var sevenButton = numbers.FindChildByAutomationId("num7Button");
                //var nineButton = numbers.FindChildByAutomationId("num9Button");

                //get operator buttons based on standardOperators object
                //var plusButton = standardOperators.FindChildByAutomationId("plusButton");
                //var minusButton = standardOperators.FindChildByAutomationId("minusButton");
                //var multiplyButton = standardOperators.FindChildByAutomationId("multiplyButton");
                //var equalsButton = standardOperators.FindChildByAutomationId("equalButton");

                //click the buttons
                //ExecWrapper(clear, "clear");
                //ExecWrapper(sevenButton, "7");
                //ExecWrapper(plusButton, "+");
                //ExecWrapper(nineButton, "9");
                //ExecWrapper(minusButton, "-");
                //ExecWrapper(oneButton, "1");
                //ExecWrapper(twoButton, "2");
                //ExecWrapper(multiplyButton, "x");
                //ExecWrapper(fiveButton, "5");
                //ExecWrapper(equalsButton, "=");

                //var result = displayPane.Name == "Display is 20";
                //Console.WriteLine(result ? "Test passed.  Calculator output is 20" : $"Test failed.  Calculator output is {displayPane.Name.Split(' ')[2].Trim()} where it should have been 20");

                System.Console.Write("Press Enter to exit");
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
