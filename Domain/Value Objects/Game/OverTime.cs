using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class OverTime : ValueObject<OverTime>
    {
        public int Value { get; }

        

        public OverTime(int value)
        {
            if (this.IsOverTime(value))
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
            return value >= 0 && value <= 30;
        }
    }
}