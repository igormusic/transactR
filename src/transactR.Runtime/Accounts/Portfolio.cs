using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using transactR.Core.Entities;

namespace transactR.Runtime.Accounts
{
    public class Portfolio:Entity
    {
        public virtual string PortfolioNumber { get; set; }
    }
}
