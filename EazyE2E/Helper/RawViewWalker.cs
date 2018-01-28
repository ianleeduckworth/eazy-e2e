//Copyright 2018 Ian Duckworth

using System.Collections.Generic;
using System.Windows.Automation;
using EazyE2E.Element;
using EazyE2E.Enums;

namespace EazyE2E.Helper
{
    public static class RawViewWalker
    {
        /// <summary>
        /// Finds a child based on TreeView.RawViewWalker; will get literally all elements in the application instead of just Control elements
        /// </summary>
        /// <param name="element"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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
            } while (walker.GetNextSibling(e) != null);

            return null;
        }

        /// <summary>
        /// Finds children based on TreeView.RawViewWalker; will get literally all elements in the application instead of just Control elements
        /// </summary>
        /// <param name="element"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<EzElement> FindChildrenRaw(this EzElement element, PropertyType type, string name)
        {
            var ezElements = new List<EzElement>();
            var aElement = element.BackingAutomationElement;

            var walker = TreeWalker.RawViewWalker;

            var count = 0;
            AutomationElement e = null;
            do
            {
                e = count == 0 ? walker.GetFirstChild(aElement) : walker.GetNextSibling(e);

                if (e == null) return ezElements;

                switch (type)
                {
                    case PropertyType.AutomationId:
                        if (e.Current.AutomationId == name) ezElements.Add(new EzElement(e));
                        break;
                    case PropertyType.Name:
                        if (e.Current.Name == name) ezElements.Add(new EzElement(e));
                        break;
                    case PropertyType.Class:
                        if (e.Current.ClassName == name) ezElements.Add(new EzElement(e));
                        break;
                }

                count++;
            } while (walker.GetNextSibling(e) != null);

            return ezElements;
        }
    }
}
