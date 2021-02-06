using SoftTissue.DataContract.CalculateResult;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStressSensitivityAnalysis operation of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStressSensitivityAnalysisRequest : CalculateResultRequest
    {
        /// <summary>
        /// List of stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Value StiffnessList { get; set; }

        /// <summary>
        /// List of initial strain.
        /// Unit: Dimensionless.
        /// </summary>
        public Value InitialStrainList { get; set; }

        /// <summary>
        /// List of viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public Value ViscosityList { get; set; }
    }
}
