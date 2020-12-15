using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public interface IQuasiLinearViscoelasticityModel<TInput, TResult, TRelaxationFunction> : IViscoelasticModel<TInput>
        where TInput : QuasiLinearViscoelasticityModelInput<TRelaxationFunction>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        TResult CalculateInitialConditions(TInput input);

        double CalculateElasticResponse(TInput input, double time);

        double CalculateElasticResponseDerivative(TInput input, double time);

        double CalculateReducedRelaxationFunction(TInput input, double time);

        double CalculateReducedRelaxationFunctionDerivative(TInput input, double time);

        /// <summary>
        /// This method calculates the stress using the equation 8.a from Fung, at page 279.
        /// That equation do not returns a satisfactory result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateStressByReducedRelaxationFunctionDerivative(TInput input, double time);

        /// <summary>
        /// This method calculates the stress using the equation 8.b from Fung, at page 279.
        /// That equation do not returns a satisfactory result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateStressByIntegralDerivative(TInput input, double time);
    }
}
