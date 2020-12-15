namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    public class QuasiLinearViscoelasticityModelResult : ViscoelasticModelResult 
    {
        public double ElasticResponse { get; set; }

        public double StressByIntegralDerivative { get; set; }

        public double StressByReducedRelaxationFunctionDerivative { get; set; }
    }
}
