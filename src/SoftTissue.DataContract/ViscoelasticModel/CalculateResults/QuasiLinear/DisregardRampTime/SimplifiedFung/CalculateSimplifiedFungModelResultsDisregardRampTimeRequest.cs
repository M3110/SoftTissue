using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the request content to CalculateSimplifiedFungModelResultsDisregardRampTime operation.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsDisregardRampTimeRequest : CalculateQuasiLinearModelResultsDisregardRampTimeRequest<CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData, SimplifiedReducedRelaxationFunctionData> { }
}
