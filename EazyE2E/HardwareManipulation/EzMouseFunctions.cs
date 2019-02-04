//Copyright 2019 Ian Duckworth

using System.Runtime.InteropServices;
using System.Threading;
using EazyE2E.Configuration;
using EazyE2E.Element;

namespace EazyE2E.HardwareManipulation
{
	/// <summary>
	/// Performs mouse physical mouse functions against application being automated.  NOTE THAT THIS CLASS WHILL PHYSICALLY MANIPULATE YOUR MOUSE
	/// </summary>
    public static class EzMouseFunctions
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x0800;

        /// <summary>
        /// Allows the user to set the amount of time inbetween mouse events.
        /// <para>NOTE THAT A VALUE OF LESS THAN 100 IS NOT RECOMMENDED BECAUSE APPLICATIONS GENERALLY CANNOT REACT QUICKLY ENOUGH AND YOU MAY GET INCORRECT CLICKS</para>
        /// </summary>
        public static int TimeInbetweenMouseEvents => Config.TimeBetweenMouseEvents;

        private static void DoMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private static void DoMouseRightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        private static void DoMouseScroll(uint scrollAmt, bool scrollUp)
        {
            var amt = (int) scrollAmt;

            amt *= 120; //120 is equivalent to one wheel click
            if (!scrollUp) amt *= -1; 

            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, amt, 0);
        }

        private static void MoveCursorToPoint(EzElement element)
        {
            var point = element.BackingAutomationElement.GetClickablePoint();
            SetCursorPos((int)point.X, (int)point.Y);
        }

        private static void PrepElement(EzElement element, bool shouldMoveCursor = true)
        {
            element.BringIntoFocus();
            if (shouldMoveCursor) MoveCursorToPoint(element);
        }

        private static void TearDown()
        {
            var sleep = TimeInbetweenMouseEvents == 0 ? 100 : TimeInbetweenMouseEvents;
            Thread.Sleep(sleep);
        }

        /// <summary>
        /// Physically moves the mouse to the center point of the passed in EzElement
        /// </summary>
        /// <param name="element">The EzElement to be acted upon</param>
        public static void MoveMouse(EzElement element)
        {
            //prep takes care of everything; brings the element into view and moves the cursor to the point it needs
            PrepElement(element);
            TearDown();
        }

        /// <summary>
        /// Physically moves the mouse to the coordinates passed in.  If you are looking hover the mouse over an EzElement, use the signature MoveMouse(EzElement element) instead
        /// </summary>
        /// <param name="x">X coordinate where the mouse is to be placed</param>
        /// <param name="y">Y coordinate where the mouse is to be placed</param>
        public static void MoveMouse(int x, int y)
        {
            SetCursorPos(x, y);
            TearDown();
        }

        /// <summary>
        /// Performs an actual left click of an element.  Will click the center point of the passed in EzElement
        /// </summary>
        /// <param name="element">The EzElement to be acted upon</param>
        public static void LeftClick(EzElement element)
        {
            PrepElement(element);
            DoMouseClick();
            TearDown();
        }

        /// <summary>
        /// Performs an actual right click of an element.  Will click the center point of the passed in EzElement
        /// </summary>
        /// <param name="element">The EzElement to be acted upon</param>
        public static void RightClick(EzElement element)
        {
            PrepElement(element);
            DoMouseRightClick();
            TearDown();
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
            Thread.Sleep(Config.DoubleClickGap);
            DoMouseClick();
            TearDown();
        }

        /// <summary>
        /// Performs an actual upward scroll of the mouse wheel.
        /// </summary>
        /// <param name="element">The EzElement to be acted upon.  In this case, since the item isn't being clicked, it will just bring the element into focus.</param>
        /// <param name="wheelClicks">The number of wheel clicks to be scrolled.  Default is one wheel click</param>
        public static void ScrollUp(EzElement element, uint wheelClicks = 1)
        {
            PrepElement(element, false);
            DoMouseScroll(wheelClicks, true);
            TearDown();
        }

        /// <summary>
        /// Performs an actual downward scroll of the mouse wheel.
        /// </summary>
        /// <param name="element">The EzElement to be acted upon.    In this case, since the item isn't being clicked, it will just bring the element into focus.</param>
        /// <param name="wheelClicks">The number of wheel clicks to be scrolled.  Default is one wheel click</param>
        public static void ScrollDown(EzElement element, uint wheelClicks = 1)
        {
            PrepElement(element, false);
            DoMouseScroll(wheelClicks, false);
            TearDown();
        }
    }
}
