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
    }
}
