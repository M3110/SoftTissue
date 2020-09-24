using SoftTissue.Core.Models;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    public interface IFungModel : IQuasiLinearViscoelasticityModel 
    {
        double CalculateStressByIntegrationDerivative(QuasiLinearViscoelasticityModelInput input, double time);

        double CalculateStressByReducedRelaxationFunctionDerivative(QuasiLinearViscoelasticityModelInput input, double time);
    }
}
