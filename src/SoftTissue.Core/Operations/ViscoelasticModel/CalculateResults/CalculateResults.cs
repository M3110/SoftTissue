using SoftTissue.Core.ConstitutiveEquations;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults
{
    /// <summary>
    /// It is responsible to calculate the results to a generic viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class CalculateResults<TRequest, TRequestData, TInput, TResult> : OperationBase<TRequest, CalculateResultsResponse, CalculateResultsResponseData>, ICalculateResults<TRequest, TRequestData, TInput, TResult>
        where TRequest : CalculateResultsRequest<TRequestData>
        where TRequestData : CalculateResultsRequestData
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
        public CalculateResults(IViscoelasticModel<TInput, TResult> viscoelasticModel)
        {
            this.ViscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method builds a list of inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract List<TInput> BuildInputList(TRequest request);

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual string CreateSolutionFile(TInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                this.TemplateBasePath,
                $"Solution_{input.SoftTissueType}_{DateTime.Now:yyyy-MM-dd}.csv"));

            if (fileInfo.Exists == false)
                return null;

            if (fileInfo.Directory.Exists == false)
                fileInfo.Directory.Create();

            return fileInfo.FullName;
        }

        /// <summary>
        /// Asynchronously, this method calculates the result and writes its into file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="streamWriter"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        protected virtual async Task CalculateAndWriteResultAsync(StreamWriter streamWriter, TInput input, double time)
        {
            TResult results = await this.ViscoelasticModel.CalculateResultsAsync(input, time).ConfigureAwait(false);
            streamWriter.WriteLine($"{results}");
        }

        /// <summary>
        /// Asynchronously, this method executes an analysis to calculate the stress for a quasi-linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<CalculateResultsResponse> ProcessOperationAsync(TRequest request)
        {
            var response = new CalculateResultsResponse { Data = new CalculateResultsResponseData { FilePaths = new List<string>() } };
            response.SetSuccessCreated();

            // Step 1 - Builds the input data list based on request.
            List<TInput> inputList = this.BuildInputList(request);

            var tasks = new List<Task>();

            foreach (TInput input in inputList)
            {
                // Step 2 - Creates the solution file.
                string solutionPath = this.CreateSolutionFile(input);

                if (solutionPath == null)
                    continue;

                // Step 3 - Maps the solution path to response.
                response.Data.FilePaths.Add(solutionPath);

                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        using (var streamWriter = new StreamWriter(solutionPath))
                        {
                            // Step 4 - Writes the header in the file.
                            streamWriter.WriteLine(this.SolutionFileHeader);

                            double time = input.InitialTime;
                            while (time <= input.FinalTime)
                            {
                                // Step 5 - Calculate the results and write it in the file.
                                await this.CalculateAndWriteResultAsync(streamWriter, input, time).ConfigureAwait(false);

                                time += input.TimeStep;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.SetInternalServerError(OperationErrorCode.InternalServerError, $"Error trying to calculate and write the result in file. {ex.Message}.");
                    }
                }));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return response;
        }

        /// <summary>
        /// Asynchronously, this method validates <see cref="TRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<CalculateResultsResponse> ValidateOperationAsync(TRequest request)
        {
            var response = await base.ValidateOperationAsync(request).ConfigureAwait(false);
            if (response.Success == false)
            {
                return response;
            }

            response
                .AddErrorIfNegativeOrZero(request.TimeStep, nameof(request.TimeStep))
                .AddErrorIfNegativeOrZero(request.FinalTime, nameof(request.TimeStep))
                .AddErrorIf(() => request.TimeStep >= request.FinalTime, "Time step must be smaller than the final time.");

            return response;
        }
    }
}
