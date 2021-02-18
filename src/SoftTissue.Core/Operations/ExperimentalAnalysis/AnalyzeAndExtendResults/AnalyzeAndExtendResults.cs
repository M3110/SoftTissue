using CsvHelper;
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
        private readonly IDerivative _derivative;

        /// <summary>
        /// The experimental results obtained in the file passed on request.
        /// </summary>
        private List<ExperimentalResult> _experimentalResults;

        /// <summary>
        /// The base path to files.
        /// </summary>
        private readonly string _templateBasePath = Path.Combine(Constants.ExperimentalBasePath, "Analyze and extend");

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
                ExperimentalResult firstResult = this._experimentalResults[0];
                csvWriter.WriteLine(new AnalyzedExperimentalResult(firstResult));

                // Step 3 - Writes the second line. It just have the derivative, because to calculate the second derivative needs two derivative previously calculated.
                ExperimentalResult secondResult = this._experimentalResults[1];

                var previousAnalyzedResult = new AnalyzedExperimentalResult(secondResult);
                previousAnalyzedResult.Derivative = this._derivative.Calculate(firstResult.Stress, secondResult.Stress, secondResult.Time - firstResult.Time);

                csvWriter.WriteLine(previousAnalyzedResult);

                // Step 4 - Analyze the experimental results.
                // Here is necessary to skip 2 lines, beacause that lines was already analyzed.
                foreach (ExperimentalResult experimentalResult in this._experimentalResults.Skip(2))
                {
                    // Step 4.1 - Converts the experimental result.
                    var analyzedResult = new AnalyzedExperimentalResult(experimentalResult);

                    // Step 4.2 - Calculates the step time.
                    double stepTime = analyzedResult.Time - previousAnalyzedResult.Time;

                    // Step 4.3 - Calculates the derivative and second derivative.
                    analyzedResult.Derivative = this._derivative.Calculate(previousAnalyzedResult.Stress, analyzedResult.Stress, stepTime);
                    analyzedResult.SecondDerivative = this._derivative.Calculate(previousAnalyzedResult.Derivative.Value, analyzedResult.Derivative.Value, stepTime);

                    // Step 4.4 - Writes the result in the file.
                    csvWriter.WriteLine(analyzedResult);

                    // Step 4.5 - Saves the current result to be used in the next iteration.
                    previousAnalyzedResult = analyzedResult;
                }
            }

            // Step 5 - Maps to response.
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

            // Reads the file and add it into a variable to be used in the operation.
            using (var streamReader = new StreamReader(Path.Combine(request.FileUri, request.FileName)))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                this._experimentalResults = csvReader.GetRecords<ExperimentalResult>().ToList();
            }

            // The file must be at least a specific number of lines to be possible to execute the operation.
            if (this._experimentalResults.Count <= Constants.MinimumFileNumberOfLines)
            {
                response.AddError(OperationErrorCode.InternalServerError, $"The file passed on request must have at least {Constants.MinimumFileNumberOfLines} lines.", HttpStatusCode.BadRequest);
                return response;
            }

            return response;
        }
    }
}
