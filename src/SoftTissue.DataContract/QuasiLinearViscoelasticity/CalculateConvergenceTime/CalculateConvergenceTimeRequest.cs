using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime
{
    /// <summary>
    /// It represents the request content to CalculateConvergenceTime operation.
    /// </summary>
    public sealed class CalculateConvergenceTimeRequest : OperationRequestBase
    {
        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; set; }

        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; set; }

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
        /// The elastic stress constant. Constant A.
        /// Unit: Pa (Pascal). In some cases, can be N (Newton).
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// The elastic power constant. Constant B.
        /// Unit: Dimensionless.
        /// </summary>
        public double ElasticPowerConstant { get; set; }

        /// <summary>
        /// The relaxation index. Constant C.
        /// Unit: Dimensionless.
        /// </summary>
        public double RelaxationIndex { get; set; }

        /// <summary>
        /// The fast relaxation time. Tau 1.
        /// Unit: s (second).
        /// </summary>
        public double FastRelaxationTime { get; set; }

        /// <summary>
        /// The slow relaxation time. Tau 2.
        /// Unit: s (second).
        /// </summary>
        public double SlowRelaxationTime { get; set; }
    }
}
