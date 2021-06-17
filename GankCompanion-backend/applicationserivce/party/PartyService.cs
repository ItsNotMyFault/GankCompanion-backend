using GankCompanion_backend.applicationserivce.party;
using GankCompanion_backend.applicationserivce.party.request;
using GankCompanion_backend.applicationserivce.party.response;
using GankCompanion_backend.applicationserivce.session;
using GankCompanion_backend.domain;
using GankCompanion_backend.infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace GankCompanion_backend.applicationserivce
{
    public class PartyService
    {
        private readonly IPartyRepository partyRepository;
        private readonly double minutesIn1Day = 1440;
        private readonly int MAX_PARTY_LIST_COUNT = 100;
        public PartyService(IPartyRepository partyRepository)
        {
            this.partyRepository = partyRepository;
        }


        public string CreateParty(PartyCreationRequest partyCreationRequest)
        {
            Party party = new Party(partyCreationRequest);
            string partyFirebaseId = partyRepository.Save(party);
            return partyFirebaseId;
        }
        public void PlayerJoinedParty(PartyJoinedRequest partyJoinedRequest)
        {
            Party party = partyRepository.FindPartyByPartyID(partyJoinedRequest.PartyId);
            partyRepository.DeletebyId(party.FirebaseId.Id);
            party.PlayerJoinParty(partyJoinedRequest.PlayerJoinedId, partyJoinedRequest.PlayerJoinedName);
            partyRepository.Save(party);
        }

        public void PlayerLeftParty(PartyLeaveRequest partyLeftRequest)
        {
            Party party = partyRepository.FindPartyByPartyID(partyLeftRequest.PartyId);
            partyRepository.DeletebyId(party.FirebaseId.Id);
            party.PlayerLeaveParty(partyLeftRequest.PlayerLeftId);
            partyRepository.Save(party);
        }

        public PartyListResponse GetPartyById(string partyId)
        {
            Party party = this.partyRepository.FindPartybyFirebaseUniqueId(partyId);
            return new PartyListResponse(party);
        }

        public PartyListResponse GetAllParties()
        {
            //TODO only retrieve most recent.
            List<Party> partyList = this.partyRepository.FindAll();
            partyList = partyList.Take(MAX_PARTY_LIST_COUNT).ToList();
            foreach (Party party in partyList)
            {
                CheckIfPartyExtends1Day(party);
            }
            return new PartyListResponse(partyList);
        }

        public PartyMembersResponse GetPartyMembers(string partyId)
        {
            Party party = this.partyRepository.FindPartybyFirebaseUniqueId(partyId);

            return party.GetPartyMembers();
        }

        public PartyReportResponse GetPartyReport(string partyId)
        {
            Party party = this.partyRepository.FindPartybyFirebaseUniqueId(partyId);
            PartyReportResponse partyReportResponse = party.GetPartyReport();
            return partyReportResponse;
        }

        public PartyMembersResponse GetPartyMembersByPartyId(string partyId)
        {
            Party party = this.partyRepository.FindPartybyFirebaseUniqueId(partyId);
            return party.GetPartyMembers();
        }

        public PartyListResponse GetPartiesByPlayerName(string playerName)
        {
            List<Party> playerInParties;

            if (playerName != null && playerName != "")
            {

                playerInParties = this.partyRepository.FindPartiesByPlayerName(playerName.ToLower());
                foreach (Party party in playerInParties)
                {
                    CheckIfPartyExtends1Day(party);
                }
            }
            else
            {
                playerInParties = new List<Party>();
            }

            PartyListResponse partyResponse = new PartyListResponse(playerInParties);
            return partyResponse;
        }

        public string CloseParty(PartyCloseRequest partyCloseRequest)
        {
            Party party = this.partyRepository.FindPartybyFirebaseUniqueId(partyCloseRequest.PartyId);
            party.CloseParty();
            this.partyRepository.DeletebyId(party.FirebaseId.Id);
            return partyRepository.Save(party);
        }

        private void CheckIfPartyExtends1Day(Party party)
        {
            if (party.IsActive && party.GetPartyDuration() > minutesIn1Day)
            {
                party.CloseParty();
                this.partyRepository.DeletebyId(party.FirebaseId.Id);
                this.partyRepository.Save(party);
            }
        }
    }
}
