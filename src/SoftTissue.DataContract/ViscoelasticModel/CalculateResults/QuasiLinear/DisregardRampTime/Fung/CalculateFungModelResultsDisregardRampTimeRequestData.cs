using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung
{
    /// <summary>
    /// It represents the 'data' content to CalculateFungModelResultsDisregardRampTime operation request.
    /// </summary>
    public sealed class CalculateFungModelResultsDisregardRampTimeRequestData : CalculateResultsDisregardRampTimeRequestData
    {
        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public ReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; } 

        #endregion
    }
}
