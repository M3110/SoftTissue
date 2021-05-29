using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearModelStress<TRequest, TRequestData, TInput, TResult> : ICalculateResults<TRequest, TRequestData, TInput, TResult>
        where TRequest : CalculateLinearModelStressRequest<TRequestData>
        where TRequestData : CalculateLinearModelStressRequestData
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    { }
}