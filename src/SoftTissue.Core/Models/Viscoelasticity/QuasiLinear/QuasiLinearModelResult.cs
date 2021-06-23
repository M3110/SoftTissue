using System.Text;

namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    /// <summary>
    /// It contains the results for a quasi-linear Viscoelastic Model.
    /// </summary>
    public class QuasiLinearModelResult : ViscoelasticModelResult
    {
        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double ElasticResponse { get; set; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public double ReducedRelaxationFunction { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double? StressByIntegralDerivative { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double? StressByReducedRelaxationFunctionDerivative { get; set; }

        /// <inheritdoc/>
        public override string ToString(string separator)
        {
            var result = new StringBuilder($"{this.Time}" +
            $"{separator}{this.Strain}" +
            $"{separator}{this.ReducedRelaxationFunction}" +
            $"{separator}{this.ElasticResponse}" +
            $"{separator}{this.Stress}");

            if (this.StressByIntegralDerivative != null)
            {
                result.Append($"{separator}{this.StressByIntegralDerivative}");
            }

            if (this.StressByReducedRelaxationFunctionDerivative != null)
            {
                result.Append($"{separator}{this.StressByReducedRelaxationFunctionDerivative}");
            }

            return result.ToString();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var result = new StringBuilder($"{this.Time},{this.Strain},{this.ReducedRelaxationFunction},{this.ElasticResponse},{this.Stress}");

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
        public const string ParametersSequence = "Time,Strain,Reduced Relaxation Function,Elastic Response,Stress,Stress by Integral,Stress by dG";
    }
}
