using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;

namespace Domain.Interfaces
{
    interface IGameDuration
    {
        MatchDuration MatchDuration { get; set; }
    }
}
