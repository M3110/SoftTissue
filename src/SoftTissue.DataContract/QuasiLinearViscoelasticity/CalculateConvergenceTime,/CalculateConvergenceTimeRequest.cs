using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime_
{
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
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// Constant B.
        /// </summary>
        public double ElasticPowerConstant { get; set; }

        /// <summary>
        /// Constant C.
        /// </summary>
        public double RelaxationIndex { get; set; }

        /// <summary>
        /// Tau 1.
        /// </summary>
        public double FastRelaxationTime { get; set; }

        /// <summary>
        /// Tau 2.
        /// </summary>
        public double SlowRelaxationTime { get; set; }
    }
}
