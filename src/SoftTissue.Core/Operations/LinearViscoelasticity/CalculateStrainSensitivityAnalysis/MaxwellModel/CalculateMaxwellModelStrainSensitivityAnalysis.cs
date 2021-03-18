using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using System.IO;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the strain to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelStrainSensitivityAnalysis : CalculateLinearViscosityStrainSensitivityAnalysis<MaxwellModelInput>, ICalculateMaxwellModelStrainSensitivityAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelStrainSensitivityAnalysis(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(BasePaths.MaxwellModel, "Strain", "Sensitivity Anlysis");
    }
}
