using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtendResults
{
    /// <summary>
    /// It represents the request content to AnalyzeAndPredictResults operation.
    /// </summary>
    public class AnalyzeAndExtendResultsRequest : OperationRequestBase
    {
        /// <summary>
        /// The file name with the experimental results.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; set; }

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
