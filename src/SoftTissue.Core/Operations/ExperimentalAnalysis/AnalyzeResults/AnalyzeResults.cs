using CsvHelper;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.ExperimentalAnalysis;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults;
using SoftTissue.DataContract.OperationBase;
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
        /// The experimental results obtained in the file passed on request.
        /// </summary>
        private List<ExperimentalResult> _experimentalResults;

        /// <summary>
        /// The header to solution file.
        /// The file is built that way to help when using Origin to plot results, separating the stress and the extrapoleted stress.
        /// </summary>
        private readonly string _fileHeader = "Time,Stress,Time,Derivative,Second Derivative";

        /// <summary>
        /// The base path to files.
        /// </summary>
        private readonly string _templateBasePath = BasePaths.AnalyzeResults;

        private readonly IDerivative _derivative;

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
                $"{Path.GetFileNameWithoutExtension(fileName)}_analyze.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// This method analyzes the experimental results informing if the current result is valid.
        /// </summary>
        /// <param name="previousResult"></param>
        /// <param name="experimentalResult"></param>
        /// <returns></returns>
        public AnalyzedExperimentalResult AnalyzeExperimentalResults(AnalyzedExperimentalResult previousResult, ExperimentalResult experimentalResult)
        {
            // Creates the result based on the experimental result.
            var currentResult = new AnalyzedExperimentalResult(experimentalResult);

            // If the previous result is null, it means that it was the first line.
            if (previousResult == null)
            {
                // Saves the current results to be used to calculate the first and second derivatives at the next step.
                this._previousResult = currentResult;

                currentResult.IsValid = true;
                return currentResult;
            }

            // The stress must decrease over the time, so the previous stress must be less than the current stress to continue to next step.
            if ((currentResult.Stress > previousResult.Stress && previousResult.IsValid == true) || currentResult.Stress > this._previousResult.Stress)
            {
                // Saves the previous results to be used to calculate the first and second derivatives at the next step.
                if (previousResult.IsValid == true)
                    this._previousResult = previousResult;

                currentResult.IsValid = false;
                return currentResult;
            }

            currentResult.Derivative = this._derivative.Calculate(previousResult.Stress, currentResult.Stress, currentResult.Time - previousResult.Time);

            // The previous derivative must exists to continue to next step.
            // The derivative only does not exist at the first result time.
            if (previousResult.Derivative.HasValue == false && previousResult.Time == this._experimentalResults[0].Time)
            {
                // Saves the current results to be used to calculate the first and second derivatives at the next step.
                this._previousResult = currentResult;

                currentResult.IsValid = true;
                return currentResult;
            }

            // The stress derivative must increase over the time, so the previous stress derivative must be greather than the current stress derivative to continue to next step.
            if ((currentResult.Derivative < previousResult.Derivative && previousResult.IsValid == true) || currentResult.Derivative < this._previousResult.Derivative)
            {
                // Saves the previous results to be used to calculate the first and second derivatives at the next step.
                if (previousResult.IsValid == true)
                    this._previousResult = previousResult;

                currentResult.IsValid = false;
                return currentResult;
            }

            if (previousResult.IsValid == true)
            {
                currentResult.IsValid = true;
                currentResult.SecondDerivative = this._derivative.Calculate(previousResult.Derivative.Value, currentResult.Derivative.Value, currentResult.Time - previousResult.Time);
                return currentResult;
            }

            currentResult.IsValid = true;
            currentResult.SecondDerivative = this._derivative.Calculate(this._previousResult.Derivative.Value, currentResult.Derivative.Value, currentResult.Time - this._previousResult.Time);
            return currentResult;
        }

        /// <summary>
        /// Asynchronously, this method analyzes the result removing the invalid points.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<AnalyzeResultsResponse> ProcessOperationAsync(AnalyzeResultsRequest request)
        {
            var response = new AnalyzeResultsResponse { Data = new AnalyzeResultsResponseData() };

            string solutionFileName = this.CreateSolutionFile(request.FileName);
            using (var streamWriter = new StreamWriter(solutionFileName))
            {
                // Step 1 - Writes the file header.
                streamWriter.WriteLine(this._fileHeader);

                // Step 2 - Instantiate the previous results.
                AnalyzedExperimentalResult previousResult = null;

                //// Step 3 - Writes the first line in the file.
                //var firstResult = new AnalyzedExperimentalResult(this._experimentalResults[0]);
                //streamWriter.WriteLine($"{firstResult.Time},{firstResult.Stress},,");
                //
                //// Step 4 - Writes the second line in the file.
                //var secondResult = new AnalyzedExperimentalResult(this._experimentalResults[1]);
                //secondResult.Derivative = this._derivative.Calculate(firstResult.Stress, secondResult.Stress, secondResult.Time - firstResult.Time);
                //streamWriter.WriteLine($"{secondResult.Time},{secondResult.Stress},{secondResult.Derivative},{secondResult.SecondDerivative}");
                //
                //// Step 5 - Writes the third line in the file.
                //var thirdResult = new AnalyzedExperimentalResult(this._experimentalResults[2]);
                //secondResult.Derivative = this._derivative.Calculate(secondResult.Stress, thirdResult.Stress, thirdResult.Time - secondResult.Time);
                //secondResult.SecondDerivative = this._derivative.Calculate(secondResult.Stress, thirdResult.Stress, thirdResult.Time - secondResult.Time);
                //streamWriter.WriteLine($"{secondResult.Time},{secondResult.Stress},{secondResult.Derivative},{secondResult.SecondDerivative}");

                // Step 3 - Analyze the experimental results avoiding the points that are invalid.
                foreach (ExperimentalResult experimentalResult in this._experimentalResults)
                {
                    // Step 3.1 - Analyzes the experimental result.
                    AnalyzedExperimentalResult analyzedResult = this.AnalyzeExperimentalResults(previousResult, experimentalResult);

                    // Step 3.2 - Writes the result in the file if it is valid.
                    if (analyzedResult.IsValid == true)
                        streamWriter.WriteLine($"{analyzedResult.Time},{analyzedResult.Stress},{analyzedResult.Derivative},{analyzedResult.SecondDerivative}");

                    // Step 3.3 - Saves the current result to be used in the next iteration.
                    previousResult = analyzedResult;
                }
            }

            // Step 4 - Maps to response.
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
