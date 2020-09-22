using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    public class CalculateFungModelStress : CalculateQuasiLinearViscoelasticityStress, ICalculateFungModelStress
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStress(IFungModel viscoelasticModel) : base(viscoelasticModel) { }
    }
}
