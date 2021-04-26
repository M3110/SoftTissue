using CsvHelper;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults;
using SoftTissue.DataContract.OperationBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeResults
{
    /// <summary>
    /// It is responsible to analyze the experimental results removing the invalid points.
    /// </summary>
    public class AnalyzeResults : OperationBase<AnalyzeResultsRequest, AnalyzeResultsResponse, AnalyzeResultsResponseData>, IAnalyzeResults
    {
        /// <summary>
        /// The last valid result.
        /// </summary>
        private AnalyzedExperimentalResult _previousResult;

        /// <summary>
        /// The header to solution file.
        /// The file is built that way to help when using Origin to plot results, separating the stress and the extrapoleted stress.
        /// </summary>
        private readonly string _fileHeader = "Time,Stress";

        /// <summary>
        /// The base path to files.
        /// </summary>
        private readonly string _templateBasePath = BasePaths.AnalyzeResults;

        private readonly IDerivative _derivative;

        /// <summary>
        /// The experimental results obtained in the file passed on request.
        /// </summary>
        public List<ExperimentalResult> ExperimentalResults { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="derivative"></param>
        public AnalyzeResults(IDerivative derivative)
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
                $"{Path.GetFileNameWithoutExtension(fileName)}_analyzed.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// This method calculates the second valid result and its index at experimental file.
        /// </summary>
        /// <param name="firstResult"></param>
        /// <param name="experimentalResults"></param>
        /// <returns></returns>
        public (AnalyzedExperimentalResult SecondResult, int SecondResultIndex) CalculateSecondResultAndIndex(AnalyzedExperimentalResult firstResult, List<ExperimentalResult> experimentalResults)
        {
            // The code below uses the method Skip(1) to avoid the first result in the list.
            foreach (var experimentalResult in experimentalResults.Skip(1))
            {
                AnalyzedExperimentalResult secondResult = new AnalyzedExperimentalResult(experimentalResult);

                // To be a valid result, stress must decrease over the time.
                if (firstResult.Stress > secondResult.Stress)
                {
                    secondResult.Derivative = this._derivative.Calculate(firstResult.Stress, secondResult.Stress, secondResult.Time - firstResult.Time);
                    secondResult.IsValid = true;

                    int secondResultIndex = experimentalResults.IndexOf(experimentalResult);

                    return (secondResult, secondResultIndex);
                }
            }

            throw new ArgumentException("Ocurred error while calculating the second result, so, it means that in the experimental results do not exist any valid result.");
        }

        /// <summary>
        /// This method calculates the third valid result and its index at experimental file.
        /// </summary>
        /// <param name="secondResult"></param>
        /// <param name="secondResultIndex"></param>
        /// <param name="experimentalResults"></param>
        /// <returns></returns>
        public (AnalyzedExperimentalResult ThirdResult, int ThirdResultIndex) CalculateThirdResultAndIndex(AnalyzedExperimentalResult secondResult, int secondResultIndex, List<ExperimentalResult> experimentalResults)
        {
            // The code below uses the method Skip(secondResultIndex + 1) to avoid the lines that was previously analyzed to calculate the second valid result.
            foreach (var experimentalResult in experimentalResults.Skip(secondResultIndex + 1))
            {
                AnalyzedExperimentalResult thirdResult = new AnalyzedExperimentalResult(experimentalResult);

                // To be a valid result, stress must decrease over the time.
                if (secondResult.Stress > thirdResult.Stress)
                {
                    thirdResult.Derivative = this._derivative.Calculate(secondResult.Stress, thirdResult.Stress, thirdResult.Time - secondResult.Time);

                    // To be a valid result, derivative must increase over the time.
                    if (secondResult.Derivative < thirdResult.Derivative)
                    {
                        thirdResult.SecondDerivative = this._derivative.Calculate(secondResult.Derivative.Value, thirdResult.Derivative.Value, thirdResult.Time - secondResult.Time);
                        thirdResult.IsValid = true;

                        int thirdResultIndex = experimentalResults.IndexOf(experimentalResult);

                        return (thirdResult, thirdResultIndex);
                    }
                }
            }

            throw new ArgumentException("Ocurred error while calculating the third result, so, it means that in the experimental results do not exist any valid result.");
        }

        /// <summary>
        /// This method analyzes the experimental results informing if the current result is valid.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="experimentalResult"></param>
        /// <returns></returns>
        public AnalyzedExperimentalResult AnalyzeExperimentalResult(AnalyzedExperimentalResult previousResult, ExperimentalResult experimentalResult)
        {
            // Creates the result based on the experimental result.
            var currentResult = new AnalyzedExperimentalResult(experimentalResult);

            // Calculates the first and second derivative using the previous result passed on method.
            currentResult.Derivative = this._derivative.Calculate(previousResult.Stress, currentResult.Stress, currentResult.Time - previousResult.Time);
            currentResult.SecondDerivative = this._derivative.Calculate(previousResult.Derivative.Value, currentResult.Derivative.Value, currentResult.Time - previousResult.Time);

            if (currentResult.Derivative.IsNegative() && currentResult.SecondDerivative.IsPositive())
            {
                this._previousResult = currentResult;

                currentResult.IsValid = true;
                return currentResult;
            }

            if (this._previousResult != null)
            {
                // Calculates the first and second derivative using the previous result passed on method.
                currentResult.Derivative = this._derivative.Calculate(this._previousResult.Stress, currentResult.Stress, currentResult.Time - this._previousResult.Time);
                currentResult.SecondDerivative = this._derivative.Calculate(this._previousResult.Derivative.Value, currentResult.Derivative.Value, currentResult.Time - this._previousResult.Time);

                if (currentResult.Derivative.IsNegative() && currentResult.SecondDerivative.IsPositive())
                {
                    this._previousResult = currentResult;

                    currentResult.IsValid = true;
                    return currentResult;
                }
            }

            currentResult.IsValid = false;
            return currentResult;
        }

        /// <summary>
        /// Asynchronously, this method analyzes the result removing the invalid points.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<AnalyzeResultsResponse> ProcessOperationAsync(AnalyzeResultsRequest request)
        {
            var response = new AnalyzeResultsResponse();
            response.SetSuccessCreated();

            string solutionFileName = this.CreateSolutionFile(request.FileName);
            using (var streamWriter = new StreamWriter(solutionFileName))
            {
                // Step 1 - Writes the file header.
                streamWriter.WriteLine(this._fileHeader);

                // Step 2 - Writes the first line in the file.
                var firstResult = new AnalyzedExperimentalResult(this.ExperimentalResults[0]);
                streamWriter.WriteLine($"{this.ExperimentalResults[0].Time},{this.ExperimentalResults[0].Stress}");

                // Step 3 - Calculates the second result and write its in the file.
                (AnalyzedExperimentalResult secondResult, int secondResultIndex) = this.CalculateSecondResultAndIndex(firstResult, this.ExperimentalResults);
                streamWriter.WriteLine($"{secondResult.Time},{secondResult.Stress}");

                // Step 4 - Calculates the third result and write its in the file.
                // The third result is the previous result that will be used in next step.
                (AnalyzedExperimentalResult previousResult, int previousResultIndex) = this.CalculateThirdResultAndIndex(secondResult, secondResultIndex, this.ExperimentalResults);
                streamWriter.WriteLine($"{previousResult.Time},{previousResult.Stress}");

                // Step 5 - Analyze the experimental results avoiding the points that are invalid.
                foreach (ExperimentalResult experimentalResult in this.ExperimentalResults.Skip(previousResultIndex + 1))
                {
                    // Step 5.1 - Analyzes the experimental result.
                    AnalyzedExperimentalResult analyzedResult = this.AnalyzeExperimentalResult(previousResult, experimentalResult);

                    // Step 5.2 - Writes the result in the file if it is valid.
                    if (analyzedResult.IsValid == true)
                        streamWriter.WriteLine($"{analyzedResult.Time},{analyzedResult.Stress}");

                    // Step 5.3 - Saves the current result to be used in the next iteration.
                    previousResult = analyzedResult;
                }
            }

            // Step 6 - Maps to response.
            response.Data.FileUri = Path.GetDirectoryName(solutionFileName);
            response.Data.FileName = Path.GetFileName(solutionFileName);

            return Task.FromResult(response);
        }

        /// <summary>
        /// Asynchronously, this method validates the <see cref="AnalyzeResultsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<AnalyzeResultsResponse> ValidateOperationAsync(AnalyzeResultsRequest request)
        {
            var response = await base.ValidateOperationAsync(request).ConfigureAwait(false);
            if (response.Success == false)
            {
                return response;
            }

            // Reads the file and add it into a variable to be used in the operation.
            using (var streamReader = new StreamReader(Path.Combine(request.FileUri, request.FileName)))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                this.ExperimentalResults = csvReader.GetRecords<ExperimentalResult>().ToList();
            }

            // The file must be at least a specific number of lines to be possible to execute the operation.
            if (this.ExperimentalResults.Count <= Constants.MinimumFileNumberOfLines)
            {
                response.SetBadRequestError(OperationErrorCode.RequestValidationError, $"The file passed on request must have at least {Constants.MinimumFileNumberOfLines} lines.");

                return response;
            }

            return response;
        }
    }
}
