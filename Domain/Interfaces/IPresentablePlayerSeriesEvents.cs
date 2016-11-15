using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPresentablePlayerSeriesEvents
    {
        IPresentablePlayerEvents this[Guid seriesId] { get; }
        IEnumerable<IPresentablePlayerEvents> this[params Guid[] seriesIds] { get; }
    }
}
