namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    public class QuasiLinearViscoelasticityModelResult : ViscoelasticModelResult
    {
        public double ElasticResponse { get; set; }

        public double StressByIntegralDerivative { get; set; }

        public double StressByReducedRelaxationFunctionDerivative { get; set; }

        public override string ToString(string separator)
            => $"{this.Strain}" +
            $"{separator}{this.ReducedRelaxationFunction}" +
            $"{separator}{this.ElasticResponse}" +
            $"{separator}{this.Stress}" +
            $"{separator}{this.StressByIntegralDerivative}" +
            $"{separator}{this.StressByReducedRelaxationFunctionDerivative}";

        public override string ToString()
            => $"{this.Strain}" +
            $",{this.ReducedRelaxationFunction}" +
            $",{this.ElasticResponse}" +
            $",{this.Stress}" +
            $",{this.StressByIntegralDerivative}" +
            $",{this.StressByReducedRelaxationFunctionDerivative}";
    }
}
