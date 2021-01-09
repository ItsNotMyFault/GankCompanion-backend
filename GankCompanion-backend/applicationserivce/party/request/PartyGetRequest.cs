using GankCompanion_backend.applicationserivce.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.applicationserivce.party.request
{
    public class PartyGetRequest
    {
        public bool IsActive { get; set; }
        public string PartyStartTime { get; set; }
        public string PartyEndTime { get; set; }
        public string PartyId{ get; set; }
        public PartyMemberRequest PartyMemberList{ get; set; }

    }
}
