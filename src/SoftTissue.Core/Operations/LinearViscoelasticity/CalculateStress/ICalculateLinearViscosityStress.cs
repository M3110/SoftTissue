using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStress<TRequest, TInput> : ICalculateResult<TRequest, CalculateStressResponse, CalculateStressResponseData, TInput>
        where TRequest : OperationRequestBase
        where TInput : LinearViscoelasticityModelInput, new()
    { }
}