using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelStress : ICalculateLinearViscosityStress<MaxwellModelInput> { }
}
