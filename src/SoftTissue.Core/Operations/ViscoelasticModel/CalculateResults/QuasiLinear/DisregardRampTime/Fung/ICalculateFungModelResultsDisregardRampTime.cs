using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung
{
    /// <summary>
    /// It is responsible to calculate the results disregarding the ramp time to Fung Model.
    /// </summary>
    public interface ICalculateFungModelResultsDisregardRampTime :
        ICalculateQuasiLinearModelResultsDisregardRampTime<
            CalculateFungModelResultsDisregardRampTimeRequest,
            CalculateFungModelResultsDisregardRampTimeRequestData,
            FungModelInput,
            FungModelResult,
            ReducedRelaxationFunctionData>
    { }
}