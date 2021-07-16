namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress.Maxwell
{
    /// <summary>
    /// It represents the 'data' content to CalculateMaxwellModelStress operation request.
    /// </summary>
    public sealed class CalculateMaxwellModelStressRequestData : CalculateLinearModelStressRequestData
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
