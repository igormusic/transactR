using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Runtime.Accounts;
using TransactRules.Runtime;
using TransactRules.Runtime.Parties;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.Accounts
{
    public class PartyRole:Entity
    {
        public virtual Account Account { get; set; }
        public virtual Party Party { get; set; }
        public virtual string PartyRoleType { get; set; }

    }
}
