using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderRelaxationFunction;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress disregarding the ramp time and considering the Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateFungModelStressDisregardRampTime : ICalculateFungModelStress<
            CalculateFungModelStressDisregardRampTimeRequest,
            CalculateFungModelStressDisregardRampTimeResponse,
            CalculateFungModelStressDisregardRampTimeResponseData,
            FungModelInput,
            ReducedRelaxationFunctionData,
            FungModelResult>
    { }
}