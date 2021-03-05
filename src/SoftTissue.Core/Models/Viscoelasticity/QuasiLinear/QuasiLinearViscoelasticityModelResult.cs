namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    /// <summary>
    /// It contains the results to Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class QuasiLinearViscoelasticityModelResult : ViscoelasticModelResult
    {
        /// <summary>
        /// The value to elastic response.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double ElasticResponse { get; set; }

        /// <summary>
        /// The value to stress calculated using the equation 8.b from Fung, at page 279.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double StressByIntegralDerivative { get; set; }

        /// <summary>
        /// The value to stress calculated using the equation 8.a from Fung, at page 279.
        /// Unit: Pa (Pascal).
        /// </summary>
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
