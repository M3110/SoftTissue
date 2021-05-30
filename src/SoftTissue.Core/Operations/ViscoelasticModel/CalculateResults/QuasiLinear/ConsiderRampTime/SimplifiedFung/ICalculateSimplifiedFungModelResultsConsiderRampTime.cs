using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung
{
    /// <summary>
    /// It is responsible to calculate the results considering the ramp time to Simplified Fung Model.
    /// </summary>
    public interface ICalculateSimplifiedFungModelResultsConsiderRampTime :
        ICalculateQuasiLinearModelResultsConsiderRampTime<
            CalculateSimplifiedFungModelResultsConsiderRampTimeRequest,
            CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData,
            SimplifiedFungModelInput,
            SimplifiedFungModelResult,
            SimplifiedReducedRelaxationFunctionData>
    { }
}