using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Fung model.
    /// </summary>
    public interface ICalculateFungModelStress : ICalculateQuasiLinearViscoelasticityStress<FungModelInput, FungModelResult> { }
}