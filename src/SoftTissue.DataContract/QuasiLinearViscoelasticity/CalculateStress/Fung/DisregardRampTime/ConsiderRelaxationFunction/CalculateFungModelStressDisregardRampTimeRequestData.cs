using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateFungModelStressDisregardRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateFungModelStressDisregardRampTimeRequestData : CalculateStressDisregardRampTimeRequestData
    {
        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public ReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; }
    }
}
