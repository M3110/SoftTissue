using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.Experimental.AnalyzeAndExtendResults
{
    /// <summary>
    /// It represents the 'data' content of AnalyzeAndPredictResults operation response.
    /// </summary>
    public class AnalyzeAndExtendResultsResponseData : OperationResponseData
    {
        /// <summary>
        /// The file name with the experimental results analyzed and predicted.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; set; }
    }
}
