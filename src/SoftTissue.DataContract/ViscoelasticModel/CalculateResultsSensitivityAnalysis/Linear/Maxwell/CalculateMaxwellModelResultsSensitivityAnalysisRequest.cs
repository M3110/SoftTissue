using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear
{
    /// <summary>
    /// It represents the request content to CalculateMaxwellModelResultsSensitivityAnalysis operation.
    /// </summary>
    public sealed class CalculateMaxwellModelResultsSensitivityAnalysisRequest : CalculateLinearModelResultsSensitivityAnalysisRequest
    {
        /// <summary>
        /// Range for viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public Range ViscosityRange { get; set; }

        /// <summary>
        /// Range for stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Range StiffnessRange { get; set; }
    }
}
