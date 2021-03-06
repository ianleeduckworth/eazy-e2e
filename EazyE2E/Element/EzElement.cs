﻿//Copyright 2019 Ian Duckworth

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using EazyE2E.Configuration;
using EazyE2E.Enums;
using EazyE2E.Exceptions;
using EazyE2E.Helper;

namespace EazyE2E.Element
{
    /// <summary>
    /// Houses all element related functionality including querying for children/descendants and all information about the current element
    /// </summary>
    public class EzElement
    {
        private readonly AutomationElement _backingAutomationElement;
        private EzElement _parent;
        private readonly InvokePattern _invokePattern;

        /// <summary>
        /// Parent element of the current element.  Will be null if current element has no parent
        /// </summary>
        public EzElement Parent => _parent ?? (_parent = new EzElement(TreeWalker.RawViewWalker.GetParent(_backingAutomationElement)));

        /// <summary>
        /// Name of the current EzElement
        /// </summary>
        public string Name => _backingAutomationElement.Current.Name;

        /// <summary>
        /// AutomationId of the current EzElement
        /// </summary>
        public string AutomationId => _backingAutomationElement.Current.AutomationId;

        /// <summary>
        /// ClassName of the current EzElement
        /// </summary>
        public string ClassName => _backingAutomationElement.Current.ClassName;

        /// <summary>
        /// ControlType of the current EzElement
        /// </summary>
        public ControlType Type => _backingAutomationElement.Current.ControlType;

        /// <summary>
        /// Creates an Ezelement based on an instance of an existing EzElement
        /// </summary>
        /// <param name="element"></param>
        public EzElement(EzElement element)
        {
            _backingAutomationElement = element._backingAutomationElement;
            object pattern;
            element.BackingAutomationElement.TryGetCurrentPattern(InvokePattern.Pattern, out pattern);
            _invokePattern = pattern as InvokePattern;
        }

        /// <summary>
        /// Creates an instance of EzElement using a backing EzRoot
        /// </summary>
        /// <param name="root"></param>
        public EzElement(EzRoot root)
        {
            _backingAutomationElement = root.RootElement._backingAutomationElement;
            object pattern;
            root.RootElement.BackingAutomationElement.TryGetCurrentPattern(InvokePattern.Pattern, out pattern);
            _invokePattern = pattern as InvokePattern;
        }

        /// <summary>
        /// Creates an instance of EzElement using a backing AutomationElement from Windows' automation framework
        /// </summary>
        /// <param name="element"></param>
        public EzElement(AutomationElement element)
        {
            _backingAutomationElement = element;
            object pattern;
            element.TryGetCurrentPattern(InvokePattern.Pattern, out pattern);
            _invokePattern = pattern as InvokePattern;
        }

        /// <summary>
        /// Backing instance of System.Windows.Automation.AutomationElement.  Can be used for more advanced testing not provided by EzE2E Framework
        /// </summary>
        public AutomationElement BackingAutomationElement => _backingAutomationElement;

        /// <summary>
        /// Uses InvokePattern.Invoke to call the underlying method on a button
        /// </summary>
        public void Click()
        {
            if (_invokePattern == null) throw new InvalidOperationException("Cannot clicke element because backing invokePattern object is null");
            _invokePattern?.Invoke();
        }

        /// <summary>
        /// Brings the current EzElement into focus.  This will bring the entire window containing the element into focus as well.
        /// </summary>
        public void BringIntoFocus()
        {
            this.BackingAutomationElement.SetFocus();
        }

