using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.domain
{
    public struct DateTimeWithZone
    {
        public DateTime utcDateTime { get; set; }
        private readonly TimeZoneInfo timeZone;

        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
        {
            var dateTimeUnspec = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTimeUnspec, timeZone);
            this.timeZone = timeZone;
        }

        public DateTimeWithZone(string date, TimeZoneInfo timeZone)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            var dateTimeUnspec = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTimeUnspec, timeZone);
            this.timeZone = timeZone;
        }

        public DateTime UniversalTime { get { return utcDateTime; } }

        //public TimeZoneInfo TimeZone { get { return timeZone; } }

        //public DateTime LocalTime {
        //    get {
        //        return TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
        //    }
        //}

        override
        public string ToString()
        {
            return this.UniversalTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
