using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress considering the ramp time and the Simplified Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateSimplifiedFungModelStressConsiderRampTime : 
        ICalculateFungModelStress<
            CalculateSimplifiedFungModelStressConsiderRampTimeRequest, 
            CalculateSimplifiedFungModelStressConsiderRampTimeResponse, 
            CalculateSimplifiedFungModelStressConsiderRampTimeResponseData,
            SimplifiedFungModelInput,
            SimplifiedReducedRelaxationFunctionData,
            SimplifiedFungModelResult> 
    { }
}