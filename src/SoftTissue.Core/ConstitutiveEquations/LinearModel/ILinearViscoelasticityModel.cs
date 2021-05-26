using SoftTissue.Core.Models.Viscoelasticity.Linear;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    /// <summary>
    /// It represents a linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ILinearViscoelasticityModel<TInput, TResult> : IViscoelasticModel<TInput, TResult>
        where TInput : LinearViscoelasticityModelInput, new()
        where TResult : LinearViscoelasticityModelResult, new()
    {
        /// <summary>
        /// This method calculates the Relaxation Function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateRelaxationFunction(TInput input, double time);

        /// <summary>
        /// This method calculates the Creep Compliance.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateCreepCompliance(TInput input, double time);
    }
}
