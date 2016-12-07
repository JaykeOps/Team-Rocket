using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class MatchDuration : ValueObject<MatchDuration>
    {
        public TimeSpan Value { get; }

        public MatchDuration(TimeSpan duration)
        {
            if (duration.IsValidMatchDuration())
            {
                this.Value = duration;
            }
            else
            {
                throw new ArgumentException("Invalid match duration format. Value must be 10-90 min");
            }
        }

        public static bool TryParse(string value, out MatchDuration result)
        {
            try
            {
                result = new MatchDuration(StringMinutesToTimeSpanConverter(value));
                return true;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
        }

        private static TimeSpan StringMinutesToTimeSpanConverter(string minutes)
        {
            var timespan = int.Parse(minutes);
            return new TimeSpan(timespan * 6000000000 / 10);
        }

        public override string ToString()
        {
            return $"{this.Value.TotalMinutes}";
        }
    }
}