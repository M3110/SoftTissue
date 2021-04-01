using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelStressDisregardRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelStressDisregardRampTimeRequestData : CalculateStressDisregardRampTimeRequestData
    {
        /// <summary>
        /// The input data to Simplified Reduced Relaxation Function.
        /// </summary>
        public SimplifiedReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; }

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
            => new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
            {
                SoftTissueType = softTissueType, 
                TimeStep = timeStep,
                FinalTime = finalTime, 
                Strain = strain, 
                InitialStress = initialStress, 
                ReducedRelaxationFunctionData = reducedRelaxationFunctionData
            };
    }
}
