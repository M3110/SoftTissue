using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.Core.Operations.Base.CalculateResult
{
    public interface ICalculateResult<TRequest, TResponse, TResponseData, TInput> : IOperationBase<TRequest, TResponse, TResponseData>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase<TResponseData>, new()
        where TResponseData : OperationResponseData, new()
        where TInput : ViscoelasticModelInput, new()
    {
        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateSolutionFile(TInput input);

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateInputFile(TInput input);

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TInput> BuildInputList(TRequest request);
    }
}