using System;
using System.Collections.Generic;
using System.Linq;
using TransactRules.Configuration;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.Schedules
{
    public class Calendar : Entity, IBusinessDayCalculator
    {
        private Dictionary<DateTime, HolidayDate> _holidaysDictionary;

        public virtual string Name { get; set; }

        public virtual IList<HolidayDate> Holidays { get; set; }

        public virtual bool IsBusinessDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            return !HolidaysDictionary.ContainsKey(date);
        }

        protected Dictionary<DateTime, HolidayDate> HolidaysDictionary
        { 
            get {
                if (_holidaysDictionary == null)
                {
                    _holidaysDictionary = Holidays.ToDictionary(h => h.Value);
                }

                return _holidaysDictionary;
            } 
        }

        public virtual DateTime GetCalculatedBusinessDay(DateTime date, BusinessDayCalculation adjsutment)
        {
            if (adjsutment == BusinessDayCalculation.AnyDay)
            {
                return date;
            }

            if (adjsutment == BusinessDayCalculation.PreviousBusinessDay)
            {
                return GetPreviousBusinessDay(date);
            }

            if (adjsutment == BusinessDayCalculation.NextBusinessDay)
            {
                return GetNextBusinessDay(date);
            }

            var previousBusinessDay = GetPreviousBusinessDay(date);
            var nextBusinessDay = GetPreviousBusinessDay(date);

            if (adjsutment == BusinessDayCalculation.ClosestBusinessDayOrNext)
            {
                if (date.Subtract(previousBusinessDay).Days > nextBusinessDay.Subtract(date).Days)
                {
                    return nextBusinessDay;
                }
                else if (date.Subtract(previousBusinessDay).Days < nextBusinessDay.Subtract(date).Days)
                {
                    return previousBusinessDay;
                }
                else {
                    return nextBusinessDay;
                }
            }

            //last option is NextBusinessDayThisMonthOrPrevious

            if (nextBusinessDay.Month == date.Month)
            {
                return nextBusinessDay;
            }

            return previousBusinessDay;

        }

        public virtual DateTime GetPreviousBusinessDay(DateTime date)
        {
            while (!IsBusinessDay(date))
            {
                date = date.AddDays(-1);
            }

            return date;
        }

        public virtual DateTime GetNextBusinessDay(DateTime date)
        {
            while (!IsBusinessDay(date))
            {
                date = date.AddDays(1);
            }

            return date;
        }
    }
}
