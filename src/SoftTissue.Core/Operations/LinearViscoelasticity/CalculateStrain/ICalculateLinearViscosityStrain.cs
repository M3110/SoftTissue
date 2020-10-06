using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStrain<TRequest, TInput> : ICalculateResult<TRequest, CalculateStrainResponse, CalculateStrainResponseData, TInput>
        where TRequest : OperationRequestBase
        where TInput : LinearViscoelasticityModelInput, new()
    { }
}