using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.Accounts
{
    public class AccountGroup:Entity
    {
        public virtual AccountGroup ParentGroup { get; set; }
        public virtual IList<Account> Accounts { get; set; }
    }
}
