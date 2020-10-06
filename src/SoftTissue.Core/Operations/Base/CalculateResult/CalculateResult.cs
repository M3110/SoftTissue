using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.Core.Operations.Base.CalculateResult
{
    public abstract class CalculateResult<TRequest, TResponse, TResponseData, TInput> : OperationBase<TRequest, TResponse, TResponseData>, ICalculateResult<TRequest, TResponse, TResponseData, TInput>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase<TResponseData>, new()
        where TResponseData : OperationResponseData, new()
        where TInput : ViscoelasticModelInput, new()
    {
        /// <summary>
        /// The header to solution file.
        /// </summary>
        public abstract string SolutionFileHeader { get; }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract string CreateSolutionFile(TInput input);

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract string CreateInputDataFile(TInput input);

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract List<TInput> BuildInputList(TRequest request);
    }
}
