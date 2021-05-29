using SoftTissue.Core.ConstitutiveEquations;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis
{
    /// <summary>
    /// It contains methods and parameters shared between operations to calculate a result .
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
        public CalculateResultsSensitivityAnalysis(IViscoelasticModel<TInput, TResult> viscoelasticModel) { }

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract List<TInput> BuildInputList(TRequest request);

        /// <summary>
        /// This method calculates the results and writes them in a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="initialTime"></param>
        /// <param name="finalTime"></param>
        /// <param name="timeStep"></param>
        public abstract void CalculateAndWriteResults(List<TInput> inputList, double initialTime, double finalTime, double timeStep);

        /// <summary>
        /// This method creates the file header.
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public virtual StringBuilder CreteFileHeader(List<TInput> inputList)
        {
            StringBuilder fileHeader = new StringBuilder("Time;");

            for (int i = 0; i < inputList.Count; i++)
            {
                fileHeader.Append($"Input {i};");
            }

            return fileHeader;
        }

        /// <summary>
        /// This method creates the path to save all inputs on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual string CreateInputFile()
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"InputData.csv"));

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
        public virtual string CreateSolutionFile(string functionName)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Solution_{functionName}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }
    }
}
