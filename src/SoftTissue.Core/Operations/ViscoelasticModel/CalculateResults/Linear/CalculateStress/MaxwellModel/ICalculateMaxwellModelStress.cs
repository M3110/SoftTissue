using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress.Maxwell;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStress.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelStress : ICalculateLinearModelStress<CalculateMaxwellModelStressRequest, CalculateMaxwellModelStressRequestData, MaxwellModelInput, MaxwellModelResult> { }
}
