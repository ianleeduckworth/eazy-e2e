using System;
using System.Windows.Automation;

namespace EazyE2E.Helper
{
    public static class TypeChecker
    {
        public static void CheckElementType(AutomationElement element, ControlType type)
        {
            if (!Equals(element.Current.ControlType, type))
                throw new InvalidOperationException($"Cannot create type EzGrid where root element's ControlType is not {type}.  Element passed in has a ControlType of {element.Current.ControlType}");
        }
    }
}
