using SoftTissue.Core.Models;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using System;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    public class FungModel : QuasiLinearViscoelasticityModel, IFungModel
    {
        private readonly ISimpsonRuleIntegration _simpsonRuleIntegration;
        private readonly IDerivative _derivative;

        public FungModel(
            ISimpsonRuleIntegration simpsonRuleIntegration,
            IDerivative derivative)
        {
            this._simpsonRuleIntegration = simpsonRuleIntegration;
            this._derivative = derivative;
        }

        public override double CalculateStrain(QuasiLinearViscoelasticityModelInput input, double time)
        {
            return input.StrainRate * time < input.MaximumStrain ? input.StrainRate * time : input.MaximumStrain;
        }

        public override double CalculateElasticResponse(QuasiLinearViscoelasticityModelInput input, double time)
        {
            double strain = this.CalculateStrain(input, time);

            return input.ElasticStressConstant * (Math.Exp(input.ElasticPowerConstant * strain) - 1);
        }

        public override double CalculateElasticResponseDerivative(QuasiLinearViscoelasticityModelInput input, double time)
        {
            double strain = this.CalculateStrain(input, time);
            double strainDerivative = this._derivative.Calculate((derivativeTime) => this.CalculateStrain(input, derivativeTime), input.TimeStep, time);
            return input.ElasticStressConstant * input.ElasticPowerConstant * strainDerivative * Math.Exp(input.ElasticPowerConstant * strain);
        }

        public override double CalculateReducedRelaxationFunctionSimplified(QuasiLinearViscoelasticityModelInput input, double time)
        {
            double result = 0;

            foreach (var simplifiedInput in input.RelaxationFunctionSimplifiedInputList)
            {
                result += simplifiedInput.VariableE * Math.Exp(-time / simplifiedInput.RelaxationTime);
            }

            return result;
        }

        public override double CalculateReducedRelaxationFunction(QuasiLinearViscoelasticityModelInput input, double time)
        {
            if (time <= Constants.Precision)
            {
                return 1;
            }

            if (time <= 1)
            {
                return (1 - input.RelaxationIndex * Constants.EulerMascheroniConstant - input.RelaxationIndex * Math.Log(time / input.SlowRelaxationTime)) / (1 + input.RelaxationIndex * Math.Log(input.SlowRelaxationTime / input.FastRelaxationTime));
            }

            return (1 + input.RelaxationIndex * (this.CalculateE1(input, time / input.SlowRelaxationTime) - this.CalculateE1(input, time / input.FastRelaxationTime))) / (1 + input.RelaxationIndex * Math.Log(input.SlowRelaxationTime / input.FastRelaxationTime));
        }

        private double CalculateE1(QuasiLinearViscoelasticityModelInput input, double variable)
        {
            var integralInput = new IntegralInput
            {
                InitialPoint = variable,
                Precision = Constants.Precision,
                Step = input.TimeStep
            };

            return this._simpsonRuleIntegration.Calculate((parameter) => Math.Exp(-parameter) / parameter, integralInput);
        }

        public override double CalculateStress(QuasiLinearViscoelasticityModelInput input, double time)
        {
            var integralInput = new IntegralInput
            {
                InitialPoint = 0,
                FinalPoint = time,
                Step = input.TimeStep
            };

            return this.CalculateElasticResponse(input, time: 0) * this.CalculateReducedRelaxationFunction(input, time) +
                this._simpsonRuleIntegration.Calculate((equationTime) => this.CalculateReducedRelaxationFunction(input, time - equationTime) * this.CalculateElasticResponseDerivative(input, equationTime), integralInput);
        }

        public double CalculateStressByIntegrationDerivative(QuasiLinearViscoelasticityModelInput input, double time)
        {
            return this._derivative.Calculate((derivativeTime) =>
            {
                var integralInput = new IntegralInput
                {
                    InitialPoint = 0,
                    FinalPoint = derivativeTime,
                    Step = input.TimeStep
                };

                return this._simpsonRuleIntegration.Calculate((integrationTime) =>
                {
                    return this.CalculateElasticResponse(input, time - integrationTime) * this.CalculateReducedRelaxationFunction(input, integrationTime);
                },
                integralInput);
            },
            input.TimeStep, time);
        }

        public double CalculateStressByReducedRelaxationFunctionDerivative(QuasiLinearViscoelasticityModelInput input, double time)
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
                    return this.CalculateElasticResponse(input, integrationTime) * this._derivative.Calculate((derivativeTime) =>
                    {
                        return this.CalculateReducedRelaxationFunction(input, derivativeTime);
                    }, input.TimeStep, time);
                },
                integralInput);
        }
    }
}
