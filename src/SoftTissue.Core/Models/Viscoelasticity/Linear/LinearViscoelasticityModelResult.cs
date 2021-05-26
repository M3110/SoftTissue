namespace SoftTissue.Core.Models.Viscoelasticity.Linear
{
    /// <summary>
    /// It contains the results for a linear Viscoelastic Model.
    /// </summary>
    public class LinearViscoelasticityModelResult : ViscoelasticModelResult
    {
        /// <summary>
        /// Unit: 
        /// </summary>
        public double CreepCompliance { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double RelaxationFunction { get; set; }

        /// <inheritdoc/>
        public override string ToString(string separator)
            => $"{this.CreepCompliance}" +
            $"{separator}{this.Strain}" +
            $"{separator}{this.RelaxationFunction}" +
            $"{separator}{this.Stress}";

        /// <inheritdoc/>
        public override string ToString() => $"{this.CreepCompliance},{this.Strain},{this.RelaxationFunction},{this.Stress}";
    }
}
