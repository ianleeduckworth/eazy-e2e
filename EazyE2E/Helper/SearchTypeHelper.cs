//Copyright 2019 Ian Duckworth

using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using EazyE2E.Enums;

namespace EazyE2E.Helper
{
	/// <summary>
	/// Provides some helper functionality that allows for easier searching
	/// </summary>
    public static class SearchTypeHelper
    {
        private static Dictionary<PropertyType, AutomationProperty> _backingDictionary;

        /// <summary>
        /// Returns an AutomationProperty based on the PropertyType passed in; used for converting between the two
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AutomationProperty GetAutomationProperty(PropertyType type)
        {
            if (_backingDictionary == null) _backingDictionary = BuildBackingDictionary();
            return _backingDictionary.FirstOrDefault(x => x.Key == type).Value;
        }

        private static Dictionary<PropertyType, AutomationProperty> BuildBackingDictionary()
        {
            var d = new Dictionary<PropertyType, AutomationProperty>
            {
                { PropertyType.AutomationId, AutomationElement.AutomationIdProperty},
                { PropertyType.Name, AutomationElement.NameProperty},
                { PropertyType.Class, AutomationElement.ClassNameProperty}
            };

            return d;
        }


    }
}