        /// <summary>
        /// Finds the first child of the current EzElement where the Name property matches the string passed in
        /// </summary>
        /// <param name="name">The string value to be compared to the Name property</param>
        /// <param name="searchRaw">Tells the framework whether or not to search all elements (true) or just elements ControlView items (false)</param>
        /// <returns></returns>
        public EzElement FindChildByName(string name, bool searchRaw = false)
        {
            if (!searchRaw)
            {
                var element = _backingAutomationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, name));
                if (element == null) return null;
                return new EzElement(element);
            }
            return this.FindChildRaw(PropertyType.Name, name);
        }

        /// <summary>
        /// Finds the first descendant of the current EzElement where the Name property matches the string passed in.  Note that if Config.AllowSearchingForDescendants is false, this will throw an UnauthorizedSearchException
        /// </summary>
        /// <param name="name">The string value to be compared to the Name property</param>
        /// <returns></returns>
        public EzElement FindDescendantByName(string name)
        {
            CheckSearchPermissions();
            var element = _backingAutomationElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, name));
            if (element == null) return null;
            return new EzElement(element);
        }

        /// <summary>
        /// Finds the first child of the current EzElement where the AutomationId property matches the string passed in
        /// </summary>
        /// <param name="name">The string value to be compared to the Name property</param>
        /// <param name="searchRaw">Tells the framework whether or not to search all elements (true) or just elements ControlView items (false)</param>
        /// <returns></returns>
        public EzElement FindChildByAutomationId(string name, bool searchRaw = false)
        {
            if (!searchRaw)
            {
                var element = _backingAutomationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.AutomationIdProperty, name));
                if (element == null) return null;
                return new EzElement(element);
            }
            return this.FindChildRaw(PropertyType.AutomationId, name);
        }

        /// <summary>
        /// Finds the first descendant of the current EzElement where the AutomationId property matches the string passed in.  Note that if Config.AllowSearchingForDescendants is false, this will throw an UnauthorizedSearchException
        /// </summary>
        /// <param name="name">The string value to be compared to the AutomationId property</param>
        /// <returns></returns>
        public EzElement FindDescendantByAutomationId(string name)
        {
            CheckSearchPermissions();
            var element = _backingAutomationElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, name));
            if (element == null) return null;
            return new EzElement(element);
        }

        /// <summary>
        /// Finds the first child of the current EzElement where the Class property matches the string passed in
        /// </summary>
        /// <param name="name">The string value to be compared to the Name property</param>
        /// <param name="searchRaw">Tells the framework whether or not to search all elements (true) or just elements ControlView items (false)</param>
        /// <returns></returns>
        public EzElement FindChildByClass(string name, bool searchRaw = false)
        {
            if (!searchRaw)
            {
                var element = _backingAutomationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ClassNameProperty, name));
                if (element == null) return null;
                return new EzElement(element);
            }
            return this.FindChildRaw(PropertyType.Class, name);
        }

        /// <summary>
        /// Finds the first child of the current EzElement where its properties match all the properties passed in
        /// </summary>
        /// <param name="properties">Array of properties to be searched for</param>
        /// <returns></returns>
        public EzElement FindChildByMultipleCriteria(params SearchTypeProperty[] properties)
        {
            var element = _backingAutomationElement.FindFirst(TreeScope.Children, new AndCondition(properties.Select(prop => new PropertyCondition(SearchTypeHelper.GetAutomationProperty(prop.PropertyType), prop.Name)).Cast<Condition>().ToArray()));
            if (element == null) return null;
            return new EzElement(element);
        }

        /// <summary>
        /// Finds the first descendant of the current EzElement where its properties match all the properties passed in.  Note that if Config.AllowSearchingForDescendants is false, this will throw an UnauthorizedSearchException
        /// </summary>
        /// <param name="properties">Array of properties to be searched for</param>
        /// <returns></returns>
        public EzElement FindDescendantByMultipleCriteria(params SearchTypeProperty[] properties)
        {
            CheckSearchPermissions();
            var element = _backingAutomationElement.FindFirst(TreeScope.Descendants, new AndCondition(properties.Select(prop => new PropertyCondition(SearchTypeHelper.GetAutomationProperty(prop.PropertyType), prop.Name)).Cast<Condition>().ToArray()));
            if (element == null) return null;
            return new EzElement(element);
        }

        /// <summary>
        /// Finds all children of the current EzElement where the Name property matches the string passed in
        /// </summary>
        /// <param name="name">The string value to be compared to the Name property</param>
        /// <param name="searchRaw">Tells the framework whether or not to search all elements (true) or just elements ControlView items (false)</param>
        /// <returns></returns>
        public IEnumerable<EzElement> FindChildrenByName(string name, bool searchRaw = false)
        {
            if (!searchRaw)
            {
                var elements = _backingAutomationElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, name));
                if (elements == null || elements.Count == 0) return null;
                return ConvertCollection(elements);
            }
            return this.FindChildrenRaw(PropertyType.Name, name);
        }

        /// <summary>
        /// Finds all descendants of the current EzElement where the Name property matches the string passed in.  Note that if Config.AllowSearchingForDescendants is false, this will throw an UnauthorizedSearchException
        /// </summary>
        /// <param name="name">The string value to be compared to the Name property</param>
        /// <returns></returns>
        public IEnumerable<EzElement> FindDescendantsByName(string name)
        {
            CheckSearchPermissions();
            var elements = _backingAutomationElement.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, name));
            if (elements == null || elements.Count == 0) return null;
            return ConvertCollection(elements);
        }

        /// <summary>
        /// Finds all children of the current EzElement where the AutomationId property matches the string passed in
        /// </summary>
        /// <param name="name">The string value to be compared to the AutomationId property</param>
        /// <returns></returns>
        public IEnumerable<EzElement> FindChildrenByAutomationId(string name)
        {
            var elements = _backingAutomationElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.AutomationIdProperty, name));
            if (elements == null || elements.Count == 0) return null;
            return ConvertCollection(elements);
        }

        /// <summary>
        /// Finds all descendants of the current EzElement where the AutomationId property matches the string passed in.  Note that if Config.AllowSearchingForDescendants is false, this will throw an UnauthorizedSearchException
        /// </summary>
        /// <param name="name">The string value to be compared to the AutomationId property</param>
        /// <returns></returns>
        public IEnumerable<EzElement> FindDescendantsByAutomationId(string name)
        {
            CheckSearchPermissions();
            var elements = _backingAutomationElement.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, name));
            if (elements == null || elements.Count == 0) return null;
            return ConvertCollection(elements);
        }

        /// <summary>
        /// Finds the all children of the current EzElement where its properties match all the properties passed in.
        /// </summary>
        /// <param name="properties">Array of properties to be searched for</param>
        /// <returns></returns>
        public IEnumerable<EzElement> FindChildrenByMultipleCriteria(params SearchTypeProperty[] properties)
        {
            var elements = _backingAutomationElement.FindAll(TreeScope.Children, new AndCondition(properties.Select(prop => new PropertyCondition(SearchTypeHelper.GetAutomationProperty(prop.PropertyType), prop.Name)).Cast<Condition>().ToArray()));
            if (elements == null || elements.Count == 0) return null;
            return ConvertCollection(elements);
        }

        /// <summary>
        /// Finds the all descendants of the current EzElement where its properties match all the properties passed in.  Note that if Config.AllowSearchingForDescendants is false, this will throw an UnauthorizedSearchException
        /// </summary>
        /// <param name="properties">Array of properties to be searched for</param>
        /// <returns></returns>
        public IEnumerable<EzElement> FindDescendantsByMultipleCriteria(params SearchTypeProperty[] properties)
        {
            CheckSearchPermissions();
            var elements = _backingAutomationElement.FindAll(TreeScope.Descendants, new AndCondition(properties.Select(prop => new PropertyCondition(SearchTypeHelper.GetAutomationProperty(prop.PropertyType), prop.Name)).Cast<Condition>().ToArray()));
            if (elements == null || elements.Count == 0) return null;
            return ConvertCollection(elements);
        }

        /// <summary>
        /// Finds all children of the current EzElement
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EzElement> GetAllChildren()
        {
            //this is super dumb.  Figure out a better way to do this.
            var elements = _backingAutomationElement.FindAll(TreeScope.Children, new NotCondition(new PropertyCondition(AutomationElement.ClassNameProperty, "dfsjkdsfjkdfskjfsdjkdsfjkfsdjkdsfsdfjkdfskjdsfjkdsfjkdsfjkdsfjkdfskjsdfkjsdfjksdfkjfdkdsf")));
            if (elements == null || elements.Count == 0) return null;
            return ConvertCollection(elements);
        }

        /// <summary>
        /// USE THIS METHOD WITH CAUTION.  IT WILL RETURN A COLLECTION OF EVERY DESCENDANT OF AN ELEMENT WICH CAN HAVE A HUGE PERFORMANCE IMPACT.  Finds all descendants of the current EzElement.  Note that if Config.AllowSearchingForDescendants is false, this will throw an UnauthorizedSearchException
        /// </summary>
        /// <returns>An IEnumerable of type EzElement</returns>
        public IEnumerable<EzElement> GetAllDescendants()
        {
            CheckSearchPermissions();
            //this is super dumb.  Figure out a better way to do this.
            var elements = _backingAutomationElement.FindAll(TreeScope.Descendants, new NotCondition(new PropertyCondition(AutomationElement.ClassNameProperty, "dfsjkdsfjkdfskjfsdjkdsfjkfsdjkdsfsdfjkdfskjdsfjkdsfjkdsfjkdsfjkdfskjsdfkjsdfjksdfkjfdkdsf")));
            if (elements == null || elements.Count == 0) return null;
            return ConvertCollection(elements);
        }

        private static void CheckSearchPermissions()
        {
            if (!Config.AllowSearchingForDescendants) throw new UnauthorizedSearchException(UnauthorizedSearchException.StandardExceptionMessage);
        }

        private static IEnumerable<EzElement> ConvertCollection(IEnumerable collection)
        {
            return from object c in collection select new EzElement(c as AutomationElement);
        }
    }
}
