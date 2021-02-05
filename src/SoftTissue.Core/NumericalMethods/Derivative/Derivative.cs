using SoftTissue.Core.Models.Viscoelasticity;
using System;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public class Derivative : IDerivative
    {
        public const double Epsilon = 2.2e-16;

        public double Calculate<TInput>(Func<TInput, double, double> Equation, TInput input, double time)
            where TInput : ViscoelasticModelInput
        {
            double actualValue = Equation(input, time);
            double nextValue = Equation(input, time + input.TimeStep);

            return (nextValue - actualValue) / input.TimeStep;
        }

        public double Calculate(Func<double, double> Equation, double step, double time)
        {
            double actualValue = Equation(time);
            double nextValue = Equation(time + step);

            return (nextValue - actualValue) / step;
        }

        public double Calculate(double initialPoint, double finalPoint, double step)
        {
            return (finalPoint - initialPoint) / step;
        }
    }
}
