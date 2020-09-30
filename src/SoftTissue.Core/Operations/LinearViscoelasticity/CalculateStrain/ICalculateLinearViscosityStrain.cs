using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStrain<TRequest, TInput> : IOperationBase<TRequest, CalculateStrainResponse, CalculateStrainResponseData>
        where TRequest : OperationRequestBase
        where TInput : LinearViscoelasticityModelInput, new()
    { }
}