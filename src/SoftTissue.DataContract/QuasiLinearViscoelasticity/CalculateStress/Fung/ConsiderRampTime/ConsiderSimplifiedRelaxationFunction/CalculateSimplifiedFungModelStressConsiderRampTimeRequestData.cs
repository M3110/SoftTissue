using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelStressConsiderRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateSimplifiedFungModelStressConsiderRampTimeRequestData : CalculateStressConsiderRampTimeRequestData
    {
        /// <summary>
        /// The input data to Simplified Reduced Relaxation Function.
        /// </summary>
        public SimplifiedReducedRelaxationFunctionData SimplifiedReducedRelaxationFunctionData { get; set; }
    }
}
