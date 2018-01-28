//Copyright 2018 Ian Duckworth

using EazyE2E.Enums;

namespace EazyE2E.LongSearch
{
    /// <summary>
    /// Class LongSearchItem to be used with in the scope of LongSearch
    /// </summary>
    public class LongSearchItem
    {
        /// <summary>
        /// Constructor for LongSearchItem.  Defines what property to search by and the name associated with the property
        /// </summary>
        /// <param name="type"></param>
        /// <param name="paramName"></param>
        public LongSearchItem(PropertyType type, string paramName)
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
