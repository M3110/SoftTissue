using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateStressConsiderRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public abstract class CalculateStressConsiderRampTimeRequestData : CalculateResultRequestData
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
        [JsonConstructor]
        protected CalculateStressConsiderRampTimeRequestData(
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
            double elasticPowerConstant) : base(softTissueType, timeStep, finalTime)
        {
            this.ViscoelasticConsideration = viscoelasticConsideration;
            this.NumerOfRelaxations = numerOfRelaxations;
            this.StrainRate = strainRate;
            this.StrainDecreaseRate = strainDecreaseRate;
            this.MaximumStrain = maximumStrain;
            this.MinimumStrain = minimumStrain;
            this.TimeWithConstantMaximumStrain = timeWithConstantMaximumStrain;
            this.TimeWithConstantMinimumStrain = timeWithConstantMinimumStrain;
            this.ElasticStressConstant = elasticStressConstant;
            this.ElasticPowerConstant = elasticPowerConstant;
        }

        #region Relaxation parameters.

        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; protected set; }

        /// <summary>
        /// The number of relaxations considered in the analysis.
        /// Unit: Dimensionless.
        /// </summary>
        public int NumerOfRelaxations { get; protected set; }

        #endregion

        #region Strain parameters

        /// <summary>
        /// The analysis strain rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainRate { get; protected set; }

        /// <summary>
        /// The absolut strain decrease rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainDecreaseRate { get; protected set; }

        /// <summary>
        /// The maximum strain, this is obtained after the ramp time.
        /// Unit: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; protected set; }

        /// <summary>
        /// The minimum strain, this is obtained after the decrease time.
        /// Unit: Dimensionless.
        /// </summary>
        public double MinimumStrain { get; protected set; }

        /// <summary>
        /// The time when the maximum strain is kept constant before strain decreases.
        /// Unit: s (second).
        /// </summary>
        public double TimeWithConstantMaximumStrain { get; protected set; }

        /// <summary>
        /// The time when the minimum strain is kept constant after the strain decreases.
        /// Unit: s (second).
        /// </summary>
        public double TimeWithConstantMinimumStrain { get; protected set; }

        #endregion

        #region Elastic Response parameters

        /// <summary>
        /// The elastic stress constant. Constant A.
        /// Unit: Pa (Pascal). In some cases, can be N (Newton).
        /// </summary>
        public double ElasticStressConstant { get; protected set; }

        /// <summary>
        /// The elastic power constant. Constant B.
        /// Unit: Dimensionless.
        /// </summary>
        public double ElasticPowerConstant { get; protected set; }

        #endregion
    }
}
