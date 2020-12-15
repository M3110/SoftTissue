using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateStressConsiderRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStressConsiderRampTimeRequestData : OperationRequestData
    {
        /// <summary>
        /// The viscoelasticy considerations to analysis.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; set; }

        /// <summary>
        /// The analysis strain rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// The meximum strain imposed to analysis.
        /// Unit: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; set; }

        /// <summary>
        /// The time where the strain ends.
        /// This property is only used when considering that the strain decrease. <see cref="ViscoelasticConsideration.GeneralViscoelasticEffectWithStrainDecrease"/>.
        /// Unit: s (second).
        /// </summary>
        public double FinalStrainTime { get; set; }

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

    }
}
