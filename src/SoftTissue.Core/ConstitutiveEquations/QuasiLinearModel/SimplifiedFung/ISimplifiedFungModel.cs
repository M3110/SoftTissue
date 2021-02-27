using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung
{
    /// <summary>
    /// It represents the viscoelastic Fung Model considering the Simplified Relaxation Function.
    /// </summary>
    public interface ISimplifiedFungModel : IQuasiLinearViscoelasticityModel<SimplifiedFungModelInput, SimplifiedFungModelResult, SimplifiedReducedRelaxationFunctionData> { }
}