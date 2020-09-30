using SoftTissue.Core.Models.Viscoelasticity;

namespace SoftTissue.Core.NumericalMethods
{
    public interface IEquation<TInput>
        where TInput : ViscoelasticModelInput
    {
        double Calculate(TInput input, double independentVariable);
    }
}
