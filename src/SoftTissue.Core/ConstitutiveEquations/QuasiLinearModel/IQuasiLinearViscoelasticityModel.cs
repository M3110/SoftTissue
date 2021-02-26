using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    /// <summary>
    /// It represents the quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public interface IQuasiLinearViscoelasticityModel<TInput, TResult, TRelaxationFunction> : IViscoelasticModel<TInput>
        where TInput : QuasiLinearViscoelasticityModelInput<TRelaxationFunction>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        /// <summary>
        /// This method calculates the initial conditions for Fung model analysis.
        /// </summary>
        /// <returns></returns>
        Task<TResult> CalculateInitialConditions();

        /// <summary>
        /// This method calculates the results for a quasi-linear viscoelastic model.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<TResult> CalculateResults(TInput input, double time);

        /// <summary>
        /// This method calculates the strain derivative.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateStrainDerivative(TInput input, double time);

        /// <summary>
        /// This method calculates the elastic response.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateElasticResponse(TInput input, double time);

        /// <summary>
        /// This method calculates the derivtive of elastic response.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateElasticResponseDerivative(TInput input, double time);

        /// <summary>
        /// This method calculates the reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateReducedRelaxationFunction(TInput input, double time);

        /// <summary>
        /// This method calculates the derivative of reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
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
