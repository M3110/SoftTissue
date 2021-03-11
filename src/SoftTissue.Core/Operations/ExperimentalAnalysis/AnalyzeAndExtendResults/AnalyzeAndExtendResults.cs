﻿using CsvHelper;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtendResults;
using SoftTissue.DataContract.OperationBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeAndExtendResults
{
    /// <summary>
    /// It is responsible to analyze the experimental results and extend.
    /// </summary>
    public class AnalyzeAndExtendResults : OperationBase<AnalyzeAndExtendResultsRequest, AnalyzeAndExtendResultsResponse, AnalyzeAndExtendResultsResponseData>, IAnalyzeAndExtendResults
    {
        private double _previousDerivative;
        private readonly IDerivative _derivative;

        /// <summary>
        /// The experimental results obtained in the file passed on request.
        /// </summary>
        private List<ExperimentalResult> _experimentalResults;

        /// <summary>
        /// The base path to files.
        /// </summary>
        private readonly string _templateBasePath = BasePaths.AnalyzeAndExtend;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="derivative"></param>
        public AnalyzeAndExtendResults(IDerivative derivative)
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
        /// <param name="currentDerivative"></param>
        /// <param name="timeStep"></param>
        /// <returns></returns>
        public double? CalculateSecondDerivative(double previousDerivative, double currentDerivative, double timeStep)
        {
            if (currentDerivative.IsPositive() || Math.Abs(currentDerivative) > Math.Abs(previousDerivative))
                return null;

            double secondDerivative = this._derivative.Calculate(this._previousDerivative, currentDerivative, timeStep);

            this._previousDerivative = previousDerivative;

            return secondDerivative;
        }

        /// <summary>
        /// This method calculates the final second derivative to be used when extending results.
        /// </summary>
        /// <param name="previousSecondDerivative"></param>
        /// <param name="currentSecondDerivative"></param>
        /// <returns></returns>
        public double CalculateFinalSecondDerivative(double previousSecondDerivative, double? currentSecondDerivative)
        {
            if (currentSecondDerivative == null || currentSecondDerivative.IsNegative())
                return previousSecondDerivative;

            if (previousSecondDerivative == 0)
                return currentSecondDerivative.Value;

            double relativeDiference = previousSecondDerivative.RelativeDiference(currentSecondDerivative.Value);

            if (relativeDiference.IsPositive() && relativeDiference < 1 - Constants.RelativePrecision && relativeDiference > Constants.RelativePrecision)
                return currentSecondDerivative.Value;

            return previousSecondDerivative;
        }

        /// <summary>
        /// This method calculates the time step that is used when extending the results.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public double CalculateTimeStep(AnalyzeAndExtendResultsRequest request)
        {
            // If should not use the time step at the experimental file, use the time step informed on request.
            if (request.UseFileTimeStep == false)
                return request.TimeStep;

            // Otherwise, calculates the time step based on the first and second lines at the experimental file.
            return this._experimentalResults[1].Time - this._experimentalResults[0].Time;
        }

        /// <summary>
        /// This method extends the results.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="previousResult"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalSecondDerivative"></param>
        /// <returns></returns>
        public AnalyzedExperimentalResult CalculateExtendedResult(AnalyzeAndExtendResultsResponse response, AnalyzedExperimentalResult previousResult, double timeStep, double finalSecondDerivative)
        {
            // Step 6.1 - Creates the analyzed experimental results.
            var extendedResult = new AnalyzedExperimentalResult { Time = previousResult.Time + timeStep };

            // If the derivative is not equals to zero, it means that the stress do not tends to asymptote.
            if (previousResult.Derivative.Value != 0)
            {
                // Step 6.2 - Calculates the derivative.
                double derivative = previousResult.Derivative.Value + timeStep * finalSecondDerivative;

                // If the derivative is not negative, it means that the sign has changed.
                // When it occurs, it is assumed that the derivative tended to zero, so the value is overwritten to zero.
                if (derivative.IsNegative() == false)
                    derivative = 0;

                extendedResult.Derivative = derivative;

                // Step 6.3 - Calculates the stress.
                double stress = previousResult.Stress + timeStep * derivative;
                extendedResult.Stress = stress;

                return extendedResult;
            }

            // If the derivative is equals to zero, the stress reached to asymptote.
            // Maps the time and stress to the response if the asymptote time don't have value.
            if (response.Data.AsymptoteTime == null)
            {
                response.Data.AsymptoteTime = previousResult.Time;
                response.Data.AsymptoteStress = previousResult.Stress;
            }

            extendedResult.Stress = previousResult.Stress;
            extendedResult.Derivative = 0;

            return extendedResult;
        }

        /// <summary>
        /// This method analyzes and predicts the experimental results.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<AnalyzeAndExtendResultsResponse> ProcessOperation(AnalyzeAndExtendResultsRequest request)
        {
            var response = new AnalyzeAndExtendResultsResponse { Data = new AnalyzeAndExtendResultsResponseData() };

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

                    // Step 4.2 - Calculates the step time to experimental result.
                    double experimentalTimeStep = analyzedResult.Time - previousResult.Time;

                    // Step 4.3 - Calculates the derivative and second derivative.
                    // For an utopian case:
                    // - The derivative must be negative to indicates that the values is decreasing.
                    // - The second derivative must be positive to indicate that the curve's concavity is upward.
                    analyzedResult.Derivative = this._derivative.Calculate(previousResult.Stress, analyzedResult.Stress, experimentalTimeStep);
                    analyzedResult.SecondDerivative = this.CalculateSecondDerivative(previousResult.Derivative.Value, analyzedResult.Derivative.Value, experimentalTimeStep);

                    // Step 4.4 - Writes the result in the file.
                    csvWriter.WriteLine(analyzedResult);

                    // Step 4.5 - Saves the current result to be used in the next iteration.
                    previousResult = analyzedResult;

                    // Step 4.6 - Sets the smallest second derivative.
                    finalSecondDerivative = this.CalculateFinalSecondDerivative(finalSecondDerivative, analyzedResult.SecondDerivative);
                }

                // Step 5 - Calculates the time step.
                double timeStep = this.CalculateTimeStep(request);

                // Step 6 - Extend the results, here will be used the last second derivative to preview the next values.
                while (previousResult.Time <= request.FinalTime)
                {
                    // Step 6.1 - Creates the analyzed experimental results.
                    // Step 6.2 - Calculates the derivative.
                    // Step 6.3 - Calculates the stress.
                    AnalyzedExperimentalResult extendedResult = this.CalculateExtendedResult(response, previousResult, timeStep, finalSecondDerivative);

                    // Step 6.4 - Writes the results in the file.
                    csvWriter.WriteLine(extendedResult);

                    // Step 6.5 - Saves the current extended result to be used in the next iteration.
                    previousResult = extendedResult;
                }
            }

            // Step 7 - Maps to response.
            response.Data.FileUri = Path.GetDirectoryName(solutionFileName);
            response.Data.FileName = Path.GetFileName(solutionFileName);

            return Task.FromResult(response);
        }

        /// <summary>
        /// This method validates the <see cref="AnalyzeAndExtendResultsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<AnalyzeAndExtendResultsResponse> ValidateOperation(AnalyzeAndExtendResultsRequest request)
        {
            var response = await base.ValidateOperation(request);
            if (response.Success == false)
            {
                return response;
            }

            if (request.UseFileTimeStep && request.TimeStep <= 0)
            {
                response.AddError(OperationErrorCode.RequestValidationError, "If should use the time step of file, the time step passed on request must be greather than 0.");
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
                response.AddError(OperationErrorCode.RequestValidationError, $"The file passed on request must have at least {Constants.MinimumFileNumberOfLines} lines.");
                return response;
            }

            return response;
        }
    }
}
