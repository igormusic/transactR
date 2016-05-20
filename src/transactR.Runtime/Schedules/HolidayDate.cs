using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.Schedules
{
    public class HolidayDate:Entity
    {
        public virtual Calendar Calendar { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Value { get; set; }
    }
}
