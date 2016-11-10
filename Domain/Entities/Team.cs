using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Team
    {
        private List<ShirtNumber> usedShirtNumbers;
        private List<ShirtNumber> unUsedShirtNumbers;
        public Guid Id { get; }
        public TeamName Name { get; set; }
        public HashSet<Guid> PlayerIds { get; }
        public ArenaName Arena { get; set; }
        public EmailAddress Email { get; set; }
        public IEnumerable<ShirtNumber> UnUsedShirtNumbers
        {
            get { return this.unUsedShirtNumbers; }
        }

        public Team(TeamName name, ArenaName arenaName, EmailAddress email)
        {
            this.Id = new Guid();
            this.Name = name;
            this.PlayerIds = new HashSet<Guid>();
            this.Arena = arenaName;
            this.Email = email;
            this.usedShirtNumbers = new List<ShirtNumber>();
            this.unUsedShirtNumbers = new List<ShirtNumber>();
            this.SetUnUsedShirtNumbersToDefaultValue();
        }

        private void SetUnUsedShirtNumbersToDefaultValue()
        {
            for (int i = 0; i < 99; i++)
            {
                this.unUsedShirtNumbers.Add(new ShirtNumber(i));
            }
        }

        public override string ToString()
        {
            return $"{this.Name.Value}";
        }
    }
}