using System;
using System.Text.RegularExpressions;

namespace Domain.Value_Objects
{
    public class ArenaName
    {
        public string Value { get; }

        public ArenaName(string arenaName)
        {
            if (IsValidArenaName(arenaName))
            {
                this.Value = arenaName;
            }
            else
            {
                throw new ArgumentException("Not a valid arenaname");
            }
        }

        public static bool TryParse(string arenaName, out ArenaName result)
        {
            try
            {
                result = new ArenaName(arenaName);
                return true;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
        }

        private static bool IsValidArenaName(string arenaName)
        {
            return Regex.IsMatch(arenaName, "^[a-zA-Z0-9åäöÅÄÖ]+{2-40}$", RegexOptions.IgnoreCase);
        }

        public override string ToString()
        {
            return $"{this.Value}";
        }
    }
}