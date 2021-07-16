using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear
{
    /// <summary>
    /// It is responsible to calculate the results to a linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    public interface ICalculateLinearModelResults<TRequest, TRequestData, TInput, TResult> : ICalculateResults<TRequest, TRequestData, TInput, TResult>
        where TRequest : CalculateLinearModelResultsRequest<TRequestData>
        where TRequestData : CalculateLinearModelResultsRequestData
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    { }
}