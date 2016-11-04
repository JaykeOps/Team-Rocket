using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
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

        public override bool Equals(object obj)
        {
            var email = obj as EmailAddress;
            return email.Value == this.Value;
        }

        public static bool operator !=(EmailAddress emailOne, EmailAddress emailTwo)
        {
            return emailOne.Value != emailTwo.Value;
        }

        public static bool operator ==(EmailAddress emailOne, EmailAddress emailTwo)
        {
            return emailOne.Value == emailTwo.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Value}";
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}