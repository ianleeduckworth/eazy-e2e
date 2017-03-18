using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EazyE2E.Process;

namespace EazyE2E.Logwatch
{
    public class EzLogwatch
    {
        private HashSet<string> _registeredWatches;

        public HashSet<string> RegisteredWatches
        {
            get
            {
                if (_registeredWatches == null) _registeredWatches = new HashSet<string>();
                return _registeredWatches;
            }
        }

        public void RegisterWatch(string watch)
        {
            var result = RegisteredWatches.Add(watch);
            if (!result) throw new InvalidOperationException($"Could not add watch '{watch}' because it has already been added.");
        }

        public void RemoveWatch(string watch)
        {
            var result = RegisteredWatches.Remove(watch);
            if (!result) throw new InvalidOperationException($"Could not remove watch'{watch}' because it didn't exist");
        }

        public EzLogwatch(EzProcess process)
        {
            
        }
    }
}
