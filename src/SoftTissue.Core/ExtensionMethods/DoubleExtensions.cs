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
		public static double ToDegrees(this double value)
        {
            return (180 / Math.PI) * value;
        }

        /// <summary>
        /// This method calculates the absolut relative diference between two values.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static double AbsolutRelativeDiference(this double value1, double value2)
        {
            return (Math.Abs(value1) - Math.Abs(value2)) / Math.Abs(value1);
        }
    }
}
