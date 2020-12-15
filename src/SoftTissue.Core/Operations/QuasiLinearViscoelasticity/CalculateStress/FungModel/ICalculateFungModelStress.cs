using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Fung model.
    /// </summary>
    public interface ICalculateFungModelStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult> : ICalculateQuasiLinearViscoelasticityStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase<TResponseData>, new()
        where TResponseData : OperationResponseData, new()
        where TInput : QuasiLinearViscoelasticityModelInput<TReducedRelaxation>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    { }
}