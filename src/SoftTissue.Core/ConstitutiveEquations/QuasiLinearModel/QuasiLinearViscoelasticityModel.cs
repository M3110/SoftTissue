using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.Infrastructure.Models;
using System;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public abstract class QuasiLinearViscoelasticityModel<TInput, TResult, TRelaxationFunction> : ViscoelasticModel<TInput>, IQuasiLinearViscoelasticityModel<TInput, TResult, TRelaxationFunction>
        where TInput : QuasiLinearViscoelasticityModelInput<TRelaxationFunction>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        private readonly ISimpsonRuleIntegration _simpsonRuleIntegration;
        private readonly IDerivative _derivative;

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
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual TResult CalculateInitialConditions(TInput input)
        {
            return new TResult
            {
                ReducedRelaxationFunction = 1,
                ElasticResponse = 0,
                Strain = 0,
                Stress = 0,
                StressByIntegralDerivative = 0,
                StressByReducedRelaxationFunctionDerivative = 0
            };
        }

        /// <summary>
        /// This method calculates the strain.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateStrain(TInput input, double time)
        {
            switch (input.ViscoelasticConsideration)
            {
                case ViscoelasticConsideration.GeneralViscoelasctiEffect:
                case ViscoelasticConsideration.ViscoelasticEffectAfterRampTime:
                    return time <= input.RampTime ? input.StrainRate * time : input.MaximumStrain;

                case ViscoelasticConsideration.GeneralViscoelasticEffectWithStrainDecrease:
                case ViscoelasticConsideration.ViscoelasticEffectAfterRampTimeWithStrainDecrease:
                    if (time <= input.RampTime)
                        return input.StrainRate * time;

                    if (time > input.RampTime && time <= input.TimeWhenStrainStartDecreasing)
                        return input.MaximumStrain;

                    if (time > input.TimeWhenStrainStartDecreasing && time <= input.TimeWhenStrainStartDecreasing + input.DecreaseTime)
                        return input.MaximumStrain - input.StrainDecreaseRate * (time - input.TimeWhenStrainStartDecreasing);

                    return input.MinimumStrain;

                case ViscoelasticConsideration.DisregardRampTime:
                    return input.MaximumStrain;
            }

            throw new ArgumentOutOfRangeException("Invalid viscoelastic consideration.");
        }

        /// <summary>
        /// This method calculates the strain derivative.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public virtual double CalculateStrainDerivative(TInput input, double time)
        {
            switch (input.ViscoelasticConsideration)
            {
                case ViscoelasticConsideration.GeneralViscoelasctiEffect:
                case ViscoelasticConsideration.ViscoelasticEffectAfterRampTime:
                    return time < input.RampTime ? input.StrainRate : 0;

                case ViscoelasticConsideration.GeneralViscoelasticEffectWithStrainDecrease:
                case ViscoelasticConsideration.ViscoelasticEffectAfterRampTimeWithStrainDecrease:
                    {
                        if (time <= input.RampTime)
                            return input.StrainRate;

                        double timeWhenStrainBeginningDecrease = input.RampTime + input.TimeWithConstantStrain;

                        if (time > input.RampTime && time <= timeWhenStrainBeginningDecrease)
                            return 0;

                        if (time > timeWhenStrainBeginningDecrease && time <= timeWhenStrainBeginningDecrease + input.DecreaseTime)
                            return -input.StrainDecreaseRate;

                        return 0;
                    }

                case ViscoelasticConsideration.DisregardRampTime:
                    return 0;
            }

            throw new ArgumentOutOfRangeException("Invalid viscoelastic consideration.");
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

            double strain = this.CalculateStrain(input, time);

            return input.ElasticStressConstant * (Math.Exp(input.ElasticPowerConstant * strain) - 1);
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

            if (time <= input.RampTime && time > Constants.Precision)
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

            if (time <= input.TimeWhenStrainStartDecreasing && time > input.RampTime)
                return this._simpsonRuleIntegration.Calculate(
                    (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * this.CalculateElasticResponseDerivative(input, integrationTime),
                    new IntegralInput
                    {
                        InitialPoint = 0,
                        FinalPoint = input.RampTime,
                        Step = input.TimeStep
                    });

            if (input.ViscoelasticConsideration == ViscoelasticConsideration.GeneralViscoelasticEffectWithStrainDecrease
                || input.ViscoelasticConsideration == ViscoelasticConsideration.ViscoelasticEffectAfterRampTimeWithStrainDecrease)
            {
                return
                    this._simpsonRuleIntegration.Calculate(
                        (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * this.CalculateElasticResponseDerivative(input, integrationTime),
                        new IntegralInput
                        {
                            InitialPoint = 0,
                            FinalPoint = input.RampTime,
                            Step = input.TimeStep
                        })
                    + this._simpsonRuleIntegration.Calculate(
                        (integrationTime) => this.CalculateReducedRelaxationFunction(input, time - integrationTime) * this.CalculateElasticResponseDerivative(input, integrationTime),
                        new IntegralInput
                        {
                            InitialPoint = input.TimeWhenStrainStartDecreasing,
                            FinalPoint = time <= input.TimeWhenStrainStartDecreasing + input.DecreaseTime ? time : input.TimeWhenStrainStartDecreasing + input.DecreaseTime,
                            Step = input.TimeStep
                        });
            }

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
    }
}