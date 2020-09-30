using SoftTissue.Core.Models.Viscoelasticity;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public interface IQuasiLinearViscoelasticityModel<TInput> : IViscoelasticModel<TInput>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
    {
        double CalculateElasticResponse(TInput input, double time);

        double CalculateElasticResponseDerivative(TInput input, double time);

        double CalculateReducedRelaxationFunction(TInput input, double time);

        double CalculateReducedRelaxationFunctionSimplified(TInput input, double time);
    }
}
