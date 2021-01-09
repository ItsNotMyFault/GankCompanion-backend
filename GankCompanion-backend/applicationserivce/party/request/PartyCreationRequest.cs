using GankCompanion_backend.applicationserivce.session;
using GankCompanion_backend.domain;
using System;
using System.Collections.Generic;

namespace GankCompanion_backend.applicationserivce
{
    public class PartyCreationRequest
    {
        public string PartyStartTime { get; set; }
        public string PartyId { get; set; }
        public string Player1Id { get; set; }
        public string Player1Name { get; set; }
        public string Player2Id { get; set; }
        public string Player2Name { get; set; }

        public PartyCreationRequest()
        {
        }


    }
}