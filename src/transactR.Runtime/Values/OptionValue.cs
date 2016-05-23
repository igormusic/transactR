using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using transactR.Core.Entities;


namespace transactR.Runtime.Values
{
    public class OptionValue:Entity
    {
        public virtual Entity Consumer { get; set; }
        public virtual string Value { get; set; }
        public virtual string OptionType { get; set; }

        public static implicit operator string(OptionValue optionValue)
        {
            return optionValue.Value;
        }

       [NotMapped]
        public IEnumerable<String> OptionValues { get; set; }
    }
}
