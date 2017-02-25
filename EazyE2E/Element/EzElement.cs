using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using EazyE2E.Enums;

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

        public EzElement FindChildByName(string name, SearchType type = SearchType.Children)
        {
            return new EzElement(_backingAutomationElement.FindFirst(type == SearchType.Children ? TreeScope.Children : TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, name)));
        }

        public EzElement FindChildByAutomationId(string name, SearchType type = SearchType.Children)
        {
            return new EzElement(_backingAutomationElement.FindFirst(type == SearchType.Children ? TreeScope.Children : TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, name)));
        }

        public IEnumerable<EzElement> FindChildrenByName(string name, SearchType type = SearchType.Children)
        {
            return ConvertCollection(_backingAutomationElement.FindAll(type == SearchType.Children ? TreeScope.Children : TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, name)));
        }

        public IEnumerable<EzElement> FindChildrenByAutomationId(string name, SearchType type = SearchType.Children)
        {
            return ConvertCollection(_backingAutomationElement.FindAll(type == SearchType.Children ? TreeScope.Children : TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, name)));
        }

        public IEnumerable<EzElement> GetAllChildren()
        {
            //this is super dumb.  Figure out a better way to do this.
            return ConvertCollection(_backingAutomationElement.FindAll(TreeScope.Children, new NotCondition(new PropertyCondition(AutomationElement.ClassNameProperty, "dfsjkdsfjkdfskjfsdjkdsfjkfsdjkdsfsdfjkdfskjdsfjkdsfjkdsfjkdsfjkdfskjsdfkjsdfjksdfkjfdkdsf"))));
        } 
        
        private IEnumerable<EzElement> ConvertCollection(AutomationElementCollection collection)
        {
            return from object c in collection select new EzElement(c as AutomationElement);
        } 
    }
}
