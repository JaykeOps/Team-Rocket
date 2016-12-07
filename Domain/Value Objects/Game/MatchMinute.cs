using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class MatchMinute : ValueObject<MatchMinute>
    {
        public int Value { get; }

        public MatchMinute(int value)
        {
            if (value.IsMatchMinute())
            {
                this.Value = value;
            }
            else
            {
                throw new ArgumentException();
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