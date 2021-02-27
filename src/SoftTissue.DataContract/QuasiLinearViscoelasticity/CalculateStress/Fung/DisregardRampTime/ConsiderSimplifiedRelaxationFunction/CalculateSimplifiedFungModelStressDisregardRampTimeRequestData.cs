using Newtonsoft.Json;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelStressDisregardRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelStressDisregardRampTimeRequestData : CalculateStressDisregardRampTimeRequestData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="softTissueType"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="strain"></param>
        /// <param name="initialStress"></param>
        [JsonConstructor]
        public CalculateSimplifiedFungModelStressDisregardRampTimeRequestData(
            string softTissueType,
            double? timeStep,
            double? finalTime,
            double strain,
            double initialStress,
            SimplifiedReducedRelaxationFunctionData reducedRelaxationFunctionData)
            : base(softTissueType, timeStep, finalTime, strain, initialStress)
        {
            this.ReducedRelaxationFunctionData = reducedRelaxationFunctionData;
        }

        /// <summary>
        /// The input data to Simplified Reduced Relaxation Function.
        /// </summary>
        public SimplifiedReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; private set; }

        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelStressDisregardRampTimeRequestData"/>.
        /// </summary>
        /// <param name="softTissueType"></param>
        /// <param name="strain"></param>
        /// <param name="initialStress"></param>
        /// <param name="reducedRelaxationFunctionData"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelStressDisregardRampTimeRequestData Create(
            string softTissueType, 
            double strain, 
            double initialStress, 
            SimplifiedReducedRelaxationFunctionData reducedRelaxationFunctionData, 
            double? timeStep = null, 
            double? finalTime = null)
            => new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData(softTissueType, timeStep, finalTime, strain, initialStress, reducedRelaxationFunctionData);
    }
}
