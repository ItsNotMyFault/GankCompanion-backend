using GankCompanion_backend.applicationserivce;
using GankCompanion_backend.applicationserivce.party;
using GankCompanion_backend.applicationserivce.party.request;
using GankCompanion_backend.applicationserivce.party.response;
using GankCompanion_backend.applicationserivce.session;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace GankCompanion_backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PartyController : Controller
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
        public JsonResult CreateParty(PartyCreationRequest sessionRequest)
        {
            string partyId = this.partyService.CreateParty(sessionRequest);
            return Json(partyId);
        }

        [HttpPost]
        [Route("CloseParty")]
        public JsonResult CloseParty(PartyCloseRequest partyCloseRequest)
        {
            string partyId = this.partyService.CloseParty(partyCloseRequest);
            return Json(partyId);
        }

        [HttpPost]
        [Route("JoinedParty")]
        public JsonResult PlayerJoinedParty(PartyJoinedRequest partyJoinedRequest)
        {
            if(partyJoinedRequest.PartyId == null)
            {
                throw new ArgumentException("Parameter PartyId is null");
            }
            this.partyService.PlayerJoinedParty(partyJoinedRequest);
            return Json(partyJoinedRequest.PartyId);
        }

        [HttpPost]
        [Route("LeftParty")]
        public JsonResult PlayerLeftParty(PartyLeaveRequest partyLeftRequest)
        {
            this.partyService.PlayerLeftParty(partyLeftRequest);
            return Json(partyLeftRequest.PartyId);
        }
    }
}
