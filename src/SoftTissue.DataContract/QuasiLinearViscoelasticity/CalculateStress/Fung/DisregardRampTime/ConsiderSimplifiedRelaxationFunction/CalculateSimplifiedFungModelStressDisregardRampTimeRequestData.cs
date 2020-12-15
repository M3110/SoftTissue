using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelStressDisregardRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateSimplifiedFungModelStressDisregardRampTimeRequestData : CalculateStressDisregardRampTimeRequestData
    {
        /// <summary>
        /// The input data to Simplified Reduced Relaxation Function.
        /// </summary>
        public SimplifiedReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; }
    }
}
