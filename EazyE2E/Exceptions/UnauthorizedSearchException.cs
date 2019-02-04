//Copyright 2019 Ian Duckworth

using System;

namespace EazyE2E.Exceptions
{
	/// <summary>
	/// Exception that can be thrown whenever a user is not authorized to search in a certain way (based on config settings)
	/// </summary>
    public class UnauthorizedSearchException : Exception
    {
		/// <summary>
		/// Provides a standard exception message that can be used if a user desires
		/// </summary>
        public const string StandardExceptionMessage = "You are not permitted to search for descendants.  Please modify your config file or speak with whoever is in charge of maintaining the application's config file";

		/// <summary>
		/// Exception that can be thrown whenever a user is not authorized to search in a certain way (based on config settings)
		/// </summary>
		/// <param name="message"></param>
		public UnauthorizedSearchException(string message) : base(message)
        {
        }
    }
}
