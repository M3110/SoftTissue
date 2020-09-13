namespace SoftTissue.Core.Models
{
    /// <summary>
    /// It contains the input data to Maxwell Model.
    /// </summary>
    public class LinearViscoelasticityModelInput : ViscoelasticModelInput
    {
        public double RelaxationTime
        {
            get
            {
                double value = Viscosity / Stiffness;
                return value;
            }
        }

        public double Stiffness { get; set; }

        public double InitialStress { get; set; }

        public double InitialStrain { get; set; }

        public double Viscosity { get; set; }
    }
}
