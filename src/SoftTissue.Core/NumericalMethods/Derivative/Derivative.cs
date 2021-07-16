using System;
using System.Threading.Tasks;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public class Derivative : IDerivative
    {
        public async Task<double> CalculateAsync(Func<double, Task<double>> Equation, double step, double time)
        {
            double previous = await Equation(time - step).ConfigureAwait(false);
            double nextValue = await Equation(time + step).ConfigureAwait(false);

            return (nextValue - previous) / (2 * step);
        }

        public double Calculate(double initialPoint, double finalPoint, double step)
        {
            return (finalPoint - initialPoint) / step;
        }
    }
}
