using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class MatchDateAndTime
    {
        public DateTime Value { get; set; }
        public const string Format = "yyyy-MM-dd-HH:mm";

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
            if (DateTime.TryParseExact(inputDateTime, Format, CultureInfo.InvariantCulture,
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

    }
}
