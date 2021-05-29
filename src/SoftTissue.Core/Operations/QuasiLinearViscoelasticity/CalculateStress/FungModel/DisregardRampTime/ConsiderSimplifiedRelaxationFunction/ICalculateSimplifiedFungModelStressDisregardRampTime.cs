using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress disregarding the ramp time and considering the Simplified Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateSimplifiedFungModelStressDisregardRampTime :
        ICalculateQuasiLinearViscoelasticityStress<
            CalculateSimplifiedFungModelResultsDisregardRampTimeRequest,
            CalculateSimplifiedFungModelResultsDisregardRampTimeResponse,
            CalculateSimplifiedFungModelResultsDisregardRampTimeResponseData,
            SimplifiedFungModelInput,
            SimplifiedReducedRelaxationFunctionData,
            SimplifiedFungModelResult> 
    { }
}