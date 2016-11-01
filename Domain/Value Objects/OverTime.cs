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

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(OverTime))
            {
                return false;
            }
            else
            {
                OverTime overTimeObject = (OverTime)obj;
                return (this.Value == overTimeObject.Value) ? true : false; 
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(OverTime overTimeOne, OverTime overTimeTwo)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(OverTime overTimeOne, OverTime overTimeTwo)
        {
            throw new NotImplementedException();
        }
    }
}
