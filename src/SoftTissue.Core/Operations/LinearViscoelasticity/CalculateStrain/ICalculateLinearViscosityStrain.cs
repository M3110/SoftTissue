using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStrain<TInput> : ICalculateResult<CalculateStrainRequest, CalculateStrainResponse, CalculateStrainResponseData, TInput>
        where TInput : LinearViscoelasticityModelInput, new()
    { }
}