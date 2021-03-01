using System.Collections.Generic;
using System.Text.Json;
using GankCompanion_backend.applicationserivce;
using GankCompanion_backend.applicationserivce.party;
using GankCompanion_backend.applicationserivce.party.request;
using GankCompanion_backend.applicationserivce.party.response;
using GankCompanion_backend.applicationserivce.session;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GankCompanion_backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PartyController : ControllerBase
    {
        private readonly PartyService partyService;
        public PartyController(PartyService partyService)
        {
            this.partyService = partyService;
        }

        [HttpGet]
        [Route("GetAllParties")]
        public PartyListResponse GetAllParties()
        {
            PartyListResponse sessionResponse = this.partyService.GetAllParties();
            return sessionResponse;
        }

        [HttpGet]
        [Route("GetPartyMembers")]
        public PartyMembersResponse GetPartyMembers(string partyId)
        {
            return this.partyService.GetPartyMembers(partyId); ;
        }

        [HttpGet]
        [Route("GetPartyReport")]
        public PartyReportResponse GetPartyReport(string partyId)
        {
            return this.partyService.GetPartyReport(partyId); ;
        }


        [HttpGet]
        [Route("GetParty")]
        public PartyListResponse GetParty(string partyId)
        {
            return this.partyService.GetPartyById(partyId);
        }

        [HttpGet]
        [Route("GetPartiesByPlayerName")]
        public PartyListResponse GetPartiesByPlayerName(string playerName)
        {
            return this.partyService.GetPartiesByPlayerName(playerName);
        }

        [HttpGet]
        [Route("GetPartyMembersByPartyId")]
        public PartyMembersResponse GetPartyMembersByPartyId(string partyId)
        {
            return this.partyService.GetPartyMembersByPartyId(partyId);
        }

        [HttpPost]
        [Route("CreateParty")]
        public string CreateParty(PartyCreationRequest sessionRequest)
        {
            string partyId = this.partyService.CreateParty(sessionRequest);
            return partyId;
        }

        [HttpPost]
        [Route("CloseParty")]
        public string CloseParty(PartyCloseRequest partyCloseRequest)
        {
            string partyId = this.partyService.CloseParty(partyCloseRequest);
            return partyId;
        }

        [HttpPost]
        [Route("JoinedParty")]
        public string PlayerJoinedParty(PartyJoinedRequest partyJoinedRequest)
        {
            this.partyService.PlayerJoinedParty(partyJoinedRequest);
            return partyJoinedRequest.PartyId;
        }

        [HttpPost]
        [Route("LeftParty")]
        public string PlayerLeftParty(PartyLeaveRequest partyLeftRequest)
        {
            this.partyService.PlayerLeftParty(partyLeftRequest);
            return partyLeftRequest.PartyId;
        }
    }
}
