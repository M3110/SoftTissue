using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStressSensitivityAnalysis operation of Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStressSensitivityAnalysisRequest : CalculateResultRequest
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="finalTime"></param>
        /// <param name="timeStep"></param>
        /// <param name="stiffnessList"></param>
        /// <param name="initialStrainList"></param>
        /// <param name="viscosityList"></param>
        [JsonConstructor]
        public CalculateStressSensitivityAnalysisRequest(
            double finalTime,
            double timeStep,
            Value stiffnessList,
            Value initialStrainList,
            Value viscosityList) : base(timeStep, finalTime)
        {
            this.StiffnessList = stiffnessList;
            this.InitialStrainList = initialStrainList;
            this.ViscosityList = viscosityList;
        }

        /// <summary>
        /// List of stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Value StiffnessList { get; private set; }

        /// <summary>
        /// List of initial strain.
        /// Unit: Dimensionless.
        /// </summary>
        public Value InitialStrainList { get; private set; }

        /// <summary>
        /// List of viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public Value ViscosityList { get; private set; }
    }
}
