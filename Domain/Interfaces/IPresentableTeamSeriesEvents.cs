using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPresentableTeamSeriesEvents
    {
        IPresentableTeamEvents this[Guid seriesId] { get; }
        IEnumerable<IPresentableTeamEvents> this [params Guid[] seriesIds] { get; }
    }
}
