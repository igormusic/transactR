using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using transactR.Core.Entities;

namespace transactR.Runtime.Schedules
{
    public class ScheduleDate:Entity
    {
        public virtual Schedule IncludeInSchedule { get; set; }
        public virtual Schedule ExcludeFromSchedule { get; set; }
        public virtual DateTime Value { get; set; }
    }
}
