using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStrain : IOperationBase<CalculateStrainRequest, CalculateStrainResponse, CalculateStrainResponseData> { }
}