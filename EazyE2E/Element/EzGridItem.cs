using System.Windows.Automation;
using EazyE2E.Configuration;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    public class EzGridItem : EzElement
    {
        private readonly GridItemPattern _gridItemPattern;

        public EzGridItem(EzElement element) : base(element)
        {
            TypeChecker.CheckElementType(element.BackingAutomationElement, ControlType.DataItem);
            _gridItemPattern = element.BackingAutomationElement.GetCurrentPattern(GridItemPattern.Pattern) as GridItemPattern;
        }

        public EzGridItem(EzRoot root) : base(root)
        {
            TypeChecker.CheckElementType(root.RootElement.BackingAutomationElement, ControlType.DataItem);
            _gridItemPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(GridItemPattern.Pattern) as GridItemPattern;
        }

        public EzGridItem(AutomationElement element) : base(element)
        {
            TypeChecker.CheckElementType(element, ControlType.DataItem);
            _gridItemPattern = element.GetCurrentPattern(GridItemPattern.Pattern) as GridItemPattern;
        }

        /// <summary>
        /// Backing UI Automation GridItemPattern
        /// </summary>
        public GridItemPattern GridItemPattern => Config.ExposeBackingWindowsPatterns ? _gridItemPattern : null;

        public int RowNum => _gridItemPattern.Current.Row;
        public int RowSpan => _gridItemPattern.Current.RowSpan;
        public int ColumnNum => _gridItemPattern.Current.Column;
        public int ColumnSpan => _gridItemPattern.Current.ColumnSpan;
    }
}
