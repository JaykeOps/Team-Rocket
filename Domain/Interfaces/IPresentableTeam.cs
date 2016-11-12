using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentableTeam
    {
        TeamName Name { get; set; }
        IEnumerable<Player> Players { get; }
        ArenaName ArenaName { get; set; }
        EmailAddress Email { get; set; }
        IPresentableTeamStats Stats { get; }
        IPresentableTeamEvents Events { get; }
        ShirtNumbers ShirtNumbers { get; }
    }
}