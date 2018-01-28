//Copyright 2018 Ian Duckworth

using System;

namespace EazyE2E.Exceptions
{
    public class TypeMismatchException : Exception
    {
        public TypeMismatchException(string message) : base(message)
        {
        }
    }
}
