using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrainSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStrainSensitivityAnalysis operation of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStrainSensitivityAnalysisRequest : OperationRequestBase
    {
        /// <summary>
        /// List of stiffness.
        /// </summary>
        public Value StiffnessList { get; set; }

        /// <summary>
        /// List of initial stress.
        /// </summary>
        public Value InitialStressList { get; set; }

        /// <summary>
        /// List of viscosity.
        /// </summary>
        public Value ViscosityList { get; set; }
    }
}
