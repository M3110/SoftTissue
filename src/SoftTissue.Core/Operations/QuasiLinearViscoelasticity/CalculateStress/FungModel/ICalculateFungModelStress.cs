using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Fung model.
    /// </summary>
    public interface ICalculateFungModelStress : ICalculateQuasiLinearViscoelasticityStress<CalculateQuasiLinearViscoelasticityStressRequest, FungModelInput, FungModelResult> { }
}