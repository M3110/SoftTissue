namespace SoftTissue.Core.Models.Viscoelasticity.Linear
{
    /// <summary>
    /// It contains the input data for linear viscoelasticity model.
    /// </summary>
    public class LinearModelInput : ViscoelasticModelInput
    {
        /// <summary>
        /// Unit: s (second).
        /// </summary>
        public double RelaxationTime => this.Viscosity / this.Stiffness;

        /// <summary>
        /// Unit: 
        /// </summary>
        public double Viscosity { get; set; }

        /// <summary>
        /// Unit: 
        /// </summary>
        public double Stiffness { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public double InitialStrain { get; set; }
    }
}
