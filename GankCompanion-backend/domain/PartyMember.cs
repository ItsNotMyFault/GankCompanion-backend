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
        public DateTime JoinedParty { get; set; }


        private int joinNumber { get; set; }

        public List<TimeInterval> timesInParty { get; set; }


        public bool isInParty;
        public PartyMember() { }
        public PartyMember(string partyMemberId, Player player, bool isPartyLead)
        {
            joinNumber = 1;
            this.timesInParty = new List<TimeInterval>();
            this.partyMemberId = partyMemberId;
            PlayerJoinParty();
            this.isInParty = true;
            this.player = player;
            this.IsPartyLead = isPartyLead;
        }


        public string GetPercentageOfTimeInParty(double partyDuration)
        {
            double partyMemberTimeInParty = GetPresenceTimeInParty();
            double partyMemberTimeInPartyPercent = (partyMemberTimeInParty * 100) / partyDuration;
            return partyMemberTimeInPartyPercent.ToString("#.##");
        }

        public double GetPresenceTimeInParty()
        {
            double presenceInPartyMinutes = 0;
            foreach (TimeInterval time in this.timesInParty)
            {
                if (time != null)
                    presenceInPartyMinutes += time.GetTimeInterval();
            }

            return presenceInPartyMinutes;
        }



        public void PlayerJoinParty()
        {
            this.isInParty = true;
            this.JoinedParty = DateTime.Now;
            this.timesInParty.Add(new TimeInterval(this.JoinedParty, DateTime.Now));
            joinNumber++;
        }

        public void SetLeaveParty()
        {
            this.isInParty = false;
            TimeInterval timeInterval = this.timesInParty.Last();
            timeInterval.SetLeaveTime(DateTime.Now);
        }

        public override string ToString()
        {
            return this.player.Name + " joined party at " + this.JoinedParty.ToString() +
                " has been in party for: " + this.GetPresenceTimeInParty().ToString() + " minutes(s)";
        }


    }

}
