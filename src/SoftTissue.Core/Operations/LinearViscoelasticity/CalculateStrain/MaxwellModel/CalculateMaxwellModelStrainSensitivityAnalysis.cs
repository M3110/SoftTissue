using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the strain to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelStrainSensitivityAnalysis : CalculateLinearViscosityStrain<CalculateStrainSensitivityAnalysisRequest>, ICalculateMaxwellModelStrainSensitivityAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelStrainSensitivityAnalysis(IMaxwellModel viscoelasticModel) : base(viscoelasticModel)
        {
            this.LoopIndex = 0;
        }

        /// <summary>
        /// The base path to files.
        /// </summary>
        private static readonly string TemplateBasePath = Path.Combine(Directory.GetCurrentDirectory(), "sheets/Solutions/Linear Viscosity/Maxwell Model/Strain/Sensitivity Analysis");

        /// <summary>
        /// The loop index that is used in files names.
        /// </summary>
        public int LoopIndex { get; set; }

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<LinearViscoelasticityModelInput> BuildInputList(CalculateStrainSensitivityAnalysisRequest request)
        {
            var inputList = new List<LinearViscoelasticityModelInput>();

            foreach (var initialStress in request.InitialStressList)
            {
                foreach (var stiffness in request.StiffnessList)
                {
                    foreach (var viscosity in request.ViscosityList)
                    {
                        inputList.Add(new LinearViscoelasticityModelInput
                        {
                            FinalTime = request.FinalTime.Value,
                            TimeStep = request.TimeStep.Value,
                            InitialTime = request.InitialTime.Value,
                            InitialStress = initialStress,
                            Stiffness = stiffness,
                            Viscosity = viscosity
                        });
                    }
                }
            }

            return inputList;
        }

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
