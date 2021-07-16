using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung
{
    /// <summary>
    /// It represents the Simplified Fung Model.
    /// The simplified Fung Model is characterized by using the Simplified Reduced Relaxation Function.
    /// </summary>
    public interface ISimplifiedFungModel : IQuasiLinearModel<SimplifiedFungModelInput, SimplifiedFungModelResult, SimplifiedReducedRelaxationFunctionData> { }
}