using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    public interface IFungModel : IQuasiLinearViscoelasticityModel<FungModelInput>
    {
        double CalculateStressByIntegrationDerivative(FungModelInput input, double time);

        double CalculateStressByReducedRelaxationFunctionDerivative(FungModelInput input, double time);
    }
}
