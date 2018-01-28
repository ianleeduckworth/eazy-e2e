//Copyright 2018 Ian Duckworth

using System;

namespace EazyE2E.Exceptions
{
    public class UnauthorizedSearchException : Exception
    {
        public const string StandardExceptionMessage = "You are not permitted to search for descendants.  Please modify your config file or speak with whoever is in charge of maintaining the application's config file";
        public UnauthorizedSearchException(string message) : base(message)
        {
        }
    }
}
