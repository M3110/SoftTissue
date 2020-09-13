using SoftTissue.Core.Models;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel
{
    public abstract class QuasiLinearViscoelasticityModel : ViscoelasticModel<QuasiLinearViscoelasticityModelInput>, IQuasiLinearViscoelasticityModel
    {
        public abstract Task<double> CalculateElasticResponse(QuasiLinearViscoelasticityModelInput input, double strain);

        public abstract Task<double> CalculateElasticResponseDerivative(QuasiLinearViscoelasticityModelInput input, double strain);

        public abstract Task<double> CalculateReducedRelaxationFunction(QuasiLinearViscoelasticityModelInput input, double time);

        public abstract Task<double> CalculateReducedRelaxationFunctionSimplified(QuasiLinearViscoelasticityModelInput input, double time);
    }
}