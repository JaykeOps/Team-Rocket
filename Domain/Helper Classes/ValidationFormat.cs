using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Helper_Classes
{
    public static class ValidationFormat
    {
        public const string NAME_REGEX = @"^[A-ZÅÄÖ][a-zåäö]{2,20}$";
        public const string EMAIL_REGEX = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public const string Cell_Phone_Number = @"\b\d{3,6}-\b\d{6,9}$";

        public static bool IsValidName(this string value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return Regex.IsMatch(value, NAME_REGEX, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.IsMatch(value, NAME_REGEX);
            }
            
        }

        public static bool IsValidCellPhoneNumber(this string value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return Regex.IsMatch(value, Cell_Phone_Number, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.IsMatch(value, Cell_Phone_Number);
            }
        }

        public static bool IsValidEmailAddress(this string value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return Regex.IsMatch(value, EMAIL_REGEX, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.IsMatch(value, EMAIL_REGEX);
            }
        }


    }
}

