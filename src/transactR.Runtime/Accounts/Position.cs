using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.Accounts
{
    public class Position:Entity
    {
        public virtual Account Account { get; set; }
        public virtual decimal Value { get; set; }
        public virtual string PositionType { get; set; }

        public static implicit operator decimal(Position position)
        {
            return position.Value;
        }
    }
}
