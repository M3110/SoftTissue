using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the strain to Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelStrainSensitivityAnalysis : ICalculateLinearViscosityStrain<CalculateStrainSensitivityAnalysisRequest, MaxwellModelInput> { }
}
