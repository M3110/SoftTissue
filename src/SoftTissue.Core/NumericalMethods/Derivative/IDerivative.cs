using SoftTissue.Core.Models.Viscoelasticity;
using System;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public interface IDerivative
    {
        double Calculate<TInput>(Func<TInput, double, double> Equation, TInput input, double time)
            where TInput : ViscoelasticModelInput;

        double Calculate(Func<double, double> Equation, double step, double time);

        double Calculate(double initialPoint, double finalPoint, double step);
    }
}