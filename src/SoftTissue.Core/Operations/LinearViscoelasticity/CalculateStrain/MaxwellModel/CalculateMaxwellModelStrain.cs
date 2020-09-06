using SoftTissue.Core.ConstitutiveEquations.LinearModel;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel
{
    public class CalculateMaxwellModelStrain : CalculateStrain, ICalculateMaxwellModelStrain
    {
        public CalculateMaxwellModelStrain(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }
    }
}
