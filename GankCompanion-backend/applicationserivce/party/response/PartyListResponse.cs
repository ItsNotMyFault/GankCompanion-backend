using GankCompanion_backend.applicationserivce.party.response;
using GankCompanion_backend.domain;
using System.Collections.Generic;

namespace GankCompanion_backend.applicationserivce.session
{
    public class PartyListResponse
    {
        public List<PartyResponse> partyMembersResponse { get; set; }
        public PartyListResponse(List<Party> partyMembers)
        {
            partyMembersResponse = new List<PartyResponse>();
            foreach (Party party in partyMembers)
            {
                PartyResponse partyResponse = ToResponse(party);
                partyMembersResponse.Add(partyResponse);
            }
        }

        public PartyListResponse(Party party)
        {
            PartyResponse partyResponse = new PartyResponse
            {
                createdDate = party.PartyStartTime.ToString(),
                duration = party.GetPartyDuration().ToString(),
                isActive = party.IsActive,
                partyFirebaseId = party.FirebaseId.Id,
                players = party.GetPlayers()
            };

            this.partyMembersResponse = new List<PartyResponse>
            {
                partyResponse
            };
        }

        private PartyResponse ToResponse(Party party)
        {
            return new PartyResponse
            {
                createdDate = party.PartyStartTime.ToString(),
                duration = party.GetPartyDuration().ToString(),
                isActive = party.IsActive,
                partyFirebaseId = party.FirebaseId.Id,
                players = party.GetPlayers()
            };
        }
    }
}
