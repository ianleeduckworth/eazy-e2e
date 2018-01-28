//Copyright 2018 Ian Duckworth

using System.Windows.Automation;
using EazyE2E.Configuration;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    public class EzGridItem : EzElement
    {
        private readonly GridItemPattern _gridItemPattern;

        /// <summary>
        /// Creates a new instance of EzGridItem based on an EzElement
        /// </summary>
        /// <param name="element"></param>
        public EzGridItem(EzElement element) : base(element)
        {
            TypeChecker.CheckElementType(element.BackingAutomationElement, ControlType.DataItem);
            _gridItemPattern = element.BackingAutomationElement.GetCurrentPattern(GridItemPattern.Pattern) as GridItemPattern;
        }

        /// <summary>
        /// Creates a new instance of EzGridItem based on an EzRoot
        /// </summary>
        /// <param name="root"></param>
        public EzGridItem(EzRoot root) : base(root)
        {
            TypeChecker.CheckElementType(root.RootElement.BackingAutomationElement, ControlType.DataItem);
            _gridItemPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(GridItemPattern.Pattern) as GridItemPattern;
        }

        /// <summary>
        /// Creates a new instance of EzGridItem based on an Automation Element from Windows' automation framework
        /// </summary>
        /// <param name="element"></param>
        public EzGridItem(AutomationElement element) : base(element)
        {
            TypeChecker.CheckElementType(element, ControlType.DataItem);
            _gridItemPattern = element.GetCurrentPattern(GridItemPattern.Pattern) as GridItemPattern;
        }

        /// <summary>
        /// Backing UI Automation GridItemPattern.  Will be null if Config.ExposeBackingWindowsPatterns is false
        /// </summary>
        public GridItemPattern GridItemPattern => Config.ExposeBackingWindowsPatterns ? _gridItemPattern : null;

        /// <summary>
        /// The current row number relative to the backing grid
        /// </summary>
        public int RowNum => _gridItemPattern.Current.Row;

        /// <summary>
        /// The current row span relative to the backing grid.  Span is the number of rows spanned by a cell or item.
        /// </summary>
        public int RowSpan => _gridItemPattern.Current.RowSpan;

        /// <summary>
        /// The current column number relative to the backing grid
        /// </summary>
        public int ColumnNum => _gridItemPattern.Current.Column;

        /// <summary>
        /// The current column span relative to the backing grid.  Span is the number of columns spanned by a cell or item.
        /// </summary>
        public int ColumnSpan => _gridItemPattern.Current.ColumnSpan;
    }
}
