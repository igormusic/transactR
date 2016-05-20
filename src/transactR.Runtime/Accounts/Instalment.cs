using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.Accounts
{
    public class Instalment:Entity
    {
        public virtual Account Account { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual decimal OpeningBalance { get; set; }
        public virtual decimal Interest { get; set; }
        public virtual decimal ClosingBalance { get; set; }
        public virtual DateTime ValueDate { get; set; }
        public virtual string InstalmentType { get; set; }
        public virtual string TransactionType { get; set; }
        public virtual bool HasFixedValue { get; set; }
    }
}
