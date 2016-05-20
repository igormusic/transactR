using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.Rates
{
    public class RateValue:Entity
    {
        public virtual Entity Consumer { get; set; }
        public virtual decimal Value { get; set; }
        public virtual DateTime? ValueDate { get; set; }
        public virtual string RateType { get; set; }

        public static implicit operator decimal(RateValue rateValue)
        {
            return rateValue.Value;
        }
    }
}
