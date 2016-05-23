using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using transactR.Configuration;

namespace transactR.Runtime.Schedules
{
    public interface IBusinessDayCalculator
    {
        DateTime GetCalculatedBusinessDay(DateTime date, BusinessDayCalculation calculation);
    }
}
