using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Configuration;

namespace TransactRules.Runtime.Schedules
{
    public interface IBusinessDayCalculator
    {
        DateTime GetCalculatedBusinessDay(DateTime date, BusinessDayCalculation calculation);
    }
}
