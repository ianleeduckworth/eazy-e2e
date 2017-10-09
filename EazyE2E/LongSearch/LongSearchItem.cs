using EazyE2E.Enums;

namespace EazyE2E.LongSearch
{
    public class LongSearchItem
    {
        public LongSearchItem(PropertyType type, string paramName)
        {
            this.PropertyType = type;
            this.ParamName = paramName;
        }

        public PropertyType PropertyType { get; }

        public string ParamName { get; }
    }
}
