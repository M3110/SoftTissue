using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel
{
    public class CalculateMaxwellModelStress : CalculateLinearViscosityStress, ICalculateMaxwellModelStress
    {
        public CalculateMaxwellModelStress(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }
    }
}
