using GankCompanion_backend.applicationserivce.party;
using GankCompanion_backend.applicationserivce.party.response;
using GankCompanion_backend.applicationserivce.session;
using GankCompanion_backend.domain;
using GankCompanion_backend.infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.applicationserivce
{
    public class PartyService
    {
        private readonly PartyRepository partyRepository;
        private double minutesIn1Day = 1440;
        public PartyService(PartyRepository partyRepository)
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
            partyRepository.DeletebyId(partyJoinedRequest.PartyId);
            party.PlayerJoinParty(partyJoinedRequest.PlayerJoinedId, partyJoinedRequest.PlayerJoinedName);
            partyRepository.Save(party);
        }

        public void PlayerLeftParty(PartyLeaveRequest partyLeftRequest)
        {
            Party party = partyRepository.FindbyId(partyLeftRequest.PartyId);
            partyRepository.DeletebyId(partyLeftRequest.PartyId);
            party.PlayerLeaveParty(partyLeftRequest.PlayerLeftId);
            partyRepository.Save(party);
        }

        public PartyListResponse GetPartyById(string partyId)
        {
            Party party = this.partyRepository.FindbyId(partyId);
            return new PartyListResponse(party);
        }

        public PartyListResponse GetAllParties()
        {
            List<Party> partyList = this.partyRepository.FindAll();
            foreach(Party party in partyList)
            {
                if (party.IsActive && party.GetPartyDuration() > minutesIn1Day)
                {
                    party.CloseParty();
                    this.partyRepository.DeletebyId(party.FirebaseId.Id);
                    this.partyRepository.Save(party);
                }
            }
            return new PartyListResponse(partyList);
        }

        public PartyMembersResponse GetPartyMembers(string partyId)
        {
            Party party = this.partyRepository.FindbyId(partyId);

            return party.GetPartyMembers();
        }

        public PartyReportResponse GetPartyReport(string partyId)
        {
            Party party = this.partyRepository.FindbyId(partyId);
            PartyReportResponse partyReportResponse = party.GetPartyReport();
            return partyReportResponse;
        }

        public PartyMembersResponse GetPartyMembersByPartyId(string playerId)
        {
            Party party = this.partyRepository.FindbyId(playerId);
            return party.GetPartyMembers();
        }

        public PartyListResponse GetPartiesByPlayerName(string playerName)
        {
            List<Party> playerInParties = this.partyRepository.FindPartiesByPlayerName(playerName);
            foreach (Party party in playerInParties)
            {
                if (party.IsActive && party.GetPartyDuration() > minutesIn1Day)
                {
                    party.CloseParty();
                    this.partyRepository.DeletebyId(party.FirebaseId.Id);
                    this.partyRepository.Save(party);
                }
            }
            PartyListResponse partyResponse = new PartyListResponse(playerInParties);
            return partyResponse;
        }
    }
}
