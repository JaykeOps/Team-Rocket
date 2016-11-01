using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }

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

        public override bool Equals(object obj)
        {
            var phone = obj as PhoneNumber;
            return phone.Value == this.Value;
        }

        public static bool operator !=(PhoneNumber phoneOne, PhoneNumber phoneTwo)
        {
            return phoneOne.Value != phoneTwo.Value;
        }

        public static bool operator ==(PhoneNumber phoneOne, PhoneNumber phoneTwo)
        {
            return phoneOne.Value == phoneTwo.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Value}";
        }
    }
}