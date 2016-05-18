namespace TransactRules.Configuration
{
    public class InstalmentType
    {
        
        public virtual string Name { get; set; }
        public virtual ScheduledTransactionTiming Timing { get; set; }
        public virtual ScheduleType ScheduleType { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual PositionType PrincipalPositionType { get; set; }
        //required for amortization calculations
        public virtual PositionType InterestAccrued { get; set; }
        public virtual PositionType InterestACapitalized { get; set; }

    }
}
