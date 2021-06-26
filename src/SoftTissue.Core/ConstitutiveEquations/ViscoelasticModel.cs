using SoftTissue.Core.Models.Viscoelasticity;
using System;
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
        where TInput : ViscoelasticModelInput
        where TResult : ViscoelasticModelResult, new()
    {
        /// <inheritdoc/>
        public virtual async Task<TResult> CalculateResultAsync(TInput input, double time)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time), "Time cannot be negative to calculate the results for viscoelastic models.");

            var tasks = new List<Task>();

            double strain = 0;
            tasks.Add(Task.Run(async () => { strain = await this.CalculateStrainAsync(input, time).ConfigureAwait(false); }));

            double stress = 0;
            tasks.Add(Task.Run(async () => { stress = await this.CalculateStressAsync(input, time).ConfigureAwait(false); }));

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return new TResult
            {
                Time = time,
                Strain = strain,
                Stress = stress
            };
        }

        /// <inheritdoc/>
        public abstract Task<double> CalculateStressAsync(TInput input, double time);

        /// <inheritdoc/>
        public abstract Task<double> CalculateStrainAsync(TInput input, double time);
    }
}
