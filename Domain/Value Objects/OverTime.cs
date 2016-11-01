using System;

namespace Domain.Value_Objects
{
    internal class OverTime
    {
        public int Value { get; }

        public OverTime(int value)
        {
            if (IsOverTime(value))
            {
                this.Value = value;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private bool IsOverTime(int value)
        {
            bool isOT = false;
            // Validation...
            return isOT;
        }
    }
}