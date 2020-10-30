using System.IO;

namespace SoftTissue.Core.Models
{
    /// <summary>
    /// It is responsible to contain all the common variables used in application.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The constant of Euler-Mascheroni.
        /// </summary>
        public static double EulerMascheroniConstant = 0.5772156649015328606065120900824024310421;

        /// <summary>
        /// The double-point precision accepted.
        /// </summary>
        public static double Precision = 1e-10;

        /// <summary>
        /// The relative double-point precision accepted.
        /// </summary>
        public static double RelativePrecision = 5e-3;

        /// <summary>
        /// The application base path.
        /// </summary>
        public static string DirectoryBasePath = Directory.GetCurrentDirectory().Replace("\\src\\SoftTissue.Application", "\\");

        /// <summary>
        /// The base path to Fung Model response files.
        /// </summary>
        public static string FungModelBasePath = Path.Combine(Constants.DirectoryBasePath, "sheets\\Solutions\\Quasi-Linear Viscosity\\Fung Model");
    }
}
