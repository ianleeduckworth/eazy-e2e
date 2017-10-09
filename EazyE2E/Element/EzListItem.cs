using System.Windows.Automation;
using EazyE2E.Configuration;
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

        public EzListItem(EzElement element) : base(element)
        {
            TypeChecker.CheckElementType(element.BackingAutomationElement, ControlType.ListItem);
            _selectionItemPattern = element.BackingAutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
            _scrollItemPattern = element.BackingAutomationElement.GetCurrentPattern(ScrollItemPattern.Pattern) as ScrollItemPattern;
            _virtualizedItemPattern = element.BackingAutomationElement.GetCurrentPattern(VirtualizedItemPattern.Pattern) as VirtualizedItemPattern;
            _virtualizedItemPattern?.Realize();

            SetBackingProperties();
        }
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
        public SelectionItemPattern SelectionItemPattern => Config.ExposeBackingWindowsPatterns ? _selectionItemPattern : null;
        /// <summary>
        /// Backing UI Automation ScrollItemPattern
        /// </summary>
        public ScrollItemPattern ScrollItemPattern => Config.ExposeBackingWindowsPatterns ? _scrollItemPattern : null;
        /// <summary>
        /// Backing UI Automation VirtualizedItemPattern
        /// </summary>
        public VirtualizedItemPattern VirtualizedItemPattern => Config.ExposeBackingWindowsPatterns ? _virtualizedItemPattern : null;

        /// <summary>
        /// EzList item that is the container for this EzListItem
        /// </summary>
        public EzList Container => _container;

        /// <summary>
        /// Scrolls current EzListItem into view regardless of current scroll position
        /// </summary>
        public void ScrollItemIntoView()
        {
            _scrollItemPattern.ScrollIntoView();
        }

        /// <summary>
        /// Performs a select operation on current EzListItem
        /// </summary>
        public void SelectItem()
        {
            _selectionItemPattern.Select();
        }

        /// <summary>
        /// Adds this EzListItem to the current selection.  This method is for managing selection of multiple items
        /// </summary>
        public void AddItemToSelection()
        {
            if (_parentCanSelectMultiple)
                _selectionItemPattern.AddToSelection();
        }

        /// <summary>
        /// Removes this EzListItem from the current selection.  This method is for managing selection of multiple items
        /// </summary>
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
