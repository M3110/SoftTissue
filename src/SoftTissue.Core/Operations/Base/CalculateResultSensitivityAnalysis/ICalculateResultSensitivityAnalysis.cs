using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;
using System.Text;

namespace SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis
{
    /// <summary>
    /// It contains methods and parameters shared between operations to calculate a result.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    public interface ICalculateResultSensitivityAnalysis<TRequest, TResponse, TResponseData, TInput> : IOperationBase<TRequest, TResponse, TResponseData>
        where TRequest : CalculateResultRequest
        where TResponse : CalculateResultResponse<TResponseData>, new()
        where TResponseData : CalculateResultResponseData, new()
        where TInput : ViscoelasticModelInput, new()
    {
        /// <summary>
        /// This method builds an input list based on request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TInput> BuildInputList(TRequest request);

        /// <summary>
        /// This method creates the file header.
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        StringBuilder CreteFileHeader(List<TInput> inputList);

        /// <summary>
        /// This method calculates the results and writes them in a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="initialTime"></param>
        /// <param name="finalTime"></param>
        /// <param name="timeStep"></param>
        void CalculateAndWriteResults(List<TInput> inputList, double initialTime, double finalTime, double timeStep);

        /// <summary>
        /// This method creates the path to save all inputs on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateInputFile();

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateSolutionFile(string functionName);
    }
}