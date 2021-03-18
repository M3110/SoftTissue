using System;

namespace SoftTissue.Core.ExtensionMethods
{
    /// <summary>
    /// It contains the extension methods to double.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// This method converts the value from radians to degrees.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public static double ToDegrees(this double value) => (180 / Math.PI) * value;

        /// <summary>
        /// This method calculates the relative diference between two values.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static double RelativeDiference(this double value1, double value2) => (value1 - value2) / value1;

        /// <summary>
        /// This method indicates if a value is positive and is not zero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPositive(this double value) => !double.IsNegative(value);

        /// <summary>
        /// This method indicates if a value is negative and is not zero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNegative(this double value) => double.IsNegative(value);

        /// <summary>
        /// This method indicates if a value is positive and is not zero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPositive(this double? value) => !double.IsNegative(value.GetValueOrDefault());

        /// <summary>
        /// This method indicates if a value is negative and is not zero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNegative(this double? value) => double.IsNegative(value.GetValueOrDefault());
    }
}
