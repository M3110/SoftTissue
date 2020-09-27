using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models;
using System.IO;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the stress to Fung model.
    /// </summary>
    public class CalculateFungModelStressSentivityAnalysis : CalculateQuasiLinearViscoelasticityStress, ICalculateFungModelStressSentivityAnalysis
    {
        private readonly IFungModel _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStressSentivityAnalysis(IFungModel viscoelasticModel) : base(viscoelasticModel) 
        {
            this.LoopIndex = 0;
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// The base path to files.
        /// </summary>
        private static readonly string TemplateBasePath = Path.Combine(Directory.GetCurrentDirectory(), "sheets/Solutions/Quasi-Linear Viscosity/Fung Model/Stress/Sensitivity Analysis");

        /// <summary>
        /// The header to solution file.
        /// </summary>
        public override string SolutionFileHeader => "Time;Strain;Reduced Relaxation Function;Elastic Response;Stress with derivative;Stress with dG;Stress with dSigma";

        /// <summary>
        /// The loop index that is used in files names.
        /// </summary>
        public int LoopIndex { get; set; }

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override string CreateInputDataFile(QuasiLinearViscoelasticityModelInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"InputData_{this.LoopIndex}.csv"));

            if (fileInfo.Exists == false && fileInfo.Directory.Exists == false)
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
        public override string CreateSolutionFile(QuasiLinearViscoelasticityModelInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Solution_{this.LoopIndex}.csv"));


            if (fileInfo.Exists == false && fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            // The loop index is iterated just when creating the solution file to keep the same index with the input data file.
            this.LoopIndex++;

            return fileInfo.FullName;
        }

        /// <summary>
        /// This method calculates the results and writes them to a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <param name="streamWriter"></param>
        public override void CalculateAndWriteResults(QuasiLinearViscoelasticityModelInput input, double time, StreamWriter streamWriter)
        {
            double strain = this._viscoelasticModel.CalculateStrain(input, time);
            double reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
            double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);
            double stress = this._viscoelasticModel.CalculateStress(input, time);
            double stressWithElasticResponseDerivative = this._viscoelasticModel.CalculateStressByIntegrationDerivative(input, time);
            double stressWithReducedRelaxationFunctionDerivative = this._viscoelasticModel.CalculateStressByReducedRelaxationFunctionDerivative(input, time);

            streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stress};{stressWithElasticResponseDerivative};{stressWithReducedRelaxationFunctionDerivative}");
        }
    }
}
