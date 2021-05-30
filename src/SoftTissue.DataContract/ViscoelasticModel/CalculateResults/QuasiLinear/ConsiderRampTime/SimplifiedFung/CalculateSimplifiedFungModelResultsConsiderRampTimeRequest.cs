using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the request content to CalculateSimplifiedFungModelResultsConsiderRampTime operation.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsConsiderRampTimeRequest : CalculateQuasiLinearModelResultsConsiderRampTimeRequest<CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData, SimplifiedReducedRelaxationFunctionData> { }
}
