namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateResultsDisregardRampTime operation request.
    /// </summary>
    public abstract class CalculateResultsDisregardRampTimeRequestData : CalculateResultsRequestData
    {
        /// <summary>
        /// The maximum strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double Strain { get; set; }

        /// <summary>
        /// The initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }
    }
}
