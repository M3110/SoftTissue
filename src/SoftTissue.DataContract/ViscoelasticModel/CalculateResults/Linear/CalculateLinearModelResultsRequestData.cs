namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear
{
    /// <summary>
    /// It represents the 'data' content to CalculateLinearModelResults operation request.
    /// </summary>
    public abstract class CalculateLinearModelResultsRequestData : CalculateResultsRequestData
    {
        /// <summary>
        /// Initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }

        /// <summary>
        /// Inital strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double InitialStrain { get; set; }
    }
}
