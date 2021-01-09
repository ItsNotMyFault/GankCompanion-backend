using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.domain
{
    public class PartyID
    {
        public PartyID(string Id)
        {
           
            this.Id = Guid.Parse(Id);
        }
        public Guid Id { get; set; }
    }
}
