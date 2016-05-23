using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace transactR.Runtime
{
    public interface ITemporalPropertyValue<T>:IPropertyValue<T>
    {
        DateTime? ValueDate { get; }
    }
}
