using Domain.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class PhoneNumber
    {
        public PhoneNumber(string phoneNumber)
        {
            if (phoneNumber.IsValidCellPhoneNumber(false))
            {
                this.Value = phoneNumber;
            }
            else
            {
                throw new FormatException($"Your entry '{phoneNumber}' "
                    + "did not follow the format restrictions for phone number." +
                    "Make sure your entry starts with 3-6 numbers followed by a '-' " +
                    "followed by 6-9 numbers.");
            }
        }

        public string Value { get; }

        public static bool TryParse(string value, out PhoneNumber result)
        {
            try
            {
                result = new PhoneNumber(value);
                return true;
            }
            catch (FormatException)
            {
                result = null;
                return false;
            }
        }

        public override string ToString()
        {
            return $"{this.Value}";
        }
    }
}
