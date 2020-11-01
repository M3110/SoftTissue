using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the stress to Fung model.
    /// </summary>
    public interface ICalculateFungModelStressSentivityAnalysis : ICalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<FungModelInput, FungModelResult> { }
}