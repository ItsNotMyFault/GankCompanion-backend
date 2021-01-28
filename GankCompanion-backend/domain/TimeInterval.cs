using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.domain
{
    public class TimeInterval
    {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeInterval(DateTime StartTime, DateTime EndTime)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

        public void SetLeaveTime(DateTime dateTime)
        {
            this.EndTime = dateTime;
        }

        public double GetTimeInterval()
        {
            TimeSpan interval = (this.EndTime - this.StartTime).Duration();
            double intervalInMinutes = interval.Minutes;
            if (intervalInMinutes == 0)
            {
                interval = (DateTime.UtcNow - this.StartTime).Duration();
                intervalInMinutes = interval.Minutes; 
            }
            return intervalInMinutes;
        }
    }
}
