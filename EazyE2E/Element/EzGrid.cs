//Copyright 2018 Ian Duckworth

using System.Windows.Automation;
using EazyE2E.Configuration;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    public class EzGrid : EzElement
    {
        private readonly GridPattern _gridPattern;

        /// <summary>
        /// Creates an instance of EzGrid based on an EzElement
        /// </summary>
        /// <param name="element"></param>
        public EzGrid(EzElement element) : base(element)
        {
            TypeChecker.CheckElementType(element.BackingAutomationElement, ControlType.DataGrid);
            _gridPattern = element.BackingAutomationElement.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }

        /// <summary>
        /// Creates an instance of EzGrid based on an EzRoot element
        /// </summary>
        /// <param name="root"></param>
        public EzGrid(EzRoot root) : base(root)
        {
            TypeChecker.CheckElementType(root.RootElement.BackingAutomationElement, ControlType.DataGrid);
            _gridPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }

        /// <summary>
        /// Creates an instance of EzGrid based on an AutomationElement (from Windows automation framework)
        /// </summary>
        /// <param name="element"></param>
        public EzGrid(AutomationElement element) : base(element)
        {
            TypeChecker.CheckElementType(element, ControlType.DataGrid);
            _gridPattern = element.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }

        /// <summary>
        /// Backing UI Automation GridPattern.  Will be null if Config.ExposeBackingWindowsPatterns is false
        /// </summary>
        public GridPattern GridPattern => Config.ExposeBackingWindowsPatterns ? _gridPattern : null;

        /// <summary>
        /// The number of columns in the current grid
        /// </summary>
        public int ColumnCount => _gridPattern.Current.ColumnCount;

        /// <summary>
        /// The number of rows in the current grid
        /// </summary>
        public int RowCount => _gridPattern.Current.RowCount;
    }
}
