using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Team
    {
        //private HashSet<ShirtNumber> unUsedShirtNumbers;
        public Guid Id { get; }

        public TeamName Name { get; set; }
        public HashSet<Guid> PlayerIds { get; }
        public ArenaName Arena { get; set; }
        public EmailAddress Email { get; set; }
        public ShirtNumbers ShirtNumbers { get; }

        public Team(TeamName name, ArenaName arenaName, EmailAddress email)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.PlayerIds = new HashSet<Guid>();
            this.Arena = arenaName;
            this.Email = email;

            this.ShirtNumbers = new ShirtNumbers(this);
        }

        public override string ToString()
        {
            return $"{this.Name.Value}";
        }
    }
}