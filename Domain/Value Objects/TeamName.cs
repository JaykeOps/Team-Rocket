using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    public class TeamName
    {
        public string Value { get; }

        public TeamName(string teamName)
        {
            if (teamName.IsValidTeamName(true))
            {
                this.Value = teamName;
            }
            else
            {
                throw new ArgumentException("Not a valid teamname");
            }
        }

        public static bool TryParse(string teamName, out TeamName result)
        {
            try
            {
                result = new TeamName(teamName);
                return true;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
        }

        public override string ToString()
        {
            return $"{this.Value}";
        }
    }
}