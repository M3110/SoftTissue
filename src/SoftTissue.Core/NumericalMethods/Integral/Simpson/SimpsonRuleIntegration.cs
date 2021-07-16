using SoftTissue.Core.Models;
using System;
using System.Threading.Tasks;

namespace SoftTissue.Core.NumericalMethods.Integral.Simpson
{
    public class SimpsonRuleIntegration : ISimpsonRuleIntegration
    {
        public async Task<double> CalculateAsync(Func<double, Task<double>> Equation, IntegralInput integralInput)
        {
            double finalPoint = integralInput.InitialPoint;

            if (integralInput.FinalPoint != null)
            {
                finalPoint = integralInput.FinalPoint.Value;
            }
            else
            {
                while (await Equation(finalPoint).ConfigureAwait(false) >= integralInput.Precision)
                {
                    finalPoint += integralInput.Step;
                }
            }

            double numberOfDivisions = (finalPoint - integralInput.InitialPoint) / integralInput.Step;

            // If the number of divisions is equals to zero, it means that the interval is null, so the integral must be zero.
            if (numberOfDivisions == 0)
            {
                return 0;
            }

            double result = 0;

            for (int i = 0; i <= numberOfDivisions; i++)
            {
                double multiplyfactor = i == 0 || i == integralInput.Step ? 1 : i % 2 != 0 ? 4 : 2;

                result += multiplyfactor * await Equation(integralInput.InitialPoint + i * integralInput.Step).ConfigureAwait(false);
            }

            result *= integralInput.Step / 3;

            return result;
        }

        public double Calculate(Func<double, double> Equation, IntegralInput integralInput)
        {
            double finalPoint = integralInput.InitialPoint;

            if (integralInput.FinalPoint != null)
            {
                finalPoint = integralInput.FinalPoint.Value;
            }
            else
            {
                while (Equation(finalPoint) >= integralInput.Precision)
                {
                    finalPoint += integralInput.Step;
                }
            }

            double numberOfDivisions = (finalPoint - integralInput.InitialPoint) / integralInput.Step;

            // If the number of divisions is equals to zero, it means that the interval is null, so the integral must be zero.
            if (numberOfDivisions == 0)
            {
                return 0;
            }

            double result = 0;

            for (int i = 0; i <= numberOfDivisions; i++)
            {
                double multiplyfactor = i == 0 || i == integralInput.Step ? 1 : i % 2 != 0 ? 4 : 2;

                result += multiplyfactor * Equation(integralInput.InitialPoint + i * integralInput.Step);
            }

            result *= integralInput.Step / 3;

            return result;
        }
    }
}
