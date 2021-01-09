using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.domain
{
    public class TimeInterval
    {

        public DateTime JoinedTime { get; set; }
        public DateTime LeaveTime { get; set; }

        public TimeInterval(DateTime joinedTime, DateTime leaveTime)
        {
            this.JoinedTime = joinedTime;
            this.LeaveTime = leaveTime;
        }

        public void SetLeaveTime(DateTime dateTime)
        {
            this.LeaveTime = dateTime;
        }

        public double GetTimeInterval()
        {
            TimeSpan interval = (DateTime.Now - this.JoinedTime).Duration();
            return interval.Minutes;
        }
    }
}
