using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderRelaxationFunction;
using SoftTissue.DataContract.Models;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress considering the ramp time and the Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateFungModelStressConsiderRampTime :
        ICalculateQuasiLinearViscoelasticityStress<
            CalculateFungModelStressConsiderRampTimeRequest,
            CalculateFungModelStressConsiderRampTimeResponse,
            CalculateFungModelStressConsiderRampTimeResponseData,
            FungModelInput,
            ReducedRelaxationFunctionData,
            FungModelResult> 
    { }
}