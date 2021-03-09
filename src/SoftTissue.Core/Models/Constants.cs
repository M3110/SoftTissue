namespace SoftTissue.Core.Models
{
    /// <summary>
    /// It is responsible to contain the constats used in application.
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
        // TODO: Pensar em outro nome mais claro.
        public const double Precision = 1e-6;

        /// <summary>
        /// The relative double-point precision accepted.
        /// </summary>
        public const double RelativePrecision = 1e-2;

        /// <summary>
        /// The final time to equation E1 must be at most 11.4, because, using Geogebra to plot the graphic for the 
        /// equation e^(-t)/t, we found to that time a value less than the precision assumed to the project.
        /// f(t) = e^(-t)/t
        /// f(11.4) = 9.820601e-7
        /// f(11.4) < Precision = 1e-6
        /// </summary>
        public const double EquationE1MaximumFinalTime = 11.4;
        
        /// <summary>
        /// The minimum number of lines accepted to read a file.
        /// </summary>
        public const int MinimumFileNumberOfLines = 3;
    }
}
