using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public interface IQuasiLinearViscoelasticityModel<TInput, TResult> : IViscoelasticModel<TInput>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        TResult CalculateInitialConditions(TInput input);

        double CalculateElasticResponse(TInput input, double time);

        double CalculateElasticResponseDerivative(TInput input, double time);

        double CalculateReducedRelaxationFunction(TInput input, double time);

        double CalculateReducedRelaxationFunctionSimplified(TInput input, double time);
    }
}
