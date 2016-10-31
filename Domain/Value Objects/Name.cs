using Domain.Helper_Classes;
using Domain.Value_Objects;
using System;

namespace football_series_manager.Domain.Entities
{
    public class Name : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }

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

        public override bool Equals(object obj)
        {
            var item = obj as Name;
            return item.FirstName == this.FirstName && item.LastName == this.LastName;
        }

        public static bool operator !=(Name nameOne, Name nameTwo)
        {
            return nameOne.FirstName != nameTwo.FirstName && nameOne.LastName != nameTwo.LastName;
        }

        public static bool operator ==(Name nameOne, Name nameTwo)
        {
            return nameOne.FirstName == nameTwo.FirstName && nameOne.LastName == nameTwo.LastName;
        }

        public override int GetHashCode()
        {
            return (this.FirstName + this.LastName).GetHashCode();
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}