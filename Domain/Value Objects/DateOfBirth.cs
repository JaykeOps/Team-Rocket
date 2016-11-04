using System;
using System.Globalization;

namespace Domain.Value_Objects
{
    public class DateOfBirth : ValueObject<DateOfBirth>
    {
        public DateTime Value { get; }

        public DateOfBirth(string dateOfbirth)
        {
            if (this.IsValid(dateOfbirth))
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

        public bool IsValid(string value)
        {
            DateTime result;
            if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result))
            {
                return !this.IsFuture(result) && !this.IsMoreThanAHundredYearsOld(result);
            }
            else
            {
                return false;
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

        private bool IsFuture(DateTime dateTime)
        {
            return dateTime.Year > DateTime.Now.Year - 3 ? true : false;
        }

        private bool IsMoreThanAHundredYearsOld(DateTime dateTime)
        {
            return dateTime.Year < 1936 ? true : false;
        }

        public override bool Equals(object obj)
        {
            var dateOfBirth = obj as DateOfBirth;
            return dateOfBirth.Value == this.Value;
        }

        public static bool operator !=(DateOfBirth dateOfBirthOne, DateOfBirth dateOfBirthTwo)
        {
            return dateOfBirthOne != dateOfBirthTwo;
        }

        public static bool operator ==(DateOfBirth dateOfBirthOne, DateOfBirth dateOfBirthTwo)
        {
            return dateOfBirthOne == dateOfBirthTwo;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
        public override string ToString()
        {
            return $"{this.Value:yyyy-MM-dd}";
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}