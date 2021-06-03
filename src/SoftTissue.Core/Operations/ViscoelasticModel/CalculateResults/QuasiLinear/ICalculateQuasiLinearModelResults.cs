using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear
{
    /// <summary>
    /// It is responsible to calculate the results for a quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public interface ICalculateQuasiLinearModelResults<TRequest, TRequestData, TInput, TResult, TRelaxationFunction> : ICalculateResults<TRequest, TRequestData, TInput, TResult>
        where TRequest : CalculateResultsRequest<TRequestData>
        where TRequestData : CalculateResultsRequestData
        where TInput : QuasiLinearModelInput<TRelaxationFunction>
        where TResult : QuasiLinearModelResult, new()
        where TRelaxationFunction : class
    { }
}