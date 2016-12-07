using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class OverTime : ValueObject<OverTime>
    {
        public int Value { get; }

        public OverTime(int value)
        {
            if (value.IsValidOverTime())
            {
                this.Value = value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}