using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using transactR.Core.Entities;
using transactR.Runtime;
using transactR.Runtime.Values;
using transactR.Runtime.Rates;

namespace transactR.Runtime.Parties
{
    public class Party:Entity
    {
        public virtual string PartyNumber { get; set; }
        public virtual IList<AmountValue> Amounts { get; set; }
        public virtual IList<RateValue> Rates { get; set; }
        public virtual IList<DateValue> Dates { get; set; }
    }
}
