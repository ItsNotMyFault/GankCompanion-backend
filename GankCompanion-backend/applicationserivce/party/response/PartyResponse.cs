using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.applicationserivce.party.response
{
    public class PartyResponse
    {

        public string partyFirebaseId { get; set; }
        public string createdDate { get; set; }
        public string players { get; set; }
        public string duration { get; set; }
        public bool isActive{ get; set; }
    }
}
