using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity;
using System;
using System.Linq;

namespace SoftTissue.Core.NumericalMethods.Integral.Simpson
{
    public class SimpsonRuleIntegration : ISimpsonRuleIntegration
    {
        public double Calculate<TInput>(Func<TInput, double, double> Equation, TInput equationInput, IntegralInput integralInput)
            where TInput : ViscoelasticModelInput
        {
            double finalPoint = integralInput.InitialPoint;

            if (integralInput.FinalPoint != null)
            {
                finalPoint = integralInput.FinalPoint.Value;
            }
            else
            {
                while (Equation(equationInput, finalPoint) >= integralInput.Precision)
                {
                    finalPoint += integralInput.Step;
                }
            }

            double numberOfDivisions = (finalPoint - integralInput.InitialPoint) / integralInput.Step;

            // TODO: Ver como funciona o Aggregate ou Sum.
            // Checar no MoreLinq.

            //double resultTemp = Enumerable.Range(0, 10).Aggregate(0, (accumulator, result, index) =>
            //{
            //    if (index == 0 || index == integralInput.Step)
            //    {
            //        return result += Equation(equationInput, integralInput.InitialPoint + index * integralInput.Step);
            //    }
                
            //    if (index % 2 != 0)
            //    {
            //        return result += 4 * Equation(equationInput, integralInput.InitialPoint + index * integralInput.Step);
            //    }
                    
            //    return result += 2 * Equation(equationInput, integralInput.InitialPoint + index * integralInput.Step);
            //});

            double result = 0;

            for (int i = 0; i <= numberOfDivisions; i++)
            {
                double multiplyfactor = i == 0 || i == integralInput.Step ? 
                    1 : 
                    i % 2 != 0 ? 4 : 2;

                result += multiplyfactor * Equation(equationInput, integralInput.InitialPoint + i * integralInput.Step);
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
                if (i == 0 || i == integralInput.FinalPoint)
                {
                    result += Equation(integralInput.InitialPoint + i * integralInput.Step);
                }
                else if (i % 2 != 0)
                {
                    result += 4 * Equation(integralInput.InitialPoint + i * integralInput.Step);
                }
                else if (i % 2 == 0)
                {
                    result += 2 * Equation(integralInput.InitialPoint + i * integralInput.Step);
                }
            }

            result *= integralInput.Step / 3;

            return result;
        }
    }
}
