using SoftTissue.Core.ConstitutiveEquations;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoftTissue.Core.Operations.Base.CalculateResult
{
    /// <summary>
    /// It contains methods and parameters shared between operations to calculate a result.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    public abstract class CalculateResultSensitivityAnalysis<TRequest, TResponse, TResponseData, TInput> : OperationBase<TRequest, TResponse, TResponseData>, ICalculateResultSensitivityAnalysis<TRequest, TResponse, TResponseData, TInput>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase<TResponseData>, new()
        where TResponseData : OperationResponseData, new()
        where TInput : ViscoelasticModelInput, new()
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateResultSensitivityAnalysis(IViscoelasticModel<TInput> viscoelasticModel) { }

        /// <summary>
        /// The base path to files.
        /// </summary>
        protected abstract string TemplateBasePath { get; }

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
                this.TemplateBasePath,
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
                this.TemplateBasePath,
                $"Solution_{functionName}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }
    }
}
