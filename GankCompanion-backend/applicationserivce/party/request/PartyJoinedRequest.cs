using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.applicationserivce.party
{
    public class PartyJoinedRequest
    {
        public string PartyId { get; set; }
        public string PlayerJoinedId { get; set; }
        public string PlayerJoinedName { get; set; }
        public PartyJoinedRequest() { }
    }
}
