namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    public class FungModelResult : QuasiLinearViscoelasticityModelResult
    {
        public double StressWithIntegralDerivative { get; set; }

        public double StressWithElasticResponseDerivative { get; set; }
        
        public double StressWithReducedRelaxationFunctionDerivative { get; set; }
    }
}
