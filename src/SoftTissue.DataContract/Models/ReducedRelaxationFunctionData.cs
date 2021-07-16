namespace SoftTissue.DataContract.Models
{
    /// <summary>
    /// It constains the input data to Reduced Relaxation Function.
    /// </summary>
    public class ReducedRelaxationFunctionData
    {
        /// <summary>
        /// The relaxation stiffness. Constant C.
        /// Unit: Dimensionless.
        /// </summary>
        public double RelaxationStiffness { get; set; }

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
