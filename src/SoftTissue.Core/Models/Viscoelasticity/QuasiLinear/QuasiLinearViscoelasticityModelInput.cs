using SoftTissue.DataContract.Models;

namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    /// <summary>
    /// It contains the input data for Quasi-Linear Viscoelasticity Model.
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
        /// Unit: Dimensionless.
        /// </summary>
        public int NumerOfRelaxations { get; set; }

        /// <summary>
        /// The relaxation number.
        /// It is iterated belong the analysis is executed.
        /// Unit: Dimensionless.
        /// </summary>
        public int RelaxationNumber { get; set; }

        /// <summary>
        /// The total time spent on the first soft tissue relaxation.
        /// Unit: s (second).
        /// </summary>
        public double FirstRelaxationTotalTime =>
            this.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime
            ? this.TimeWithConstantMaximumStrain : this.FirstRampTime + this.TimeWithConstantMaximumStrain;

        /// <summary>
        /// The total time spent with the soft tissue relaxing.
        /// Unit: s (second).
        /// </summary>
        public double RelaxationTotalTime => this.DecreaseTime + this.TimeWithConstantMinimumStrain + this.RampTime + this.TimeWithConstantMaximumStrain;

        #endregion

        #region Strain parameters

        /// <summary>
        /// The analysis strain rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// The analysis strain decrease rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainDecreaseRate { get; set; }

        /// <summary>
        /// The maximum strain, this is obtained after the ramp time.
        /// Unit: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; set; }

        /// <summary>
        /// The minimum strain, this is obtained after the decrease time.
        /// Unit: Dimensionless.
        /// </summary>
        public double MinimumStrain { get; set; }

        /// <summary>
        /// The time taken for the strain reaches the maximum value.
        /// Unit: s (second).
        /// </summary>
        public double FirstRampTime => this.MaximumStrain / this.StrainRate;

        /// <summary>
        /// The time taken for the strain reaches the maximum value beginning from the minimum value.
        /// Unit: s (second).
        /// </summary>
        public double RampTime => (this.MaximumStrain - this.MinimumStrain) / this.StrainRate;

        /// <summary>
        /// The strain decrease time.
        /// The time when the strain is equals to minimum strain (<see cref="MinimumStrain"/>).
        /// Unit: s (second).
        /// </summary>
        public double DecreaseTime => (this.MaximumStrain - this.MinimumStrain) / this.StrainDecreaseRate;

        /// <summary>
        /// The time when the maximum strain is kept constant after the strain increases.
        /// Unit: s (second).
        /// </summary>
        public double TimeWithConstantMaximumStrain { get; set; }

        /// <summary>
        /// The time when the minimum strain is kept constant after the strain decreases.
        /// Unit: s (second).
        /// </summary>
        public double TimeWithConstantMinimumStrain { get; set; }

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