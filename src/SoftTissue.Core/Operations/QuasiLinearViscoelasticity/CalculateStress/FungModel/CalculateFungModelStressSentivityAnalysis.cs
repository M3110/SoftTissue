using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to do a sensitivity analysis while calculating the stress to Fung model.
    /// </summary>
    public class CalculateFungModelStressSentivityAnalysis : CalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<FungModelInput, FungModelResult>, ICalculateFungModelStressSentivityAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStressSentivityAnalysis(IFungModel viscoelasticModel) : base(viscoelasticModel) { }
    }
}
