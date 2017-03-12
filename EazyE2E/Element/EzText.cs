using System;
using System.Runtime.Remoting.Messaging;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using EazyE2E.ElementHelper;
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

        private EzTextResult<string> _fontName;
        private EzTextResult<double> _fontSize;
        private EzTextResult<int> _fontWeight;
        private EzTextResult<int> _backgroundColor;
        private EzTextResult<int> _foregroundColor;
        private EzTextResult<HorizontalTextAlignment> _horizontalAlignment;
        private EzTextResult<bool> _isHidden;
        private EzTextResult<bool> _isReadOnly;
        private EzTextResult<bool> _isItalic;
        private EzTextResult<bool> _isSubscript;
        private EzTextResult<bool> _isSuperscript;
        private EzTextResult<int> _firstLineIndentation;
        private EzTextResult<double> _indentationLeading;
        private EzTextResult<double> _indentationTrailing;
        private EzTextResult<double> _marginTop;
        private EzTextResult<double> _marginBottom;
        private EzTextResult<OutlineStyles> _outlineStyle;
        private EzTextResult<int> _overlineColor;
        private EzTextResult<TextDecorationLineStyle> _overlineStyle;
        private EzTextResult<int> _strikethroughColor;
        private EzTextResult<TextDecorationLineStyle> _strikethroughStyle;
        private EzTextResult<int> _underlineColor;
        private EzTextResult<TextDecorationLineStyle> _underlineStyle;
        private EzTextResult<double[]> _tabStops;
        private EzTextResult<FlowDirections> _textFlowDirection;
        private string _value;


        public EzText(EzElement element) : base(element)
        {
            TypeChecker.CheckElementType(element.BackingAutomationElement, ControlType.Text);
            _textPattern = element.BackingAutomationElement.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
        }

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
        public EzTextResult<string> FontName => GetEzTextProp(_fontName, TextPattern.FontNameAttribute);

        /// <summary>
        /// Gets the font size used for the text element
        /// </summary>
        public EzTextResult<double> FontSize => GetEzTextProp(_fontSize, TextPattern.FontSizeAttribute);

        /// <summary>
        /// Gets the font weight used for the text element
        /// </summary>
        public EzTextResult<int> FontWeight => GetEzTextProp(_fontWeight, TextPattern.FontWeightAttribute);

        /// <summary>
        /// Gets the background color used for the text element
        /// </summary>
        public EzTextResult<int> BackgroundColor => GetEzTextProp(_backgroundColor, TextPattern.BackgroundColorAttribute);

        /// <summary>
        /// Gets the foreground color used for the text element
        /// </summary>
        public EzTextResult<int> ForegroundColor => GetEzTextProp(_foregroundColor, TextPattern.ForegroundColorAttribute);

        /// <summary>
        /// Gets the horizontal alighment used for the text element
        /// </summary>
        public EzTextResult<HorizontalTextAlignment> HorizontalAlignment => GetEzTextProp(_horizontalAlignment, TextPattern.HorizontalTextAlignmentAttribute);

        /// <summary>
        /// Checks whether or not the text is hidden
        /// </summary>
        public EzTextResult<bool> IsHidden => GetEzTextProp(_isHidden, TextPattern.IsHiddenAttribute);

        /// <summary>
        /// Checks whether or not the text is readonly
        /// </summary>
        public EzTextResult<bool> IsReadOnly => GetEzTextProp(_isReadOnly, TextPattern.IsReadOnlyAttribute);

        /// <summary>
        /// Checks whether or not the text is italic
        /// </summary>
        public EzTextResult<bool> IsItalic => GetEzTextProp(_isItalic, TextPattern.IsItalicAttribute);

        /// <summary>
        /// Checks whether or not the text is subscript
        /// </summary>
        public EzTextResult<bool> IsSubscript => GetEzTextProp(_isSubscript, TextPattern.IsSubscriptAttribute);

        /// <summary>
        /// Checks whether or not the text is superscript
        /// </summary>
        public EzTextResult<bool> IsSuperscript => GetEzTextProp(_isSuperscript, TextPattern.IsSuperscriptAttribute);

        /// <summary>
        /// Gets the first line's indentation for the text element
        /// </summary>
        public EzTextResult<int> FirstLineIndentation => GetEzTextProp(_firstLineIndentation, TextPattern.IndentationFirstLineAttribute);

        /// <summary>
        /// Gets the leading indentation for the text element
        /// </summary>
        public EzTextResult<double> IndentationLeading => GetEzTextProp(_indentationLeading, TextPattern.IndentationLeadingAttribute);

        /// <summary>
        /// Gets the trailing indentation for the text element
        /// </summary>
        public EzTextResult<double> IndentationTrailing => GetEzTextProp(_indentationTrailing, TextPattern.IndentationTrailingAttribute);

        /// <summary>
        /// Gets the top margin for the text element
        /// </summary>
        public EzTextResult<double> MarginTop => GetEzTextProp(_marginTop, TextPattern.MarginTopAttribute);

        /// <summary>
        /// Gets the bottom margin for the text element
        /// </summary>
        public EzTextResult<double> MarginBottom => GetEzTextProp(_marginBottom, TextPattern.MarginBottomAttribute);

        /// <summary>
        /// Gets the outline style of the text element
        /// </summary>
        public EzTextResult<OutlineStyles> OutlineStyle => GetEzTextProp(_outlineStyle, TextPattern.OutlineStylesAttribute);

        /// <summary>
        /// Gets the overline color of the text element
        /// </summary>
        public EzTextResult<int> OverlineColor => GetEzTextProp(_overlineColor, TextPattern.OverlineColorAttribute);

        /// <summary>
        /// Gets the overline style of the text element
        /// </summary>
        public EzTextResult<TextDecorationLineStyle> OverlineStyle => GetEzTextProp(_overlineStyle, TextPattern.OverlineStyleAttribute);

        /// <summary>
        /// Gets the strikethrough color of the text element
        /// </summary>
        public EzTextResult<int> StrikethroughColor => GetEzTextProp(_strikethroughColor, TextPattern.StrikethroughColorAttribute);

        /// <summary>
        /// Gets the strikethrough style of the text element
        /// </summary>
        public EzTextResult<TextDecorationLineStyle> StrikethroughStyle => GetEzTextProp(_strikethroughStyle, TextPattern.StrikethroughStyleAttribute);

        /// <summary>
        /// Gets the underline color of the text element
        /// </summary>
        public EzTextResult<int> UnderlineColor => GetEzTextProp(_underlineColor, TextPattern.UnderlineColorAttribute);

        /// <summary>
        /// Gets the underline style of the text element
        /// </summary>
        public EzTextResult<TextDecorationLineStyle> UnderlineStyle => GetEzTextProp(_underlineStyle, TextPattern.UnderlineStyleAttribute);

        /// <summary>
        /// Gets an array of tab stops in points in the text element
        /// </summary>
        public EzTextResult<double[]> TabStops => GetEzTextProp(_tabStops, TextPattern.TabsAttribute);

        /// <summary>
        /// Gets the flow direction of the text in the text element
        /// </summary>
        public EzTextResult<FlowDirections> TextFlowDirection => GetEzTextProp(_textFlowDirection, TextPattern.TextFlowDirectionsAttribute);

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

        private EzTextResult<T> GetEzTextProp<T>(EzTextResult<T> instance, AutomationTextAttribute attribute)
        {
            if (instance != null) return instance;

            var result = new EzTextResult<T>();
            instance = SetResult(result, _textPattern.DocumentRange.GetAttributeValue(attribute));

            return instance;
        }

        private static EzTextResult<T> SetResult<T>(EzTextResult<T> objectToSet, object input)
        {
            objectToSet.NotSupported = input == AutomationElement.NotSupported;
            if (objectToSet.NotSupported)
            {
                objectToSet.Result = default(T);
                return objectToSet;
            }

            objectToSet.IsMixed = input == TextPattern.MixedAttributeValue;
            if (objectToSet.IsMixed)
            {
                objectToSet.Result = default(T);
                return objectToSet;
            }

            objectToSet.Result = (T)input;
            return objectToSet;
        }

    }
}
