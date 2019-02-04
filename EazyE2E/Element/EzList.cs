//Copyright 2019 Ian Duckworth

using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using EazyE2E.Configuration;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
	/// <summary>
	/// Houses all functionality for list elements.  Note that the underlying element must have a ControlType of List
	/// </summary>
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
            _scrollPattern = element.BackingAutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
            _selectionPattern = element.BackingAutomationElement.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
        }

        /// <summary>
        /// Creates a new instance of EzList based on an EzRoot
        /// </summary>
        /// <param name="root"></param>
        public EzList(EzRoot root) : base(root)
        {
            TypeChecker.CheckElementType(root.RootElement.BackingAutomationElement, ControlType.List);
            _scrollPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
            _selectionPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
        }

        /// <summary>
        /// Creates a new instance of EzList based an an AutomationElement from Windows' automation framework
        /// </summary>
        /// <param name="element"></param>
        public EzList(AutomationElement element) : base(element)
        {
            TypeChecker.CheckElementType(element, ControlType.List);
            _scrollPattern = element.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
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

		/// <summary>
		/// Current amount that the element is scrolled vertically
		/// </summary>
        public double VerticalScrollPct => _scrollPattern.Current.VerticalScrollPercent;

		/// <summary>
		/// Current amount that the element is scrolled horizontally
		/// </summary>
        public double HorizontalScrollPct => _scrollPattern.Current.HorizontalScrollPercent;

		/// <summary>
		/// The vertical size of the viewable region as a percentage of the total content area within the UI Automation element
		/// </summary>
		public double VerticalViewSize => _scrollPattern.Current.VerticalViewSize;

		/// <summary>
		/// The horizontal size of the viewable region as a percentage of the total content area within the UI Automation element.
		/// </summary>
		public double HorizontalViewSize => _scrollPattern.Current.HorizontalViewSize;

		/// <summary>
		/// Whether or not a user can select multiple items in a list simultaneously
		/// </summary>
        public bool CanSelectMultiple => _selectionPattern.Current.CanSelectMultiple;

		/// <summary>
		/// Whether or not an item in the list must be selected
		/// </summary>
        public bool IsSelectionRequired => _selectionPattern.Current.IsSelectionRequired;

		/// <summary>
		/// All currently selected items in the list
		/// </summary>
        public IEnumerable<EzElement> SelectedItems => GetSelection(); 

		/// <summary>
		/// Scrolls vertically by a specified amount
		/// </summary>
		/// <param name="amount"></param>
        public void ScrollVertical(ScrollAmount amount)
        {
            _scrollPattern.Scroll(ScrollAmount.NoAmount, amount);
        }

		/// <summary>
		/// Scrolls horizontally by a specified amount
		/// </summary>
		/// <param name="amount"></param>
        public void ScrollHorizontal(ScrollAmount amount)
        {
            if (_scrollPattern.Current.HorizontallyScrollable)
                _scrollPattern.Scroll(amount, ScrollAmount.NoAmount);
        }

		/// <summary>
		/// Scrolls horizontally and vertically at the same time by specified amounts
		/// </summary>
		/// <param name="horizontalAmount"></param>
		/// <param name="verticalAmount"></param>
        public void ScrollHorizontalAndVertical(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            if (_scrollPattern.Current.VerticallyScrollable && _scrollPattern.Current.HorizontallyScrollable)
                _scrollPattern.Scroll(horizontalAmount, verticalAmount);
        }

		/// <summary>
		/// Sets the horizontal scroll to a specific percent
		/// </summary>
		/// <param name="percent"></param>
        public void SetHorizontalScrollPct(double percent)
        {
            if (_scrollPattern.Current.HorizontallyScrollable)
                _scrollPattern.SetScrollPercent(percent, ScrollPattern.NoScroll);
        }

		/// <summary>
		/// Sets the vertical scroll to a specific percent
		/// </summary>
		/// <param name="percent"></param>
        public void SetVerticalScrollPct(double percent)
        {
            if (_scrollPattern.Current.VerticallyScrollable)
                _scrollPattern.SetScrollPercent(ScrollPattern.NoScroll, percent);
        }

		/// <summary>
		/// Sets the horizontal and vertical scrolls to specified percents simultaneiously
		/// </summary>
		/// <param name="horizontalPercent"></param>
		/// <param name="verticalPercent"></param>
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
