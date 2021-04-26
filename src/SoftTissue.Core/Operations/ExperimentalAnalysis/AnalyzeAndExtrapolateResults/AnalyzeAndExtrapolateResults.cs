using CsvHelper;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.Operations.Base;
using SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeResults;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtrapolateResults;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults;
using SoftTissue.DataContract.OperationBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeAndExtrapolateResults
{
    /// <summary>
    /// It is responsible to analyze the experimental results and extrapolate.
    /// </summary>
    public class AnalyzeAndExtrapolateResults : OperationBase<AnalyzeAndExtrapolateResultsRequest, AnalyzeAndExtrapolateResultsResponse, AnalyzeAndExtrapolateResultsResponseData>, IAnalyzeAndExtrapolateResults
    {
        /// <summary>
        /// The experimental results obtained in the file passed on request.
        /// </summary>
        private List<ExperimentalResult> _experimentalResults;

        /// <summary>
        /// The header to solution file.
        /// The file is built that way to help when using Origin to plot results, separating the stress and the extrapoleted stress.
        /// </summary>
        private readonly string _fileHeader = "Time,Stress,Time,Extrapolated Stress";

        /// <summary>
        /// The base path to files.
        /// </summary>
        private readonly string _templateBasePath = BasePaths.AnalyzeAndExtrapolateResults;

        private readonly IAnalyzeResults _analyzeResults;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="analyzeResults"></param>
        /// <param name="derivative"></param>
        public AnalyzeAndExtrapolateResults(IAnalyzeResults analyzeResults)
        {
            this._analyzeResults = analyzeResults;
        }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string CreateSolutionFile(string fileName)
        {
            var fileInfo = new FileInfo(Path.Combine(
                this._templateBasePath,
                $"{Path.GetFileNameWithoutExtension(fileName)}_rate.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// This method calculates the final second derivative to be used when extrapolating results.
        /// </summary>
        /// <param name="previousSecondDerivative"></param>
        /// <param name="currentSecondDerivative"></param>
        /// <returns></returns>
        public double CalculateFinalSecondDerivative(double previousSecondDerivative, double currentSecondDerivative)
        {
            if (previousSecondDerivative == 0)
                return currentSecondDerivative;

            // The second derivative cannot be smallest than initial stress divided by last time squared.
            // Mathematic equation => d²(stress)/d²t < stress / time².
            if (currentSecondDerivative < this._experimentalResults[0].Stress / Math.Pow(this._experimentalResults.Last().Time, 2))
            {
                return previousSecondDerivative;
            }

            return currentSecondDerivative < previousSecondDerivative ? currentSecondDerivative : previousSecondDerivative;
        }

        /// <summary>
        /// This method calculates the time step that is used when extrapolating the results.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public double CalculateTimeStep(AnalyzeAndExtrapolateResultsRequest request)
        {
            // If should not use the time step at the experimental file, use the time step informed on request.
            if (request.UseFileTimeStep == false)
                return request.TimeStep;

            // Otherwise, calculates the time step based on the first and second lines at the experimental file.
            return this._experimentalResults[1].Time - this._experimentalResults[0].Time;
        }

        /// <summary>
        /// This method extrapolates the results.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalSecondDerivative"></param>
        /// <returns></returns>
        public AnalyzedExperimentalResult ExtrapolateResult(AnalyzedExperimentalResult previousResult, double timeStep, double finalSecondDerivative)
        {
            // Step 6.1 - Creates the analyzed experimental results.
            var extrapolatedResult = new AnalyzedExperimentalResult { Time = previousResult.Time + timeStep };

            // If the derivative is not equals to zero, it means that the stress do not tends to asymptote.
            if (previousResult.Derivative.Value != 0)
            {
                // Step 6.2 - Calculates the derivative.
                double derivative = previousResult.Derivative.Value + timeStep * finalSecondDerivative;

                // If the derivative is not negative, it means that the sign has changed.
                // When it occurs, it is assumed that the derivative tended to zero, so the value is overwritten to zero.
                if (derivative.IsPositive())
                    derivative = 0;

                extrapolatedResult.Derivative = derivative;

                // Step 6.3 - Calculates the stress.
                extrapolatedResult.Stress = previousResult.Stress + timeStep * derivative;

                return extrapolatedResult;
            }

            extrapolatedResult.Stress = previousResult.Stress;
            extrapolatedResult.Derivative = 0;

            return extrapolatedResult;
        }

        /// <summary>
        /// Asynchronously, this method analyzes and extrapolates the experimental results.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<AnalyzeAndExtrapolateResultsResponse> ProcessOperationAsync(AnalyzeAndExtrapolateResultsRequest request)
        {
            var response = new AnalyzeAndExtrapolateResultsResponse();
            response.SetSuccessCreated();

            string solutionFileName = this.CreateSolutionFile(request.FileName);
            using (var streamWriter = new StreamWriter(solutionFileName))
            {
                // Step 1 - Writes the file header.
                streamWriter.WriteLine(this._fileHeader);

                // Step 2 - Writes the first line in the file.
                var firstResult = new AnalyzedExperimentalResult(this._experimentalResults[0]);
                streamWriter.WriteLine($"{this._experimentalResults[0].Time},{this._experimentalResults[0].Stress}");

                // Step 3 - Calculates the second result and write its in the file.
                // The code below uses the method Skip(1) to avoid the first result in the list.
                (AnalyzedExperimentalResult secondResult, int secondResultIndex) = this._analyzeResults.CalculateSecondResultAndIndex(firstResult, this._experimentalResults);
                streamWriter.WriteLine($"{secondResult.Time},{secondResult.Stress},,");

                // Step 4 - Calculates the third result and write its in the file.
                // The third result is the previous result that will be used in next step.
                // The code below uses the method Skip(secondResultIndex + 1) to avoid the lines that was previously analyzed to calculate the second valid result.
                (AnalyzedExperimentalResult previousResult, int previousResultIndex) = this._analyzeResults.CalculateThirdResultAndIndex(secondResult, secondResultIndex, this._experimentalResults);
                streamWriter.WriteLine($"{previousResult.Time},{previousResult.Stress},,");

                double finalSecondDerivative = 0;

                // Step 5 - Analyze the experimental results avoiding the points that are invalid.
                foreach (ExperimentalResult experimentalResult in this._experimentalResults.Skip(previousResultIndex + 1))
                {
                    // Step 5.1 - Analyzes the experimental result.
                    AnalyzedExperimentalResult analyzedResult = this._analyzeResults.AnalyzeExperimentalResult(previousResult, experimentalResult);

                    if (analyzedResult.IsValid == true)
                    {
                        // Step 5.2 - Writes the valid result in the file.
                        streamWriter.WriteLine($"{analyzedResult.Time},{analyzedResult.Stress},,");

                        // Step 5.3 - Sets the smallest second derivative.
                        finalSecondDerivative = this.CalculateFinalSecondDerivative(finalSecondDerivative, analyzedResult.SecondDerivative.Value);
                    }

                    // Step 5.4 - Saves the current result to be used in the next iteration.
                    previousResult = analyzedResult;
                }

                // Step 6 - Calculates the time step.
                double timeStep = this.CalculateTimeStep(request);

                // Step 7 - Extrapolate the results, here will be used the last second derivative to preview the next values.
                while (previousResult.Time <= request.FinalTime)
                {
                    // Step 7.1 - Creates the analyzed experimental results.
                    // Step 7.2 - Calculates the derivative.
                    // Step 7.3 - Calculates the stress.
                    AnalyzedExperimentalResult extrapolatedResult = this.ExtrapolateResult(previousResult, timeStep, finalSecondDerivative);

                    // If the derivative is equals to zero, the stress reached to asymptote, so should map the time and stress to the response.
                    if (extrapolatedResult.Derivative == 0 && response.Data.AsymptoteTime.HasValue == false)
                    {
                        response.Data.AsymptoteTime = extrapolatedResult.Time;
                        response.Data.AsymptoteStress = extrapolatedResult.Stress;
                    }

                    // Step 7.4 - Writes the results in the file.
                    streamWriter.WriteLine($",,{extrapolatedResult.Time},{extrapolatedResult.Stress}");

                    // Step 7.5 - Saves the current extrapolateed result to be used in the next iteration.
                    previousResult = extrapolatedResult;
                }
            }

            // Step 7 - Maps to response.
            response.Data.FileUri = Path.GetDirectoryName(solutionFileName);
            response.Data.FileName = Path.GetFileName(solutionFileName);

            return Task.FromResult(response);
        }

        /// <summary>
        /// Asynchronously, this method validates the <see cref="AnalyzeAndExtrapolateResultsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<AnalyzeAndExtrapolateResultsResponse> ValidateOperationAsync(AnalyzeAndExtrapolateResultsRequest request)
        {
            var response = await base.ValidateOperationAsync(request).ConfigureAwait(false);
            if (response.Success == false)
            {
                return response;
            }

            if (request.UseFileTimeStep == false && request.TimeStep <= 0)
            {
                response.SetBadRequestError(OperationErrorCode.RequestValidationError, "If should not use the time step of file, the time step passed on request must be greather than 0.");

                return response;
            }

            AnalyzeResultsResponse analyzeResultResponse = await this._analyzeResults.ValidateOperationAsync(new AnalyzeResultsRequest
            {
                FileName = request.FileName,
                FileUri = request.FileUri
            }).ConfigureAwait(false);

            if (analyzeResultResponse.Success == false)
            {
                response.AddErrors(analyzeResultResponse.Errors);
                response.SetBadRequestError(OperationErrorCode.RequestValidationError, "Ocurred error while validating the request for operation AnalyzeResults.");

                return response;
            }

            // Gets the experimental results that was already read in the operation AnalyzeResults.
            this._experimentalResults = this._analyzeResults.ExperimentalResults;

            return response;
        }
    }
}
