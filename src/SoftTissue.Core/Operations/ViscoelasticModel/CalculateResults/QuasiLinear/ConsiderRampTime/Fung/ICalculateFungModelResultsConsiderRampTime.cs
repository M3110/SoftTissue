using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung
{
    /// <summary>
    /// It is responsible to calculate the results considering the ramp time for Fung Model.
    /// </summary>
    public interface ICalculateFungModelResultsConsiderRampTime :
        ICalculateQuasiLinearModelResultsConsiderRampTime<
            CalculateFungModelResultsConsiderRampTimeRequest,
            CalculateFungModelResultsConsiderRampTimeRequestData,
            FungModelInput,
            FungModelResult,
            ReducedRelaxationFunctionData>
    { }
}