using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    public class ArenaName : ValueObject<ArenaName>
    {
        public string Value { get; }

        public ArenaName(string arenaName)
        {
            if (arenaName.IsValidArenaName(true))
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

        public override string ToString()
        {
            return $"{this.Value}";
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}