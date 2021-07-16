using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    /// <summary>
    /// It represents a quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public interface IQuasiLinearModel<TInput, TResult, TRelaxationFunction> : IViscoelasticModel<TInput, TResult>
        where TInput : QuasiLinearModelInput<TRelaxationFunction>
        where TResult : QuasiLinearModelResult, new()
        where TRelaxationFunction : class
    {
        /// <summary>
        /// This method calculates the strain derivative.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateStrainDerivative(TInput input, double time);

        /// <summary>
        /// Asynchronously, this method calculates the elastic response.
        /// Equation used: Elastic stress = A * [exp(B * strain) - 1]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<double> CalculateElasticResponseAsync(TInput input, double time);

        /// <summary>
        /// Asynchronously, this method calculates the derivative of elastic response.
        /// Equation used: Elastic Stress Derivative = A * B * (d/dt)(strain) * exp(B * strain)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<double> CalculateElasticResponseDerivativeAsync(TInput input, double time);

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
        /// Asynchronously, this method calculates the stress using the equation 8.a from Fung, at page 279.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<double> CalculateStressByReducedRelaxationFunctionDerivative(TInput input, double time);

        /// <summary>
        /// Asynchronously, this method calculates the stress using the equation 8.b from Fung, at page 279.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<double> CalculateStressByIntegralDerivative(TInput input, double time);
    }
}
