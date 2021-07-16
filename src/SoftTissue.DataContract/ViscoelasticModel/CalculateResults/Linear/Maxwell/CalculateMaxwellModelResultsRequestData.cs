namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.Maxwell
{
    /// <summary>
    /// It represents the 'data' content to CalculateMaxwellModelResults operation request.
    /// </summary>
    public sealed class CalculateMaxwellModelResultsRequestData : CalculateLinearModelResultsRequestData
    {
        /// <summary>
        /// Stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double Stiffness { get; set; }

        /// <summary>
        /// Viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public double Viscosity { get; set; }
    }
}
