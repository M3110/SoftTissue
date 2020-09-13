using SoftTissue.Core.Models;
using System.Threading.Tasks;

namespace SoftTissue.Core.NumericalMethods.Derivative
{
    public interface IDerivative
    {
        Task<double> Calculate<TInput>(IEquation<TInput> equation, TInput input, double time)
            where TInput : ViscoelasticModelInput;
    }
}