using GankCompanion_backend.domain;
using System.Collections.Generic;

namespace GankCompanion_backend.infrastructure
{
    public interface PartyRepository
    {
        public string Save(Party party);
        public List<Party> FindAll();
        public Party FindbyId(string partyId);
        public List<Party> FindPartiesByPlayerName(string playerName);
        public void DeletebyId(string firebaseUniqueId);
    }
}