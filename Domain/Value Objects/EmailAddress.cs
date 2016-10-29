using Domain.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class EmailAddress
    {
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

        public string Value { get; }

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
