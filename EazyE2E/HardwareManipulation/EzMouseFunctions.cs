using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EazyE2E.Element;

namespace EazyE2E.HardwareManipulation
{
    public static class EzMouseFunctions
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        private static void DoMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private static void DoMouseRightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        private static void MoveCursorToPoint(EzElement element)
        {
            var point = element.BackingAutomationElement.GetClickablePoint();
            SetCursorPos((int)point.X, (int)point.Y);
        }

        private static void PrepElement(EzElement element)
        {
            element.BringIntoFocus();
            MoveCursorToPoint(element);
        }

        /// <summary>
        /// Performs an actual left click of an element.  Will click the center point of the passed in EzElement
        /// </summary>
        /// <param name="element">The EzElement to be acted upon</param>
        public static void LeftClick(EzElement element)
        {
            PrepElement(element);
            DoMouseClick();
        }

        /// <summary>
        /// Performs an actual right click of an element.  Will click the center point of the passed in EzElement
        /// </summary>
        /// <param name="element">The EzElement to be acted upon</param>
        public static void RightClick(EzElement element)
        {
            PrepElement(element);
            DoMouseRightClick();
        }

        /// <summary>
        /// Performs an actual double (left) click of an element.  Will click the center point of the passed in EzElement
        /// </summary>
        /// <param name="element">The EzElement to be acted upon</param>
        /// <param name="spacing">The amount of time between the two click events.  Default is 500 since this is also Windows' default</param>
        public static void DoubleClick(EzElement element, int spacing = 500)
        {
            PrepElement(element);
            DoMouseClick();
            Thread.Sleep(spacing);
            DoMouseClick();
        }
    }
}
