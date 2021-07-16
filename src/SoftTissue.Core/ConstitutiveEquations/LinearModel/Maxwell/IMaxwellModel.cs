using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell
{
    /// <summary>
    /// It represents the Maxwell Model.
    /// </summary>
    public interface IMaxwellModel : ILinearModel<MaxwellModelInput, MaxwellModelResult> { }
}