using System.Collections.Generic;
using System.Linq;

using TransactRules.Core.Entities;
using TransactRules.Runtime.Values;
using TransactRules.Runtime.Rates;
using TransactRules.Runtime.Schedules;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactRules.Runtime.Accounts
{
    public class Account:Entity
    {
        public Account()
        {
            Amounts = new List<AmountValue>();
            Transactions = new List<Transaction>();
            Positions = new List<Position>();
            Rates = new List<RateValue>();
            Dates = new List<DateValue>();
            Options = new List<OptionValue>();
            Instalments = new List<Instalment>();
            Schedules = new List<Schedule>();
            PartyRoles = new List<PartyRole>();
        }

        public virtual string AccountNumber { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public virtual AccountGroup AccountGroup { get; set; }
        public virtual bool IsActive { get; set; }

     
        public virtual IList<PartyRole> PartyRoles { get; set; }

        public virtual IList<Transaction> Transactions { get; set; }

        
        public virtual IList<Instalment> Instalments { get; set; }

        public virtual IList<Position> Positions { get; set; }

        [InverseProperty("Consumer")]
        public virtual IList<RateValue> Rates { get; set; }

        [InverseProperty("Consumer")]
        public virtual IList<DateValue> Dates { get; set; }

        [InverseProperty("Consumer")]
        public virtual IList<OptionValue> Options { get; set; }

        [InverseProperty("Consumer")]
        public virtual IList<Schedule> Schedules { get; set; }

        [InverseProperty("Consumer")]
        public virtual IList<AmountValue> Amounts { get; set; }

        [NotMapped]
        public virtual IBusinessDayCalculator BusinessDayCalculator { get; set; }

        public virtual Position GetPosition(string positionType)
        {
            Position position;

            position = Positions.FirstOrDefault(p => p.PositionType == positionType);

            if (position == null)
            {
                position = new Position() { Account = this, PositionType = positionType, Value = 0 };
                Positions.Add(position);
            }
            
            return position;
        }

        public virtual AmountValue GetAmount(string dateType)
        {
            AmountValue dateValue;

            dateValue = Amounts.FirstOrDefault(p => p.AmountType == dateType);

            if (dateValue == null)
            {
                dateValue = new AmountValue() { Consumer = this, AmountType = dateType, Value = 0 };
                Amounts.Add(dateValue);
            }

            return dateValue;
        }

        public virtual DateValue GetDate(string DateType)
        {
            DateValue DateValue;

            DateValue = Dates.FirstOrDefault(p => p.DateType == DateType);

            if (DateValue == null)
            {
                DateValue = new DateValue() { Consumer = this, DateType = DateType };
                Dates.Add(DateValue);
            }

            return DateValue;
        }

        public virtual Schedule GetSchedule(string scheduleType)
        {
            return Schedules.FirstOrDefault(s => s.ScheduleType == scheduleType);
        }

        public virtual OptionValue GetOption(string optionType)
        {
            return Options.FirstOrDefault(o => o.OptionType == optionType);
        }

        public virtual RateValue GetRate(string rateType)
        {
            return Rates.FirstOrDefault(r => r.RateType == rateType);
        }

        public virtual IEnumerable<Instalment> GetInstalments(string instalmentType)
        {
            return Instalments.Where(i => i.InstalmentType == instalmentType);
        }

        private IList<Position> _snapshotPositions;
        private IList<Transaction> _snapshotTransactions;

        public virtual void Snapshot()
        {
            _snapshotPositions = new List<Position>(Positions);
            _snapshotTransactions = Transactions;

            foreach (var position in Positions)
            {
                position.Value = 0;
            }

            Transactions = new List<Transaction>();
        }

        public virtual void RestoreSnapshot()
        {
            foreach (var position in Positions)
            {
                var existingValue = _snapshotPositions.FirstOrDefault(p=> p.PositionType == position.PositionType);
                if (existingValue != null)
                {
                    position.Value = existingValue.Value;
                }
            }

            Transactions = _snapshotTransactions;
        }
    }
}
