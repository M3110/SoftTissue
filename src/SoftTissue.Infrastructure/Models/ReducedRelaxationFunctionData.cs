namespace SoftTissue.Infrastructure.Models
{
    public class ReducedRelaxationFunctionData
    {
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
