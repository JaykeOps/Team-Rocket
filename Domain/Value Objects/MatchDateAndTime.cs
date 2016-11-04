using System;
using System.Globalization;

namespace Domain.Value_Objects
{
    public class MatchDateAndTime : ValueObject<MatchDateAndTime>
    {
        public const string FORMAT = "yyyy-MM-dd HH:mm";
        public DateTime Value { get; }

        public MatchDateAndTime(DateTime dateTime)
        {
            if (IsValidMatchDateAndTime(dateTime))
            {
                this.Value = dateTime;
            }
            else
            {
                throw new ArgumentException("Invalid date. Date cannot be in the past and cannot be more than 2 years from today");
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
                if (IsValidMatchDateAndTime(dateTime))
                {
                    result = new MatchDateAndTime(dateTime);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private static bool IsValidMatchDateAndTime(DateTime date)
        {
            return date > DateTime.Now && date < DateTime.Now + TimeSpan.FromDays(365 * 2);
        }

        public override string ToString()
        {
            return $"{this.Value:yyyy-MM-dd HH:mm tt}";
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}