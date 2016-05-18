using System.ComponentModel.DataAnnotations;

namespace TransactRules.Configuration
{
    public class OptionType
    {
        
        public virtual string Name { get; set; }
        [StringLength(64000)]
        public virtual string OptionListExpression { get; set; }
    }
}
