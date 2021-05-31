using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear
{
    /// <summary>
    /// It represents the request content to CalculateLinearModelResultsSensitivityAnalysis operation.
    /// </summary>
    public abstract class CalculateLinearModelResultsSensitivityAnalysisRequest : CalculateResultsSensitivityAnalysisRequest
    {
        /// <summary>
        /// Range for initial strain.
        /// Unit: Dimensionless.
        /// </summary>
        public Range InitialStrainRange { get; set; }

        /// <summary>
        /// Range for initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Range InitialStressRange { get; set; }
    }
}
