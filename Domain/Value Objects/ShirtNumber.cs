using System;
using System.Collections.Generic;
using Domain.Value_Objects;

namespace DomainTests.Entities
{
    public class ShirtNumber:ValueObject<ShirtNumber>
    {
        public int Value { get; }

        public ShirtNumber(int number)
        {
            if (number >= 0 && number < 100)
            {
                if (this.IsAvailable(number))
                {
                    this.Value = number;
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
            return !this.TeamShirtNumbersTempSimulation(number);
        }

        public bool TeamShirtNumbersTempSimulation(int number)
        {
            var teamShirtNumbersSimulaton = new HashSet<int>()
            {
                0, 3, 6, 7, 8, 9, 14, 12, 2, 19, 27, 32, 99, 14, 18, 19, 20, 32, 55
            };

            return teamShirtNumbersSimulaton.Contains(number);
        }

        public static bool TryParse(int value, out ShirtNumber result)
        {
            try
            {
                result = new ShirtNumber(value);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}