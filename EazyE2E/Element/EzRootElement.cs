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
        public EzElement RootElement;

        public EzRoot(string processName)
        {
            RootElement = Set(processName);
        }

        public EzRoot(EzProcess process)
        {
            RootElement = Set(process.ProcessName);
        }
        private EzElement Set(string processName)
        {
            return new EzElement(AutomationElement.RootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, processName)));
        }


    }
}
