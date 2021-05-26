using SoftTissue.Core.Models.Viscoelasticity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations
{
    /// <summary>
    /// It represents a generic viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ViscoelasticModel<TInput, TResult> : IViscoelasticModel<TInput, TResult>
        where TInput : ViscoelasticModelInput, new()
        where TResult : ViscoelasticModelResult, new()
    {
        /// <summary>
        /// Asynchronously, this method calculates the initial conditions for a generic viscoelastic model analysis.
        /// </summary>
        /// <returns></returns>
        public virtual Task<TResult> CalculateInitialConditionsAsync()
        {
            return Task.FromResult(new TResult
            {
                Time = 0,
                Strain = 0,
                Stress = 0
            });
        }

        /// <summary>
        /// Asynchronously, this method calculates the results for a generic viscoelastic model analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        public virtual async Task<TResult> CalculateResultsAsync(TInput input, double time)
        {
            if (time <= 0)
                return await this.CalculateInitialConditionsAsync().ConfigureAwait(false);

            var tasks = new List<Task>();

            double strain = 0;
            tasks.Add(Task.Run(() => { strain = this.CalculateStrain(input, time); }));

            double stress = 0;
            tasks.Add(Task.Run(() => { stress = this.CalculateStress(input, time); }));

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return new TResult
            {
                Time = time,
                Strain = strain,
                Stress = stress
            };
        }

        /// <summary>
        /// This method calculates the stress for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract double CalculateStress(TInput input, double time);

        /// <summary>
        /// This method calculates the strain for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract double CalculateStrain(TInput input, double time);
    }
}
