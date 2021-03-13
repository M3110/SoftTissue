using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtrapolateResults;

namespace SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeAndExtrapolateResults
{
    /// <summary>
    /// It is responsible to analyze and predict the experimental results.
    /// </summary>
    public interface IAnalyzeAndExtrapolateResults : IOperationBase<AnalyzeAndExtrapolateResultsRequest, AnalyzeAndExtrapolateResultsResponse, AnalyzeAndExtrapolateResultsResponseData> { }
}
