using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateFungModelStressConsiderRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateFungModelStressConsiderRampTimeRequestData : CalculateStressConsiderRampTimeRequestData
    {
        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public ReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; }
    }
}
