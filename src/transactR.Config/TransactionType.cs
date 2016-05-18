using System.Collections.Generic;

namespace TransactRules.Configuration
{
    public class TransactionType
    {
        
        public virtual string Name { get; set; }
        public virtual bool HasMaximumPrecission { get; set; }
        public virtual IList<TransactionRuleType> TransactionRules { get; set; }
    }
}
