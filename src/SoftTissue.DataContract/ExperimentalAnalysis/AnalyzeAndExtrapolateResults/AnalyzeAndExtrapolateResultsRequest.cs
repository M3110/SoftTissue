using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtrapolateResults
{
    /// <summary>
    /// It represents the request content to AnalyzeAndPredictResults operation.
    /// </summary>
    public sealed class AnalyzeAndExtrapolateResultsRequest : OperationRequestBase
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

        /// <summary>
        /// True, if should be used the time step at the file. False, if must be used the time step passed on request to extrapolate the results.
        /// </summary>
        public bool UseFileTimeStep { get; set; }
    }
}
