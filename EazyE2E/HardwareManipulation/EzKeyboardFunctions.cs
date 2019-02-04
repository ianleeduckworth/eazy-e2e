//Copyright 2019 Ian Duckworth

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using EazyE2E.Element;

namespace EazyE2E.HardwareManipulation
{
	/// <summary>
	/// Performs mouse physical keyboard functions against application being automated.  NOTE THAT THIS CLASS WHILL PHYSICALLY MANIPULATE YOUR KEYBOARD
	/// </summary>
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

		/// <summary>
		/// Sends the string passed in with a wait (in milliseconds) inbetween each character.  Simulates a slower typer
		/// </summary>
		/// <param name="element"></param>
		/// <param name="text"></param>
		/// <param name="wait"></param>
		public static void SendTextWithWait(EzElement element, string text, int wait)
		{
			element.BringIntoFocus();
			foreach (var character in text)
			{
				SendKeys.SendWait(character.ToString());
				Thread.Sleep(wait);
			}
		}

		/// <summary>
		/// Sends a key to the application a specific number of times
		/// </summary>
		/// <param name="element"></param>
		/// <param name="repeatNumber"></param>
		/// <param name="key"></param>
		public static void RepeatKey(EzElement element, int repeatNumber, Key key)
		{
			element.BringIntoFocus();
			for (int i = 0; i < repeatNumber; i++)
				PressKey(element, key);
		}


		/// <summary>
		/// Performs a key combination; holds down shift while pressing all keys passed in
		/// </summary>
		/// <param name="element"></param>
		/// <param name="keys"></param>
		public static void ShiftCombination(EzElement element, params Key[] keys)
		{
			ShiftCombination(element, Aggregate(keys));
		}

		/// <summary>
		/// Performs a key combination; holds down shift while pressing all keys passed in
		/// </summary>
		/// <param name="element"></param>
		/// <param name="keys"></param>
		public static void ShiftCombination(EzElement element, string keys)
		{
			element.BringIntoFocus();
			SendKeys.SendWait($"+({keys})");
		}

		/// <summary>
		/// Performs a key combination; holds down control while pressing all keys passed in
		/// </summary>
		/// <param name="element"></param>
		/// <param name="keys"></param>
		public static void CtrlCombination(EzElement element, params Key[] keys)
		{
			CtrlCombination(element, Aggregate(keys));
		}

		/// <summary>
		/// Performs a key combination; holds down control while pressing all keys passed in
		/// </summary>
		/// <param name="element"></param>
		/// <param name="keys"></param>
		public static void CtrlCombination(EzElement element, string keys)
		{
			element.BringIntoFocus();
			SendKeys.SendWait($"^({keys})");
		}

		/// <summary>
		/// Performs a key combination; holds down alt while pressing all keys passed in
		/// </summary>
		/// <param name="element"></param>
		/// <param name="keys"></param>
		public static void AltCombination(EzElement element, params Key[] keys)
		{
			AltCombination(element, Aggregate(keys));
		}

		/// <summary>
		/// Performs a key combination; holds down alt while pressing all keys passed in
		/// </summary>
		/// <param name="element"></param>
		/// <param name="keys"></param>
		public static void AltCombination(EzElement element, string keys)
		{
			element.BringIntoFocus();
			SendKeys.SendWait($"%({keys})");
		}

		private static string Aggregate(params Key[] keys)
		{
			return keys.Select(x => x.ToString()).Aggregate((a, b) => a.ToString() + b.ToString());
		}

        private class KeyboardKey
        {
            private readonly Dictionary<Key, string> _backingDictionary;
            private static readonly object LockObj = new object();
            private static KeyboardKey _instance;

            /// <summary>
            /// Gets the current (and only) instance of this class.  Use this object to perform keyboard related tasks
            /// </summary>
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

            /// <summary>
            /// Gets the string value of a key based on the Key instance passed in
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public string LookupKey(Key key)
            {
                return _backingDictionary.FirstOrDefault(x => x.Key == key).Value;
            }

        }
    }
}

