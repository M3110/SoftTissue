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
            //double stepTime;
            //if (variable == 0)
            //{
            //    stepTime = Math.Sqrt(Epsilon);
            //}
            //else
            //{
            //    stepTime = Math.Sqrt(Epsilon) * variable;
            //}

            double actualValue = Equation(input, time);
            double nextValue = Equation(input, time + input.TimeStep);

            return (nextValue - actualValue) / input.TimeStep;
        }

        public double Calculate(Func<double, double> Equation, double step, double time)
        {
            //double stepTime;
            //if (variable == 0)
            //{
            //    stepTime = Math.Sqrt(Epsilon);
            //}
            //else
            //{
            //    stepTime = Math.Sqrt(Epsilon) * variable;
            //}

            double actualValue = Equation(time);
            double nextValue = Equation(time + step);

            return (nextValue - actualValue) / step;
        }
    }
}
