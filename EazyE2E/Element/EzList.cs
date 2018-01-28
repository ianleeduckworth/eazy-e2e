//Copyright 2018 Ian Duckworth

using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using EazyE2E.Configuration;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    public class EzList : EzElement
    {
        private readonly ScrollPattern _scrollPattern;
        private readonly SelectionPattern _selectionPattern;

        /// <summary>
        /// Creates a new instance of EzList based on an EzElement
        /// </summary>
        /// <param name="element"></param>
        public EzList(EzElement element) : base(element)
        {
            TypeChecker.CheckElementType(element.BackingAutomationElement, ControlType.List);
            _scrollPattern = element.BackingAutomationElement.GetCurrentPattern(System.Windows.Automation.ScrollPattern.Pattern) as ScrollPattern;
            _selectionPattern = element.BackingAutomationElement.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
        }

        /// <summary>
        /// Creates a new instance of EzList based on an EzRoot
        /// </summary>
        /// <param name="root"></param>
        public EzList(EzRoot root) : base(root)
        {
            TypeChecker.CheckElementType(root.RootElement.BackingAutomationElement, ControlType.List);
            _scrollPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(System.Windows.Automation.ScrollPattern.Pattern) as ScrollPattern;
            _selectionPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
        }

        /// <summary>
        /// Creates a new instance of EzList based an an AutomationElement from Windows' automation framework
        /// </summary>
        /// <param name="element"></param>
        public EzList(AutomationElement element) : base(element)
        {
            TypeChecker.CheckElementType(element, ControlType.List);
            _scrollPattern = element.GetCurrentPattern(System.Windows.Automation.ScrollPattern.Pattern) as ScrollPattern;
            _selectionPattern = element.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
        }

        /// <summary>
        /// Backing UI Automation ScrollPattern
        /// </summary>
        public ScrollPattern ScrollPattern => Config.ExposeBackingWindowsPatterns ? _scrollPattern : null;
        /// <summary>
        /// Backing UI Automation SelectionPattern
        /// </summary>
        public SelectionPattern SelectionPattern => Config.ExposeBackingWindowsPatterns ? _selectionPattern : null;

        public double VerticalScrollPct => _scrollPattern.Current.VerticalScrollPercent;
        public double HorizontalScrollPct => _scrollPattern.Current.HorizontalScrollPercent;
        public double VerticalViewSize => _scrollPattern.Current.VerticalViewSize;
        public double HorizontalViewSize => _scrollPattern.Current.HorizontalViewSize;
        public bool CanSelectMultiple => _selectionPattern.Current.CanSelectMultiple;
        public bool IsSelectionRequired => _selectionPattern.Current.IsSelectionRequired;
        public IEnumerable<EzElement> SelectedItems => GetSelection(); 

        public void ScrollVertical(ScrollAmount amount)
        {
            _scrollPattern.Scroll(ScrollAmount.NoAmount, amount);
        }

        public void ScrollHorizontal(ScrollAmount amount)
        {
            if (_scrollPattern.Current.HorizontallyScrollable)
                _scrollPattern.Scroll(amount, ScrollAmount.NoAmount);
        }

        public void ScrollHorizontalAndVertical(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            if (_scrollPattern.Current.VerticallyScrollable && _scrollPattern.Current.HorizontallyScrollable)
                _scrollPattern.Scroll(horizontalAmount, verticalAmount);
        }

        public void SetHorizontalScrollPct(double percent)
        {
            if (_scrollPattern.Current.HorizontallyScrollable)
                _scrollPattern.SetScrollPercent(percent, System.Windows.Automation.ScrollPattern.NoScroll);
        }

        public void SetVerticalScrollPct(double percent)
        {
            if (_scrollPattern.Current.VerticallyScrollable)
                _scrollPattern.SetScrollPercent(System.Windows.Automation.ScrollPattern.NoScroll, percent);
        }

        public void SetVerticalAndHorizontalScrollPct(double horizontalPercent, double verticalPercent)
        {
            if (_scrollPattern.Current.VerticallyScrollable && _scrollPattern.Current.HorizontallyScrollable)
                _scrollPattern.SetScrollPercent(horizontalPercent, verticalPercent);
        }

        private IEnumerable<EzElement> GetSelection()
        {
            return _selectionPattern.Current.GetSelection().Select(x => new EzElement(x));
        }


    }
}
