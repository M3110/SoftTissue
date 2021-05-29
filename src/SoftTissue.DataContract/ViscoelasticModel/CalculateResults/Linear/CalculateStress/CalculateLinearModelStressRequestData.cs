namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress
{
    /// <summary>
    /// It represents the 'data' content to CalculateLinearModelStress operation request.
    /// </summary>
    public abstract class CalculateLinearModelStressRequestData : CalculateResultsRequestData
    {
        /// <summary>
        /// Inital strain.
        /// Dimensionless.
        /// </summary>
        public double InitialStrain { get; set; }
    }
}
