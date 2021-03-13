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
    /// It represents the quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class QuasiLinearViscoelasticityModel<TInput, TResult, TRelaxationFunction> : ViscoelasticModel<TInput>, IQuasiLinearViscoelasticityModel<TInput, TResult, TRelaxationFunction>
        where TInput : QuasiLinearViscoelasticityModelInput<TRelaxationFunction>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
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
        public QuasiLinearViscoelasticityModel(ISimpsonRuleIntegration simpsonRuleIntegration, IDerivative derivative)
        {
            this._simpsonRuleIntegration = simpsonRuleIntegration;
            this._derivative = derivative;
        }

        /// <summary>
        /// This method calculates the initial conditions for Fung model analysis.
        /// </summary>
        /// <returns></returns>
        public virtual Task<TResult> CalculateInitialConditions()
        {
            return Task.FromResult(new TResult
            {
                ReducedRelaxationFunction = 1,
                Strain = 0,
                ElasticResponse = 0,
                Stress = 0,
                StressByIntegralDerivative = 0,
                StressByReducedRelaxationFunctionDerivative = 0
            });
        }

        /// <summary>
        /// This method calculates the results for a quasi-linear viscoelastic model.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <param name="streamWriter"></param>
        public virtual async Task<TResult> CalculateResults(TInput input, double time)
        {
            if (time <= Constants.Precision)
                return await this.CalculateInitialConditions();

            input.RelaxationNumber = this.CalculateRelaxationNumber(input, time);

            var tasks = new List<Task>();

            double strain = 0;
            tasks.Add(Task.Run(() =>
            {
                strain = this.CalculateStrain(input, time);
            }));

            double reducedRelaxationFunction = 0;
            tasks.Add(Task.Run(() =>
            {
                reducedRelaxationFunction = this.CalculateReducedRelaxationFunction(input, time);
            }));

            double elasticResponse = 0;
            tasks.Add(Task.Run(() =>
            {
                elasticResponse = this.CalculateElasticResponse(input, time);
            }));

            double stress = 0;
            tasks.Add(Task.Run(() =>
            {
                stress = this.CalculateStress(input, time);
            }));

            double stressByReducedRelaxationFunctionDerivative = 0;
            tasks.Add(Task.Run(() =>
            {
                stressByReducedRelaxationFunctionDerivative = this.CalculateStressByReducedRelaxationFunctionDerivative(input, time);
            }));

            double stressByIntegralDerivative = 0;
            tasks.Add(Task.Run(() =>
            {
                stressByIntegralDerivative = this.CalculateStressByIntegralDerivative(input, time);
            }));

            await Task.WhenAll(tasks);

            return new TResult
            {
                Strain = strain,
                ReducedRelaxationFunction = reducedRelaxationFunction,
                ElasticResponse = elasticResponse,
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
        /// This method calculates the strain.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateStrain(TInput input, double time)
        {
            if (this._relaxationTimes.Equals(default) == true)
                this._relaxationTimes = this.BuildRelaxationTimes(input);

            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return input.MaximumStrain;

            // time = 0 
            //     --> strain = 0
            if (time <= Constants.Precision)
                return 0;

            // 0 < time <= first ramp time
            //     --> strain = strain rate * time
            if (time > Constants.Precision && time <= input.FirstRampTime)
                return input.StrainRate * time;

            // first ramp time < time <= first relaxation total time
            // OBS: first relaxation total time = first ramp time + time with constant maximum strain
            //     --> strain = maximum strain
            if (time > input.FirstRampTime && time <= input.FirstRelaxationTotalTime)
                return input.MaximumStrain;

            // This part is used only for the second relaxations and next.
            if (time > input.FirstRelaxationTotalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainDecreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return input.MaximumStrain -
                    input.StrainDecreaseRate * (time - (input.FirstRelaxationTotalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime));

            if (time > this._relaxationTimes.StrainDecreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return input.MinimumStrain;

            if (time > this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainIncreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return input.MinimumStrain +
                    input.StrainRate * (time - (this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime));

            if (time > this._relaxationTimes.StrainIncreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainIncreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return input.MaximumStrain;

            // If the time is bigger than all relaxations time, it means that it is on the last relaxation and the strain is kept at the maximum till the end of analysis.
            if (time > input.FirstRelaxationTotalTime + (input.NumerOfRelaxations - 1) * input.RelaxationTotalTime)
                return input.MaximumStrain;

            // The default value returned must be equals to zero.
            return 0;
        }

        /// <summary>
        /// This method calculates the strain derivative.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual double CalculateStrainDerivative(TInput input, double time)
        {
            if (this._relaxationTimes.Equals(default) == true)
                this._relaxationTimes = this.BuildRelaxationTimes(input);

            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return 0;

            if (time <= input.FirstRampTime)
                return input.StrainRate;

            if (time > input.FirstRampTime && time <= input.FirstRelaxationTotalTime)
                return 0;

            if (time > input.FirstRelaxationTotalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainDecreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return -input.StrainDecreaseRate;

            if (time > this._relaxationTimes.StrainDecreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return 0;

            if (time > this._relaxationTimes.StrainDecreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainIncreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return input.StrainRate;

            if (time > this._relaxationTimes.StrainIncreaseStartTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime &&
                time <= this._relaxationTimes.StrainIncreaseFinalTime + (input.RelaxationNumber - 1) * input.RelaxationTotalTime)
                return 0;

            // The default value returned must be equals to zero.
            return 0;
        }

        /// <summary>
        /// This method calculates the elastic response.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual double CalculateElasticResponse(TInput input, double time)
        {
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return input.InitialStress;

            // Elastic stress = A * [exp(B * strain) - 1]
            return input.ElasticStressConstant * (Math.Exp(input.ElasticPowerConstant * this.CalculateStrain(input, time)) - 1);
        }

        /// <summary>
        /// This method calculates the derivtive of elastic response.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual double CalculateElasticResponseDerivative(TInput input, double time)
        {
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return 0;

            double strain = this.CalculateStrain(input, time);
            double strainDerivative = this.CalculateStrainDerivative(input, time);

            // Derivative of elastic stress = A * B * (d/dt)(strain) * exp(B * strain)
            return input.ElasticStressConstant * input.ElasticPowerConstant * strainDerivative * Math.Exp(input.ElasticPowerConstant * strain);
        }

        /// <summary>
        /// This method calculates the reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract double CalculateReducedRelaxationFunction(TInput input, double time);

        /// <summary>
        /// This method calculates the derivative of reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract double CalculateReducedRelaxationFunctionDerivative(TInput input, double time);

        /// <summary>
        /// This method calculates the stress using the equation 7 from Fung, at page 279.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateStress(TInput input, double time)
        {
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                return input.InitialStress * this.CalculateReducedRelaxationFunction(input, time);

            if (time <= Constants.Precision)
                return 0;

            if (time > Constants.Precision && time <= input.FirstRampTime)
            {
                if (input.ViscoelasticConsideration == ViscoelasticConsideration.ViscoelasticEffectAfterRampTime)
                    return this.CalculateElasticResponse(input, time);

                return this._simpsonRuleIntegration.Calculate(
                    (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * this.CalculateElasticResponseDerivative(input, integrationTime),
                    new IntegralInput
                    {
                        InitialPoint = 0,
                        FinalPoint = time,
                        Step = input.TimeStep
                    });
            }

            if (time <= input.FirstRelaxationTotalTime && time > input.FirstRampTime)
                return this._simpsonRuleIntegration.Calculate(
                    (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * this.CalculateElasticResponseDerivative(input, integrationTime),
                    new IntegralInput
                    {
                        InitialPoint = 0,
                        FinalPoint = input.FirstRampTime,
                        Step = input.TimeStep
                    });

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
            return this._simpsonRuleIntegration.Calculate(
                (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * this.CalculateElasticResponseDerivative(input, integrationTime),
                new IntegralInput
                {
                    InitialPoint = 0,
                    FinalPoint = time,
                    Step = input.TimeStep
                });
        }

        /// <summary>
        /// This method calculates the stress using the equation 8.a from Fung, at page 279.
        /// That equation do not returns a satisfactory result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual double CalculateStressByReducedRelaxationFunctionDerivative(TInput input, double time)
        {
            if (time <= Constants.Precision)
                return 0;

            return this.CalculateReducedRelaxationFunction(input, time: 0) * this.CalculateElasticResponse(input, time) +
                this._simpsonRuleIntegration.Calculate(
                    (integrationTime) => this.CalculateElasticResponse(input, time - integrationTime) * this.CalculateReducedRelaxationFunctionDerivative(input, integrationTime),
                    new IntegralInput
                    {
                        InitialPoint = 0,
                        FinalPoint = time,
                        Step = input.TimeStep
                    });
        }

        /// <summary>
        /// This method calculates the stress using the equation 8.b from Fung, at page 279.
        /// That equation do not returns a satisfactory result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual double CalculateStressByIntegralDerivative(TInput input, double time)
        {
            if (time <= Constants.Precision)
                return 0;

            return this._derivative.Calculate(
                (derivativeTime) => this._simpsonRuleIntegration.Calculate(
                    (integrationTime) => this.CalculateElasticResponse(input, derivativeTime - integrationTime) * this.CalculateReducedRelaxationFunction(input, integrationTime),
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