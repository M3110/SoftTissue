using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateConvergenceTime operation response.
    /// </summary>
    public sealed class CalculateConvergenceTimeResponseData : CalculateResultsResponseData
    {
        /// <summary>
        /// The convergence time.
        /// Unit: s (second).'
        /// </summary>
        public double ConvergenceTime { get; set; }
    }
}
