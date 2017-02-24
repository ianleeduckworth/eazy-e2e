using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EazyE2E.HardwareManipulation
{
    public class EzKeyboardFunctions
    {
        private static EzKeyboardFunctions _instance;
        private static readonly object LockObj = new object();

        private readonly IInputElement _target;
        private readonly RoutedEvent _routedEvent;

        private EzKeyboardFunctions()
        {
            _target = Keyboard.FocusedElement;
            _routedEvent = Keyboard.KeyDownEvent;
        }

        public static EzKeyboardFunctions Instance
        {
            get
            {
                lock (LockObj)
                {
                    return _instance ?? (_instance = new EzKeyboardFunctions());
                }
            }
        }

        public void PressKey(Key key)
        {
            var visual = (Visual) _target;
            if (visual == null) throw new InvalidCastException("Target object could not be cast to type Visual");
            var source = PresentationSource.FromVisual(visual);
            if (source == null) throw new InvalidOperationException("Could not create Source object.");

            var keyEventArgs = new KeyEventArgs(Keyboard.PrimaryDevice, source, 0, key)
            {
                RoutedEvent = _routedEvent
            };
            _target.RaiseEvent(keyEventArgs);
        }

        public void SendText(string text)
        {
            var textCompositionEventArgs = new TextCompositionEventArgs(InputManager.Current.PrimaryKeyboardDevice, new TextComposition(InputManager.Current, _target, text))
            {
                RoutedEvent = _routedEvent
            };
            _target.RaiseEvent(textCompositionEventArgs);
        }
    }
}
