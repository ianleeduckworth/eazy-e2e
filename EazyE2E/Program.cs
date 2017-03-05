using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Input;
using EazyE2E.Element;
using EazyE2E.Enums;
using EazyE2E.HardwareManipulation;
using EazyE2E.Process;

namespace EazyE2E
{
    class Program
    {
        [STAThread]
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
            var navBtn = root.FindDescendantByAutomationId("NavButton");

            ExecWrapper(navBtn, "NavButton"); //click the nav button to launch the flyout
            var standardButton = root.FindDescendantByName("Standard Calculator");
            EzMouseFunctions.MoveMouse(standardButton); //hover over the standard button so the scrolls will actually work

            Console.WriteLine("Scrolling down 8 times");
            EzMouseFunctions.ScrollDown(navBtn, 8);
            Thread.Sleep(1000);

            Console.WriteLine("Scrolling up 8 times");
            EzMouseFunctions.ScrollUp(navBtn, 8);
            Thread.Sleep(1000);

            //clear.Click();
            //EzKeyboardFunctions.PressKey(displayPane, Key.NumPad7);
            //var result = displayPane.Name.Split(' ')[2].Trim() == "7";
            //Console.WriteLine(result ? "Display pane is currently displaying 7" : $"There was an error.  Display pane is currenly displaying: ${displayPane.Name}");
            //Thread.Sleep(1000);
            //EzKeyboardFunctions.PressKey(displayPane, Key.Back);
            //result = displayPane.Name.Split(' ')[2].Trim() == "0";
            //Console.WriteLine(result ? "Display pane is currently displaying nothing, which is correct." : $"There ws an error.  Display pane is currently displaying: ${displayPane.Name}");

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

            Console.Write("Press Enter to exit");
            Console.ReadLine();

        }

        private static void ExecWrapper(EzElement element, string outputKey)
        {
            element.Click();
            Console.WriteLine($"Clicking {outputKey} element");
            Thread.Sleep(1000);
        }
    }
}
