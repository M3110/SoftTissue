using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress disregarding the ramp time and considering the Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateFungModelStressDisregardRampTime : 
        ICalculateQuasiLinearViscoelasticityStress<
            CalculateFungModelResultsDisregardRampTimeRequest,
            CalculateFungModelResultsDisregardRampTimeResponse,
            CalculateFungModelResultsDisregardRampTimeResponseData,
            FungModelInput,
            ReducedRelaxationFunctionData,
            FungModelResult>
    { }
}