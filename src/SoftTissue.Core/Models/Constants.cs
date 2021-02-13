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
        public const double EulerMascheroniConstant = 0.5772156649015328606065120900824024310421;

        /// <summary>
        /// The double-point precision accepted.
        /// </summary>
        public const double Precision = 1e-6;

        /// <summary>
        /// The relative double-point precision accepted.
        /// </summary>
        public const double RelativePrecision = 1e-3;

        /// <summary>
        /// The final time to equation E1 must be at most 11.4, because, using Geogebra to plot the graphic for the 
        /// equation e^(-t)/t, we found to that time a value less than the precision assumed to the project.
        /// f(t) = e^(-t)/t
        /// f(11.4) = 9.820601e-7
        /// f(11.4) < Precision = 1e-6
        /// </summary>
        public const double EquationE1MaximumFinalTime = 11.4;

        /// <summary>
        /// The application base path.
        /// </summary>
        public readonly static string DirectoryBasePath = Directory.GetCurrentDirectory().Replace("\\src\\SoftTissue.Application", "\\");

        /// <summary>
        /// The base path to solution response files.
        /// </summary>
        public readonly static string SolutionBasePath = Path.Combine(Constants.DirectoryBasePath, "sheets\\Solutions");

        /// <summary>
        /// The base path to response files of Quasi-Linear Model.
        /// </summary>
        public readonly static string QuasiLinearModelBasePath = Path.Combine(Constants.SolutionBasePath, "Quasi-Linear Viscoelastic");

        /// <summary>
        /// The base path to response files of Fung Model.
        /// </summary>
        public readonly static string FungModelBasePath = Path.Combine(Constants.QuasiLinearModelBasePath, "Fung Model");

        /// <summary>
        /// The base path to response files of Simplified Fung Model.
        /// </summary>
        public readonly static string SimplifiedFungModelBasePath = Path.Combine(Constants.QuasiLinearModelBasePath, "Simplified Fung Model");

        /// <summary>
        /// The base path to response files of Linear Viscoelastic Model.
        /// </summary>
        public readonly static string LinearModelBasePath = Path.Combine(Constants.SolutionBasePath, "Linear Viscoelastic");

        /// <summary>
        /// The base path to response files of Maxwell Model.
        /// </summary>
        public readonly static string MaxwellModelBasePath = Path.Combine(Constants.LinearModelBasePath, "Maxwell Model");

        /// <summary>
        /// The base path to response files of Experimental operations.
        /// </summary>
        public readonly static string ExperimentalBasePath = Path.Combine(Constants.SolutionBasePath, "Experimental");
    }
}
