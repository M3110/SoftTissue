using System;
using System.Threading.Tasks;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public interface IDerivative
    {
        Task<double> CalculateAsync(Func<double, Task<double>> Equation, double step, double time);

        double Calculate(double initialPoint, double finalPoint, double step);
    }
}