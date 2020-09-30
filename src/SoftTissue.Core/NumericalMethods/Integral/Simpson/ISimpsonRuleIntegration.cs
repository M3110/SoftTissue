using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity;
using System;

namespace SoftTissue.Core.NumericalMethods.Integral.Simpson
{
    public interface ISimpsonRuleIntegration
    {
        double Calculate<TInput>(Func<TInput, double, double> Equation, TInput equationInput, IntegralInput integralInput)
            where TInput : ViscoelasticModelInput;

        double Calculate(Func<double, double> Equation, IntegralInput integralInput);
    }
}
