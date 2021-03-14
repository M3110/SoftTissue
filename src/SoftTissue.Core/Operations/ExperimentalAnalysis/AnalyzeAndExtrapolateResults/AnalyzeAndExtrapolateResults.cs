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
        /// The last valid derivative.
        /// </summary>
        private double _previousDerivative;

        /// <summary>
        /// The time of last valid derivative.
        /// </summary>
        private double _previousTime;

        private readonly IDerivative _derivative;

        /// <summary>
        /// The experimental results obtained in the file passed on request.
        /// </summary>
        private List<ExperimentalResult> _experimentalResults;

        /// <summary>
        /// The base path to files.
        /// </summary>
        private readonly string _templateBasePath = BasePaths.AnalyzeAndExtrapolateResults;

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
        /// This method calculates the second derivative.
        /// </summary>
        /// <param name="previousDerivative"></param>
        /// <param name="previousTime"></param>
        /// <param name="currentDerivative"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public double? CalculateSecondDerivative(double previousDerivative, double previousTime, double currentDerivative, double currentTime)
        {
            // If the previous derivative is valid, this value will be saved.
            if ((this._previousDerivative == 0 && previousDerivative.IsNegative()) || Math.Abs(previousDerivative) < Math.Abs(this._previousDerivative))
            {
                this._previousTime = previousTime;
                this._previousDerivative = previousDerivative;
            }

            // Checks if the current derivative is valid. If not, the second derivative cannot be calculated to current time.
            if (currentDerivative.IsPositive() || (Math.Abs(currentDerivative) > Math.Abs(previousDerivative) && Math.Abs(currentDerivative) > Math.Abs(this._previousDerivative)))
            {
                return null;
            }

            return this._derivative.Calculate(this._previousDerivative, currentDerivative, currentTime - this._previousTime);
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
        public AnalyzedExperimentalResult CalculateExtrapolatedResult(AnalyzedExperimentalResult previousResult, double timeStep, double finalSecondDerivative)
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
        /// This method analyzes and predicts the experimental results.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<AnalyzeAndExtrapolateResultsResponse> ProcessOperation(AnalyzeAndExtrapolateResultsRequest request)
        {
            var response = new AnalyzeAndExtrapolateResultsResponse { Data = new AnalyzeAndExtrapolateResultsResponseData() };

            string solutionFileName = this.CreateSolutionFile(request.FileName);
            using (var streamWriter = new StreamWriter(solutionFileName))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                // Step 1 - Writes the file header.
                csvWriter.WriteHeader<AnalyzedExperimentalResult>();
                csvWriter.NextRecord();

                // Step 2 - Writes the first line. It is equals to the first line of the file analyzed.
                ExperimentalResult firstExperimentalResult = this._experimentalResults[0];
                csvWriter.WriteLine(new AnalyzedExperimentalResult(firstExperimentalResult));

                // Step 3 - Writes the second line. It just have the derivative, because to calculate the second derivative needs two derivative previously calculated.
                ExperimentalResult secondExperimentalResult = this._experimentalResults[1];

                var previousResult = new AnalyzedExperimentalResult(secondExperimentalResult);
                previousResult.Derivative = this._derivative.Calculate(firstExperimentalResult.Stress, secondExperimentalResult.Stress, secondExperimentalResult.Time - firstExperimentalResult.Time);

                csvWriter.WriteLine(previousResult);

                // Instantiate the variable to contain the final second derivative.
                double finalSecondDerivative = 0;

                // Step 4 - Analyze the experimental results.
                // Here is necessary to skip 2 lines, beacause that lines was already analyzed.
                foreach (ExperimentalResult experimentalResult in this._experimentalResults.Skip(2))
                {
                    // Step 4.1 - Converts the experimental result.
                    var analyzedResult = new AnalyzedExperimentalResult(experimentalResult);

                    // Step 4.2 - Calculates the derivative and second derivative.
                    // For an utopian case:
                    // - The derivative must be negative to indicates that the values is decreasing.
                    // - The second derivative must be positive to indicate that the curve's concavity is upward.
                    analyzedResult.Derivative = this._derivative.Calculate(previousResult.Stress, analyzedResult.Stress, analyzedResult.Time - previousResult.Time);
                    analyzedResult.SecondDerivative = this.CalculateSecondDerivative(previousResult.Derivative.Value, previousResult.Time, analyzedResult.Derivative.Value, analyzedResult.Time);

                    // Step 4.3 - Writes the result in the file.
                    csvWriter.WriteLine(analyzedResult);

                    // Step 4.4 - Saves the current result to be used in the next iteration.
                    previousResult = analyzedResult;

                    // Step 4.5 - Sets the smallest second derivative.
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
                    AnalyzedExperimentalResult extrapolateedResult = this.CalculateExtrapolatedResult(previousResult, timeStep, finalSecondDerivative);

                    // If the derivative is equals to zero, the stress reached to asymptote, so should map the time and stress to the response.
                    if (extrapolateedResult.Derivative == 0 && response.Data.AsymptoteTime.HasValue == false)
                    {
                        response.Data.AsymptoteTime = extrapolateedResult.Time;
                        response.Data.AsymptoteStress = extrapolateedResult.Stress;
                    }

                    // Step 6.4 - Writes the results in the file.
                    csvWriter.WriteLine(extrapolateedResult);

                    // Step 6.5 - Saves the current extrapolateed result to be used in the next iteration.
                    previousResult = extrapolateedResult;
                }
            }

            // Step 7 - Maps to response.
            response.Data.FileUri = Path.GetDirectoryName(solutionFileName);
            response.Data.FileName = Path.GetFileName(solutionFileName);

            return Task.FromResult(response);
        }

        /// <summary>
        /// This method validates the <see cref="AnalyzeAndExtrapolateResultsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<AnalyzeAndExtrapolateResultsResponse> ValidateOperation(AnalyzeAndExtrapolateResultsRequest request)
        {
            var response = await base.ValidateOperation(request);
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
