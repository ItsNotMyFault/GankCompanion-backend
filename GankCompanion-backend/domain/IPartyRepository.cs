using GankCompanion_backend.domain;
using System.Collections.Generic;

namespace GankCompanion_backend.infrastructure
{
    public interface IPartyRepository
    {
        public string Save(Party party);
        public List<Party> FindAll();
        public Party FindbyFirebaseUniqueId(string partyId);
        public Party FindPartyByPartyID(string partyID);
        public List<Party> FindPartiesByPlayerName(string playerName);
        public void DeletebyId(string firebaseUniqueId);
    }
}