using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.domain
{
    public class FirebaseUniqueID
    {
        public FirebaseUniqueID(string Id)
        {

            this.Id = Id;
        }
        public string Id { get; set; }
    }
}
