using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using transactR.Core.Entities;

namespace transactR.Runtime.Accounts
{
    public class AccountGroup:Entity
    {
        public virtual AccountGroup ParentGroup { get; set; }
        public virtual IList<Account> Accounts { get; set; }
    }
}
