using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentableTeamEvents
    {
        string TeamName { get; }
        IEnumerable<Game> Games { get; }
        IEnumerable<Goal> Goals { get; }
    }
}