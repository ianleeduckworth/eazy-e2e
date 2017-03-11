using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EazyE2E.ElementHelper
{
    public class EzTextResult<T>
    {
        public bool NotSupported { get; set; }
        public bool IsMixed { get; set; }
        public T Result { get; set; }

        public void HandleResult(Action ifUnsupported, Action ifMixed, Action<T> final)
        {
            if (this.NotSupported)
                ifUnsupported();
            else if (this.IsMixed)
                ifMixed();
            else
                final(Result);
        }
    }

}
