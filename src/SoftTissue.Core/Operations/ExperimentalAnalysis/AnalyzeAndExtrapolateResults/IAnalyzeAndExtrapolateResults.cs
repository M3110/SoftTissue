using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtrapolateResults;

namespace SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeAndExtrapolateResults
{
    /// <summary>
    /// It is responsible to analyze and predict the experimental results.
    /// </summary>
    public interface IAnalyzeAndExtrapolateResults : IOperationBase<AnalyzeAndExtrapolateResultsRequest, AnalyzeAndExtrapolateResultsResponse, AnalyzeAndExtrapolateResultsResponseData> 
    {
        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string CreateSolutionFile(string fileName);

        /// <summary>
        /// This method analyzes the results.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="experimentalResult"></param>
        /// <returns></returns>
        AnalyzedExperimentalResult AnalyzeResults(AnalyzedExperimentalResult previousResult, ExperimentalResult experimentalResult);

        /// <summary>
        /// This method calculates the final second derivative to be used when extrapolateing results.
        /// </summary>
        /// <param name="previousSecondDerivative"></param>
        /// <param name="currentSecondDerivative"></param>
        /// <returns></returns>
        double CalculateFinalSecondDerivative(double previousSecondDerivative, double? currentSecondDerivative);

        /// <summary>
        /// This method calculates the time step that is used when extrapolateing the results.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        double CalculateTimeStep(AnalyzeAndExtrapolateResultsRequest request);

        /// <summary>
        /// This method extrapolates the results.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalSecondDerivative"></param>
        /// <returns></returns>
        AnalyzedExperimentalResult ExtrapolateResult(AnalyzedExperimentalResult previousResult, double timeStep, double finalSecondDerivative);
    }
}
