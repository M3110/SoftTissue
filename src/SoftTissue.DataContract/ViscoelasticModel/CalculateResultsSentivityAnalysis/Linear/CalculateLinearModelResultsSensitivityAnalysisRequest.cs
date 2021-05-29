using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis.Linear
{
    /// <summary>
    /// It represents the request content to CalculateLinearModelResultsSensitivityAnalysis operation.
    /// </summary>
    public abstract class CalculateLinearModelResultsSensitivityAnalysisRequest : CalculateResultsSentivityAnalysisRequest
    {
        /// <summary>
        /// List of initial strain.
        /// Unit: Dimensionless.
        /// </summary>
        public Value InitialStrainList { get; set; }

        /// <summary>
        /// List of initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Value InitialStressList { get; set; }
    }
}
