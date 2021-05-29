using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelResultsConsiderRampTime operation request.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData : CalculateResultsConsiderRampTimeRequestData
    {
        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public SimplifiedReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; }

        #endregion
    }
}
