using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class EmailAddress : ValueObject<EmailAddress>
    {
        public string Value { get; }

        public EmailAddress(string emailAddress)
        {
            if (emailAddress.IsValidEmailAddress(true))
            {
                this.Value = emailAddress;
            }
            else
            {
                throw new FormatException($"Your entry '{emailAddress}' " +
                    "did not follow the email format restrictions." +
                    "Make sure your entry does not exceed 30 characters, a domain address and an '@'.");
            }
        }

        public static bool TryParse(string value, out EmailAddress result)
        {
            try
            {
                result = new EmailAddress(value);
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