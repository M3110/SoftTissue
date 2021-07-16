using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung
{
    /// <summary>
    /// It represents the request content to CalculateFungModelResultsDisregardRampTime operation.
    /// </summary>
    public sealed class CalculateFungModelResultsDisregardRampTimeRequest : CalculateQuasiLinearModelResultsDisregardRampTimeRequest<CalculateFungModelResultsDisregardRampTimeRequestData, ReducedRelaxationFunctionData> { }
}
