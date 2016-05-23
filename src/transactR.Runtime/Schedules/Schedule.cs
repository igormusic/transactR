using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using transactR.Core.Entities;
using transactR.Configuration;
using transactR.Runtime.Accounts;

using transactR.Core.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace transactR.Runtime.Schedules
{
    public class Schedule:Entity
    {
        public Schedule() {
            IncludeDates = new List<ScheduleDate>();
            ExcludeDates = new List<ScheduleDate>();
        }

        public virtual Entity Consumer { get; set; }
        public virtual string ScheduleType { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual ScheduleEndType EndType {get;set;}
        public virtual ScheduleFrequency Frequency { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual int Interval { get; set; }
        public virtual int? NumberOfRepeats { get; set; }
        public virtual BusinessDayCalculation BusinessDayCalculation { get; set; }

       [NotMapped]
        public virtual IBusinessDayCalculator BusinessDayCalculator { get; set; }

        public virtual IList<ScheduleDate> IncludeDates { get; set; }

        public virtual IList<ScheduleDate> ExcludeDates { get; set; }

        #region Operations

        public virtual bool IsDue(DateTime date)
        {
            if (IsSimpleDailySchedule())
            {
                if (EndType == ScheduleEndType.NoEnd)
                {
                    return date >= this.StartDate;
                }
                else if (EndType == ScheduleEndType.EndDate)
                {
                    return date >= this.StartDate && date <= this.EndDate;
                }
            }


            var dates = GetAllDates(LastPossibleDate());

            return dates.Contains(date);
        }

        //get next fifty years of dates assuming no schedule will be longer than that
        private DateTime LastPossibleDate()
        {
            return StartDate.AddYears(50);
        }

        private bool IsSimpleDailySchedule()
        {
            return this.Frequency == ScheduleFrequency.Daily && this.Interval == 1 && this.BusinessDayCalculation == Configuration.BusinessDayCalculation.AnyDay;
        }

        public virtual bool IsDue()
        {
            return IsDue(SessionState.Current.ValueDate);
        }

        private Dictionary<DateTime, List<DateTime>> _cachedDates = new Dictionary<DateTime, List<DateTime>>();


        public virtual IEnumerable<DateTime> GetAllDates()
        {
            return GetAllDates(LastPossibleDate());
        }

        public virtual IEnumerable<DateTime> GetAllDates(DateTime toDate)
        {
            if (_cachedDates.ContainsKey(toDate))
                return _cachedDates[toDate];

            List<DateTime> dates = new List<DateTime>();

            int repeats = 1;
            var iterator = StartDate;
            DateTime adjustedDate = BusinessDayCalculator.GetCalculatedBusinessDay(iterator, this.BusinessDayCalculation);

            while (!IsCompleted(repeats, adjustedDate, toDate))
            {
                dates.Add(adjustedDate);

                iterator = GetNextDate(repeats, iterator);

                adjustedDate = BusinessDayCalculator.GetCalculatedBusinessDay(iterator, this.BusinessDayCalculation);

                repeats++;
            }


            dates.AddRange(from d in this.IncludeDates
                           select d.Value);


            foreach (var excludeDate in this.ExcludeDates)
            {
                dates.Remove(excludeDate.Value);
            }

            dates.Sort();

            _cachedDates[toDate] = dates;

            return dates;
        }

        private DateTime GetNextDate(int repeats, DateTime iterator)
        {
            if (this.Frequency == ScheduleFrequency.Daily)
            {
                iterator = StartDate.AddDays(Interval * repeats);
            }
            else
            {
                iterator = StartDate.AddMonths(Interval * repeats);
            }

            return iterator;
        }

        private bool IsCompleted(int repeats, DateTime lastDate, DateTime endDate)
        {
            if (lastDate > endDate) 
            {
                return true;
            }

            if (this.EndType == ScheduleEndType.EndDate && lastDate > EndDate)
            {
                return true;
            }


            if (this.EndType == Configuration.ScheduleEndType.NoEnd)
            {
                return false;
            }

            if (this.EndType == Configuration.ScheduleEndType.Repeats &&
               repeats > this.NumberOfRepeats)
            {
                return true;
            }

            return lastDate > this.EndDate;

        }

        #endregion
    }
}
