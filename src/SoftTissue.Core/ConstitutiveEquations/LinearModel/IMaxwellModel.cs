using SoftTissue.Core.Models;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    /// <summary>
    /// It represents the Maxwell Model to Linear Viscoelastic.
    /// </summary>
    public interface IMaxwellModel : IViscoelasticModel<LinearModelInput> { }
}