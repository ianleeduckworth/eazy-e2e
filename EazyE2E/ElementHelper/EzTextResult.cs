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
        /// <summary>
        /// Object to house the results of an EzText property
        /// </summary>
        /// <param name="notSupported"></param>
        /// <param name="isMixed"></param>
        /// <param name="result"></param>
        public EzTextResult(bool notSupported, bool isMixed, T result)
        {
            this.NotSupported = notSupported;
            this.IsMixed = isMixed;
            this.Result = result;
        } 

        /// <summary>
        /// The pattern was not supported for the EzText element
        /// </summary>
        public bool NotSupported { get; }

        /// <summary>
        /// the pattern returned a mixed result for the EzText element (for example, if there are two different font sizes in the EzText element, IsMixed will be true)
        /// </summary>
        public bool IsMixed { get; }

        /// <summary>
        /// The actual value for EzText element property
        /// </summary>
        public T Result { get; }

        /// <summary>
        /// Method to handle the result of a query to a EzText property.  One of the actions passed in will be called depending on the outcome
        /// </summary>
        /// <param name="ifUnsupported"></param>
        /// <param name="ifMixed"></param>
        /// <param name="final"></param>
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
