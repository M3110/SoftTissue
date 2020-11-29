using SoftTissue.Core.Models.Viscoelasticity.Linear;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStressSensitivityAnalysis.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the stress to Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelStressSensitivityAnalysis : ICalculateLinearViscosityStressSensitivityAnalysis<MaxwellModelInput> { }
}
