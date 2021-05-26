using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the strain to Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelStrainSensitivityAnalysis : ICalculateLinearViscosityStrainSensitivityAnalysis<MaxwellModelInput> { }
}
