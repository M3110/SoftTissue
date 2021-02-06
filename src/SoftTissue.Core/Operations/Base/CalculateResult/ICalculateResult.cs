using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.Core.Operations.Base.CalculateResult
{
    /// <summary>
    /// It contains methods and parameters shared between operations to calculate a result.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    public interface ICalculateResult<TRequest, TResponse, TResponseData, TInput> : IOperationBase<TRequest, TResponse, TResponseData>
        where TRequest : CalculateResultRequest
        where TResponse : CalculateResultResponse<TResponseData>, new()
        where TResponseData : CalculateResultResponseData, new()
        where TInput : ViscoelasticModelInput, new()
    {
        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TInput> BuildInputList(TRequest request);

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateInputFile(TInput input);

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateSolutionFile(TInput input);
    }
}