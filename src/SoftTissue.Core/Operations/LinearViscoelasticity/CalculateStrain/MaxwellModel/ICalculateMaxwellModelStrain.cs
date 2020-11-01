using SoftTissue.Core.Models.Viscoelasticity.Linear;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the strain to Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelStrain : ICalculateLinearViscosityStrain<MaxwellModelInput> { }
}
