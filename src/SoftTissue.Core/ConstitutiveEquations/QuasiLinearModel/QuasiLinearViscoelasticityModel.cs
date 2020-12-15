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
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
            {
                return input.MaximumStrain;
            }
            else if (input.ViscoelasticConsideration == ViscoelasticConsideration.GeneralViscoelasctiEffectWithConstantStrain)
            {
                return time < input.RampTime ? input.StrainRate * time : input.MaximumStrain;
            }
            else if (input.ViscoelasticConsideration == ViscoelasticConsideration.GeneralViscoelasticEffectWithStrainDecrease)
            {
                if (time < input.RampTime) return input.StrainRate * time;
                else if (time > input.RampTime && time < input.FinalStrainTime) return input.MaximumStrain;
                else return 0;
            }

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
            {
                return input.InitialStress;
            }

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
            double strain = this.CalculateStrain(input, time);

            // After the ramp time, the strain is constant, so the its derivative is zero.
            double strainDerivative = time <= input.RampTime ? input.StrainRate : 0;

            return input.ElasticStressConstant * input.ElasticPowerConstant * strainDerivative * Math.Exp(input.ElasticPowerConstant * strain);
        }
        
        public abstract double CalculateReducedRelaxationFunction(TInput input, double time);

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
            {
                return input.InitialStress * this.CalculateReducedRelaxationFunction(input, time);
            }
            else if (input.ViscoelasticConsideration == ViscoelasticConsideration.ViscoelasticEffectAfterRampTime)
            {
                if (time <= input.RampTime)
                {
                    return this.CalculateElasticResponse(input, time);
                }
                else
                {
                    return this._simpsonRuleIntegration.Calculate(
                        (equationTime) => this.CalculateReducedRelaxationFunction(input, time - equationTime) * this.CalculateElasticResponseDerivative(input, equationTime),
                        new IntegralInput
                        {
                            InitialPoint = 0,
                            FinalPoint = input.RampTime,
                            Step = input.TimeStep
                        });
                }
            }

            // The default way to calculate the stress is considering the viscoelastic effect to all time domain and the strain is constant after the ramp time.
            return this._simpsonRuleIntegration.Calculate(
                (equationTime) => this.CalculateReducedRelaxationFunction(input, time - equationTime) * this.CalculateElasticResponseDerivative(input, equationTime),
                new IntegralInput
                {
                    InitialPoint = 0,
                    FinalPoint = time < input.RampTime ? time : input.RampTime,
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
            var integralInput = new IntegralInput
            {
                InitialPoint = 0,
                FinalPoint = time,
                Step = input.TimeStep
            };

            return this.CalculateReducedRelaxationFunction(input, time: 0) * this.CalculateElasticResponse(input, time) +
                this._simpsonRuleIntegration.Calculate((integrationTime) =>
                {
                    return this.CalculateElasticResponse(input, integrationTime) * this.CalculateReducedRelaxationFunctionDerivative(input, time);
                },
                integralInput);
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
            return this._derivative.Calculate((derivativeTime) =>
            {
                var integralInput = new IntegralInput
                {
                    InitialPoint = 0,
                    FinalPoint = derivativeTime,
                    Step = input.TimeStep
                };

                return this._simpsonRuleIntegration.Calculate(
                    (integrationTime) => this.CalculateElasticResponse(input, time - integrationTime) * this.CalculateReducedRelaxationFunction(input, integrationTime),
                    integralInput);
            },
            input.TimeStep, time);
        }
    }
}