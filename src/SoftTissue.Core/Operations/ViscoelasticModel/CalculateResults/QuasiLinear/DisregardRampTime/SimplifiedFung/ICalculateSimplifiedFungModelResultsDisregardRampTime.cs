using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung
{
    /// <summary>
    /// It is responsible to calculate the results disregarding the ramp time to Simplified Fung Model.
    /// </summary>
    public interface ICalculateSimplifiedFungModelResultsDisregardRampTime :
        ICalculateQuasiLinearModelResultsDisregardRampTime<
            CalculateSimplifiedFungModelResultsDisregardRampTimeRequest,
            CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData,
            SimplifiedFungModelInput,
            SimplifiedFungModelResult,
            SimplifiedReducedRelaxationFunctionData>
    { }
}