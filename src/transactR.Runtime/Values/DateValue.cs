using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using transactR.Core.Entities;
using transactR.Runtime.Accounts;
using transactR.Core.Utilities;

namespace transactR.Runtime.Values
{
    public class DateValue:Entity
    {
        public virtual Entity Consumer { get; set; }
        public virtual DateTime Value { get; set; }
        public virtual string DateType { get; set; }

        public static implicit operator DateTime(DateValue dateValue)
        {
            return dateValue.Value;
        }

        public virtual bool IsDue()
        {
            return Value == SessionState.Current.ValueDate;
        }
    }
}
