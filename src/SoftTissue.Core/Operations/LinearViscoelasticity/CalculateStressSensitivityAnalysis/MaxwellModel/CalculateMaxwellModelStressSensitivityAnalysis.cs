using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using System.IO;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStressSensitivityAnalysis.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the stress to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelStressSensitivityAnalysis : CalculateLinearViscosityStressSensitivityAnalysis<MaxwellModelInput>, ICalculateMaxwellModelStressSensitivityAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelStressSensitivityAnalysis(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(Constants.MaxwellModelBasePath, "Stress", "Sensitivity Analysis");
    }
}
