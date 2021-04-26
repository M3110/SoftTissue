using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults
{
    /// <summary>
    /// It represents the 'data' content of AnalyzeResults operation response.
    /// </summary>
    public sealed class AnalyzeResultsResponseData : OperationResponseData
    {
        /// <summary>
        /// The file name with the experimental results analyzed and extrapolateed.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; set; }
    }
}
