using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStressSensitivityAnalysis operation of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStressSensitivityAnalysisRequest : OperationRequestBase
    {
        /// <summary>
        /// List of stiffness.
        /// </summary>
        public Value StiffnessList { get; set; }

        /// <summary>
        /// List of initial strain.
        /// </summary>
        public Value InitialStrainList { get; set; }

        /// <summary>
        /// List of viscosity.
        /// </summary>
        public Value ViscosityList { get; set; }
    }
}
