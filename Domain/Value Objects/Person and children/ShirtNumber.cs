using Domain.Helper_Classes;
using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class ShirtNumber : ValueObject<ShirtNumber>
    {
        public Guid PlayerTeamId { get; }

        public int Value { get; }

        public ShirtNumber(Guid teamId, int number)
        {
            this.PlayerTeamId = teamId;
            this.Value = this.Value.IsAvailableShirtNumber(this.PlayerTeamId) ? number : -1;
        }

        public ShirtNumber(Guid teamId)
        {
            this.PlayerTeamId = teamId;
            this.Value = -1;
        }
    }
}