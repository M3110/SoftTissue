using SoftTissue.Core.Models;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using System;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    public class FungModel : QuasiLinearViscoelasticityModel, IFungModel
    {
        private readonly ISimpsonRuleIntegration _simpsonRuleIntegration;

        public FungModel(ISimpsonRuleIntegration simpsonRuleIntegration)
        {
            this._simpsonRuleIntegration = simpsonRuleIntegration;
        }

        public override Task<double> CalculateElasticResponse(QuasiLinearViscoelasticityModelInput input, double strain)
        {
            double result = input.VariableA * (Math.Exp(input.VariableB * strain) - 1);

            return Task.FromResult(result);
        }

        public override Task<double> CalculateElasticResponseDerivative(QuasiLinearViscoelasticityModelInput input, double strain)
        {
            double result = input.VariableA * input.VariableB * Math.Exp(input.VariableB * strain);

            return Task.FromResult(result);
        }

        public override Task<double> CalculateReducedRelaxationFunction(QuasiLinearViscoelasticityModelInput input, double time)
        {
            if (time == 0)
            {
                return Task.FromResult<double>(1);
            }

            double result = (1 + input.VariableC * (this.CalculateE1(time / input.RelaxationTime2) - this.CalculateE1(time / input.RelaxationTime1))) / (1 + input.VariableC * Math.Log(input.RelaxationTime2 / input.RelaxationTime1));

            return Task.FromResult(result);
        }

        public override Task<double> CalculateReducedRelaxationFunctionSimplified(QuasiLinearViscoelasticityModelInput input, double time)
        {
            double result = 0;

            foreach (var simplifiedInput in input.RelaxationFunctionSimplifiedInputList)
            {
                result += simplifiedInput.VariableC * Math.Exp(-simplifiedInput.RelaxationTime / time);
            }

            return Task.FromResult(result);
        }

        public async override Task<double> CalculateStress(QuasiLinearViscoelasticityModelInput input, double time, double strain)
        {
            if (time == 0)
            {
                return 0;
            }

            double reducedRelaxationFunction = await this.CalculateReducedRelaxationFunction(input, time).ConfigureAwait(false);
            double elasticResponseDerivative = await this.CalculateElasticResponseDerivative(input, strain).ConfigureAwait(false);

            double result = 0;

            return result;
        }

        private double CalculateE1(double variable)
        {
            // Original equation:
            //  f(x) = e^(-x)/x
            // Transformed equation:
            //  f(t) = e^(-(a + t/(1 - t)))/(1 - t)^2
            //  a --> initial point
            // This step is necessary the interval of original equation goes to infinity and it's impossible to integrate.
            // With that step, the interval is changed to 0 from 1.
            Func<double, double> equation = (parameter) =>
            {
                return Math.Exp(-(variable + (parameter / (1 - parameter)))) / (variable + (parameter / (1 - parameter))) / Math.Pow(1 - parameter, 2);
            };

            double numberOfDivisions = 10;
            double step = 1 / numberOfDivisions;

            double result = 0;

            for (int i = 0; i < numberOfDivisions; i++)
            {
                if (i == 0)
                {
                    result += equation(variable);
                }
                else if (i % 2 != 0)
                {
                    result += 4 * equation(i * step);
                }
                else
                {
                    result += 2 * equation(i * step);
                }
            }

            result *= step / 3;

            return result;
        }
    }
}
