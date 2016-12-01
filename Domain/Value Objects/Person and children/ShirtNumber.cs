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
                if (this.PlayerTeamId == Guid.Empty)
                {
                    this.Value = -1;
                }
                this.Value = number.IsValidShirtNumber(this.PlayerTeamId) ? number : -1;
            }
            catch (ShirtNumberAlreadyInUseException ex)
            {
                throw ex;
            }
            catch (IndexOutOfRangeException ex)
            {
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