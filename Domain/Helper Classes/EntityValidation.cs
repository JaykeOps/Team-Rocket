using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Value_Objects;

namespace Domain.Helper_Classes
{
    public static class EntityValidation
    {
        public static bool IsValidGame(this Game game)
        {
            return game.Id != Guid.Empty &&
                   game.AwayTeamId != Guid.Empty &&
                   game.HomeTeamId != Guid.Empty &&
                   game.SeriesId != Guid.Empty &&
                   game.MatchDuration.Value.IsValidMatchDuration() &&
                   game.MatchDuration.Value.IsValidMatchDuration() &&
                   game.Location.Value.IsValidArenaName(true) &&
        }

        private static bool IsGameProtocolValid(GameProtocol protocol)
        {
            return protocol.Goals != null &&
                protocol.Assists != null &&
                protocol.Penalties != null &&
                protocol.Cards != null &&
                protocol.
                protocol.AwayTeamId != Guid.Empty &&
        }
    }
}
