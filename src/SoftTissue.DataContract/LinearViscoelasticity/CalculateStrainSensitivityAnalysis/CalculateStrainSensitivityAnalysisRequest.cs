using SoftTissue.DataContract.CalculateResult;
using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrainSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStrainSensitivityAnalysis operation of Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStrainSensitivityAnalysisRequest : CalculateResultRequest
    {
        /// <summary>
        /// List of stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Value StiffnessList { get; set; }

        /// <summary>
        /// List of initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Value InitialStressList { get; set; }

        /// <summary>
        /// List of viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public Value ViscosityList { get; set; }
    }
}
