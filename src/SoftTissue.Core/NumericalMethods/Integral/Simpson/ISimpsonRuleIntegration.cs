using SoftTissue.Core.Models;
using System;
using System.Threading.Tasks;

namespace SoftTissue.Core.NumericalMethods.Integral.Simpson
{
    public interface ISimpsonRuleIntegration
    {
        Task<double> CalculateAsync(Func<double, Task<double>> Equation, IntegralInput integralInput);

        double Calculate(Func<double, double> Equation, IntegralInput integralInput);
    }
}
