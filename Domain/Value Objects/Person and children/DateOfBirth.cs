using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class DateOfBirth : ValueObject<DateOfBirth>
    {
        public DateTime Value { get; }

        public DateOfBirth(string dateOfbirth)
        {
            if (dateOfbirth.IsValidBirthOfDate())
            {
                this.Value = Convert.ToDateTime(dateOfbirth);
            }
            else
            {
                this.Value = DateTime.MinValue;
                throw new FormatException($"Date of birth declaration '{dateOfbirth}'" +
                    "failed to follow format restriciton 'yyyy-MM-dd'!");
            }
        }

        public static bool TryParse(string value, out DateOfBirth result)
        {
            try
            {
                result = new DateOfBirth(value);
                return true;
            }
            catch (FormatException)
            {
                result = null;
                return false;
            }
        }

        public override string ToString()
        {
            return $"{this.Value:yyyy-MM-dd}";
        }
    }
}