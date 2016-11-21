using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;

namespace Domain.Interfaces
{
    public interface IPresentableTeamSeriesEvents
    {
        TeamEvents this[Guid seriesId] { get; }
        IEnumerable<TeamEvents> this [params Guid[] seriesIds] { get; }
    }
}
