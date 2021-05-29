using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress considering the ramp time and the Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateFungModelStressConsiderRampTime :
        ICalculateQuasiLinearViscoelasticityStress<
            CalculateFungModelResultsConsiderRampTimeRequest,
            CalculateFungModelResultsConsiderRampTimeResponse,
            CalculateFungModelResultsConsiderRampTimeResponseData,
            FungModelInput,
            ReducedRelaxationFunctionData,
            FungModelResult> 
    { }
}