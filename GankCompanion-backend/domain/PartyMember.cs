using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.domain
{

    public class PartyMember
    {
        public Player player;
        public string partyMemberId;
        public bool IsPartyLead;
        public DateTimeWithZone JoinedParty { get; set; }//todo changer pour simple date
        public List<TimeInterval> timesInParty { get; set; }


        public bool isInParty;
        public PartyMember() { }
        public PartyMember(string partyMemberId, Player player, bool isPartyLead)
        {
            this.timesInParty = new List<TimeInterval>();
            this.partyMemberId = partyMemberId;
            PlayerJoinParty();
            this.player = player;
            this.IsPartyLead = isPartyLead;
        }


        public string GetPercentageOfTimeInParty(double partyDuration)
        {
            double partyMemberTimeInParty = GetPresenceTimeInParty();
            double partyMemberTimeInPartyPercent = (partyMemberTimeInParty * 100) / partyDuration;
            return Math.Round(partyMemberTimeInPartyPercent, 0).ToString();
        }

        public double GetPresenceTimeInParty()
        {
            double presenceInPartyMinutes = 0;
            foreach (TimeInterval time in this.timesInParty)
            {
                if (time != null)
                {
                    presenceInPartyMinutes += time.GetTimeInterval();
                }
            }

            return Math.Round(presenceInPartyMinutes, 2); 
        }

        public void PlayerJoinParty()
        {
            this.isInParty = true;
            this.JoinedParty = new DateTimeWithZone(DateTime.Now, TimeZoneInfo.Local);
            TimeInterval timeInterval = new TimeInterval(this.JoinedParty.UniversalTime, this.JoinedParty.UniversalTime);
            this.timesInParty.Add(timeInterval);
        }

        public void SetLeaveParty()
        {
            this.isInParty = false;
            TimeInterval timeInterval = this.timesInParty.Last();
            DateTime leaveTime = new DateTimeWithZone(DateTime.Now, TimeZoneInfo.Local).UniversalTime;
            timeInterval.SetLeaveTime(leaveTime);
        }

        public override string ToString()
        {
            return this.player.Name + " joined party at " + this.JoinedParty.ToString() +
                " has been in party for: " + this.GetPresenceTimeInParty().ToString() + " minutes(s)";
        }


    }

}
