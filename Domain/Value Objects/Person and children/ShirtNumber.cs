using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class ShirtNumber : ValueObject<ShirtNumber>
    {
        public Guid PlayerTeamId { get; }

        public int? Value { get; }

        public ShirtNumber()
        {
            
        }
        public ShirtNumber(Guid teamId, int? number)
        {
            this.PlayerTeamId = teamId;
            this.Value = number;
        }

        public ShirtNumber(int? number)
        {
            this.Value = number;
        }
    }
}