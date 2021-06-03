using SoftTissue.Core.Models.Viscoelasticity.Linear;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    /// <summary>
    /// It represents a linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class LinearModel<TInput, TResult> : ViscoelasticModel<TInput, TResult>, ILinearModel<TInput, TResult>
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    {
        /// <summary>
        /// Asynchronously, this method calculates the results for a linear viscoelastic model.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override async Task<TResult> CalculateResultsAsync(TInput input, double time)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time), "Time cannot be negative to calculate the results for viscoelastic models.");

            var tasks = new List<Task>();

            double creepCompliance = 0;
            tasks.Add(Task.Run(() => { creepCompliance = this.CalculateCreepCompliance(input, time); }));

            double strain = 0;
            tasks.Add(Task.Run(() => { strain = this.CalculateStrain(input, time); }));

            double relaxationFunction = 0;
            tasks.Add(Task.Run(() => { relaxationFunction = this.CalculateRelaxationFunction(input, time); }));

            double stress = 0;
            tasks.Add(Task.Run(() => { stress = this.CalculateStress(input, time); }));

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return new TResult
            {
                Time = time,
                CreepCompliance = creepCompliance,
                Strain = strain,
                RelaxationFunction = relaxationFunction,
                Stress = stress
            };
        }

        /// <summary>
        /// This method calculates the Relaxation Function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract double CalculateRelaxationFunction(TInput input, double time);

        /// <summary>
        /// This method calculates the Creep Compliance.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract double CalculateCreepCompliance(TInput input, double time);
    }
}
