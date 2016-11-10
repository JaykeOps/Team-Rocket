namespace Domain.Helper_Classes
{
    public static class StringUtils
    {
        public static bool Contains(this string str, string str2, bool ignoreCase)
        {
            str = (ignoreCase) ? str.ToLower() : str;
            str2 = (ignoreCase) ? str2.ToLower() : str2;
            return str.Contains(str2);
        }
    }
}