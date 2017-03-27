using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EazyE2E.ElementHelper
{
    public class EzTextResult<T>
    {
        public EzTextResult(bool notSupported, bool isMixed, T result)
        {
            this.NotSupported = notSupported;
            this.IsMixed = isMixed;
            this.Result = result;
        } 

        public bool NotSupported { get; }
        public bool IsMixed { get; }
        public T Result { get; }

        public void HandleResult(Action ifUnsupported, Action ifMixed, Action<T> final)
        {
            if (this.NotSupported)
                ifUnsupported?.Invoke();
            else if (this.IsMixed)
                ifMixed?.Invoke();
            else
                final?.Invoke(this.Result);
        }
    }

}
