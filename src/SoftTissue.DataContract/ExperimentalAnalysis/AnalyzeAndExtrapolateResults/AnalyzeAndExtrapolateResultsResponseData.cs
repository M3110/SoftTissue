using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtrapolateResults
{
    /// <summary>
    /// It represents the 'data' content of AnalyzeAndPredictResults operation response.
    /// </summary>
    public sealed class AnalyzeAndExtrapolateResultsResponseData : OperationResponseData
    {
        /// <summary>
        /// The file name with the experimental results analyzed and predicted.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; set; }

        /// <summary>
        /// The time when the stress tends to an asymptote value.
        /// </summary>
        public double? AsymptoteTime { get; set; }

        /// <summary>
        /// The value that the stress tends.
        /// </summary>
        public double? AsymptoteStress { get; set; }
    }
}
