namespace SoftTissue.Core.Models.Viscoelasticity
{
    public class ViscoelasticModelResult
    {
        public double Strain { get; set; }

        public double ReducedRelaxationFunction { get; set; }

        public double CreepCompliance { get; set; }

        public double Stress { get; set; }
    }
}
