using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Models.Viscoelasticity
{
    public class QuasiLinearViscoelasticityModelInput : ViscoelasticModelInput
    {
        public double StrainRate { get; set; }

        public double MaximumStrain { get; set; }

        public double RampTime => this.MaximumStrain / this.StrainRate;

        /// <summary>
        /// Constant A.
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// Constant B.
        /// </summary>
        public double ElasticPowerConstant { get; set; }

        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        public ReducedRelaxationFunctionData ReducedRelaxationFunctionInput { get; set; }

        public SimplifiedReducedRelaxationFunctionData SimplifiedReducedRelaxationFunctionInput { get; set; }
    }
}