using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.applicationserivce.party
{
    public class PartyReportResponse
    {
        public string startTime { get; set; }
        public string duration { get; set; }
        public string totalPlayers { get; set; }
        public string totalKills { get; set; }
 
    }
}
