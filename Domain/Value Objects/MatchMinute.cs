using System;

namespace Domain.Value_Objects
{
    public class MatchMinute
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
    }
}