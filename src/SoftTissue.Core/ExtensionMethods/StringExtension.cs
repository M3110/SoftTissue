using System;

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
        public static (double, double) ToTimeAndStress(this string line, char separator)
        {
            string[] values = line.Split(separator);

            if (values.Length != 2)
                throw new Exception("The number of variables at line must be equals to 2.");

            return (double.Parse(values[0]), double.Parse(values[1]));
        }

        /// <summary>
        /// This method converts a line to a value of time and stress.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="separator"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public static (double, double) ToTimeAndStress(this string line, char separator, IFormatProvider formatProvider)
        {
            string[] values = line.Split(separator);

            if (values.Length != 2)
                throw new Exception("The number of variables at line must be equals to 2.");

            return (double.Parse(values[0], formatProvider), double.Parse(values[1], formatProvider));
        }
    }
}
