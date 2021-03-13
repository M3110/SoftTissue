using Newtonsoft.Json;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime;
using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateFungModelStressDisregardRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateFungModelStressDisregardRampTimeRequestData : CalculateStressDisregardRampTimeRequestData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="softTissueType"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="strain"></param>
        /// <param name="initialStress"></param>
        /// <param name="reducedRelaxationFunctionData"></param>
        [JsonConstructor]
        public CalculateFungModelStressDisregardRampTimeRequestData(
            string softTissueType, 
            double? timeStep, 
            double? finalTime, 
            double strain, 
            double initialStress,
            ReducedRelaxationFunctionData reducedRelaxationFunctionData) 
            : base(softTissueType, timeStep, finalTime, strain, initialStress)
        {
            this.ReducedRelaxationFunctionData = reducedRelaxationFunctionData;
        }

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public ReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; private set; }
    }
}
