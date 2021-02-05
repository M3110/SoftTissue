using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.Experimental.AnalyzeAndExtendResults;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.Experimental
{
    /// <summary>
    /// It is responsible to analyze the experimental results and extend.
    /// </summary>
    public class AnalyzeAndExtendResults : OperationBase<AnalyzeAndExtendResultsRequest, AnalyzeAndExtendResultsResponse, AnalyzeAndExtendResultsResponseData>, IAnalyzeAndExtendResults
    {
        private readonly IDerivative _derivative;

        /// <summary>
        /// The time step to operation.
        /// </summary>
        private double _timeStep;

        /// <summary>
        /// The base path to files.
        /// </summary>
        protected string TemplateBasePath = Path.Combine(Constants.ExperimentalBasePath, "Analyze and extend");

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
                this.TemplateBasePath,
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

            using (StreamReader streamReader = new StreamReader(Path.Combine(request.FileUri, request.FileName)))
            {
                string fileHeader = streamReader.ReadLine();
                string firstLine = streamReader.ReadLine();
                string previousLine = streamReader.ReadLine();

                double previousDerivate = this.CalculateRate(firstLine, previousLine);

                string solutionFileName = this.CreateSolutionFile(request.FileName);
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                    // Writes the file header and the first and second lines into the file.
                    streamWriter.WriteLine($"{fileHeader},First Derivative,Second Derivative");
                    streamWriter.WriteLine($"{firstLine},,");
                    streamWriter.WriteLine($"{previousLine},{previousDerivate},");

                    while (streamReader.EndOfStream == false)
                    {
                        string line = streamReader.ReadLine();
                        double derivative = this.CalculateRate(previousLine, line);

                        double secondDerivative = this._derivative.Calculate(previousDerivate, derivative, this._timeStep);

                        streamWriter.WriteLine($"{line},{derivative},{secondDerivative}");

                        previousLine = line;
                        previousDerivate = derivative;
                    }
                }

                // Maps to response.
                response.Data.FileUri = Path.GetDirectoryName(solutionFileName);
                response.Data.FileName = Path.GetFileName(solutionFileName);
            }

            return Task.FromResult(response);
        }

        public double CalculateRate(string initialLine, string finalLine)
        {
            char separator = ',';

            (double initialTime, double initialStress) = initialLine.ToTimeAndStress(separator);
            (double finalTime, double finalStress) = finalLine.ToTimeAndStress(separator);

            this._timeStep = finalTime - initialTime;

            return this._derivative.Calculate(initialStress, finalStress, this._timeStep);
        }
    }
}
