using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    /// <summary>
    ///  It contains the input to Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public abstract class QuasiLinearViscoelasticityModelInput<TRelaxationFunction> : ViscoelasticModelInput
    {
        #region Relaxation parameters.

        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; set; }

        /// <summary>
        /// The number of relaxations considered in the analysis.
        /// Unity: Dimensionless.
        /// </summary>
        public int NumerOfRelaxations { get; set; }

        /// <summary>
        /// The relaxation number.
        /// It is iterated belong the analysis is executed.
        /// Unity: Dimensionless.
        /// </summary>
        public int RelaxationNumber { get; set; }

        /// <summary>
        /// The total time spent with the soft tissue relaxing.
        /// Unity: s (second).
        /// </summary>
        public double RelaxationTotalTime => this.TimeWithConstantMaximumStrain + this.DecreaseTime + this.TimeWithConstantMinimumStrain;

        #endregion

        #region Strain parameters

        /// <summary>
        /// The analysis strain rate.
        /// Unity: Dimensionless.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// The analysis strain decrease rate.
        /// This property is only used when considering that the strain decreases.
        /// Unity: Dimensionless.
        /// </summary>
        public double StrainDecreaseRate { get; set; }

        /// <summary>
        /// The maximum strain, this is obtained after the ramp time.
        /// Unity: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; set; }

        /// <summary>
        /// The minimum strain, this is obtained after the decrease time (<see cref="DecreaseTime"/>).
        /// This property is only used when considering that the strain decreases.
        /// Unity: Dimensionless.
        /// </summary>
        public double MinimumStrain { get; set; }

        /// <summary>
        /// The time taken for the strain reaches the maximum value.
        /// Unity: s (second).
        /// </summary>
        public double FirstRampTime => this.MaximumStrain / this.StrainRate;

        /// <summary>
        /// The time taken for the strain reaches the maximum value beginning from the minimum value.
        /// Unity: s (second).
        /// </summary>
        public double RampTime => (this.MaximumStrain - this.MinimumStrain) / this.StrainRate;

        /// <summary>
        /// The strain decrease time.
        /// The time when the strain is equals to minimum strain (<see cref="MinimumStrain"/>).
        /// This property is only used when considering that the strain decrease.
        /// Unity: s (second).
        /// </summary>
        public double DecreaseTime => (this.MaximumStrain - this.MinimumStrain) / this.StrainDecreaseRate;

        /// <summary>
        /// The time when the maximum strain is kept constant after the strain increases.
        /// This property is only used when considering that the strain decreases.
        /// Unity: s (second).
        /// </summary>
        public double TimeWithConstantMaximumStrain { get; set; }

        /// <summary>
        /// The time when the minimum strain is kept constant after the strain decreases.
        /// This property is only used when considering that the strain decreases.
        /// Unity: s (second).
        /// </summary>
        public double TimeWithConstantMinimumStrain { get; set; }

        /// <summary>
        /// The time when the strain starts decreasing.
        /// Unity: s (second).
        /// </summary>
        public double TimeWhenStrainStartDecreasing => this.FirstRampTime + this.TimeWithConstantMaximumStrain;

        #endregion

        #region Elastic Response parameters

        /// <summary>
        /// The maximum stress.
        /// This property is only used when disregarding the ramp time. <see cref="ViscoelasticConsideration.DisregardRampTime"/>
        /// Unity: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }

        /// <summary>
        /// The elastic stress constante. Constant A.
        /// Unity: Pa (Pascal). In some cases, can be N (Newton).
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// The elastic power constant. Constant B.
        /// Unity: Dimensionless.
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