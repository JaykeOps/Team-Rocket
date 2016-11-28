using Domain.Helper_Classes;
using System;
using System.Globalization;

namespace Domain.Value_Objects
{
    [Serializable]
    public class MatchDateAndTime : ValueObject<MatchDateAndTime>
    {
        public const string FORMAT = "yyyy-MM-dd HH:mm";
        public DateTime Value { get; }

        public MatchDateAndTime(DateTime dateTime)
        {
            if (dateTime.IsValidMatchDateAndTime())
            {
                this.Value = dateTime;
            }
            else
            {
                throw new FormatException("Invalid date. Date cannot be in the past and cannot be more than 2 years from today");
            }
        }

        public static bool TryParse(string inputDateTime, out MatchDateAndTime result)
        {
            result = null;
            DateTime dateTime;
            if (DateTime.TryParseExact(inputDateTime, FORMAT, CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateTime))
            {
                if (dateTime.IsValidMatchDateAndTime())
                {
                    result = new MatchDateAndTime(dateTime);
                    return true;
                }
                return false;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{this.Value:yyyy-MM-dd HH:mm}";
        }
    }
}