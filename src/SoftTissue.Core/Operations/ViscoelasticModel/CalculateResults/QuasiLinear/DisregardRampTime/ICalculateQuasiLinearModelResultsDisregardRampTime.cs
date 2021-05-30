using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime
{
    /// <summary>
    /// It is responsible to calculate the results disregarding ramp time to a quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public interface ICalculateQuasiLinearModelResultsDisregardRampTime<TRequest, TRequestData, TInput, TResult, TRelaxationFunction> : ICalculateQuasiLinearModelResults<TRequest, TRequestData, TInput, TResult, TRelaxationFunction>
        where TRequest : CalculateQuasiLinearModelResultsDisregardRampTimeRequest<TRequestData, TRelaxationFunction>
        where TRequestData : CalculateQuasiLinearModelResultsDisregardRampTimeRequestData<TRelaxationFunction>
        where TInput : QuasiLinearModelInput<TRelaxationFunction>
        where TResult : QuasiLinearModelResult, new()
        where TRelaxationFunction : class
    { }
}