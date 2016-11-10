using System;

namespace Domain.Value_Objects
{
    public class MatchMinute : ValueObject<MatchMinute>
    {
        public int Value { get; }

        public MatchMinute(int value)
        {
            if (IsMatchMinute(value))
            {
                this.Value = value;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private bool IsMatchMinute(int value)
        {
            if (value >= 1 && value <= 90 + 30) // Standard game time is 90 minutes (or whatever the series defines it as) and maximum overtime is 30 minutes.
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            if (this.Value <= 90)
            {
                return $"{this.Value}";
            }
            else
            {
                return $"90+{this.Value - 90}";
            }
        }
    }
}