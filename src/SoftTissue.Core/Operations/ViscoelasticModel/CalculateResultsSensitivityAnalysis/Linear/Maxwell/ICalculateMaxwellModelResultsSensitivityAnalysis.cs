using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the results with sensitivity analysis for Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelResultsSensitivityAnalysis : ICalculateLinearModelResultsSensitivityAnalysis<CalculateMaxwellModelResultsSensitivityAnalysisRequest, MaxwellModelInput, MaxwellModelResult> { }
}
