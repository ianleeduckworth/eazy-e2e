using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using EazyE2E.Element;
using EazyE2E.Enums;
using EazyE2E.Process;

namespace EazyE2E
{
    class Program
    {
        static void Main()
        {
            //setup
            var process = new EzProcess("Calculator");
            var root = new EzRoot(process).RootElement;
            var displayPane = root.FindDescendantByAutomationId("CalculatorResults");

            //get the clear button
            var clear = root.FindDescendantByAutomationId("clearButton");

            //get parent elements for numbers and operators respectfully
            var numbers = root.FindDescendantByAutomationId("NumberPad");
            var standardOperators = root.FindDescendantByAutomationId("StandardOperators");

            //get numbers buttons based on numbers object
            var oneButton = numbers.FindChildByAutomationId("num1Button");
            var twoButton = numbers.FindChildByAutomationId("num2Button");
            var fiveButton = numbers.FindChildByAutomationId("num5Button");
            var sevenButton = numbers.FindChildByAutomationId("num7Button");
            var nineButton = numbers.FindChildByAutomationId("num9Button");

            //get operator buttons based on standardOperators object
            var plusButton = standardOperators.FindChildByAutomationId("plusButton");
            var minusButton = standardOperators.FindChildByAutomationId("minusButton");
            var multiplyButton = standardOperators.FindChildByAutomationId("multiplyButton");
            var equalsButton = standardOperators.FindChildByAutomationId("equalButton");

            //click the buttons
            ExecWrapper(clear, "clear");
            ExecWrapper(sevenButton, "7");
            ExecWrapper(plusButton, "+");
            ExecWrapper(nineButton, "9");
            ExecWrapper(minusButton, "-");
            ExecWrapper(oneButton, "1");
            ExecWrapper(twoButton, "2");
            ExecWrapper(multiplyButton, "x");
            ExecWrapper(fiveButton, "5");
            ExecWrapper(equalsButton, "=");

            var result = displayPane.Name == "Display is 20";
            Console.WriteLine(result ? "Test passed.  Calculator output is 20" : $"Test failed.  Calculator output is {displayPane.Name.Split(' ')[2].Trim()} where it should have been 20");

            Console.Write("Press Enter to exit");
            Console.ReadLine();

        }

        private static void ExecWrapper(EzElement element, string outputKey)
        {
            element.Click();
            Console.WriteLine($"Clicking {outputKey} element");
            Thread.Sleep(4000);
        }
    }
}
