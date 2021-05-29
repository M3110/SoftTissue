using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress considering the ramp time and the Simplified Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateSimplifiedFungModelStressConsiderRampTime :
        ICalculateQuasiLinearViscoelasticityStress<
            CalculateSimplifiedFungModelResultsConsiderRampTimeRequest, 
            CalculateSimplifiedFungModelResultsConsiderRampTimeResponse, 
            CalculateSimplifiedFungModelResultsConsiderRampTimeResponseData,
            SimplifiedFungModelInput,
            SimplifiedReducedRelaxationFunctionData,
            SimplifiedFungModelResult> 
    { }
}