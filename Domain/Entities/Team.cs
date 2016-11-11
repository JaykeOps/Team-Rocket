using Domain.Services;
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
        //public HashSet<ShirtNumber> UnUsedShirtNumbers
        //{
        //    get { return this.unUsedShirtNumbers; }
        //}

        public Team(TeamName name, ArenaName arenaName, EmailAddress email)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.PlayerIds = new HashSet<Guid>();
            this.Arena = arenaName;
            this.Email = email;
            //this.unUsedShirtNumbers = new HashSet<ShirtNumber>();
            //this.SetUnUsedShirtNumbersToDefaultValue();
            this.ShirtNumbers = new ShirtNumbers(this);
            
        }

        //private void SetUnUsedShirtNumbersToDefaultValue()
        //{
        //    for (int i = 0; i < 99; i++)
        //    {
        //        this.unUsedShirtNumbers.Add(new ShirtNumber(this.Id, i));
        //    }
        //}

        public override string ToString()
        {
            return $"{this.Name.Value}";
        }
    }
}