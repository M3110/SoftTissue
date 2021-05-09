using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults;
using SoftTissue.DataContract.Models;
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
        /// This method checks if the stress is valid comparing with the previous stress.
        /// </summary>
        /// <param name="previousStress"></param>
        /// <param name="currentStress"></param>
        /// <param name="stressDirection"></param>
        /// <returns></returns>
        public bool IsStressValid(double previousStress, double currentStress, StressDirection stressDirection = StressDirection.Decrease);

        /// <summary>
        /// This method checks if the stress derivative is valid comparing with the previous stress derivative.
        /// </summary>
        /// <param name="previousDerivative"></param>
        /// <param name="currentDerivative"></param>
        /// <param name="stressDirection"></param>
        /// <returns></returns>
        public bool IsDerivativeValid(double previousDerivative, double currentDerivative, StressDirection stressDirection = StressDirection.Decrease);

        /// <summary>
        /// This method checks if the current result is valid.
        /// </summary>
        /// <param name="currentResult"></param>
        /// <param name="stressDirection"></param>
        /// <returns></returns>
        public bool IsResultValid(AnalyzedExperimentalResult currentResult, StressDirection stressDirection = StressDirection.Decrease);

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
        /// <param name="stressDirection"></param>
        /// <returns></returns>
        public (AnalyzedExperimentalResult SecondResult, int SecondResultIndex) CalculateSecondResultAndIndex(AnalyzedExperimentalResult firstResult, List<ExperimentalResult> experimentalResults, StressDirection stressDirection = StressDirection.Decrease);

        /// <summary>
        /// This method calculates the third valid result and its index at experimental file.
        /// </summary>
        /// <param name="secondResult"></param>
        /// <param name="secondResultIndex"></param>
        /// <param name="experimentalResults"></param>
        /// <param name="stressDirection"></param>
        /// <returns></returns>
        public (AnalyzedExperimentalResult ThirdResult, int ThirdResultIndex) CalculateThirdResultAndIndex(AnalyzedExperimentalResult secondResult, int secondResultIndex, List<ExperimentalResult> experimentalResults, StressDirection stressDirection = StressDirection.Decrease);

        /// <summary>
        /// This method analyzes the experimental results informing if the current result is valid.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="experimentalResult"></param>
        /// <param name="stressDirection"></param>
        /// <returns></returns>
        AnalyzedExperimentalResult AnalyzeExperimentalResult(AnalyzedExperimentalResult previousResult, ExperimentalResult experimentalResult, StressDirection stressDirection = StressDirection.Decrease);
    }
}