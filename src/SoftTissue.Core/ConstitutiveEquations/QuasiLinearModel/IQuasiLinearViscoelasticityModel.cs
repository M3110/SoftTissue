using SoftTissue.Core.Models;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public interface IQuasiLinearViscoelasticityModel : IViscoelasticModel<QuasiLinearViscoelasticityModelInput>
    {
        Task<double> CalculateElasticResponse(QuasiLinearViscoelasticityModelInput input, double strain);

        Task<double> CalculateElasticResponseDerivative(QuasiLinearViscoelasticityModelInput input, double strain);

        Task<double> CalculateReducedRelaxationFunction(QuasiLinearViscoelasticityModelInput input, double time);

        Task<double> CalculateReducedRelaxationFunctionSimplified(QuasiLinearViscoelasticityModelInput input, double time);
    }
}
