using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults
{
    /// <summary>
    /// It contains methods and parameters shared between operations to calculate a result.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ICalculateResults<TRequest, TRequestData, TInput, TResult> : IOperationBase<TRequest, CalculateResultsResponse, CalculateResultsResponseData>
        where TRequest : CalculateResultsRequest<TRequestData>
        where TRequestData : CalculateResultsRequestData
        where TInput : ViscoelasticModelInput
        where TResult : ViscoelasticModelResult, new()
    { }
}