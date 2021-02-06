using SoftTissue.DataContract.CalculateResult;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime_
{
    /// <summary>
    /// It represents the 'data' content to CalculateConvergenceTime operation response.
    /// </summary>
    public class CalculateConvergenceTimeResponseData : CalculateResultResponseData
    {
        /// <summary>
        /// The convergence time.
        /// Unit: s (second).'
        /// </summary>
        public double ConvergenceTime { get; set; }
    }
}
