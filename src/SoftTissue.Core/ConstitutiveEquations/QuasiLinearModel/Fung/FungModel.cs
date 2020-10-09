using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using System;
using System.Linq;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    public class FungModel : QuasiLinearViscoelasticityModel<FungModelInput, FungModelResult>, IFungModel
    {
        private readonly ISimpsonRuleIntegration _simpsonRuleIntegration;
        private readonly IDerivative _derivative;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="simpsonRuleIntegration"></param>
        /// <param name="derivative"></param>
        public FungModel(
            ISimpsonRuleIntegration simpsonRuleIntegration,
            IDerivative derivative)
        {
            this._simpsonRuleIntegration = simpsonRuleIntegration;
            this._derivative = derivative;
        }

        public delegate double FungModelEquation(FungModelInput input, double time);

        /// <summary>
        /// This method calculates the initial conditions for Fung model analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override FungModelResult CalculateInitialConditions(FungModelInput input)
        {
            return new FungModelResult
            {
                ReducedRelaxationFunction = 1,
                ElasticResponse = 0,
                Strain = 0,
                Stress = 0,
                StressWithIntegralDerivative = 0,
                StressWithReducedRelaxationFunctionDerivative = 0
            };
        }

        /// <summary>
        /// This method calculates the strain.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateStrain(FungModelInput input, double time)
        {
            return input.StrainRate * time < input.MaximumStrain ? input.StrainRate * time : input.MaximumStrain;
        }

        /// <summary>
        /// This method calculates the elastic response.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateElasticResponse(FungModelInput input, double time)
        {
            double strain = this.CalculateStrain(input, time);

            return input.ElasticStressConstant * (Math.Exp(input.ElasticPowerConstant * strain) - 1);
        }

        /// <summary>
        /// This method calculates the derivtive of elastic response.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateElasticResponseDerivative(FungModelInput input, double time)
        {
            double strain = this.CalculateStrain(input, time);
            double strainDerivative = this._derivative.Calculate((derivativeTime) => this.CalculateStrain(input, derivativeTime), input.TimeStep, time);
            return input.ElasticStressConstant * input.ElasticPowerConstant * strainDerivative * Math.Exp(input.ElasticPowerConstant * strain);
        }

        /// <summary>
        /// This method calculates the simplified reduced relaxation funtion.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunctionSimplified(FungModelInput input, double time)
        {
            double result = 0;

            for(int i = 0; i < input.SimplifiedReducedRelaxationFunctionInput.VariableEList.Count(); i++)
            {
                result += input.SimplifiedReducedRelaxationFunctionInput.VariableEList.ElementAt(i) * Math.Exp(-time / input.SimplifiedReducedRelaxationFunctionInput.RelaxationTimeList.ElementAt(i));
            }

            return result;
        }

        /// <summary>
        /// This method calculates the reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunction(FungModelInput input, double time)
        {
            if (time <= Constants.Precision)
            {
                return 1;
            }

            var inputG = input.ReducedRelaxationFunctionInput;

            if (time <= 1)
            {
                return (1 - inputG.RelaxationIndex * Constants.EulerMascheroniConstant - inputG.RelaxationIndex * Math.Log(time / inputG.SlowRelaxationTime)) / (1 + inputG.RelaxationIndex * Math.Log(inputG.SlowRelaxationTime / inputG.FastRelaxationTime));
            }

            return (1 + inputG.RelaxationIndex * (this.CalculateE1(input, time / inputG.SlowRelaxationTime) - this.CalculateE1(input, time / inputG.FastRelaxationTime))) / (1 + inputG.RelaxationIndex * Math.Log(inputG.SlowRelaxationTime / inputG.FastRelaxationTime));
        }

        private double CalculateE1(FungModelInput input, double variable)
        {
            var integralInput = new IntegralInput
            {
                InitialPoint = variable,
                Precision = Constants.Precision,
                Step = input.TimeStep
            };

            return this._simpsonRuleIntegration.Calculate((parameter) => Math.Exp(-parameter) / parameter, integralInput);
        }

        public override double CalculateStress(FungModelInput input, double time)
        {
            FungModelEquation calculatedReducedRelaxationFunction = this.SetReducedRelaxationFunction(input);

            var integralInput = new IntegralInput
            {
                InitialPoint = 0,
                FinalPoint = time,
                Step = input.TimeStep
            };

            return this.CalculateElasticResponse(input, time: 0) * calculatedReducedRelaxationFunction(input, time) +
                this._simpsonRuleIntegration.Calculate((equationTime) => calculatedReducedRelaxationFunction(input, time - equationTime) * this.CalculateElasticResponseDerivative(input, equationTime), integralInput);
        }

        public double CalculateStressByIntegrationDerivative(FungModelInput input, double time)
        {
            FungModelEquation calculatedReducedRelaxationFunction = this.SetReducedRelaxationFunction(input);

            return this._derivative.Calculate((derivativeTime) =>
            {
                var integralInput = new IntegralInput
                {
                    InitialPoint = 0,
                    FinalPoint = derivativeTime,
                    Step = input.TimeStep
                };

                return this._simpsonRuleIntegration.Calculate(
                    (integrationTime) => this.CalculateElasticResponse(input, time - integrationTime) * calculatedReducedRelaxationFunction(input, integrationTime),
                    integralInput);
            },
            input.TimeStep, time);
        }

        public double CalculateStressByReducedRelaxationFunctionDerivative(FungModelInput input, double time)
        {
            FungModelEquation calculatedReducedRelaxationFunction = this.SetReducedRelaxationFunction(input);

            var integralInput = new IntegralInput
            {
                InitialPoint = 0,
                FinalPoint = time,
                Step = input.TimeStep
            };

            return calculatedReducedRelaxationFunction(input, time: 0) * this.CalculateElasticResponse(input, time) +
                this._simpsonRuleIntegration.Calculate((integrationTime) =>
                {
                    return this.CalculateElasticResponse(input, integrationTime) * this._derivative.Calculate(
                        (derivativeTime) => calculatedReducedRelaxationFunction(input, derivativeTime),
                        input.TimeStep,
                        time);
                },
                integralInput);
        }

        private FungModelEquation SetReducedRelaxationFunction(FungModelInput input)
        {
            if (input.UseSimplifiedReducedRelaxationFunction == false)
                return this.CalculateReducedRelaxationFunction;
            else
                return this.CalculateReducedRelaxationFunctionSimplified;
        }
    }
}
