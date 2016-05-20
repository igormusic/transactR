using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.Accounts
{
    public class Portfolio:Entity
    {
        public virtual string PortfolioNumber { get; set; }
    }
}
