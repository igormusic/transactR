using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace transactR.Configuration
{
    public class TransactionRuleType
    {
        public virtual string PositionTypeName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual TransactionOperation TransactionOperation { get; set; }
    }
}
