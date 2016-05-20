using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;

using TransactRules.Runtime.Accounts;

namespace TransactRules.Runtime.Values
{
    public class AmountValue:Entity, ITemporalPropertyValue<decimal>
    {
        public virtual Entity Consumer{ get; set; }
        public virtual decimal Value { get; set; }
        public virtual DateTime? ValueDate { get; set; }
        public virtual string AmountType { get; set; }

        public static implicit operator decimal(AmountValue amountValue)
        {
            return amountValue.Value;
        }

       [NotMapped]
        DateTime? ITemporalPropertyValue<decimal>.ValueDate
        {
            get { return ValueDate; }
        }

       [NotMapped]
        string IPropertyValue<decimal>.Name
        {
            get { return AmountType; }
        }
    }
}
