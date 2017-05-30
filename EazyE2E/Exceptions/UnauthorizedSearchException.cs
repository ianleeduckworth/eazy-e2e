using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EazyE2E.Exceptions
{
    public class UnauthorizedSearchException : Exception
    {
        public UnauthorizedSearchException(string message) : base(message)
        {
        }
    }
}
