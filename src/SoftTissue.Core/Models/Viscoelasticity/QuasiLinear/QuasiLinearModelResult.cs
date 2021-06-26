using System.Text;

namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    /// <summary>
    /// It contains the results for a quasi-linear Viscoelastic Model.
    /// </summary>
    public class QuasiLinearModelResult : ViscoelasticModelResult
    {
        /// <summary>
        /// Unit: /s (Per second).
        /// </summary>
        public double StrainDerivative { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double ElasticResponse { get; set; }

        /// <summary>
        /// Unit: Pa/s (Pascal per second).
        /// </summary>
        public double ElasticResponseDerivative { get; set; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public double ReducedRelaxationFunction { get; set; }

        /// <summary>
        /// Unit: /s (Per second).
        /// </summary>
        public double ReducedRelaxationFunctionDerivative { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double? StressByIntegralDerivative { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double? StressByReducedRelaxationFunctionDerivative { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var result = new StringBuilder($"{this.Time},{this.Strain},{this.StrainDerivative}," +
                $"{this.ReducedRelaxationFunction},{this.ReducedRelaxationFunctionDerivative}," +
                $"{this.ElasticResponse},{this.ElasticResponseDerivative},{this.Stress}");

            if (this.StressByIntegralDerivative != null)
            {
                result.Append($",{this.StressByIntegralDerivative}");
            }

            if (this.StressByReducedRelaxationFunctionDerivative != null)
            {
                result.Append($",{this.StressByReducedRelaxationFunctionDerivative}");
            }

            return result.ToString();
        }

        /// <summary>
        /// The sequence of the parameters, indicanting the order that it is writen in method <see cref="ToString()"/>
        /// </summary>
        public const string ParametersSequence = "Time,Strain,Strain Derivative,Reduced Relaxation Function,Reduced Relaxation Function Derivative,Elastic Response,Elastic Response Derivative,Stress,Stress by Integral,Stress by dG";
    }
}
