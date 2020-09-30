using SoftTissue.Core.Models.Viscoelasticity;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public abstract class QuasiLinearViscoelasticityModel<TInput> : ViscoelasticModel<TInput>, IQuasiLinearViscoelasticityModel<TInput>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
    {
        public abstract double CalculateElasticResponse(TInput input, double time);

        public abstract double CalculateElasticResponseDerivative(TInput input, double time);

        public abstract double CalculateReducedRelaxationFunction(TInput input, double time);

        public abstract double CalculateReducedRelaxationFunctionSimplified(TInput input, double time);
    }
}