using System.Text.RegularExpressions;

namespace Domain.Helper_Classes
{
    public static class ValidationFormat
    {
        public const string NAME_REGEX = "^[a-z A-ZÄäÀàÁáÂâÃãÅåǍǎĄąĂăÆæĀāÇçĆćĈĉČčĎđĐďð" +
            "ÈèÉéÊêËëĚěĘęĖėĒēĜĝĢģĞğĤĥÌìÍíÎîÏïıĪīĮįĴĵĶķĹĺĻļŁłĽľÑñŃńŇňŅņÖöÒòÓóÔôÕõŐőØø" 
            +"ŒœŔŕŘřẞßŚśŜŝŞşŠšȘșŤťŢţÞþȚțÜüÙùÚúÛûŰűŨũŲųŮůŪūŴŵÝýŸÿŶŷŹźŽžŻż]{2,20}$";
        public const string EMAIL_REGEX = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public const string CELL_PHONE_NUMBER = @"\b\d{3,6}-\b\d{6,9}$";
        public const string TEAMNAME_REGEX = "^[a-z A-ZÄäÀàÁáÂâÃãÅåǍǎĄąĂăÆæĀāÇçĆćĈĉČčĎđĐďð" +
            "ÈèÉéÊêËëĚěĘęĖėĒēĜĝĢģĞğĤĥÌìÍíÎîÏïıĪīĮįĴĵĶķĹĺĻļŁłĽľÑñŃńŇňŅņÖöÒòÓóÔôÕõŐőØø" +
            "ŒœŔŕŘřẞßŚśŜŝŞşŠšȘșŤťŢţÞþȚțÜüÙùÚúÛûŰűŨũŲųŮůŪūŴŵÝýŸÿŶŷŹźŽžŻż0-9]{2,40}$";
        public const string ARENANAME_REGEX = "^[a-z A-ZÄäÀàÁáÂâÃãÅåǍǎĄąĂăÆæĀāÇçĆćĈĉČčĎđĐďð" +
            "ÈèÉéÊêËëĚěĘęĖėĒēĜĝĢģĞğĤĥÌìÍíÎîÏïıĪīĮįĴĵĶķĹĺĻļŁłĽľÑñŃńŇňŅņÖöÒòÓóÔôÕõŐőØø" +
            "ŒœŔŕŘřẞßŚśŜŝŞşŠšȘșŤťŢţÞþȚțÜüÙùÚúÛûŰűŨũŲųŮůŪūŴŵÝýŸÿŶŷŹźŽžŻż0-9]{2,40}$";
        

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
                return Regex.IsMatch(value, CELL_PHONE_NUMBER, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.IsMatch(value, CELL_PHONE_NUMBER);
            }
        }

        public static bool IsValidEmailAddress(this string value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return Regex.IsMatch(value, EMAIL_REGEX, RegexOptions.IgnoreCase)
                    && value.Length < 30;
            }
            else
            {
                return Regex.IsMatch(value, EMAIL_REGEX);
            }
        }

        public static bool IsValidTeamName(this string value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return Regex.IsMatch(value, TEAMNAME_REGEX, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.IsMatch(value, TEAMNAME_REGEX);
            }
        }

        public static bool IsValidArenaName(this string value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return Regex.IsMatch(value, ARENANAME_REGEX, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.IsMatch(value, ARENANAME_REGEX);
            }
        }
    }
}