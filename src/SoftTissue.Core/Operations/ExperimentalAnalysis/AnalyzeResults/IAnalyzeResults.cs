using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults;
using System.Collections.Generic;

namespace SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeResults
{
    /// <summary>
    /// It is responsible to analyze the experimental results removing the invalid points.
    /// </summary>
    public interface IAnalyzeResults : IOperationBase<AnalyzeResultsRequest, AnalyzeResultsResponse, AnalyzeResultsResponseData>
    {
        /// <summary>
        /// The experimental results obtained in the file passed on request.
        /// </summary>
        public List<ExperimentalResult> ExperimentalResults { get; }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string CreateSolutionFile(string fileName);

        /// <summary>
        /// This method calculates the second valid result and its index at experimental file.
        /// </summary>
        /// <param name="firstResult"></param>
        /// <param name="experimentalResults"></param>
        /// <returns></returns>
        public (AnalyzedExperimentalResult SecondResult, int SecondResultIndex) CalculateSecondResultAndIndex(AnalyzedExperimentalResult firstResult, List<ExperimentalResult> experimentalResults);

        /// <summary>
        /// This method calculates the third valid result and its index at experimental file.
        /// </summary>
        /// <param name="secondResult"></param>
        /// <param name="secondResultIndex"></param>
        /// <param name="experimentalResults"></param>
        /// <returns></returns>
        public (AnalyzedExperimentalResult ThirdResult, int ThirdResultIndex) CalculateThirdResultAndIndex(AnalyzedExperimentalResult secondResult, int secondResultIndex, List<ExperimentalResult> experimentalResults);

        /// <summary>
        /// This method analyzes the experimental results informing if the current result is valid.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="experimentalResult"></param>
        /// <returns></returns>
        AnalyzedExperimentalResult AnalyzeExperimentalResult(AnalyzedExperimentalResult previousResult, ExperimentalResult experimentalResult);
    }
}