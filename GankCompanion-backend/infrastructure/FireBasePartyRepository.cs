using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using GankCompanion_backend.domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace GankCompanion_backend.infrastructure
{
    public class FireBasePartyRepository : PartyRepository
    {
        private readonly string DATABSE_ENDPOINT = "https://gankcompanion-default-rtdb.firebaseio.com/";
        private string ENDPOINT_URL;
        private static readonly HttpClient client = new HttpClient();

        private IFirebaseConfig firebaseConfig;

        private IFirebaseClient firebaseClient;

        public FireBasePartyRepository()
        {
            this.ENDPOINT_URL = DATABSE_ENDPOINT + "party.json";
            this.firebaseConfig = new FirebaseConfig
            {
                AuthSecret = "",
                BasePath = DATABSE_ENDPOINT,
            };
        }

        public string Save(Party party)
        {
            firebaseClient = new FirebaseClient(firebaseConfig);
            PushResponse response = firebaseClient.Push("party/", party);
            PushResult responseResult = response.Result;
            string partyFirebaseId = responseResult.name;
            firebaseClient.Set("party/" + partyFirebaseId, party);
            return partyFirebaseId;
        }

        public List<Party> FindAll()
        {

            firebaseClient = new FirebaseClient(firebaseConfig);
            FirebaseResponse response = firebaseClient.Get("party");
            Dictionary<string, Party> data = JsonConvert.DeserializeObject<Dictionary<string, Party>>(response.Body);
            List<Party> parties = data.Select(i =>
            {
                i.Value.FirebaseId = new FirebaseUniqueID(i.Key);
                return i.Value;
            }).ToList();
            return parties;
        }

        public List<Party> FindPartiesByPlayerName(string playerName)
        {

            firebaseClient = new FirebaseClient(firebaseConfig);
            FirebaseResponse response = firebaseClient.Get("party");
            if (response.Body == "null") throw new Exception("This player has never been in a party.");
            Dictionary<string, Party> data = JsonConvert.DeserializeObject<Dictionary<string, Party>>(response.Body);

            List<Party> parties = data.Select(i =>
            {
                i.Value.FirebaseId = new FirebaseUniqueID(i.Key);
                return i.Value;
            }).ToList();
            parties = parties.FindAll(x =>
            {
                PartyMember partyMember = x.PartyMemberList.Find(x => x.player.Name.Equals(playerName)); ;
                if (partyMember == null)
                    return false;
                return true;
            }).ToList();
            return parties;
        }


        public Party FindbyId(string firebaseUniqueId)
        {
            firebaseClient = new FirebaseClient(firebaseConfig);
            FirebaseResponse firebaseResponse = firebaseClient.Get("party/" + firebaseUniqueId);
            Party party = JsonConvert.DeserializeObject<Party>(firebaseResponse.Body);
            if (party == null)
                throw new Exception("This partyId doesn't exist");
            party.FirebaseId = new FirebaseUniqueID(firebaseUniqueId);
            return party;
        }

        public void DeletebyId(string firebaseUniqueId)
        {
            firebaseClient = new FirebaseClient(firebaseConfig);
            FirebaseResponse firebaseResponse = firebaseClient.Delete("party/" + firebaseUniqueId);
            if (firebaseResponse.Body != "null")
                throw new Exception("La suppression du Party n'a pas pu être possible");
        }


        public void AuthenticateToFirebase(string firebaseUniqueId)
        {
            firebaseClient = new FirebaseClient(firebaseConfig);

            //firebaseClient.CreateUser
            //FirebaseResponse firebaseResponse = firebaseClient.Delete("party/" + firebaseUniqueId);
            //if (firebaseResponse.Body != "null")
            //    throw new Exception("La suppression du Party n'a pas pu être possible");
        }

    }
}
