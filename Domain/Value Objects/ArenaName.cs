using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class ArenaName
    {
        public string Value { get; set; }

        public ArenaName(string arenaName)
        {
            if (IsArenaName(arenaName))
            {
                this.Value = arenaName;
            }
            else
            {
                throw new ArgumentException("Not a valid Arenaname");
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

        private static bool IsArenaName(string arenaName)
        {
            return Regex.IsMatch(arenaName, "^[a-zA-Z0-9åäöÅÄÖ]+$", RegexOptions.IgnoreCase) && arenaName.Length < 40;
        }
    }
}
