using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    class OverTime
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
