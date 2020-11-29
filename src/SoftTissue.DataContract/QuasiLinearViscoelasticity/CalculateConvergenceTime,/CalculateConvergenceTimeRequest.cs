using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime_
{
    /// <summary>
    /// It represents the request content to CalculateConvergenceTime operation.
    /// </summary>
    public class CalculateConvergenceTimeRequest : OperationRequestBase
    {
        /// <summary>
        /// The analysis strain rate.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// The meximum strain imposed to analysis.
        /// </summary>
        public double MaximumStrain { get; set; }

        /// <summary>
        /// Constant A.
        /// The elastic stress constant.
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// Constant B.
        /// The elastic power constant.
        /// </summary>
        public double ElasticPowerConstant { get; set; }

        /// <summary>
        /// Constant C.
        /// The relaxation index.
        /// </summary>
        public double RelaxationIndex { get; set; }

        /// <summary>
        /// Tau 1.
        /// The fast relaxation time.
        /// </summary>
        public double FastRelaxationTime { get; set; }

        /// <summary>
        /// Tau 2.
        /// The slow relaxation time.
        /// </summary>
        public double SlowRelaxationTime { get; set; }
    }
}
