namespace TransactRules.Configuration
{
    public enum ScheduleFrequency
    { 
        Daily,
        Monthly
    }

    public enum ScheduleEndType
    { 
        NoEnd,
        EndDate,
        Repeats
    }

    public class ScheduleType
    {
        
        public virtual string Name { get; set; }
        public virtual ScheduleFrequency Frequency { get; set; }
        public virtual ScheduleEndType EndType { get; set; }
        public virtual BusinessDayCalculation BusinessDayCalculation { get; set; }
        public virtual string StartDateExpression { get; set; }
        public virtual string EndDateExpression { get; set; }
        public virtual string NumberOfRepeatsExpression { get; set; }
        public virtual string IntervalExpression { get; set; }
        public virtual bool IsCalculated { get; set; }
    }
}
