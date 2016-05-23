using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace transactR.Configuration
{
    public class InstalmentType
    {
        
        public virtual string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual ScheduledTransactionTiming Timing { get; set; }
        public virtual string ScheduleTypeName { get; set; }
        public virtual string TransactionTypeName { get; set; }
        public virtual string PrincipalPositionTypeName { get; set; }
        //required for amortization calculations
        public virtual string InterestAccruedPositionName { get; set; }
        public virtual string InterestCapitalizedPositionName { get; set; }

    }
}
