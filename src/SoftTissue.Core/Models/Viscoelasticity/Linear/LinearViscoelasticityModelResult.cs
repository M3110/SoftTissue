namespace SoftTissue.Core.Models.Viscoelasticity.Linear
{
    public class LinearViscoelasticityModelResult : ViscoelasticModelResult 
    {
        public double CreepCompliance { get; set; }

        public override string ToString(string separator)
            => $"{this.Strain}" +
            $"{separator}{this.ReducedRelaxationFunction}" +
            $"{separator}{this.CreepCompliance}" +
            $"{separator}{this.Stress}";

        public override string ToString()
            => $"{this.Strain},{this.ReducedRelaxationFunction},{this.CreepCompliance},{this.Stress}";
    }
}
