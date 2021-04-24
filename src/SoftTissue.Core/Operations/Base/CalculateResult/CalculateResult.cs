using SoftTissue.Core.ConstitutiveEquations;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.CalculateResult;
using System;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.Base.CalculateResult
{
    /// <summary>
    /// It contains methods and parameters shared between operations to calculate a result.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    public abstract class CalculateResult<TRequest, TResponse, TResponseData, TInput> : OperationBase<TRequest, TResponse, TResponseData>, ICalculateResult<TRequest, TResponse, TResponseData, TInput>
        where TRequest : CalculateResultRequest
        where TResponse : CalculateResultResponse<TResponseData>, new()
        where TResponseData : CalculateResultResponseData, new()
        where TInput : ViscoelasticModelInput, new()
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateResult(IViscoelasticModel<TInput> viscoelasticModel) { }

        /// <summary>
        /// The base path to files.
        /// </summary>
        protected abstract string TemplateBasePath { get; }

        /// <summary>
        /// The header to solution file.
        /// </summary>
        protected abstract string SolutionFileHeader { get; }

        /// <summary>
        /// This method builds a list of inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract List<TInput> BuildInputList(TRequest request);

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual string CreateInputFile(TInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                this.TemplateBasePath,
                $"InputData_{input.SoftTissueType}_{DateTime.Now:yyyy-MM-dd}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

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

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }
    }
}
