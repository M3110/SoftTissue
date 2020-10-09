using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    public class FungModelInput : QuasiLinearViscoelasticityModelInput
    {
        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        public ReducedRelaxationFunctionData ReducedRelaxationFunctionInput { get; set; }

        public SimplifiedReducedRelaxationFunctionData SimplifiedReducedRelaxationFunctionInput { get; set; }
    }
}
