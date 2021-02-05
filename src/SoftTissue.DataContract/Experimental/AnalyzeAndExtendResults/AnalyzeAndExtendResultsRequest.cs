using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.Experimental.AnalyzeAndExtendResults
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
        /// True, if should use the time step from experimental file. False, otherwise.
        /// </summary>
        public bool UseTimeStepFromFile { get; set; }
    }
}
