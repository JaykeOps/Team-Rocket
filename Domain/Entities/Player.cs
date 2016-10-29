using Domain.Value_Objects;
using football_series_manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests.Entities
{
    public class Player : Person
    {
        public Player(Name name, DateOfBirth dateOfBirth, PersonContactInformation contactInformation,
            PlayerPosition position) 
            : base(name, dateOfBirth, contactInformation)
        {
            
        }

        public PlayerPosition Position { get; }
        public PlayerStatus Status { get; }
        public ShirtNumber ShirtNumber { get; }
    }
}
