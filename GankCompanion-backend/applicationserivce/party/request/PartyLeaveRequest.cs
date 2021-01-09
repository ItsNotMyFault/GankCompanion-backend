using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.applicationserivce.party
{
    public class PartyLeaveRequest
    {
        public string PartyId { get; set; }
        public string PlayerLeftId { get; set; }
        public PartyLeaveRequest() { }
    }
}
