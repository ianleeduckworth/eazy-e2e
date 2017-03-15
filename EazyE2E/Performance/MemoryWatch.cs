using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EazyE2E.Process;

namespace EazyE2E.Performance
{
    public class MemoryWatch
    {
        public MemoryWatch(MemoryType type, long amount)
        {
            this.Amount = amount;
            this.Type = type;
        }

        public MemoryType Type { get; }

        public long Amount { get; }
    }
}
