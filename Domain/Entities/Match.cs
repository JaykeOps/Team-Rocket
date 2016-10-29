using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Value_Objects;

namespace Domain.Entities
{
    public class Match:IGameDuration
    {
        public Guid Id { get; set; }
        public ArenaName Location { get; set; }
        public MatchDuration MatchDuration { get; set; }


    }
}
