using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;
using TransactRules.Runtime;
using TransactRules.Runtime.Values;
using TransactRules.Runtime.Rates;

namespace TransactRules.Runtime.Parties
{
    public class Party:Entity
    {
        public virtual string PartyNumber { get; set; }
        public virtual IList<AmountValue> Amounts { get; set; }
        public virtual IList<RateValue> Rates { get; set; }
        public virtual IList<DateValue> Dates { get; set; }
    }
}
