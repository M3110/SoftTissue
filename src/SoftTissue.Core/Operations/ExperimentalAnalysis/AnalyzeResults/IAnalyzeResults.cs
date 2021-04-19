using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults;

namespace SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeResults
{
    /// <summary>
    /// It is responsible to analyze the experimental results removing the invalid points.
    /// </summary>
    public interface IAnalyzeResults : IOperationBase<AnalyzeResultsRequest, AnalyzeResultsResponse, AnalyzeResultsResponseData>
    {
        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string CreateSolutionFile(string fileName);

        /// <summary>
        /// This method analyzes the experimental results informing if the current result is valid.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="experimentalResult"></param>
        /// <returns></returns>
        AnalyzedExperimentalResult AnalyzeExperimentalResults(AnalyzedExperimentalResult previousResult, ExperimentalResult experimentalResult);
    }
}