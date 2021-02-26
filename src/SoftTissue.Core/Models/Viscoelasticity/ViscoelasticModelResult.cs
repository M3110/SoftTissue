namespace SoftTissue.Core.Models.Viscoelasticity
{
    public class ViscoelasticModelResult
    {
        public double Time { get; set; }

        public double Strain { get; set; }

        public double ReducedRelaxationFunction { get; set; }

        public double Stress { get; set; }

        public virtual string ToString(string separator)
            => $"{this.Time}" +
            $"{separator}{this.Strain}" +
            $"{separator}{this.ReducedRelaxationFunction}" +
            $"{separator}{this.Stress}";

        public override string ToString()
            => $"{this.Strain},{this.ReducedRelaxationFunction},{this.Stress}";
    }
}
