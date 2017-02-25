using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using EazyE2E.Element;

namespace EazyE2E.HardwareManipulation
{
    public static class EzKeyboardFunctions
    {
        /// <summary>
        /// Sends an individual key
        /// </summary>
        /// <param name="element">The element to which the key should be sent</param>
        /// <param name="key">The key that is being pressed</param>
        public static void PressKey(EzElement element, Key key)
        {
            element.BringIntoFocus();
            SendKeys.SendWait(KeyboardKey.Instance.LookupKey(key));
        }

        /// <summary>
        /// When called, the keyboard will send a string of text.  Note that this method will not work if you want to use special keys like DELETE and PAGE UP for example
        /// </summary>
        /// <param name="element">The element to which the key should be sent</param>
        /// <param name="text">The key that is being pressed</param>
        public static void SendText(EzElement element, string text)
        {
            element.BringIntoFocus();
            SendKeys.SendWait(text);
        }

        public class KeyboardKey
        {
            private readonly Dictionary<Key, string> _backingDictionary;
            private static readonly object LockObj = new object();
            private static KeyboardKey _instance;

            public static KeyboardKey Instance
            {
                get
                {
                    lock (LockObj)
                    {
                        return _instance ?? (_instance = new KeyboardKey());
                    }
                }
            }

            private KeyboardKey()
            {
                _backingDictionary = new Dictionary<Key, string>();
                _backingDictionary.Add(Key.A, "A");
                _backingDictionary.Add(Key.B, "B");
                _backingDictionary.Add(Key.C, "C");
                _backingDictionary.Add(Key.D, "D");
                _backingDictionary.Add(Key.E, "E");
                _backingDictionary.Add(Key.F, "F");
                _backingDictionary.Add(Key.G, "G");
                _backingDictionary.Add(Key.H, "H");
                _backingDictionary.Add(Key.I, "I");
                _backingDictionary.Add(Key.J, "J");
                _backingDictionary.Add(Key.K, "K");
                _backingDictionary.Add(Key.L, "L");
                _backingDictionary.Add(Key.M, "M");
                _backingDictionary.Add(Key.N, "N");
                _backingDictionary.Add(Key.O, "O");
                _backingDictionary.Add(Key.P, "P");
                _backingDictionary.Add(Key.Q, "Q");
                _backingDictionary.Add(Key.R, "R");
                _backingDictionary.Add(Key.S, "S");
                _backingDictionary.Add(Key.T, "T");
                _backingDictionary.Add(Key.U, "U");
                _backingDictionary.Add(Key.V, "V");
                _backingDictionary.Add(Key.W, "W");
                _backingDictionary.Add(Key.X, "X");
                _backingDictionary.Add(Key.Y, "Z");
                _backingDictionary.Add(Key.Z, "Z");

                _backingDictionary.Add(Key.Delete, "{DEL}");
                _backingDictionary.Add(Key.Back, "{BACKSPACE}");
                _backingDictionary.Add(Key.Up, "{UP}");
                _backingDictionary.Add(Key.Down, "{DOWN}");
                _backingDictionary.Add(Key.Left, "{LEFT}");
                _backingDictionary.Add(Key.Right, "{RIGHT}");

                _backingDictionary.Add(Key.F1, "{F1}");
                _backingDictionary.Add(Key.F2, "{F2}");
                _backingDictionary.Add(Key.F3, "{F3}");
                _backingDictionary.Add(Key.F4, "{F4}");
                _backingDictionary.Add(Key.F5, "{F5}");
                _backingDictionary.Add(Key.F6, "{F6}");
                _backingDictionary.Add(Key.F7, "{F7}");
                _backingDictionary.Add(Key.F8, "{F8}");
                _backingDictionary.Add(Key.F9, "{F9}");
                _backingDictionary.Add(Key.F10, "{F10}");
                _backingDictionary.Add(Key.F11, "{F11}");
                _backingDictionary.Add(Key.F12, "{F12}");
                _backingDictionary.Add(Key.F13, "{F13}");

                _backingDictionary.Add(Key.NumPad0, "0");
                _backingDictionary.Add(Key.NumPad1, "1");
                _backingDictionary.Add(Key.NumPad2, "2");
                _backingDictionary.Add(Key.NumPad3, "3");
                _backingDictionary.Add(Key.NumPad4, "4");
                _backingDictionary.Add(Key.NumPad5, "5");
                _backingDictionary.Add(Key.NumPad6, "6");
                _backingDictionary.Add(Key.NumPad7, "7");
                _backingDictionary.Add(Key.NumPad8, "8");
                _backingDictionary.Add(Key.NumPad9, "9");
            }

            public string LookupKey(Key key)
            {
                return _backingDictionary.FirstOrDefault(x => x.Key == key).Value;
            }

        }
    }
}

