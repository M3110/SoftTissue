using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.Maxwell;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the strain to Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelStrain : ICalculateLinearModelStrain<CalculateMaxwellModelStrainRequest, CalculateMaxwellModelStrainRequestData, MaxwellModelInput, MaxwellModelResult> { }
}
