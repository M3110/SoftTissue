using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelStressConsiderRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelStressConsiderRampTimeRequestData : CalculateStressConsiderRampTimeRequestData
    {
        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public SimplifiedReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; }

        #endregion
    }
}
