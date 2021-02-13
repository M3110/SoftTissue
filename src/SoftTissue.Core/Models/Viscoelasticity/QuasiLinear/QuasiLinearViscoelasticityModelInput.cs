using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    /// <summary>
    ///  It contains the input to Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public abstract class QuasiLinearViscoelasticityModelInput<TRelaxationFunction> : ViscoelasticModelInput
    {
        /// <summary>
        /// The viscoelasctic consideration.
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
        /// The time when the soft tissue is submited at the maximum strain.
        /// Unit: s (second).
        /// </summary>
        public double RampTime => this.MaximumStrain / this.StrainRate;

        /// <summary>
        /// The time when the maximum strain is kept constant before strain decreases.
        /// This property is only used when considering that the strain decreases.
        /// Unit: s (second).
        /// </summary>
        public double TimeWithConstantStrain { get; set; }

        /// <summary>
        /// The time when the strain starts decreasing.
        /// Unity: s (second).
        /// </summary>
        public double TimeWhenStrainStartDecreasing => this.RampTime + this.TimeWithConstantStrain;

        /// <summary>
        /// The absolut strain decrease rate.
        /// This property is only used when considering that the strain decreases.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainDecreaseRate { get; set; }

        /// <summary>
        /// The minimum strain, this is obtained after the decrease time (<see cref="DecreaseTime"/>).
        /// This property is only used when considering that the strain decreases.
        /// Unit: Dimensionless.
        /// </summary>
        public double MinimumStrain { get; set; }

        /// <summary>
        /// The strain decrease time.
        /// The time when the strain is equals to minimum strain (<see cref="MinimumStrain"/>).
        /// This property is only used when considering that the strain decrease.
        /// Unit: s (second).
        /// </summary>
        public double DecreaseTime => (this.MaximumStrain - this.MinimumStrain) / this.StrainDecreaseRate;

        #endregion

        #region Elastic Response parameters

        /// <summary>
        /// The maximum stress.
        /// This property is only used when disregarding the ramp time. <see cref="ViscoelasticConsideration.DisregardRampTime"/>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }

        /// <summary>
        /// The elastic stress constante. Constant A.
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
        public TRelaxationFunction ReducedRelaxationFunctionInput { get; set; }

        #endregion
    }
}