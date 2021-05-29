using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelResultsDisregardRampTime operation request.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData : CalculateResultsDisregardRampTimeRequestData
    {
        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Simplified Reduced Relaxation Function.
        /// </summary>
        public SimplifiedReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; } 

        #endregion
    }
}
