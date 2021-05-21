using SoftTissue.Core.Models.Viscoelasticity;
using System;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public class Derivative : IDerivative
    {
        // TODO: Melhorar o método de derivada utilizado, para que aumente a precisão do valor calculado.
        // PROPOSTA: Usar um método de ordem maior.

        public double Calculate<TInput>(Func<TInput, double, double> Equation, TInput input, double time)
            where TInput : ViscoelasticModelInput
        {
            double currentValue = Equation(input, time - input.TimeStep);
            double nextValue = Equation(input, time + input.TimeStep);

            return (nextValue - currentValue) / (2 * input.TimeStep);
        }

        public double Calculate(Func<double, double> Equation, double step, double time)
        {
            double currentValue = Equation(time - step);
            double nextValue = Equation(time + step);

            return (nextValue - currentValue) / (2 * step);
        }

        public double Calculate(double initialPoint, double finalPoint, double step)
        {
            return (finalPoint - initialPoint) / step;
        }
    }
}
