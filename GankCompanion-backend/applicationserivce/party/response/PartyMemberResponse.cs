namespace GankCompanion_backend.applicationserivce.party
{
    public class PartyMemberResponse
    {
        public string playerName { get; set; }
        public string joinedTime { get; set; }
        public string timeInParty{ get; set; }
        public string percentTimeInParty { get; set; }
        public bool playerIsInParty { get; set; }
    }
}
