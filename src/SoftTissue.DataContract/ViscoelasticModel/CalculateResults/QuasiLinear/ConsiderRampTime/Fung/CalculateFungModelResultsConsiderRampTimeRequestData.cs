using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung
{
    /// <summary>
    /// It represents the 'data' content to CalculateFungModelResultsConsiderRampTime operation request.
    /// </summary>
    public sealed class CalculateFungModelResultsConsiderRampTimeRequestData : CalculateResultsConsiderRampTimeRequestData
    {
        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public ReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; }

        #endregion
    }
}
