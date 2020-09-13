using SoftTissue.Core.Models;
using System;

namespace SoftTissue.Core.NumericalMethods.Integral.Simpson
{
    public interface ISimpsonRuleIntegration
    {
        double Calculate<TInput>(Func<TInput, double, double> equation, TInput equationInput, IntegralInput integralInput)
            where TInput : ViscoelasticModelInput;

        double Calculate(Func<double, double> equation, IntegralInput integralInput);
    }
}
