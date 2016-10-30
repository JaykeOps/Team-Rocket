using System;
using System.Text.RegularExpressions;

namespace Domain.Value_Objects
{
    public class TeamName
    {
        public string Value { get; }

        public TeamName(string teamName)
        {
            if (IsValidTeamName(teamName))
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

        private static bool IsValidTeamName(string teamName)
        {
            return Regex.IsMatch(teamName, "^[a-z A-Z0-9åäöÅÄÖ]{1,40}$", RegexOptions.IgnoreCase);
        }

        public override string ToString()
        {
            return $"{this.Value}"; 
        }
    }
}