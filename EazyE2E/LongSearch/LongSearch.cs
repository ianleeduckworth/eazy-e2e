//Copyright 2018 Ian Duckworth

using System;
using EazyE2E.Element;
using EazyE2E.Enums;

namespace EazyE2E.LongSearch
{
    /// <summary>
    /// This class allows the automation engineer to write efficient search code more easily and elegantly without having to save a variable for each way point in the UI Automation Tree.
    /// If you find yourself saving variables for EzElements which you only need for navigation to another element further down the tree, consider using this class
    /// </summary>
    public static class LongSearch
    {
        /// <summary>
        /// Performs a Long Search.  This method will look through all the params that you pass in with the searches object and it will find each one in succession.  Once it has walked
        /// through all elements mentioned, it will return the destination element.
        /// </summary>
        /// <param name="startingElement">The element on which to begin the search</param>
        /// <param name="searches">A list of parameters to act as way points to navigate the UI Automation tree</param>
        /// <returns>The EzElement that is found based on search items</returns>
        public static EzElement PerformSearch(EzElement startingElement, params LongSearchItem[] searches)
        {
            foreach (var search in searches)
            {
                switch (search.PropertyType)
                {
                    case PropertyType.AutomationId:
                        startingElement = startingElement.FindChildByAutomationId(search.ParamName);
                        break;
                    case PropertyType.Class:
                        startingElement = startingElement.FindChildByClass(search.ParamName);
                        break;
                    case PropertyType.Name:
                        startingElement = startingElement.FindChildByName(search.ParamName);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"PropertyType passed in was not recognized.  ${search.PropertyType} is invalid");
                }
            }

            return startingElement;
        }
    }
}
