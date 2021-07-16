using SoftTissue.Core.ConstitutiveEquations;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis
{
    /// <summary>
    /// It is responsible to calculate the results with sensitivity analysis for a generic viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class CalculateResultsSensitivityAnalysis<TRequest, TInput, TResult> : OperationBase<TRequest, CalculateResultsSensitivityAnalysisResponse, CalculateResultsSensitivityAnalysisResponseData>, ICalculateResultsSensitivityAnalysis<TRequest, TInput, TResult>
        where TRequest : CalculateResultsSensitivityAnalysisRequest
        where TInput : ViscoelasticModelInput
        where TResult : ViscoelasticModelResult, new()
    {
        /// <summary>
        /// The base path to files.
        /// </summary>
        protected abstract string TemplateBasePath { get; }

        /// <summary>
        /// The header to solution file.
        /// </summary>
        protected abstract string SolutionFileHeader { get; }

        protected IViscoelasticModel<TInput, TResult> ViscoelasticModel { get; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateResultsSensitivityAnalysis(IViscoelasticModel<TInput, TResult> viscoelasticModel)
        {
            this.ViscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// Asynchronously, this method executes an analysis to calculate the stress for a quasi-linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<CalculateResultsSensitivityAnalysisResponse> ProcessOperationAsync(TRequest request)
        {
            var response = new CalculateResultsSensitivityAnalysisResponse();
            response.SetSuccessCreated();

            // Step 1 - Builds the input data list based on request.
            List<TInput> inputList = this.BuildInputList(request);

            // Step 2 - Builds the tasks that have to be executed.
            List<Task> tasks = this.BuildTasks(inputList, request.TimeStep, request.FinalTime);

            await Task.WhenAll(tasks);

            return response;
        }

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract List<TInput> BuildInputList(TRequest request);

        /// <summary>
        /// This method builds the tasks that have to be executed.
        /// The aim of this method is run asynchronously the sensitivity analysis.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <returns></returns>
        protected virtual List<Task> BuildTasks(List<TInput> inputList, double timeStep, double finalTime)
        {
            return new List<Task>
            {
                Task.Run(async () => await this.RunSensitivityAnalysis(inputList, timeStep, finalTime, "Stress", this.ViscoelasticModel.CalculateStressAsync).ConfigureAwait(false)),
                Task.Run(async () => await this.RunSensitivityAnalysis(inputList, timeStep, finalTime, "Strain", this.ViscoelasticModel.CalculateStrainAsync).ConfigureAwait(false))
            };
        }

        /// <summary>
        /// Asynchronously, this method runs a sensitivity analysis for a specific function.
        /// It must be used when adding a sensitivity analysis for any function.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="functionName"></param>
        /// <param name="task"></param>
        protected async Task RunSensitivityAnalysis(List<TInput> inputList, double timeStep, double finalTime, string functionName, Func<TInput, double, Task<double>> task)
        {
            // Step 3 - Creates the solution file based on function.
            using (StreamWriter streamWriter = new StreamWriter(this.CreateSolutionFile(functionName)))
            {
                // Step 4 - Creates the file header and writes it in the file.
                StringBuilder fileHeader = this.CreteFileHeader(inputList);
                streamWriter.WriteLine(fileHeader);

                // Here is consider that the time will always begin in zero.
                double time = 0;

                while (time <= finalTime)
                {
                    // Step 5 - Creates the string that contain the results beginning with the time.
                    StringBuilder results = new StringBuilder($"{time},");

                    foreach (var input in inputList)
                    {
                        // Step 6 - Calculates the result and concatenates with the previous ones.
                        double result = await task(input, time).ConfigureAwait(false);
                        results.Append($"{result},");
                    }

                    // Step 7 - Writes the results in file.
                    streamWriter.WriteLine(results);

                    // Step 8 - Iterate the time.
                    time += timeStep;
                }
            }
        }

        /// <summary>
        /// This method runs a sensitivity analysis for a specific function.
        /// It must be used when adding a sensitivity analysis for any function.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="functionName"></param>
        /// <param name="action"></param>
        protected void RunSensitivityAnalysis(List<TInput> inputList, double timeStep, double finalTime, string functionName, Func<TInput, double, double> action)
        {
            // Step 3 - Creates the solution file based on function.
            using (StreamWriter streamWriter = new StreamWriter(this.CreateSolutionFile(functionName)))
            {
                // Step 4 - Creates the file header and writes it in the file.
                StringBuilder fileHeader = this.CreteFileHeader(inputList);
                streamWriter.WriteLine(fileHeader);

                // Here is consider that the time will always begin in zero.
                double time = 0;

                while (time <= finalTime)
                {
                    // Step 5 - Creates the string that contain the results beginning with the time.
                    StringBuilder results = new StringBuilder($"{time},");

                    foreach (var input in inputList)
                    {
                        // Step 6 - Calculates the result and concatenates with the previous ones.
                        double result = action(input, time);
                        results.Append($"{result},");
                    }

                    // Step 7 - Writes the results in file.
                    streamWriter.WriteLine(results);

                    // Step 8 - Iterate the time.
                    time += timeStep;
                }
            }
        }

        /// <summary>
        /// This method creates the file header.
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        protected virtual StringBuilder CreteFileHeader(List<TInput> inputList)
        {
            StringBuilder fileHeader = new StringBuilder("Time,");

            for (int i = 0; i < inputList.Count; i++)
            {
                fileHeader.Append($"Input {i},");
            }

            return fileHeader;
        }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        protected virtual string CreateSolutionFile(string functionName)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Solution_{functionName}_{DateTime.Now:yyyy-MM-dd}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// Asynchronously, this method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<CalculateResultsSensitivityAnalysisResponse> ValidateOperationAsync(TRequest request)
        {
            var response = await base.ValidateOperationAsync(request).ConfigureAwait(false);
            if (response.Success == false)
            {
                return response;
            }

            response
                .AddErrorIfNegativeOrZero(request.TimeStep, nameof(request.TimeStep))
                .AddErrorIfNegativeOrZero(request.FinalTime, nameof(request.FinalTime))
                .AddErrorIf(() => request.TimeStep >= request.FinalTime, "Time step must be smaller than the final time.");

            return response;
        }
    }
}
