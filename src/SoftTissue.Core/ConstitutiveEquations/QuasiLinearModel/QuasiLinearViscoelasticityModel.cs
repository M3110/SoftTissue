using SoftTissue.Core.Models;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public abstract class QuasiLinearViscoelasticityModel : ViscoelasticModel<QuasiLinearViscoelasticityModelInput>, IQuasiLinearViscoelasticityModel
    {
        public abstract double CalculateElasticResponse(QuasiLinearViscoelasticityModelInput input, double time);

        public abstract double CalculateElasticResponseDerivative(QuasiLinearViscoelasticityModelInput input, double time);

        public abstract double CalculateReducedRelaxationFunction(QuasiLinearViscoelasticityModelInput input, double time);

        public abstract double CalculateReducedRelaxationFunctionSimplified(QuasiLinearViscoelasticityModelInput input, double time);
    }
}