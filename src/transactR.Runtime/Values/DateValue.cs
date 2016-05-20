using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;
using TransactRules.Runtime.Accounts;
using TransactRules.Core.Utilities;

namespace TransactRules.Runtime.Values
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
