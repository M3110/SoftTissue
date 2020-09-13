using SoftTissue.Core.Models;
using System;
using System.Threading.Tasks;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public class Derivative : IDerivative
    {
        public const double Epsilon = 2.2e-16;

        public Task<double> Calculate<TInput>(IEquation<TInput> equation, TInput input, double time)
            where TInput : ViscoelasticModelInput
        {
            double stepTime;
            if (time == 0)
            {
                stepTime = Math.Sqrt(Epsilon);
            }
            else
            {
                stepTime = Math.Sqrt(Epsilon) * time;
            }

            double actualValue = equation.Calculate(input, time);
            double nextValue = equation.Calculate(input, time + stepTime);

            double result = (nextValue - actualValue) / stepTime;
            return Task.FromResult(result);
        }
    }
}
