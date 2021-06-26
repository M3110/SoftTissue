using SoftTissue.Core.Models.Viscoelasticity;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations
{
    /// <summary>
    /// It represents a generic viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IViscoelasticModel<TInput, TResult>
        where TInput : ViscoelasticModelInput
        where TResult : ViscoelasticModelResult, new()
    {
        /// <summary>
        /// Asynchronously, this method calculates the result for a generic viscoelastic model.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        Task<TResult> CalculateResultAsync(TInput input, double time);

        /// <summary>
        /// Asynchronously, this method calculates the stress.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<double> CalculateStressAsync(TInput input, double time);

        /// <summary>
        /// Asynchronously, this method calculates the strain.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<double> CalculateStrainAsync(TInput input, double time);
    }
}