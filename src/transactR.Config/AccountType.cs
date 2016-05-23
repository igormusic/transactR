using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace transactR.Configuration
{
    public class AccountType 
    {
        public AccountType() {
            PositionTypes = new List<PositionType>();
            TransactionTypes = new List<TransactionType>();
            ScheduleTypes = new List<ScheduleType>();
            AmountTypes = new List<AmountType>();
            DateTypes = new List<DateType>();
            RateTypes = new List<RateType>();
            OptionTypes = new List<OptionType>();
            PartyRoleTypes = new List<PartyRoleType>();
            ScheduledTransactions = new List<ScheduledTransaction>();
            InstalmentTypes = new List<InstalmentType>();
        }

        [Required]
        public virtual string Name { get; set; }

        public virtual IList<PositionType> PositionTypes { get; set; }

        public virtual IList<TransactionType> TransactionTypes { get; set; }

        public virtual IList<ScheduleType> ScheduleTypes { get; set; }

        public virtual IList<AmountType> AmountTypes { get; set; }

        public virtual IList<DateType> DateTypes { get; set; }

        public virtual IList<OptionType> OptionTypes { get; set; }

        public virtual IList<RateType> RateTypes { get; set; }

        public virtual IList<ScheduledTransaction> ScheduledTransactions { get; set; }

        public virtual IList<InstalmentType> InstalmentTypes { get; set; }
        
        public virtual IList<PartyRoleType> PartyRoleTypes { get; set; }
    }
}
