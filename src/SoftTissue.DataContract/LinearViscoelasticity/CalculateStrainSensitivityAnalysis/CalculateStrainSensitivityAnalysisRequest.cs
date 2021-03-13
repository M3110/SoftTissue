using Newtonsoft.Json;
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
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="stiffnessList"></param>
        /// <param name="initialStressList"></param>
        /// <param name="viscosityList"></param>
        [JsonConstructor]
        public CalculateStrainSensitivityAnalysisRequest(
            double timeStep,
            double finalTime,
            Value stiffnessList, 
            Value initialStressList, 
            Value viscosityList) : base(timeStep, finalTime)
        {
            this.StiffnessList = stiffnessList;
            this.InitialStressList = initialStressList;
            this.ViscosityList = viscosityList;
        }

        /// <summary>
        /// List of stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Value StiffnessList { get; private set; }

        /// <summary>
        /// List of initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public Value InitialStressList { get; private set; }

        /// <summary>
        /// List of viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public Value ViscosityList { get; private set; }
    }
}
