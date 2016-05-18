namespace TransactRules.Configuration
{
    public class TransactionRuleType
    {
        public virtual PositionType PositionType { get; set; }
        public virtual TransactionOperation TransactionOperation { get; set; }
    }
}
