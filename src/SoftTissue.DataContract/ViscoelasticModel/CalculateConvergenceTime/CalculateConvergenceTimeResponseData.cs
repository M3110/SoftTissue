using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateConvergenceTime operation response.
    /// </summary>
    public sealed class CalculateConvergenceTimeResponseData : OperationResponseData
    {
        /// <summary>
        /// The list of result file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The convergence time.
        /// Unit: s (second).'
        /// </summary>
        public double ConvergenceTime { get; set; }
    }
}
