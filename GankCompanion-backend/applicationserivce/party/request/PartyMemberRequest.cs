using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.applicationserivce.session
{
    public class PartyMemberRequest
    {
        public string PartyId { get; set; }
        public string PlayerName { get; set; }
        public byte[] PartyMemberId { get; set; }
        public string JoinedPartyAt { get; set; }
        public string LeftPartyAt { get; set; }
    }
}
