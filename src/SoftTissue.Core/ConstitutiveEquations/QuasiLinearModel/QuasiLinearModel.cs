using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.DataContract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    /// <summary>
    /// It represents a quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class QuasiLinearModel<TInput, TResult, TRelaxationFunction> : ViscoelasticModel<TInput, TResult>, IQuasiLinearModel<TInput, TResult, TRelaxationFunction>
        where TInput : QuasiLinearModelInput<TRelaxationFunction>, new()
        where TResult : QuasiLinearModelResult, new()
        where TRelaxationFunction : class
    {
        /// <summary>
        /// The important relaxation times.
        /// </summary>
        protected RelaxationTimes _relaxationTimes;

        protected readonly ISimpsonRuleIntegration _simpsonRuleIntegration;
        protected readonly IDerivative _derivative;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="simpsonRuleIntegration"></param>
        /// <param name="derivative"></param>
        public QuasiLinearModel(ISimpsonRuleIntegration simpsonRuleIntegration, IDerivative derivative)
        {
            this._simpsonRuleIntegration = simpsonRuleIntegration;
            this._derivative = derivative;
        }

        /// <summary>
        /// Asynchronously, this method calculates the results for a quasi-linear viscoelastic model.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        public override async Task<TResult> CalculateResultAsync(TInput input, double time)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time), "Time cannot be negative to calculate the results for viscoelastic models.");

            input.RelaxationNumber = this.CalculateRelaxationNumber(input, time);

            if (this._relaxationTimes.IsEmpty() == true)
                this._relaxationTimes = this.BuildRelaxationTimes(input);

            double stress = 0;
            double? stressByReducedRelaxationFunctionDerivative = null;
            double? stressByIntegralDerivative = null;

            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
            {
                stress = await this.CalculateStressAsync(input, time).ConfigureAwait(false);
            }
            else if (input.ViscoelasticConsideration == ViscoelasticConsideration.GeneralViscoelasctiEffect)
            {
                var tasks = new List<Task>
                {
                    Task.Run(async () => { stress = await this.CalculateStressAsync(input, time).ConfigureAwait(false); }),
                    Task.Run(async () => { stressByReducedRelaxationFunctionDerivative = await this.CalculateStressByReducedRelaxationFunctionDerivative(input, time).ConfigureAwait(false); }),
                    Task.Run(async () => { stressByIntegralDerivative = await this.CalculateStressByIntegralDerivative(input, time).ConfigureAwait(false); })
                };
                await Task.WhenAll(tasks);
            }

            return new TResult
            {
                Time = time,
                Strain = await this.CalculateStrainAsync(input, time).ConfigureAwait(false),
                StrainDerivative = this.CalculateStrainDerivative(input, time),
                ReducedRelaxationFunction = this.CalculateReducedRelaxationFunction(input, time),
                ReducedRelaxationFunctionDerivative = this.CalculateReducedRelaxationFunctionDerivative(input, time),
                ElasticResponse = await this.CalculateElasticResponseAsync(input, time).ConfigureAwait(false),
                ElasticResponseDerivative = await this.CalculateElasticResponseDerivativeAsync(input, time).ConfigureAwait(false),
                Stress = stress,
                StressByReducedRelaxationFunctionDerivative = stressByReducedRelaxationFunctionDerivative,
                StressByIntegralDerivative = stressByIntegralDerivative
            };
        }

        public int CalculateRelaxationNumber(TInput input, double time)
        {
            // If the time is less than the first relaxation total time, it is in the first relaxation.
            if (time < input.FirstRelaxationTotalTime)
                return 0;

            for (int i = 1; i < input.NumerOfRelaxations; i++)
            {
                if (input.FirstRelaxationTotalTime + (i - 1) * input.RelaxationTotalTime < time &&
                    time <= input.FirstRelaxationTotalTime + i * input.RelaxationTotalTime)
                    return i;
            }

            // If the time is bigger than all relaxations time, it means that it is on the last relaxation.
            if (time > input.FirstRelaxationTotalTime + (input.NumerOfRelaxations - 1) * input.RelaxationTotalTime)
                return input.NumerOfRelaxations - 1;

            throw new ArgumentOutOfRangeException("Time", $"Time: {time} is not in a valid range to calculate the relaxation number.");
        }

        /// <summary>
        /// Asynchronously, this method calculates the strain.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override Task<double> CalculateStrainAsync(TInput input, double time)
        {
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return Task.FromResult(input.MaximumStrain);

            // time = 0 
            //     --> strain = 0
            if (time == 0)
                return Task.FromResult<double>(0);

            // 0 < time < first ramp time
            //     --> strain = strain rate * time
            if (time > 0 && time <= input.FirstRampTime)
                return Task.FromResult(input.StrainRate * time);

            // first ramp time <= time < first relaxation total time
            // OBS: first relaxation total time = first ramp time + time with constant maximum strain
            //     --> strain = maximum strain
            if (time > input.FirstRampTime && time < input.FirstRelaxationTotalTime)
                return Task.FromResult(input.MaximumStrain);

            // This part is used only for the second relaxations and next.
            if (time >= input.FirstRelaxationTotalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainDecreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return Task.FromResult(input.MaximumStrain -
                    input.StrainDecreaseRate * (time - (input.FirstRelaxationTotalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)));

            if (time > this._relaxationTimes.StrainDecreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time < this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return Task.FromResult(input.MinimumStrain);

            if (time >= this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainIncreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return Task.FromResult(input.MinimumStrain +
                    input.StrainRate * (time - (this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)));

            if (time > this._relaxationTimes.StrainIncreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time < this._relaxationTimes.StrainIncreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return Task.FromResult(input.MaximumStrain);

            // If the time is bigger than all relaxations time, it means that it is on the last relaxation and the strain is kept at the maximum till the end of analysis.
            if (time > input.FirstRelaxationTotalTime + (input.NumerOfRelaxations - 1) * input.RelaxationTotalTime)
                return Task.FromResult(input.MaximumStrain);

            // The default value returned must be equals to zero.
            return Task.FromResult<double>(0);
        }

        /// <inheritdoc/>
        public virtual double CalculateStrainDerivative(TInput input, double time)
        {
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return 0;

            if (time == 0)
                return 0;

            if (time > 0 && time <= input.FirstRampTime)
                return input.StrainRate;

            if (time > input.FirstRampTime && time < input.FirstRelaxationTotalTime)
                return 0;

            if (time >= input.FirstRelaxationTotalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainDecreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return -input.StrainDecreaseRate;

            if (time > this._relaxationTimes.StrainDecreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time < this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return 0;

            if (time >= this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainIncreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return input.StrainRate;

            if (time > this._relaxationTimes.StrainIncreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time < this._relaxationTimes.StrainIncreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return 0;

            // The default value returned must be equals to zero.
            return 0;
        }

        /// <inheritdoc/>
        public virtual async Task<double> CalculateElasticResponseAsync(TInput input, double time)
        {
            if (time == 0)
                return 0;

            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return input.InitialStress;

            return input.ElasticStressConstant * (Math.Exp(input.ElasticPowerConstant * await this.CalculateStrainAsync(input, time).ConfigureAwait(false)) - 1);
        }

        /// <inheritdoc/>
        public virtual async Task<double> CalculateElasticResponseDerivativeAsync(TInput input, double time)
        {
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return 0;

            if (time == 0)
                return 0;

            double strain = await this.CalculateStrainAsync(input, time).ConfigureAwait(false);
            double strainDerivative = this.CalculateStrainDerivative(input, time);

            return input.ElasticStressConstant * input.ElasticPowerConstant * strainDerivative * Math.Exp(input.ElasticPowerConstant * strain);
        }

        /// <inheritdoc/>
        public abstract double CalculateReducedRelaxationFunction(TInput input, double time);

        /// <inheritdoc/>
        public abstract double CalculateReducedRelaxationFunctionDerivative(TInput input, double time);

        /// <summary>
        /// Asynchronously, this method calculates the stress using the equation 7 from Fung, at page 279.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override async Task<double> CalculateStressAsync(TInput input, double time)
        {
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return input.InitialStress * this.CalculateReducedRelaxationFunction(input, time);

            if (time < Constants.Precision)
                return 0;

            if (time >= Constants.Precision && time < input.FirstRampTime)
            {
                return await this._simpsonRuleIntegration.Calculate(
                    async (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * await this.CalculateElasticResponseDerivativeAsync(input, integrationTime).ConfigureAwait(false),
                    new IntegralInput
                    {
                        InitialPoint = 0,
                        FinalPoint = time,
                        Step = input.TimeStep
                    }).ConfigureAwait(false);
            }

            if (time < input.FirstRelaxationTotalTime && time >= input.FirstRampTime)
                return await this._simpsonRuleIntegration.Calculate(
                    async (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * await this.CalculateElasticResponseDerivativeAsync(input, integrationTime).ConfigureAwait(false),
                    new IntegralInput
                    {
                        InitialPoint = 0,
                        FinalPoint = input.FirstRampTime,
                        Step = input.TimeStep
                    }).ConfigureAwait(false);

            //if (input.ViscoelasticConsideration == ViscoelasticConsideration.GeneralViscoelasticEffectWithStrainDecrease
            //    || input.ViscoelasticConsideration == ViscoelasticConsideration.ViscoelasticEffectAfterRampTimeWithStrainDecrease)
            //{
            //    return
            //        this._simpsonRuleIntegration.Calculate(
            //            (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * this.CalculateElasticResponseDerivative(input, integrationTime),
            //            new IntegralInput
            //            {
            //                InitialPoint = 0,
            //                FinalPoint = input.FirstRampTime,
            //                Step = input.TimeStep
            //            })
            //        + this._simpsonRuleIntegration.Calculate(
            //            (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * this.CalculateElasticResponseDerivative(input, integrationTime),
            //            new IntegralInput
            //            {
            //                InitialPoint = input.FirstRelaxationTotalTime,
            //                FinalPoint = time <= input.FirstRelaxationTotalTime + input.DecreaseTime ? time : input.FirstRelaxationTotalTime + input.DecreaseTime,
            //                Step = input.TimeStep
            //            });
            //}

            // The default way to calculate the stress is using the convolution between the reduced relaxation function and derivative of elastic response.
            return await this._simpsonRuleIntegration.Calculate(
                async (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * await this.CalculateElasticResponseDerivativeAsync(input, integrationTime).ConfigureAwait(false),
                new IntegralInput
                {
                    InitialPoint = 0,
                    FinalPoint = time,
                    Step = input.TimeStep
                }).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<double> CalculateStressByReducedRelaxationFunctionDerivative(TInput input, double time)
        {
            if (time <= Constants.Precision)
                return 0;

            return this.CalculateReducedRelaxationFunction(input, time: 0) * await this.CalculateElasticResponseAsync(input, time).ConfigureAwait(false) +
                await this._simpsonRuleIntegration.Calculate(
                    async (integrationTime) => await this.CalculateElasticResponseAsync(input, time - integrationTime).ConfigureAwait(false) * this.CalculateReducedRelaxationFunctionDerivative(input, integrationTime),
                    new IntegralInput
                    {
                        InitialPoint = 0,
                        FinalPoint = time,
                        Step = input.TimeStep
                    });
        }

        /// <inheritdoc/>
        public virtual async Task<double> CalculateStressByIntegralDerivative(TInput input, double time)
        {
            if (time <= Constants.Precision)
                return 0;

            return await this._derivative.Calculate(
                async (derivativeTime) => await this._simpsonRuleIntegration.Calculate(
                    async (integrationTime) => await this.CalculateElasticResponseAsync(input, derivativeTime - integrationTime).ConfigureAwait(false) * this.CalculateReducedRelaxationFunction(input, integrationTime),
                    new IntegralInput
                    {
                        InitialPoint = 0,
                        FinalPoint = derivativeTime,
                        Step = input.TimeStep
                    }),
                input.TimeStep, time);
        }

        /// <summary>
        /// This method calculates the important relaxation times.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected RelaxationTimes BuildRelaxationTimes(TInput input)
            => new RelaxationTimes
            (
                input.FirstRelaxationTotalTime + input.DecreaseTime,
                input.FirstRelaxationTotalTime + input.DecreaseTime + input.TimeWithConstantMinimumStrain,
                input.FirstRelaxationTotalTime + input.DecreaseTime + input.TimeWithConstantMinimumStrain + input.RampTime,
                input.FirstRelaxationTotalTime + input.DecreaseTime + input.TimeWithConstantMinimumStrain + input.RampTime + input.TimeWithConstantMaximumStrain
            );
    }
}