namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain
{
    /// <summary>
    /// It represents the 'data' content to CalculateLinearModelStrain operation request.
    /// </summary>
    public abstract class CalculateLinearModelStrainRequestData : CalculateResultsRequestData
    {
        /// <summary>
        /// Inital stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }
    }
}
