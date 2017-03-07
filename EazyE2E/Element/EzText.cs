using System.Windows.Automation;
using System.Windows.Automation.Text;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    /// <summary>
    /// Represents a class that will get text attributes.
    /// <para>Note that if you are using this class to check an element whose text is changing, make sure to call Reset() in order to update all properties on this class</para>
    /// </summary>
    public class EzText : EzElement
    {
        private readonly TextPattern _textPattern;

        private string _fontName;
        private double? _fontSize;
        private int? _fontWeight;
        private string _backgroundColor;
        private int? _foregroundColor;
        private HorizontalTextAlignment? _horizontalAlignment;
        private bool? _isHidden;
        private bool? _isReadOnly;
        private bool? _isItalic;
        private bool? _isSubscript;
        private bool? _isSuperscript;
        private int? _firstLineIndentation;
        private double? _indentationLeading;
        private double? _indentationTrailing;
        private double? _marginTop;
        private double? _marginBottom;
        private OutlineStyles? _outlineStyle;
        private int? _overlineColor;
        private TextDecorationLineStyle? _overlineStyle;
        private int? _strikethroughColor;
        private TextDecorationLineStyle? _strikethroughStyle;
        private int? _underlineColor;
        private TextDecorationLineStyle? _underlineStyle;
        private double[] _tabStops;
        private FlowDirections? _textFlowDirection;
        private string _value;


        public EzText(EzRoot root) : base(root)
        {
            TypeChecker.CheckElementType(root.RootElement.BackingAutomationElement, ControlType.Text);
            _textPattern = root.RootElement.BackingAutomationElement.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
        }

        public EzText(AutomationElement element) : base(element)
        {
            TypeChecker.CheckElementType(element, ControlType.Text);
            _textPattern = element.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
        }

        /// <summary>
        /// Backing UI Automation TextPattern
        /// </summary>
        public TextPattern TextPattern => _textPattern;

        /// <summary>
        /// Gets the name of the font used for the text element
        /// </summary>
        public string FontName => _fontName ?? (_fontName = (string)_textPattern.DocumentRange.GetAttributeValue(TextPattern.FontNameAttribute));
        /// <summary>
        /// Gets the font size used for the text element
        /// </summary>
        public double FontSize => (double)(_fontSize ?? (_fontSize = (double)_textPattern.DocumentRange.GetAttributeValue(TextPattern.FontSizeAttribute)));
        /// <summary>
        /// Gets the font weight used for the text element
        /// </summary>
        public int FontWeight => (int)(_fontWeight ?? (_fontWeight = (int)_textPattern.DocumentRange.GetAttributeValue(TextPattern.FontWeightAttribute)));
        /// <summary>
        /// Gets the background color used for the text element
        /// </summary>
        public string BackgroundColor => _backgroundColor ?? (_backgroundColor = (string)_textPattern.DocumentRange.GetAttributeValue(TextPattern.BackgroundColorAttribute));
        /// <summary>
        /// Gets the foreground color used for the text element
        /// </summary>
        public int ForegroundColor => (int)(_foregroundColor ?? (_foregroundColor = (int)_textPattern.DocumentRange.GetAttributeValue(TextPattern.ForegroundColorAttribute)));
        /// <summary>
        /// Gets the horizontal alighment used for the text element
        /// </summary>
        public HorizontalTextAlignment HorizontalAlignment => (HorizontalTextAlignment)(_horizontalAlignment ?? (_horizontalAlignment = (HorizontalTextAlignment)_textPattern.DocumentRange.GetAttributeValue(TextPattern.HorizontalTextAlignmentAttribute)));
        /// <summary>
        /// Checks whether or not the text is hidden
        /// </summary>
        public bool IsHidden => (bool)(_isHidden ?? (_isHidden = (bool)_textPattern.DocumentRange.GetAttributeValue(TextPattern.IsHiddenAttribute)));
        /// <summary>
        /// Checks whether or not the text is readonly
        /// </summary>
        public bool IsReadOnly => (bool)(_isReadOnly ?? (_isReadOnly = (bool)_textPattern.DocumentRange.GetAttributeValue(TextPattern.IsReadOnlyAttribute)));
        /// <summary>
        /// Checks whether or not the text is italic
        /// </summary>
        public bool IsItalic => (bool)(_isItalic ?? (_isItalic = (bool)_textPattern.DocumentRange.GetAttributeValue(TextPattern.IsItalicAttribute)));
        /// <summary>
        /// Checks whether or not the text is subscript
        /// </summary>
        public bool IsSubscript => (bool)(_isSubscript ?? (_isSubscript = (bool)_textPattern.DocumentRange.GetAttributeValue(TextPattern.IsSubscriptAttribute)));
        /// <summary>
        /// Checks whether or not the text is superscript
        /// </summary>
        public bool IsSuperscript => (bool)(_isSuperscript ?? (_isSuperscript = (bool)_textPattern.DocumentRange.GetAttributeValue(TextPattern.IsSuperscriptAttribute)));
        /// <summary>
        /// Gets the first line's indentation for the text element
        /// </summary>
        public int FirstLineIndentation => (int)(_firstLineIndentation ?? (_firstLineIndentation = (int)_textPattern.DocumentRange.GetAttributeValue(TextPattern.IndentationFirstLineAttribute)));
        /// <summary>
        /// Gets the leading indentation for the text element
        /// </summary>
        public double IndentationLeading => (double)(_indentationLeading ?? (_indentationLeading = (double)_textPattern.DocumentRange.GetAttributeValue(TextPattern.IndentationLeadingAttribute)));
        /// <summary>
        /// Gets the trailing indentation for the text element
        /// </summary>
        public double IndentationTrailing => (double)(_indentationTrailing ?? (_indentationTrailing = (double)_textPattern.DocumentRange.GetAttributeValue(TextPattern.IndentationTrailingAttribute)));
        /// <summary>
        /// Gets the top margin for the text element
        /// </summary>
        public double MarginTop => (double)(_marginTop ?? (_marginTop = (double)_textPattern.DocumentRange.GetAttributeValue(TextPattern.MarginTopAttribute)));
        /// <summary>
        /// Gets the bottom margin for the text element
        /// </summary>
        public double MarginBottom => (double)(_marginBottom ?? (_marginBottom = (double)_textPattern.DocumentRange.GetAttributeValue(TextPattern.MarginBottomAttribute)));
        /// <summary>
        /// Gets the outline style of the text element
        /// </summary>
        public OutlineStyles OutlineStyle => (OutlineStyles)(_outlineStyle ?? (_outlineStyle = (OutlineStyles)_textPattern.DocumentRange.GetAttributeValue(TextPattern.OutlineStylesAttribute)));
        /// <summary>
        /// Gets the overline color of the text element
        /// </summary>
        public int OverlineColor => (int)(_overlineColor ?? (_overlineColor = (int)_textPattern.DocumentRange.GetAttributeValue(TextPattern.OverlineColorAttribute)));
        /// <summary>
        /// Gets the overline style of the text element
        /// </summary>
        public TextDecorationLineStyle OverlineStyle => (TextDecorationLineStyle)(_overlineStyle ?? (_overlineStyle = (TextDecorationLineStyle)_textPattern.DocumentRange.GetAttributeValue(TextPattern.OverlineStyleAttribute)));
        /// <summary>
        /// Gets the strikethrough color of the text element
        /// </summary>
        public int StrikethroughColor => (int)(_strikethroughColor ?? (_strikethroughColor = (int)_textPattern.DocumentRange.GetAttributeValue(TextPattern.StrikethroughColorAttribute)));
        /// <summary>
        /// Gets the strikethrough style of the text element
        /// </summary>
        public TextDecorationLineStyle StrikethroughStyle => (TextDecorationLineStyle)(_strikethroughStyle ?? (_strikethroughStyle = (TextDecorationLineStyle)_textPattern.DocumentRange.GetAttributeValue(TextPattern.StrikethroughStyleAttribute)));
        /// <summary>
        /// Gets the underline color of the text element
        /// </summary>
        public int UnderlineColor => (int)(_underlineColor ?? (_underlineColor = (int)_textPattern.DocumentRange.GetAttributeValue(TextPattern.UnderlineColorAttribute)));
        /// <summary>
        /// Gets the underline style of the text element
        /// </summary>
        public TextDecorationLineStyle UnderlineStyle => (TextDecorationLineStyle)(_underlineStyle ?? (_underlineStyle = (TextDecorationLineStyle)_textPattern.DocumentRange.GetAttributeValue(TextPattern.UnderlineStyleAttribute)));
        /// <summary>
        /// Gets an array of tab stops in points in the text element
        /// </summary>
        public double[] TabStops => _tabStops.Length == 0 ? _tabStops = (double[])_textPattern.DocumentRange.GetAttributeValue(TextPattern.TabsAttribute) : _tabStops;
        /// <summary>
        /// Gets the flow direction of the text in the text element
        /// </summary>
        public FlowDirections TextFlowDirection => (FlowDirections)(_textFlowDirection ?? (_textFlowDirection = (FlowDirections)_textPattern.DocumentRange.GetAttributeValue(TextPattern.TextFlowDirectionsAttribute)));

        /// <summary>
        /// Gets the current value of the text element's text
        /// </summary>
        /// <param name="maxLength">Defines the max length of the returned string; once the max length is reached the string will cut off.  Param is -1 by default which means that no max length is defined.</param>
        /// <returns></returns>
        public string GetTextValue(int maxLength = -1)
        {
            return _value ?? (_value = _textPattern.DocumentRange.GetText(maxLength));
        }

        /// <summary>
        /// If any underlying text elements have changed, it is necessary to call this method in order to have this class re-query the element for values
        /// </summary>
        public void Reset()
        {
            _fontName = null;
            _fontSize = null;
            _fontWeight = null;
            _backgroundColor = null;
            _foregroundColor = null;
            _horizontalAlignment = null;
            _isHidden = null;
            _isReadOnly = null;
            _isItalic = null;
            _isSubscript = null;
            _isSuperscript = null;
            _firstLineIndentation = null;
            _indentationLeading = null;
            _indentationTrailing = null;
            _marginTop = null;
            _marginBottom = null;
            _outlineStyle = null;
            _overlineColor = null;
            _overlineStyle = null;
            _strikethroughColor = null;
            _strikethroughStyle = null;
            _underlineColor = null;
            _underlineStyle = null;
            _tabStops = null;
            _textFlowDirection = null;
            _value = null;
        }
    }
}
