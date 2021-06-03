using SoftTissue.Core.Models.Viscoelasticity;
using System;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public class Derivative : IDerivative
    {
        public double Calculate<TInput>(Func<TInput, double, double> Equation, TInput input, double time)
            where TInput : ViscoelasticModelInput
        {
            double previous = Equation(input, time - input.TimeStep);
            double nextValue = Equation(input, time + input.TimeStep);

            return (nextValue - previous) / (2 * input.TimeStep);
        }

        public double Calculate(Func<double, double> Equation, double step, double time)
        {
            double previous = Equation(time - step);
            double nextValue = Equation(time + step);

            return (nextValue - previous) / (2 * step);
        }

        public double Calculate(double initialPoint, double finalPoint, double step)
        {
            return (finalPoint - initialPoint) / step;
        }
    }
}
