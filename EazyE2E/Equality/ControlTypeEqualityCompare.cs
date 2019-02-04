//Copyright 2019 Ian Duckworth

using System.Collections.Generic;
using System.Windows.Automation;

namespace EazyE2E.Equality
{
	/// <summary>
	/// Allows for an equality comparison to determine if control types are equal
	/// </summary>
    public class ControlTypeEqualityCompare : IEqualityComparer<ControlType>
    {
		/// <summary>
		/// Determines if two instances of ControlType are equal
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
        public bool Equals(ControlType x, ControlType y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.LocalizedControlType == y.LocalizedControlType &&
                x.ProgrammaticName == y.ProgrammaticName &&
                x.Id == y.Id;
        }

		/// <summary>
		/// Gets a custom hash code based on ControlType passed in
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
        public int GetHashCode(ControlType obj)
        {
            var lc = string.IsNullOrEmpty(obj.LocalizedControlType) ? 0 : obj.LocalizedControlType.GetHashCode();
            var id = obj.Id.GetHashCode();
            var pn = string.IsNullOrEmpty(obj.ProgrammaticName) ? 0 : obj.ProgrammaticName.GetHashCode();

            return lc ^ id ^ pn;
        }
    }
}
