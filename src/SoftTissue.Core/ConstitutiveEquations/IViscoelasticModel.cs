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
        /// Asynchronously, this method calculates the results for a generic viscoelastic model.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        Task<TResult> CalculateResultsAsync(TInput input, double time);

        /// <summary>
        /// This method calculates the stress for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateStress(TInput input, double time);

        /// <summary>
        /// This method calculates the strain for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateStrain(TInput input, double time);
    }
}