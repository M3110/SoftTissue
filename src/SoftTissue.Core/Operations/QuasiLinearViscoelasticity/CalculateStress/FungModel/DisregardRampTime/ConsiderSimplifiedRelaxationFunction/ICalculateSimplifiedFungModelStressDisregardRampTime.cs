using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress disregarding the ramp time and considering the Simplified Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateSimplifiedFungModelStressDisregardRampTime :
        ICalculateQuasiLinearViscoelasticityStress<
            CalculateSimplifiedFungModelStressDisregardRampTimeRequest,
            CalculateSimplifiedFungModelStressDisregardRampTimeResponse,
            CalculateSimplifiedFungModelStressDisregardRampTimeResponseData,
            SimplifiedFungModelInput,
            SimplifiedReducedRelaxationFunctionData,
            SimplifiedFungModelResult> 
    { }
}