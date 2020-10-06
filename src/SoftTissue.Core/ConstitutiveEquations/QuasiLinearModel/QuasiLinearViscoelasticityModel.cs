using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public abstract class QuasiLinearViscoelasticityModel<TInput, TResult> : ViscoelasticModel<TInput>, IQuasiLinearViscoelasticityModel<TInput, TResult>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        public abstract TResult CalculateInitialConditions(TInput input);

        public abstract double CalculateElasticResponse(TInput input, double time);

        public abstract double CalculateElasticResponseDerivative(TInput input, double time);
        
        public abstract double CalculateReducedRelaxationFunction(TInput input, double time);

        public abstract double CalculateReducedRelaxationFunctionSimplified(TInput input, double time);
    }
}