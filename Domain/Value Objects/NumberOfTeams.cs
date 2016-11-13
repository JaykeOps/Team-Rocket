using System;

namespace Domain.Value_Objects
{
    public class NumberOfTeams
    {
        public int Value { get; }

        public NumberOfTeams(int numberOfTeams)
        {
            if (IsNumberOfTeamsValid(numberOfTeams))
            {
                this.Value = numberOfTeams;
            }
            else
            {
                throw new ArgumentException("Number Of teams must be even");
            }
        }

        private static bool IsNumberOfTeamsValid(int numberOfTeams)
        {
            return numberOfTeams % 2 == 0 && numberOfTeams > 2;
        }

        public static bool TryParse(string numberOfTeams, out NumberOfTeams result)
        {
            result = null;
            int numbersOfTeams;
            if (int.TryParse(numberOfTeams, out numbersOfTeams))
            {
                if (IsNumberOfTeamsValid(numbersOfTeams))
                {
                    result = new NumberOfTeams(numbersOfTeams);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{this.Value}";
        }
    }
}