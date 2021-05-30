using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung
{
    /// <summary>
    /// It represents the Fung Model considering the Simplified Relaxation Function.
    /// </summary>
    public interface ISimplifiedFungModel : IQuasiLinearModel<SimplifiedFungModelInput, SimplifiedFungModelResult, SimplifiedReducedRelaxationFunctionData> { }
}