using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime_
{
    /// <summary>
    /// It represents the 'data' content to CalculateConvergenceTime operation response.
    /// </summary>
    public class CalculateConvergenceTimeResponseData : OperationResponseData 
    {
        /// <summary>
        /// The convergence time.
        /// Unity: s (second).'
        /// </summary>
        public double ConvergenceTime { get; set; }
    }
}
