using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis.FungModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the stress to Fung model.
    /// </summary>
    public interface ICalculateFungModelStressSentivityAnalysis : ICalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<FungModelInput, FungModelResult> { }
}