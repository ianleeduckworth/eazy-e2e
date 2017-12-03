using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace EazyE2E.Equality
{
    public class ControlTypeEqualityCompare : IEqualityComparer<ControlType>
    {
        public bool Equals(ControlType x, ControlType y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.LocalizedControlType == y.LocalizedControlType &&
                x.ProgrammaticName == y.ProgrammaticName &&
                x.Id == y.Id;
        }

        public int GetHashCode(ControlType obj)
        {
            var lc = string.IsNullOrEmpty(obj.LocalizedControlType) ? 0 : obj.LocalizedControlType.GetHashCode();
            var id = obj.Id.GetHashCode();
            var pn = string.IsNullOrEmpty(obj.ProgrammaticName) ? 0 : obj.ProgrammaticName.GetHashCode();

            return lc ^ id ^ pn;
        }
    }
}
