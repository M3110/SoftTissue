using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateStressConsiderRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public abstract class CalculateStressConsiderRampTimeRequestData<T> : CalculateResultRequestData
    {
        /// <summary>
        /// The viscoelasticy considerations to analysis.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; set; }

        #region Strain parameters

        /// <summary>
        /// The analysis strain rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// The maximum strain, this is obtained after the ramp time.
        /// Unit: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; set; }

        /// <summary>
        /// The time when the maximum strain is kept constant before strain decreases.
        /// This property is only used when considering that the strain decreases.
        /// Unit: s (second).
        /// </summary>
        public double TimeWithConstantStrain { get; set; }

        /// <summary>
        /// The absolut strain decrease rate.
        /// This property is only used when considering that the strain decreases.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainDecreaseRate { get; set; }

        /// <summary>
        /// The minimum strain, this is obtained after the decrease time.
        /// This property is only used when considering that the strain decreases.
        /// Unit: Dimensionless.
        /// </summary>
        public double MinimumStrain { get; set; }

        #endregion

        #region Elastic Response parameters
        
        /// <summary>
        /// The elastic stress constant. Constant A.
        /// Unit: Pa (Pascal). In some cases, can be N (Newton).
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// The elastic power constant. Constant B.
        /// Unit: Dimensionless.
        /// </summary>
        public double ElasticPowerConstant { get; set; }

        #endregion

        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public T ReducedRelaxationFunctionData { get; set; } 

        #endregion
    }
}
