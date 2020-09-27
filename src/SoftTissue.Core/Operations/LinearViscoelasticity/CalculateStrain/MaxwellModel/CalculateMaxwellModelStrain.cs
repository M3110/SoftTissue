using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the strain to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelStrain : CalculateLinearViscosityStrain<CalculateStrainRequest>, ICalculateMaxwellModelStrain
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelStrain(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// The base path to files.
        /// </summary>
        private static readonly string TemplateBasePath = Path.Combine(Directory.GetCurrentDirectory(), "sheets/Solutions/Linear Viscosity/Maxwell Model/Strain");

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<LinearViscoelasticityModelInput> BuildInputList(CalculateStrainRequest request)
        {
            var inputList = new List<LinearViscoelasticityModelInput>();

            foreach (var requestData in request.RequestDataList)
            {
                inputList.Add(new LinearViscoelasticityModelInput
                {
                    FinalTime = request.FinalTime ?? requestData.FinalTime,
                    TimeStep = request.TimeStep ?? requestData.TimeStep,
                    InitialTime = request.InitialTime ?? requestData.InitialTime,
                    InitialStress = requestData.InitialStress,
                    Stiffness = requestData.Stiffness,
                    Viscosity = requestData.Viscosity,
                    AnalysisType = requestData.AnalysisType
                });
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
                $"InputData_{input.AnalysisType}.csv"));

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
                $"Solution_{input.AnalysisType}.csv"));

            if (fileInfo.Exists == false || fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }
    }
}
