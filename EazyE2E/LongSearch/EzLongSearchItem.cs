using EazyE2E.Enums;

namespace EazyE2E.LongSearch
{
    /// <summary>
    /// Class EzLongSearchItem to be used with in the scope of EzLongSearch
    /// </summary>
    public class EzLongSearchItem
    {
        /// <summary>
        /// Constructor for EzLongSearchItem.  Defines what property to search by and the name associated with the property
        /// </summary>
        /// <param name="type"></param>
        /// <param name="paramName"></param>
        public EzLongSearchItem(PropertyType type, string paramName)
        {
            this.PropertyType = type;
            this.ParamName = paramName;
        }

        /// <summary>
        /// PropertyType to be searched by
        /// </summary>
        public PropertyType PropertyType { get; }

        /// <summary>
        /// Name of property being searched by
        /// </summary>
        public string ParamName { get; }
    }
}
