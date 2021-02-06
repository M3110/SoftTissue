using System;

namespace SoftTissue.Core.ExtensionMethods
{
    /// <summary>
    /// It contains the extension methods to double.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// This method converts the value from radians to degree.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public static double ToDegree(this double value)
        {
            return (Math.PI / 180) * value;
        }
	}
}
