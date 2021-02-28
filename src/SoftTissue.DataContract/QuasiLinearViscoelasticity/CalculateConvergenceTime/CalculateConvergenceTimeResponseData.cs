using SoftTissue.DataContract.CalculateResult;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateConvergenceTime operation response.
    /// </summary>
    public sealed class CalculateConvergenceTimeResponseData : CalculateResultResponseData
    {
        /// <summary>
        /// The convergence time.
        /// Unit: s (second).'
        /// </summary>
        public double ConvergenceTime { get; set; }
    }
}
