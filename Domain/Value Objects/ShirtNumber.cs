using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;

namespace Domain.Entities
{
    public class ShirtNumber : ValueObject<ShirtNumber>
    {
        private int? value;
        public Guid PlayerTeamId { get; }
        public int? Value
        {
            get
            {
                if (value.Equals(null))
                {
                    throw new ShirtNumberMissingTeamIdException("The player can't have a Shirt number value, " +
                        "since the player has no team assigned.");
                }
                else
                {
                    return this.value;
                }
            }
        }

        public ShirtNumber()
        {
            this.PlayerTeamId = Guid.Empty;
            this.value = null;
        }
        public ShirtNumber(Guid playerTeamId, int number)
        {
            if (number >= 0 && number < 100)
            {
                if (this.IsAvailable(number))
                {
                    this.value = number;
                }
                else
                {
                    throw new ShirtNumberAlreadyInUseException("The number you entered could not be assigned. " +
                        $"The team has already assigned '{number}' to a player.");
                }
            }
            else
            {
                throw new IndexOutOfRangeException($"Your entry '{number}' is not a valid football shirt number. " +
                    "A football shirt number can't be less than 0 or greater than 100.");
            }
        }

        private bool IsAvailable(int number)
        {
            var teamService = new TeamService();
            var teams = teamService.GetAll();
            var team = teams.Where(x => x.Id.Equals(this.PlayerTeamId)).FirstOrDefault();
            if (team.Id.Equals(Guid.Empty))
            {
                throw new ShirtNumberMissingTeamIdException("The player can't have a Shirt number value, " +
                        "since the player has no team assigned.");
            }
            else
            {
                return !team.UnUsedShirtNumbers.Select(x => x.Value).Contains(number);
            }
            //TODO: Test this algorithm
        }

        public static bool TryParse(Guid teamId, int value, out ShirtNumber result)
        {
            try
            {
                result = new ShirtNumber(teamId, value);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}