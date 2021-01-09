using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GankCompanion_backend.domain
{
    public class Player
    {
        public Player(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public int GankParticipationCount { get; set; }
    }
}

