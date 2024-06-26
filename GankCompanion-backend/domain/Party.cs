﻿using GankCompanion_backend.applicationserivce;
using GankCompanion_backend.applicationserivce.party;
using GankCompanion_backend.applicationserivce.party.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.domain
{


    public class Party
    {
        public PartyID PartyId { get; set; }
        public FirebaseUniqueID FirebaseId;
        public TimeInterval PartyTimeInterval { get; set; }
        public List<PartyMember> PartyMemberList { get; set; }
        public bool IsActive;

        public Party()
        {
            this.IsActive = true;
            this.PartyMemberList = new List<PartyMember>();
        }

        public Party(PartyCreationRequest partyCreationRequest)
        {
            this.PartyId = new PartyID(partyCreationRequest.PartyId);
            DateTimeWithZone PartyStartTime = new DateTimeWithZone(partyCreationRequest.PartyStartTime, TimeZoneInfo.Local);
            this.PartyTimeInterval = new TimeInterval(PartyStartTime.UniversalTime, PartyStartTime.UniversalTime);

            this.IsActive = true;
            this.PartyMemberList = new List<PartyMember>
            {
                CreatePartyMember(partyCreationRequest.Player1Id, partyCreationRequest.Player1Name, true),
                CreatePartyMember(partyCreationRequest.Player2Id, partyCreationRequest.Player2Name, false)
            };
        }

        public string GetPlayers()
        {
            List<string> playerNames = this.PartyMemberList.Where(x => x != null).Select(x => x.player.Name).ToList();
            string players = string.Join(",", playerNames);
            return players;
        }

        public void PlayerJoinParty(string playerId, string playerName)
        {
            PartyMember partyPlayer = this.PartyMemberList.Find(x => x.partyMemberId.SequenceEqual(playerId));
            if (partyPlayer != null)
            {
                partyPlayer.PlayerJoinParty();
            }
            else
            {
                PartyMember partyMember = CreatePartyMember(playerId, playerName, false);
                this.PartyMemberList.Add(partyMember);
            }
        }

        private PartyMember CreatePartyMember(string playerId, string playerName, bool isPartyLead)
        {
            return new PartyMember(playerId, new Player(playerName.ToLower()), isPartyLead);
        }


        public List<PartyMember> GetActivePartyMembers()
        {
            return this.PartyMemberList.FindAll(x => x.isInParty == true);
        }

        public override string ToString()
        {
            return "Contains " + PartyMemberList.Count + " member(s)";
        }

        public void PlayerLeaveParty(string PlayerLeavingId)
        {
            PartyMember ptMember = this.PartyMemberList.Find(x => x.partyMemberId.Equals(PlayerLeavingId));
            if (ptMember == null)
                throw new Exception("Player is not in party");
            if (ptMember.IsPartyLead)
            {
                CloseParty();
            }
            else
            {
                ptMember.SetLeaveParty();
            }
        }

        public void CloseParty()
        {
            SetAllMemberToLeftParty();
            this.IsActive = false;
            this.PartyTimeInterval.EndTime = new DateTimeWithZone(DateTime.Now, TimeZoneInfo.Local).UniversalTime;
        }

        public void SetAllMemberToLeftParty()
        {
            foreach (PartyMember pMember in PartyMemberList)
            {
                pMember.SetLeaveParty();
            }
        }

        public PartyMembersResponse GetPartyMembers()
        {
            double partyDuration = this.GetPartyDuration();
            PartyMembersResponse partyMembersResponse = new PartyMembersResponse
            {
                partyMemberResponses = new List<PartyMemberResponse>(),
                duration = partyDuration.ToString()
            };

            foreach (PartyMember partyMember in this.PartyMemberList)
            {
                if(partyMember != null)
                {
                    PartyMemberResponse partyMemberResponse = new PartyMemberResponse()
                    {
                        joinedTime = partyMember.JoinedParty.ToString(),
                        percentTimeInParty = partyMember.GetPercentageOfTimeInParty(partyDuration),
                        playerName = partyMember.player.Name,
                        timeInParty = partyMember.GetPresenceTimeInParty().ToString("#.##"),
                        playerIsInParty = partyMember.isInParty
                    };
                    partyMembersResponse.partyMemberResponses.Add(partyMemberResponse);
                }
            }
            return partyMembersResponse;
        }

        public PartyReportResponse GetPartyReport()
        {
            PartyReportResponse partyReportResponse = new PartyReportResponse
            {
                startTime = this.PartyTimeInterval.StartTime.ToString(),
                duration = this.GetPartyDuration().ToString(),
                totalPlayers = this.PartyMemberList.Count.ToString(),
                totalKills = "Too many to count"
            };
            return partyReportResponse;
        }

        //make method get list of events x players joins/leave party.

        public double GetPartyDuration()
        {
            if (IsActive)
            {
                this.PartyTimeInterval.EndTime = DateTime.UtcNow;
            }
            return this.PartyTimeInterval.GetTimeInterval();
        }


    }
}
