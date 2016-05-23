using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace transactR.Configuration
{
    public enum ScheduledTransactionTiming { 
        StartOfDay,
        EndOfDay
    }

    public class ScheduledTransaction
    {
        
        public virtual string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual ScheduledTransactionTiming Timing { get; set; }
        public virtual string ScheduleTypeName { get; set; }
        public virtual string DateTypeName { get; set; }
        public virtual string TransactionTypeName { get; set; }
        [StringLength(64000)]
        public virtual string AmountExpression { get; set; }
        public virtual int Sequence { get; set; }
    }
}
