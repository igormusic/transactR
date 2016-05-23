using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace transactR.Runtime
{
    public interface IPropertyValue<T>
    {
        string Name { get; }
        T Value { get;  }
    }
}
