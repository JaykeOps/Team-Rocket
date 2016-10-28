using System;
using System.Globalization;

namespace Domain.Value_Objects
{
    public class DateOfBirth
    {
        public DateOfBirth(string dateOfbirth)
        {
            if (this.IsValid(dateOfbirth))
            {
                this.Value = Convert.ToDateTime(dateOfbirth);
            }
        }

        public DateTime Value { get; }

        public bool IsValid(string value)
        {
            DateTime result;
            if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result))
            {
                return !this.IsFuture(result) && !this.IsMoreThanAHundredYearsOld(result) ?
                    true : false;
            }
            else
            {
                return false;
            }
        }

        private bool IsFuture(DateTime dateTime)
        {
            return dateTime > DateTime.Now ? true : false;
        }

        private bool IsMoreThanAHundredYearsOld(DateTime dateTime)
        {
            return dateTime.Year < DateTime.Now.Year - 100 ? true : false;
        }

        public override string ToString()
        {
            return $"{this.Value: yyyy-MM-dd}";
        }
    }
}