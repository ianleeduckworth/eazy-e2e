//Copyright 2019 Ian Duckworth

using EazyE2E.Enums;

namespace EazyE2E.Helper
{
	/// <summary>
	/// Acts as a key value pair for searching by multiple criteria
	/// </summary>
    public class SearchTypeProperty
    {
        /// <summary>
        /// PropertyType of the current search property
        /// </summary>
        public PropertyType PropertyType { get; set; }

        /// <summary>
        /// Name of the current search property
        /// </summary>
        public string Name { get; set; }
    }
}
