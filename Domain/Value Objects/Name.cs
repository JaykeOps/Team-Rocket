using Domain.Helper_Classes;
using System;
using System.Text.RegularExpressions;

namespace football_series_manager.Domain.Entities
{
    public class Name
    {
        public Name(string firstName, string lastName)
        {
            if (firstName.IsValidName(true) && lastName.IsValidName(true))
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

        public string FirstName { get; }
        public string LastName { get; }

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

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}