using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.Experimental.AnalyzeAndExtendResults;

namespace SoftTissue.Core.Operations.Experimental
{
    /// <summary>
    /// It is responsible to analyze and predict the experimental results.
    /// </summary>
    public interface IAnalyzeAndExtendResults : IOperationBase<AnalyzeAndExtendResultsRequest, AnalyzeAndExtendResultsResponse, AnalyzeAndExtendResultsResponseData> { }
}
