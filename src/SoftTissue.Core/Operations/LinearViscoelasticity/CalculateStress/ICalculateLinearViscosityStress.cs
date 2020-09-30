using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStress<TRequest, TInput> : IOperationBase<TRequest, CalculateStressResponse, CalculateStressResponseData> 
        where TRequest : OperationRequestBase
        where TInput : LinearViscoelasticityModelInput, new()
    { }
}