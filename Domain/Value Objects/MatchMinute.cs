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
            bool isMM = false;
            // Validation...
            return isMM;
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
    }
}
