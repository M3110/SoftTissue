using SoftTissue.Core.Models;
using System;

namespace SoftTissue.Core.NumericalMethods.Integral.Simpson
{
    public class SimpsonRuleIntegration : ISimpsonRuleIntegration
    {
        public double Calculate<TInput>(Func<TInput, double, double> equation, TInput equationInput, IntegralInput integralInput)
            where TInput : ViscoelasticModelInput
        {
            double step = (integralInput.FinalPoint - integralInput.InitialPoint) / integralInput.PeriodDivision;

            double result = 0;

            for (int i = 0; i <= integralInput.PeriodDivision; i++)
            {
                if (i == 0 || i == integralInput.PeriodDivision)
                {
                    result += equation(equationInput, integralInput.InitialPoint + i * step);
                }
                else if (i % 2 != 0)
                {
                    result += 4 * equation(equationInput, integralInput.InitialPoint + i * step); ;
                }
                else
                {
                    result += 2 * equation(equationInput, integralInput.InitialPoint + i * step); ;
                }
            }

            result *= step / 3;

            return result;
        }

        public double Calculate(Func<double, double> equation, IntegralInput integralInput)
        {
            double step = (integralInput.FinalPoint - integralInput.InitialPoint) / integralInput.PeriodDivision;

            double result = 0;

            for (int i = 0; i <= integralInput.PeriodDivision; i++)
            {
                if (i == 0 || i == integralInput.PeriodDivision)
                {
                    result += equation(integralInput.InitialPoint + i * step);
                }
                else if (i % 2 != 0)
                {
                    result += 4 * equation(integralInput.InitialPoint + i * step); ;
                }
                else
                {
                    result += 2 * equation(integralInput.InitialPoint + i * step); ;
                }
            }

            result *= step / 3;

            return result;
        }
    }
}
