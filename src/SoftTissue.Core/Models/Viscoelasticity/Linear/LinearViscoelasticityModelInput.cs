namespace SoftTissue.Core.Models.Viscoelasticity.Linear
{
    /// <summary>
    /// It contains the input data to linear viscoelasticity model.
    /// </summary>
    public class LinearViscoelasticityModelInput : ViscoelasticModelInput
    {
        public double RelaxationTime => this.Viscosity / this.Stiffness;

        public double Stiffness { get; set; }

        public double InitialStress { get; set; }

        public double InitialStrain { get; set; }

        public double Viscosity { get; set; }
    }
}
