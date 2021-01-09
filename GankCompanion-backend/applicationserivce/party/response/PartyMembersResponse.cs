using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.applicationserivce.party.response
{
    public class PartyMembersResponse
    {
        public string duration { get; set; }
        public List<PartyMemberResponse> partyMemberResponses { get; set; }
    }
}
