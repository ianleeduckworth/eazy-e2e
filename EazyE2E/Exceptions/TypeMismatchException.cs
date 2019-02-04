//Copyright 2019 Ian Duckworth

using System;

namespace EazyE2E.Exceptions
{
	/// <summary>
	/// Exception that can be thrown whenever two types do not match
	/// </summary>
    public class TypeMismatchException : Exception
    {
		/// <summary>
		/// Exception that can be thrown whenever two types do not match
		/// </summary>
		/// <param name="message"></param>
		public TypeMismatchException(string message) : base(message)
        {
        }
    }
}
