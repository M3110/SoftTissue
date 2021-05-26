using SoftTissue.Core.Models.Viscoelasticity.Linear;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    /// <summary>
    /// It represents a linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class LinearViscoelasticityModel<TInput, TResult> : ViscoelasticModel<TInput, TResult>, ILinearViscoelasticityModel<TInput, TResult>
        where TInput : LinearViscoelasticityModelInput, new()
        where TResult : LinearViscoelasticityModelResult, new()
    {
        /// <summary>
        /// Asynchronously, this method calculates the initial conditions for a linear viscoelastic model analysis.
        /// </summary>
        /// <returns></returns>
        public override Task<TResult> CalculateInitialConditionsAsync()
        {
            return Task.FromResult(new TResult
            {
                Time = 0,
                Strain = 0,
                CreepCompliance = 0,
                RelaxationFunction = 0
            });
        }

        /// <summary>
        /// Asynchronously, this method calculates the results for a linear viscoelastic model analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override async Task<TResult> CalculateResultsAsync(TInput input, double time)
        {
            if (time <= 0)
                return await this.CalculateInitialConditionsAsync().ConfigureAwait(false);

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
