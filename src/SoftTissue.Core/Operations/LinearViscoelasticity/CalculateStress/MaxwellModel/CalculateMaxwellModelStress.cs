using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel
{
    public class CalculateMaxwellModelStress : CalculateStress, ICalculateMaxwellModelStress
    {
        public CalculateMaxwellModelStress(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }
    }
}
