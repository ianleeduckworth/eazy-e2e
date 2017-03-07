using System.Windows.Automation;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    public class EzListItem : EzElement
    {
        private readonly SelectionItemPattern _selectionItemPattern;
        private readonly ScrollItemPattern _scrollItemPattern;
        private readonly VirtualizedItemPattern _virtualizedItemPattern;

        private EzList _container;

        private bool _parentCanSelectMultiple;

        public EzListItem(EzRoot root) : base(root)
        {
            TypeChecker.CheckElementType(root.RootElement.BackingAutomationElement, ControlType.ListItem);
            _selectionItemPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
            _scrollItemPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(ScrollItemPattern.Pattern) as ScrollItemPattern;
            _virtualizedItemPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(VirtualizedItemPattern.Pattern) as VirtualizedItemPattern;
            _virtualizedItemPattern?.Realize();

            SetBackingProperties();
        }

        public EzListItem(AutomationElement element) : base(element)
        {
            TypeChecker.CheckElementType(element, ControlType.ListItem);
            _selectionItemPattern = element.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
            _scrollItemPattern = element.GetCurrentPattern(ScrollItemPattern.Pattern) as ScrollItemPattern;
            _virtualizedItemPattern = element.GetCurrentPattern(VirtualizedItemPattern.Pattern) as VirtualizedItemPattern;
            _virtualizedItemPattern?.Realize();

            SetBackingProperties();
        }

        /// <summary>
        /// Backing UI Automation SelectionItemPattern
        /// </summary>
        public SelectionItemPattern SelectionItemPattern => _selectionItemPattern;
        /// <summary>
        /// Backing UI Automation ScrollItemPattern
        /// </summary>
        public ScrollItemPattern ScrollItemPattern => _scrollItemPattern;
        /// <summary>
        /// Backing UI Automation VirtualizedItemPattern
        /// </summary>
        public VirtualizedItemPattern VirtualizedItemPattern => _virtualizedItemPattern;

        public EzList Container => _container;

        public void ScrollItemIntoView()
        {
            _scrollItemPattern.ScrollIntoView();
        }

        public void SelectItem()
        {
            _selectionItemPattern.Select();
        }

        public void AddItemToSelection()
        {
            if (_parentCanSelectMultiple)
                _selectionItemPattern.AddToSelection();
        }

        public void RemoveItemFromSelection()
        {
            if (_parentCanSelectMultiple)
                _selectionItemPattern.RemoveFromSelection();
        }

        private void SetBackingProperties()
        {
            var container = new EzList(_selectionItemPattern.Current.SelectionContainer);
            _container = container;

            _parentCanSelectMultiple = container.CanSelectMultiple;
        }
    }
}
