using System;
using System.Globalization;

namespace SoftTissue.Core.ExtensionMethods
{
    /// <summary>
    /// It contains the extension method to string.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// This method converts a line to a value of time and stress.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static (double, double) ToTimeAndStress(this string line, char separator, IFormatProvider formatProvider = null)
        {
            string[] values = line.Split(separator);

            if (values.Length != 2)
                throw new Exception("The number of variables at line must be equals to 2.");

            return (double.Parse(values[0], formatProvider ?? CultureInfo.InvariantCulture), double.Parse(values[1], formatProvider ?? CultureInfo.InvariantCulture));
        }
    }
}
