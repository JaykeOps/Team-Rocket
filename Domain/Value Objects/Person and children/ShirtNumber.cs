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
            try
            {
                if (this.Value.IsValidShirtNumber(this.PlayerTeamId))
                {
                    this.Value = number;
                }
            }
            catch (ShirtNumberAlreadyInUseException ex)
            {
                this.Value = -1;
                throw ex;
            }
            catch (IndexOutOfRangeException ex)
            {
                this.Value = -1;
                throw ex;
            }
        }

        public ShirtNumber(Guid teamId)
        {
            this.PlayerTeamId = teamId;
            this.Value = -1;
        }
    }
}