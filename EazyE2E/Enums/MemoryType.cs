//Copyright 2019 Ian Duckworth

namespace EazyE2E.Enums
{
	/// <summary>
	/// Memoyr types that can be measured
	/// </summary>
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
