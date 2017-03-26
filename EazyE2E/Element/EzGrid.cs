using System.Windows.Automation;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    public class EzGrid : EzElement
    {
        private readonly GridPattern _gridPattern;

        public EzGrid(EzElement element) : base(element)
        {
            TypeChecker.CheckElementType(element.BackingAutomationElement, ControlType.DataGrid);
            _gridPattern = element.BackingAutomationElement.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }

        public EzGrid(EzRoot root) : base(root)
        {
            TypeChecker.CheckElementType(root.RootElement.BackingAutomationElement, ControlType.DataGrid);
            _gridPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }

        public EzGrid(AutomationElement element) : base(element)
        {
            TypeChecker.CheckElementType(element, ControlType.DataGrid);
            _gridPattern = element.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }
        /// <summary>
        /// Backing UI Automation GridPattern
        /// </summary>
        public GridPattern GridPattern => _gridPattern;

        public int ColumnCount => _gridPattern.Current.ColumnCount;
        public int RowCount => _gridPattern.Current.RowCount;
    }
}
