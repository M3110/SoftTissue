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

        /// <summary>
        /// The strain rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// The maximum strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; set; }

        /// <summary>
        /// The time when the soft tissue is submited at the maximum strain.
        /// Unit: s (second).
        /// </summary>
        public double RampTime => this.MaximumStrain / this.StrainRate;

        /// <summary>
        /// The time where the strain ends.
        /// This property is only used when considering that the strain decrease. <see cref="ViscoelasticConsideration.GeneralViscoelasticEffectWithStrainDecrease"/>.
        /// Unit: s (second).
        /// </summary>
        public double FinalStrainTime { get; set; }

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

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public TRelaxationFunction ReducedRelaxationFunctionInput { get; set; }
    }
}