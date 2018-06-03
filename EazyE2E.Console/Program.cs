using System;
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
            const string appPath = "C:\\Windows\\System32\\notepad.exe";
            using (var process = new EzProcess(appPath, "Notepad"))
            {
                process.StartProcess();

                var root = new EzRoot(process).RootElement;
				var mainEditor = root.FindChildByName("Text Editor");

				Thread.Sleep(2000);

				EzMouseFunctions.LeftClick(mainEditor);
				EzKeyboardFunctions.CtrlCombination(mainEditor, Key.O, Key.J, Key.F);


                //pause
                System.Console.ReadLine();
            }
        }
    }
}
