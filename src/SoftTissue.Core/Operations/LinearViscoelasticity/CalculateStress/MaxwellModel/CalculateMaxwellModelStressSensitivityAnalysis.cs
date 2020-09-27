using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using System.IO;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the stress to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelStressSensitivityAnalysis : CalculateLinearViscosityStress, ICalculateMaxwellModelStressSensitivityAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelStressSensitivityAnalysis(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) 
        {
            this.LoopIndex = 0;
        }

        /// <summary>
        /// The base path to files.
        /// </summary>
        private static readonly string TemplateBasePath = Path.Combine(Directory.GetCurrentDirectory(), "sheets/Solutions/Linear Viscosity/Maxwell Model/Stress/Sensitivity Analysis");

        /// <summary>
        /// The loop index that is used in files names.
        /// </summary>
        public int LoopIndex { get; set; }

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override string CreateInputDataFile(LinearViscoelasticityModelInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"InputData_{this.LoopIndex}.csv"));

            if (fileInfo.Exists == false || fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override string CreateSolutionFile(LinearViscoelasticityModelInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Solution_{this.LoopIndex}.csv"));

            if (fileInfo.Exists == false || fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            this.LoopIndex++;

            return fileInfo.FullName;
        }
    }
}
