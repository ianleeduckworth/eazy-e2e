using System;
using System.Linq;
using System.Windows.Automation;
using EazyE2E.Equality;

namespace EazyE2E.Helper
{
    public static class TypeChecker
    {
        public static void CheckElementType(AutomationElement element, params ControlType[] types)
        {
            if (!types.Contains(element.Current.ControlType, new ControlTypeEqualityCompare()))
                throw new ArrayTypeMismatchException($"Cannot create element due to type mismatch.  Element type: {element.Current.ControlType}.  Accepted ControlTypes: {types.Select(x => x.LocalizedControlType).Aggregate((a, b) => a + ", " + b)}");
        }
    }
}
