using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime
{
    /// <summary>
    /// It is responsible to calculate the results considering ramp time for a quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public interface ICalculateQuasiLinearModelResultsConsiderRampTime<TRequest, TRequestData, TInput, TResult, TRelaxationFunction> : ICalculateQuasiLinearModelResults<TRequest, TRequestData, TInput, TResult, TRelaxationFunction>
        where TRequest : CalculateQuasiLinearModelResultsConsiderRampTimeRequest<TRequestData, TRelaxationFunction>
        where TRequestData : CalculateQuasiLinearModelResultsConsiderRampTimeRequestData<TRelaxationFunction>
        where TInput : QuasiLinearModelInput<TRelaxationFunction>
        where TResult : QuasiLinearModelResult, new()
        where TRelaxationFunction : class
    { }
}