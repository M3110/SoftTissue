using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung
{
    public interface ISimplifiedFungModel : IQuasiLinearViscoelasticityModel<SimplifiedFungModelInput, SimplifiedFungModelResult, SimplifiedReducedRelaxationFunctionData> { }
}