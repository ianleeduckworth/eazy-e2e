using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EazyE2E.Process;

namespace EazyE2E
{
    public class PerformanceMonitor
    {
        private readonly EzProcess _process;

        public PerformanceMonitor(EzProcess process)
        {
            _process = process;
        }

        public long NonpagedSystemMemorySize => _process.Process.NonpagedSystemMemorySize64;
        public long PagedMemorySize => _process.Process.PagedMemorySize64;
        public long PagedSystemMemorySize => _process.Process.PagedSystemMemorySize64;
        public long PeakPagedMemorySize => _process.Process.PeakPagedMemorySize64;
        public long PeakVirtualMemorySize => _process.Process.PeakVirtualMemorySize64;
        public long PeakWorkingSet => _process.Process.PeakWorkingSet64;
        public long PrivateMemorySize => _process.Process.PrivateMemorySize64;
        public long VirtualMemorySize => _process.Process.VirtualMemorySize64;
        public long WorkingSet => _process.Process.WorkingSet64;

        public void StartSyncWatch(MemoryType type, int time, long amount, Action ifFail, Action ifSuccess)
        {
            time *= 1000;

            for (var i = 0; i < time; i++)
            {
                CheckMemory(type, amount, ifFail);
                Thread.Sleep(1000); //todo I don't really like Thread.sleep...
            }

            ifSuccess();
        }

        private void CheckMemory(MemoryType type, long amount, Action ifFail)
        {
            switch (type)
            {
                case MemoryType.NonpagedSystemMemorySize:
                    if (this.NonpagedSystemMemorySize > amount) ifFail();
                    break;

                    //todo fill out the rest.
            }

        }
    }
}
