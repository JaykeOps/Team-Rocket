using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class NumberOfTeams : ValueObject<NumberOfTeams>
    {
        public int Value { get; }

        public NumberOfTeams(int numberOfTeams)
        {
            if (numberOfTeams.IsValidNumberOfTeams())
            {
                this.Value = numberOfTeams;
            }
            else
            {
                throw new ArgumentException("Number Of teams must be even");
            }
        }

        public static bool TryParse(string numberOfTeams, out NumberOfTeams result)
        {
            result = null;
            int numbersOfTeams;
            if (int.TryParse(numberOfTeams, out numbersOfTeams))
            {
                try
                {
                    result = new NumberOfTeams(numbersOfTeams);
                    return true;
                }
                catch (ArgumentException)
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