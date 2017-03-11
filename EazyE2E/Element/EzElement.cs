using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using EazyE2E.Enums;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    public class EzElement
    {
        private readonly AutomationElement _backingAutomationElement;
        private EzElement _parent;
        private readonly InvokePattern _invokePattern;

        public EzElement Parent => _parent ?? (_parent = new EzElement(_backingAutomationElement.CachedParent));
        public string Name => _backingAutomationElement.Current.Name;
        public string AutomationId => _backingAutomationElement.Current.AutomationId;
        public string ClassName => _backingAutomationElement.Current.ClassName;
        public ControlType Type => _backingAutomationElement.Current.ControlType;

        public EzElement(EzElement element)
        {
            _backingAutomationElement = element._backingAutomationElement;
            object pattern;
            element.BackingAutomationElement.TryGetCurrentPattern(InvokePattern.Pattern, out pattern);
            _invokePattern = pattern as InvokePattern;
        }

        public EzElement(EzRoot root)
        {
            _backingAutomationElement = root.RootElement._backingAutomationElement;
            object pattern;
            root.RootElement.BackingAutomationElement.TryGetCurrentPattern(InvokePattern.Pattern, out pattern);
            _invokePattern = pattern as InvokePattern;
        }

        public EzElement(AutomationElement element)
        {
            _backingAutomationElement = element;
            object pattern;
            element.TryGetCurrentPattern(InvokePattern.Pattern, out pattern);
            _invokePattern = pattern as InvokePattern;
        }

        public AutomationElement BackingAutomationElement => _backingAutomationElement;


        public void Click()
        {
            _invokePattern?.Invoke();
        }

        public void BringIntoFocus()
        {
            this.BackingAutomationElement.SetFocus();
        }

        public EzElement FindChildByName(string name, bool searchRaw = false)
        {
            if (!searchRaw) return new EzElement(_backingAutomationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, name)));

            var walker = TreeWalker.RawViewWalker;
            var firstChild = walker.GetFirstChild(_backingAutomationElement);
            if (firstChild.Current.Name == name) return new EzElement(firstChild);

            return null;
        }
            
        public EzElement FindDescendantByName(string name)
        {
            return new EzElement(_backingAutomationElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, name)));
        }

        public EzElement FindChildByAutomationId(string name)
        {
            return new EzElement(_backingAutomationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.AutomationIdProperty, name)));
        }

        public EzElement FindChildByClass(string name)
        {
            return new EzElement(_backingAutomationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ClassNameProperty, name)));
        }

        public EzElement FindDescendantByAutomationId(string name)
        {
            return new EzElement(_backingAutomationElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, name)));
        }
        public EzElement FindChildByMultipleCriteria(params SearchTypeProperty[] properties)
        {
            return new EzElement(_backingAutomationElement.FindFirst(TreeScope.Children, new AndCondition(properties.Select(prop => new PropertyCondition(SearchTypeHelper.GetAutomationProperty(prop.PropertyType), prop.Name)).Cast<Condition>().ToArray())));
        }

        public EzElement FindDescendantByMultipleCriteria(params SearchTypeProperty[] properties)
        {
            return new EzElement(_backingAutomationElement.FindFirst(TreeScope.Descendants, new AndCondition(properties.Select(prop => new PropertyCondition(SearchTypeHelper.GetAutomationProperty(prop.PropertyType), prop.Name)).Cast<Condition>().ToArray())));
        }

        public IEnumerable<EzElement> FindChildrenByName(string name)
        {
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, name)));
        }

        public IEnumerable<EzElement> FindDescendantsByName(string name)
        {
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, name)));
        }

        public IEnumerable<EzElement> FindChildrenByAutomationId(string name)
        {
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.AutomationIdProperty, name)));
        }

        public IEnumerable<EzElement> FindDescendantsByAutomationId(string name)
        {
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, name)));
        }

        public IEnumerable<EzElement> FindChildrenByMultipleCriteria(params SearchTypeProperty[] properties)
        {
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Children, new AndCondition(properties.Select(prop => new PropertyCondition(SearchTypeHelper.GetAutomationProperty(prop.PropertyType), prop.Name)).Cast<Condition>().ToArray())));
        }

        public IEnumerable<EzElement> FindDescendantsByMultipleCriteria(params SearchTypeProperty[] properties)
        {
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Descendants, new AndCondition(properties.Select(prop => new PropertyCondition(SearchTypeHelper.GetAutomationProperty(prop.PropertyType), prop.Name)).Cast<Condition>().ToArray())));
        }

        public IEnumerable<EzElement> GetAllChildren()
        {
            //this is super dumb.  Figure out a better way to do this.
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Children, new NotCondition(new PropertyCondition(AutomationElement.ClassNameProperty, "dfsjkdsfjkdfskjfsdjkdsfjkfsdjkdsfsdfjkdfskjdsfjkdsfjkdsfjkdsfjkdfskjsdfkjsdfjksdfkjfdkdsf"))));
        }

        /// <summary>
        /// USE THIS METHOD WITH CAUTION.  IT WILL RETURN A COLLECTION OF EVERY DESCENDANT OF AN ELEMENT WICH CAN HAVE A HUGE PERFORMANCE IMPACT
        /// </summary>
        /// <returns>An IEnumerable of type EzElement</returns>
        public IEnumerable<EzElement> GetAllDescendants()
        {
            //this is super dumb.  Figure out a better way to do this.
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Descendants, new NotCondition(new PropertyCondition(AutomationElement.ClassNameProperty, "dfsjkdsfjkdfskjfsdjkdsfjkfsdjkdsfsdfjkdfskjdsfjkdsfjkdsfjkdsfjkdfskjsdfkjsdfjksdfkjfdkdsf"))));
        }

        private IEnumerable<EzElement> ConvertCollection(AutomationElementCollection collection)
        {
            return from object c in collection select new EzElement(c as AutomationElement);
        } 
    }
}
