using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using EazyE2E.Element;
using EazyE2E.Enums;

namespace EazyE2E.Helper
{
    public static class RawViewWalker
    {
        public static EzElement FindChildRaw(this EzElement element, PropertyType type, string name)
        {
            var aElement = element.BackingAutomationElement;

            var walker = TreeWalker.RawViewWalker;

            var count = 0;
            AutomationElement e = null;
            do
            {
                e = count == 0 ? walker.GetFirstChild(aElement) : walker.GetNextSibling(e);

                if (e == null) return null;

                switch (type)
                {
                    case PropertyType.AutomationId:
                        if (e.Current.AutomationId == name) return new EzElement(e);
                        break;
                    case PropertyType.Name:
                        if (e.Current.Name == name) return new EzElement(e);
                        break;
                    case PropertyType.Class:
                        if (e.Current.ClassName == name) return new EzElement(e);
                        break;
                }

                count++;
            } while (true);
        }
    }
}
