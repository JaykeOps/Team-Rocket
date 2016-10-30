using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Team
    {
        public Guid Id { get;}
        public TeamName Name { get;}
        public HashSet<Guid> PlayerIds { get;}
        public ArenaName Arena { get;}

        public Team(TeamName name, ArenaName arenaName)
        {
            this.Id = new Guid();
            this.Name = name;
            this.PlayerIds = new HashSet<Guid>();
            this.Arena = arenaName;


        }

        public override string ToString()
        {
            return $"{this.Name.Value}"; ;
        }
    }
}