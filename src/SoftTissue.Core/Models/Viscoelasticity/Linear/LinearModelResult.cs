namespace SoftTissue.Core.Models.Viscoelasticity.Linear
{
    /// <summary>
    /// It contains the results for a linear Viscoelastic Model.
    /// </summary>
    public class LinearModelResult : ViscoelasticModelResult
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
        public override string ToString() => $"{this.CreepCompliance},{this.Strain},{this.RelaxationFunction},{this.Stress}";

        /// <summary>
        /// The sequence of the parameters, indicanting the order that it is writen in method <see cref="ToString()"/>
        /// </summary>
        public const string ParametersSequence = "Creep Compliance,Strain,Relaxation Function,Stress";
    }
}
