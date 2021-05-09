using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults
{
    /// <summary>
    /// It represents the request content to AnalyzeResults operation.
    /// </summary>
    public sealed class AnalyzeResultsRequest : OperationRequestBase
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
        /// The stress direction, indicating if stress increase or decrease.
        /// </summary>
        public StressDirection StressDirection { get; set; }
    }
}
