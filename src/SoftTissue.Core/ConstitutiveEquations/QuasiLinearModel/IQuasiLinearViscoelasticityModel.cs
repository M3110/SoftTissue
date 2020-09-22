using SoftTissue.Core.Models;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public interface IQuasiLinearViscoelasticityModel : IViscoelasticModel<QuasiLinearViscoelasticityModelInput>
    {
        double CalculateElasticResponse(QuasiLinearViscoelasticityModelInput input, double time);

        double CalculateElasticResponseDerivative(QuasiLinearViscoelasticityModelInput input, double time);

        double CalculateReducedRelaxationFunction(QuasiLinearViscoelasticityModelInput input, double time);

        double CalculateReducedRelaxationFunctionSimplified(QuasiLinearViscoelasticityModelInput input, double time);
    }
}
