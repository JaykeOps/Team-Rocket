using Domain.Interfaces;
using Domain.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Domain.Helper_Classes
{
    public static class ValueObjectValidation
    {
        private const string NAME_REGEX = "^[a-z A-ZÄäÀàÁáÂâÃãÅåǍǎĄąĂăÆæĀāÇçĆćĈĉČčĎđĐďð" +
            "ÈèÉéÊêËëĚěĘęĖėĒēĜĝĢģĞğĤĥÌìÍíÎîÏïıĪīĮįĴĵĶķĹĺĻļŁłĽľÑñŃńŇňŅņÖöÒòÓóÔôÕõŐőØø"
            + "ŒœŔŕŘřẞßŚśŜŝŞşŠšȘșŤťŢţÞþȚțÜüÙùÚúÛûŰűŨũŲųŮůŪūŴŵÝýŸÿŶŷŹźŽžŻż]{2,20}$";

        private const string CELL_PHONE_NUMBER = @"\b\d{3,6}-\b\d{6,9}$";

        private const string TEAMNAME_REGEX = "^[a-z A-ZÄäÀàÁáÂâÃãÅåǍǎĄąĂăÆæĀāÇçĆćĈĉČčĎđĐďð" +
            "ÈèÉéÊêËëĚěĘęĖėĒēĜĝĢģĞğĤĥÌìÍíÎîÏïıĪīĮįĴĵĶķĹĺĻļŁłĽľÑñŃńŇňŅņÖöÒòÓóÔôÕõŐőØø" +
            "ŒœŔŕŘřẞßŚśŜŝŞşŠšȘșŤťŢţÞþȚțÜüÙùÚúÛûŰűŨũŲųŮůŪūŴŵÝýŸÿŶŷŹźŽžŻż0-9]{2,40}$";

        private const string ARENANAME_REGEX = "^[a-z A-ZÄäÀàÁáÂâÃãÅåǍǎĄąĂăÆæĀāÇçĆćĈĉČčĎđĐďð" +
            "ÈèÉéÊêËëĚěĘęĖėĒēĜĝĢģĞğĤĥÌìÍíÎîÏïıĪīĮįĴĵĶķĹĺĻļŁłĽľÑñŃńŇňŅņÖöÒòÓóÔôÕõŐőØø" +
            "ŒœŔŕŘřẞßŚśŜŝŞşŠšȘșŤťŢţÞþȚțÜüÙùÚúÛûŰűŨũŲųŮůŪūŴŵÝýŸÿŶŷŹźŽžŻż0-9]{2,40}$";

        private const string SERIESNAME_REGEX = "^[a-z A-ZÄäÀàÁáÂâÃãÅåǍǎĄąĂăÆæĀāÇçĆćĈĉČčĎđĐďð" +
            "ÈèÉéÊêËëĚěĘęĖėĒēĜĝĢģĞğĤĥÌìÍíÎîÏïıĪīĮįĴĵĶķĹĺĻļŁłĽľÑñŃńŇňŅņÖöÒòÓóÔôÕõŐőØø" +
            "ŒœŔŕŘřẞßŚśŜŝŞşŠšȘșŤťŢţÞþȚțÜüÙùÚúÛûŰűŨũŲųŮůŪūŴŵÝýŸÿŶŷŹźŽžŻż0-9]{2,30}$";

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
                return Regex.IsMatch(value, CELL_PHONE_NUMBER, RegexOptions.CultureInvariant);
            }
            else
            {
                return Regex.IsMatch(value, CELL_PHONE_NUMBER);
            }
        }

        public static bool IsValidEmailAddress(this string value, bool ignoreCase)
        {
            try
            {
                MailAddress m = new MailAddress(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
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

        public static bool IsValidSeriesName(this string value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return Regex.IsMatch(value, SERIESNAME_REGEX, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.IsMatch(value, SERIESNAME_REGEX);
            }
        }

        public static bool IsValidMatchDuration(this TimeSpan value)
        {
            return value.TotalMinutes <= 90 && value.TotalMinutes >= 10;
        }

        public static bool IsValidNumberOfTeams(this int value)
        {
            return value % 2 == 0 && value > 2;
        }

        public static bool IsValidMatchDateAndTime(this DateTime value)
        {
            return value > DateTime.Now && value < DateTime.Now + TimeSpan.FromDays(365 * 2);
        }

        public static bool IsValidBirthOfDate(this string value)
        {
            DateTime result;
            if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result))
            {
                return result.Year > 1936 && result.Year < DateTime.Now.Year - 3;
            }
            else
            {
                return false;
            }
        }

        public static bool IsMatchMinute(this int value)
        {
            return value >= 1 && value <= 90 + 30;
        }

        public static bool IsValidOverTime(this int value)
        {
            return value >= 0 && value <= 30;
        }

        public static bool IsScoreValid(this int score)
        {
            return score >= 0 && score <= 50;
        }

        public static bool IsValidShirtNumber(this int value, Guid teamId)
        {
            var team = DomainService.FindTeamById(teamId);
            var shirtNumberIsAlreadyInUse = (value != -1) && team.playerIds.Any(x =>
            DomainService.FindPlayerById(x).ShirtNumber.Value == value);

            return value >= 0 && value < 100 && !shirtNumberIsAlreadyInUse;
        }

        public static bool IsValidShirtNumber(this int value, IExposablePlayer player, Guid teamId)
        {
            var team = DomainService.FindTeamById(teamId);
            var shirtNumberIsAlreadyInUse = (value != -1) && team.playerIds.Any(x =>
            DomainService.FindPlayerById(x).ShirtNumber.Value == value);

            if (player.ShirtNumber.Value == value)
            {
                return true;
            }
            return value >= 0 && value < 100 && !shirtNumberIsAlreadyInUse;
        }
    }
}