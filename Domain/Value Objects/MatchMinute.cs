using System;

namespace Domain.Value_Objects
{
    class MatchMinute : ValueObject
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

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(MatchMinute))
            {
                return false;
            }
            else
            {
                MatchMinute matchMinuteObject = (MatchMinute)obj;
                return (this.Value == matchMinuteObject.Value) ? true : false; 
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(MatchMinute matchMinuteOne, MatchMinute matchMinuteTwo)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(MatchMinute matchMinuteOne, MatchMinute matchMinuteTwo)
        {
            throw new NotImplementedException();
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
