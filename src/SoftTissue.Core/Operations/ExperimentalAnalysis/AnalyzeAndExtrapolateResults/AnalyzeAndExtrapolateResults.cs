using CsvHelper;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtrapolateResults;
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
        /// The last valid result.
        /// </summary>
        private AnalyzedExperimentalResult _previousResult;

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

        private readonly IDerivative _derivative;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="derivative"></param>
        public AnalyzeAndExtrapolateResults(IDerivative derivative)
        {
            this._derivative = derivative;
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
        /// This method analyzes the results.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="experimentalResult"></param>
        /// <returns></returns>
        public AnalyzedExperimentalResult AnalyzeResults(AnalyzedExperimentalResult previousResult, ExperimentalResult experimentalResult)
        {
            // Creates the result based on the experimental result.
            var currentResult = new AnalyzedExperimentalResult(experimentalResult);

            // If the previous result is null, it means that it was the first line.
            if (previousResult == null)
            {
                // It is necessary to set the previous result like null to ensure that it will only be filled in when the current result was invalid.
                this._previousResult = null;

                currentResult.IsValid = true;
                return currentResult;
            }

            // The stress must decrease over the time, so the previous stress must be less than the current stress to continue to next step.
            if (currentResult.Stress > previousResult.Stress && currentResult.Stress > this._previousResult.Stress)
            {
                // Saves the previous results to be used to calculate the first and second derivatives at the next step.
                this._previousResult = previousResult;

                currentResult.IsValid = false;
                return currentResult;
            }

            currentResult.Derivative = this._derivative.Calculate(previousResult.Stress, currentResult.Stress, currentResult.Time - previousResult.Time);

            // The previous derivative must exists to continue to next step.
            if (previousResult.Derivative.HasValue == false)
            {
                // It is necessary to set the previous result like null to ensure that it will only be filled in when the current result was invalid.
                this._previousResult = null;

                currentResult.IsValid = true;
                return currentResult;
            }

            // The stress derivative must increase over the time, so the previous stress derivative must be greather than the current stress derivative to continue to next step.
            if (currentResult.Derivative < previousResult.Derivative && currentResult.Derivative < this._previousResult.Derivative)
            {
                // Saves the previous results to be used to calculate the first and second derivatives at the next step.
                this._previousResult = previousResult;

                currentResult.IsValid = false;
                return currentResult;
            }

            if (this._previousResult != null)
            {
                currentResult.SecondDerivative = this._derivative.Calculate(this._previousResult.Derivative.Value, currentResult.Derivative.Value, currentResult.Time - this._previousResult.Time);
                return currentResult;
            }

            currentResult.SecondDerivative = this._derivative.Calculate(previousResult.Derivative.Value, currentResult.Derivative.Value, currentResult.Time - previousResult.Time);
            return currentResult;
        }

        /// <summary>
        /// This method calculates the final second derivative to be used when extrapolateing results.
        /// </summary>
        /// <param name="previousSecondDerivative"></param>
        /// <param name="currentSecondDerivative"></param>
        /// <returns></returns>
        public double CalculateFinalSecondDerivative(double previousSecondDerivative, double? currentSecondDerivative)
        {
            // If the current second derivative is negative, it indicates that the curve's concavity changed to downward.
            // When it occurs, it means that some error happened while calculating the second derivative.
            if (currentSecondDerivative == null || currentSecondDerivative.IsNegative())
                return previousSecondDerivative;

            if (previousSecondDerivative == 0)
                return currentSecondDerivative.Value;

            double relativeDiference = previousSecondDerivative.RelativeDiference(currentSecondDerivative.Value);

            // If the relative diference between the previous and current second derivative is positive, it indicates that the second derivative decrease as expected.
            if (relativeDiference.IsPositive() && relativeDiference < 1 - Constants.RelativePrecision && relativeDiference > Constants.RelativePrecision)
                return currentSecondDerivative.Value;

            return previousSecondDerivative;
        }

        /// <summary>
        /// This method calculates the time step that is used when extrapolateing the results.
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
        /// Asynchronously, this method analyzes and predicts the experimental results.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<AnalyzeAndExtrapolateResultsResponse> ProcessOperationAsync(AnalyzeAndExtrapolateResultsRequest request)
        {
            var response = new AnalyzeAndExtrapolateResultsResponse { Data = new AnalyzeAndExtrapolateResultsResponseData() };

            string solutionFileName = this.CreateSolutionFile(request.FileName);
            using (var streamWriter = new StreamWriter(solutionFileName))
            {
                // Step 1 - Writes the file header.
                streamWriter.WriteLine(this._fileHeader);

                // Step 2 - Writes the first line. It is equals to the first line of the file analyzed.
                ExperimentalResult firstExperimentalResult = this._experimentalResults[0];
                streamWriter.WriteLine($"{firstExperimentalResult.Time},{firstExperimentalResult.Stress},,");

                // Step 3 - Writes the second line. It just have the derivative, because to calculate the second derivative needs two derivative previously calculated.
                ExperimentalResult secondExperimentalResult = this._experimentalResults[1];

                var previousResult = new AnalyzedExperimentalResult(secondExperimentalResult);
                previousResult.Derivative = this._derivative.Calculate(firstExperimentalResult.Stress, secondExperimentalResult.Stress, secondExperimentalResult.Time - firstExperimentalResult.Time);

                streamWriter.WriteLine($"{previousResult.Time},{previousResult.Stress},,");

                // Instantiate the variable to contain the final second derivative.
                double finalSecondDerivative = 0;

                // Step 4 - Analyze the experimental results avoiding the points that are invalid.
                foreach (ExperimentalResult experimentalResult in this._experimentalResults)
                {
                    // Step 4.1 - Analyzes the experimental result.
                    AnalyzedExperimentalResult analyzedResult = this.AnalyzeResults(previousResult, experimentalResult);

                    // Step 4.2 - Writes the result in the file if it is valid.
                    if (analyzedResult.IsValid == true)
                        streamWriter.WriteLine($"{analyzedResult.Time},{analyzedResult.Stress},,");

                    // Step 4.3 - Saves the current result to be used in the next iteration.
                    previousResult = analyzedResult;

                    // Step 4.4 - Sets the smallest second derivative.
                    finalSecondDerivative = this.CalculateFinalSecondDerivative(finalSecondDerivative, analyzedResult.SecondDerivative);
                }

                // Step 5 - Calculates the time step.
                double timeStep = this.CalculateTimeStep(request);

                // Step 6 - Extrapolate the results, here will be used the last second derivative to preview the next values.
                while (previousResult.Time <= request.FinalTime)
                {
                    // Step 6.1 - Creates the analyzed experimental results.
                    // Step 6.2 - Calculates the derivative.
                    // Step 6.3 - Calculates the stress.
                    AnalyzedExperimentalResult extrapolatedResult = this.ExtrapolateResult(previousResult, timeStep, finalSecondDerivative);

                    // If the derivative is equals to zero, the stress reached to asymptote, so should map the time and stress to the response.
                    if (extrapolatedResult.Derivative == 0 && response.Data.AsymptoteTime.HasValue == false)
                    {
                        response.Data.AsymptoteTime = extrapolatedResult.Time;
                        response.Data.AsymptoteStress = extrapolatedResult.Stress;
                    }

                    // Step 6.4 - Writes the results in the file.
                    streamWriter.WriteLine($",,{extrapolatedResult.Time},{extrapolatedResult.Stress}");

                    // Step 6.5 - Saves the current extrapolateed result to be used in the next iteration.
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

            // Reads the file and add it into a variable to be used in the operation.
            using (var streamReader = new StreamReader(Path.Combine(request.FileUri, request.FileName)))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                this._experimentalResults = csvReader.GetRecords<ExperimentalResult>().ToList();
            }

            // The file must be at least a specific number of lines to be possible to execute the operation.
            if (this._experimentalResults.Count <= Constants.MinimumFileNumberOfLines)
            {
                response.SetBadRequestError(OperationErrorCode.RequestValidationError, $"The file passed on request must have at least {Constants.MinimumFileNumberOfLines} lines.");

                return response;
            }

            return response;
        }
    }
}
