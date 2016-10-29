using System;
using System.Text.RegularExpressions;

namespace football_series_manager.Domain.Entities
{
    public class Name
    {
        public Name(string firstName, string lastName)
        {
            if (IsValid(firstName) && IsValid(lastName))
            {
                this.FirstName = firstName;
                this.LastName = lastName;
            }
            else
            {
                throw new FormatException("First/last name entry did not follow format restrictions!\n" +
                    "A first/last name must consist of 2-20 letters.");
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsValid(string value)
        {
            return Regex.IsMatch(value, @"^[A-ZÅÄÖ][a-zåäö]{2,20}$");
        }

        public static bool TryParse(string firstNameValue, string lastNameValue, out Name result)
        {
            try
            {
                result = new Name(firstNameValue, lastNameValue);
                return true;
            }
            catch (FormatException)
            {
                result = null;
                return false;
            }
        }
    }
}