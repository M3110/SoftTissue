namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.Maxwell
{
    /// <summary>
    /// It represents the 'data' content to CalculateMaxwellModelStrain operation request.
    /// </summary>
    public sealed class CalculateMaxwellModelStrainRequestData : CalculateLinearModelStrainRequestData
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
