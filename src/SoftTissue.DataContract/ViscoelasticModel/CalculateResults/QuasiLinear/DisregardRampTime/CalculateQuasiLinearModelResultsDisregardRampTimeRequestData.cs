namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateResultsDisregardRampTime operation request.
    /// </summary>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class CalculateQuasiLinearModelResultsDisregardRampTimeRequestData<TRelaxationFunction> : CalculateResultsRequestData
    {
        #region Strain parameter

        /// <summary>
        /// The maximum strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double Strain { get; set; }

        #endregion

        #region Elastic Response parameter

        /// <summary>
        /// The initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }

        #endregion

        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public TRelaxationFunction ReducedRelaxationFunctionData { get; set; }

        #endregion
    }
}
