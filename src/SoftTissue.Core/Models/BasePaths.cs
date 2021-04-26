using System.IO;

namespace SoftTissue.Core.Models
{
    /// <summary>
    /// It contains the base paths used in the application.
    /// </summary>
    public class BasePaths
    {
        /// <summary>
        /// The application base path.
        /// </summary>
        public static string Application => Directory.GetCurrentDirectory().Replace("\\src\\SoftTissue.Application", "\\");

        /// <summary>
        /// The base path to solution response files.
        /// </summary>
        public static string Solution => Path.Combine(BasePaths.Application, "solutions");

        /// <summary>
        /// The base path to response files of Quasi-Linear Model.
        /// </summary>
        public static string QuasiLinearModel => Path.Combine(BasePaths.Solution, "Quasi-Linear Viscoelastic");

        /// <summary>
        /// The base path to response files of Fung Model.
        /// </summary>
        public static string FungModel => Path.Combine(BasePaths.QuasiLinearModel, "Fung Model");

        /// <summary>
        /// The base path to response files of Simplified Fung Model.
        /// </summary>
        public static string SimplifiedFungModel => Path.Combine(BasePaths.QuasiLinearModel, "Simplified Fung Model");

        /// <summary>
        /// The base path to response files of Linear Viscoelastic Model.
        /// </summary>
        public static string LinearModel => Path.Combine(BasePaths.Solution, "Linear Viscoelastic");

        /// <summary>
        /// The base path to response files of Maxwell Model.
        /// </summary>
        public static string MaxwellModel => Path.Combine(BasePaths.LinearModel, "Maxwell Model");

        /// <summary>
        /// The base path to response files of Experimental operations.
        /// </summary>
        public static string Experimental => Path.Combine(BasePaths.Solution, "Experimental");

        /// <summary>
        /// The base path to response files of AnalyzeAndExtrapolateResults operation.
        /// </summary>
        public static string AnalyzeAndExtrapolateResults => Path.Combine(BasePaths.Experimental, "Analyze and extrapolate");

        /// <summary>
        /// The base path to response files of AnalyzeResults operation.
        /// </summary>
        public static string AnalyzeResults => Path.Combine(BasePaths.Experimental, "Analyze");

        /// <summary>
        /// The base path to response files of SkipPoints operation.
        /// </summary>
        public static string SkipPoints => Path.Combine(BasePaths.Experimental, "Skip points");
    }
}
