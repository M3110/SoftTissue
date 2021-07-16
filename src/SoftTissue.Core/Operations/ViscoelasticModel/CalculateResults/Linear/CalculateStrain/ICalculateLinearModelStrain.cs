using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStrain
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearModelStrain<TRequest, TRequestData, TInput, TResult> : ICalculateResults<TRequest, TRequestData, TInput, TResult>
        where TRequest : CalculateLinearModelStrainRequest<TRequestData>
        where TRequestData : CalculateLinearModelStrainRequestData
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    { }
}