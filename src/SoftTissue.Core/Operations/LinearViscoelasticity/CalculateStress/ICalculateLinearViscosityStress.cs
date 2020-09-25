using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStress : IOperationBase<CalculateStressRequest, CalculateStressResponse, CalculateStressResponseData> { }
}