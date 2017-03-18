using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EazyE2E.Process
{
    public enum MemoryType
    {
        NonpagedSystemMemorySize,
        PagedMemorySize,
        PagedSystemMemorySize,
        PeakPagedMemorySize,
        PeakVirtualMemorySize,
        PeakWorkingSet,
        PrivateMemorySize,
        VirtualMemorySize,
        WorkingSet
    }
}
