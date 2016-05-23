using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using transactR.Runtime.Accounts;
using transactR.Runtime;
using transactR.Runtime.Parties;
using transactR.Core.Entities;

namespace transactR.Runtime.Accounts
{
    public class PartyRole:Entity
    {
        public virtual Account Account { get; set; }
        public virtual Party Party { get; set; }
        public virtual string PartyRoleType { get; set; }

    }
}
