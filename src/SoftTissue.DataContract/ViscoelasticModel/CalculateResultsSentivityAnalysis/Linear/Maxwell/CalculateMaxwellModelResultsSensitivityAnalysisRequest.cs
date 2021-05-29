using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis.Linear
{
    /// <summary>
    /// It represents the request content to CalculateMaxwellModelResultsSensitivityAnalysis operation.
    /// </summary>
    public sealed class CalculateMaxwellModelResultsSensitivityAnalysisRequest : CalculateLinearModelResultsSensitivityAnalysisRequest
    {
        /// <summary>
        /// List of viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public Value ViscosityList { get; set; }

        /// <summary>
        /// List of stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Value StiffnessList { get; set; }
    }
}
