using SoftTissue.Core.Models;
using System;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public class Derivative : IDerivative
    {
        public const double Epsilon = 2.2e-16;

        public double Calculate<TInput>(Func<TInput, double, double> Equation, TInput input, double variable)
            where TInput : ViscoelasticModelInput
        {
            double stepTime;
            if (variable == 0)
            {
                stepTime = Math.Sqrt(Epsilon);
            }
            else
            {
                stepTime = Math.Sqrt(Epsilon) * variable;
            }

            double actualValue = Equation(input, variable);
            double nextValue = Equation(input, variable + stepTime);

            return (nextValue - actualValue) / stepTime;
        }

        public double Calculate(Func<double, double> Equation, double variable)
        {
            double stepTime;
            if (variable == 0)
            {
                stepTime = Math.Sqrt(Epsilon);
            }
            else
            {
                stepTime = Math.Sqrt(Epsilon) * variable;
            }

            double actualValue = Equation(variable);
            double nextValue = Equation(variable + stepTime);

            return (nextValue - actualValue) / stepTime;
        }
    }
}
