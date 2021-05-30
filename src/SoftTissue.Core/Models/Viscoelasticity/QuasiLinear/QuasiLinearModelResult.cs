﻿namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
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
        public double StressByIntegralDerivative { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double StressByReducedRelaxationFunctionDerivative { get; set; }

        /// <inheritdoc/>
        public override string ToString(string separator)
            => $"{this.Time}" +
            $"{separator}{this.Strain}" +
            $"{separator}{this.ReducedRelaxationFunction}" +
            $"{separator}{this.ElasticResponse}" +
            $"{separator}{this.Stress}" +
            $"{separator}{this.StressByIntegralDerivative}" +
            $"{separator}{this.StressByReducedRelaxationFunctionDerivative}";

        /// <inheritdoc/>
        public override string ToString() 
            => $"{this.Time},{this.Strain},{this.ReducedRelaxationFunction},{this.ElasticResponse},{this.Stress},{this.StressByIntegralDerivative},{this.StressByReducedRelaxationFunctionDerivative}";

        /// <summary>
        /// The sequence of the values, indicanting the order that it is writen in method <see cref="ToString()"/>
        /// </summary>
        public const string ValueSequence = "Creep Compliance,Strain,Relaxation Function,Stress";
    }
}