using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using EazyE2E.Process;

namespace EazyE2E.Element
{
    public class EzRoot
    {
        private readonly EzProcess _process;
        private EzElement _rootElement;

        public EzElement RootElement => _rootElement ?? (_rootElement = GetRootElement());

        public EzRoot(EzProcess process)
        {
            _process = process;
        }

        /// <summary>
        /// Gets the root element of the process passed in during instantiation
        /// </summary>
        /// <returns></returns>
        public EzElement GetRootElement()
        {
            var propertyCondition = new PropertyCondition(AutomationElement.ProcessIdProperty, _process.ProcessId);
            var element = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, propertyCondition); //todo use TreeScope.Children instead of TreeScope.Subtree
            if (element == null) throw new NullReferenceException($"Root element not found where process name is ${_process.ProcessName}.");
            return new EzElement(element);
        }
    }
}
