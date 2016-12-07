using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class SeriesName : ValueObject<SeriesName>
    {
        public string Value { get; }

        public SeriesName(string seriesName)
        {
            if (seriesName.IsValidSeriesName(true))
            {
                this.Value = seriesName;
            }
            else
            {
                throw new FormatException("Not a valid series name.");
            }
        }

        public static bool TryParse(string seriesName, out SeriesName result)
        {
            try
            {
                result = new SeriesName(seriesName);
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