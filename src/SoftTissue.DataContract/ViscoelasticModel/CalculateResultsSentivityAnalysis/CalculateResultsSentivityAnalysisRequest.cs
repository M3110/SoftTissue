using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateResultsSentivityAnalysis operations.
    /// </summary>
    public abstract class CalculateResultsSentivityAnalysisRequest : OperationRequestBase
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
    }
}
