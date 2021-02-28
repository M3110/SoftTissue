using Newtonsoft.Json;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateFungModelStressConsiderRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateFungModelStressConsiderRampTimeRequestData : CalculateStressConsiderRampTimeRequestData 
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticConsideration"></param>
        /// <param name="numerOfRelaxations"></param>
        /// <param name="strainRate"></param>
        /// <param name="strainDecreaseRate"></param>
        /// <param name="maximumStrain"></param>
        /// <param name="minimumStrain"></param>
        /// <param name="timeWithConstantMaximumStrain"></param>
        /// <param name="timeWithConstantMinimumStrain"></param>
        /// <param name="elasticStressConstant"></param>
        /// <param name="elasticPowerConstant"></param>
        /// <param name="reducedRelaxationFunctionData"></param>
        [JsonConstructor]
        public CalculateFungModelStressConsiderRampTimeRequestData(
            string softTissueType,
            double? timeStep,
            double? finalTime,
            ViscoelasticConsideration viscoelasticConsideration, 
            int numerOfRelaxations, 
            double strainRate, 
            double strainDecreaseRate, 
            double maximumStrain, 
            double minimumStrain, 
            double timeWithConstantMaximumStrain, 
            double timeWithConstantMinimumStrain, 
            double elasticStressConstant, 
            double elasticPowerConstant,
            ReducedRelaxationFunctionData reducedRelaxationFunctionData) 
            : base(softTissueType, timeStep, finalTime, viscoelasticConsideration, numerOfRelaxations, strainRate, strainDecreaseRate, maximumStrain, minimumStrain, timeWithConstantMaximumStrain, timeWithConstantMinimumStrain, elasticStressConstant, elasticPowerConstant)
        {
            this.ReducedRelaxationFunctionData = reducedRelaxationFunctionData;
        }

        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public ReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; private set; }

        #endregion
    }
}
