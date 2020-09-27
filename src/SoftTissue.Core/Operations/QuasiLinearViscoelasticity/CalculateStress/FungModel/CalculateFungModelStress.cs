using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Fung model.
    /// </summary>
    public class CalculateFungModelStress : CalculateQuasiLinearViscoelasticityStress<CalculateFungModelStressRequest>, ICalculateFungModelStress
    {
        private readonly IFungModel _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStress(IFungModel viscoelasticModel) : base(viscoelasticModel) 
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// The base path.
        /// </summary>
        private static readonly string TemplateBasePath = Path.Combine(Directory.GetCurrentDirectory(), "sheets/Solutions/Quasi-Linear Viscosity/Fung Model/Stress");

        /// <summary>
        /// The header to solution file.
        /// </summary>
        public override string SolutionFileHeader => "Time;Strain;Reduced Relaxation Function;Elastic Response;Stress with derivative;Stress with dG;Stress with dSigma";


        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<QuasiLinearViscoelasticityModelInput> BuildInputList(CalculateFungModelStressRequest request)
        {
            var inputList = new List<QuasiLinearViscoelasticityModelInput>();

            foreach (var requestData in request.RequestDataList)
            {
                inputList.Add(new QuasiLinearViscoelasticityModelInput
                {
                    ElasticStressConstant = requestData.ElasticStressConstant,
                    ElasticPowerConstant = requestData.ElasticPowerConstant,
                    RelaxationIndex = requestData.RelaxationIndex,
                    FastRelaxationTime = requestData.FastRelaxationTime,
                    SlowRelaxationTime = requestData.SlowRelaxationTime,
                    MaximumStrain = requestData.MaximumStrain,
                    StrainRate = requestData.StrainRate,
                    FinalTime = request.FinalTime ?? requestData.FinalTime,
                    TimeStep = request.TimeStep ?? requestData.TimeStep,
                    InitialTime = request.InitialTime ?? requestData.InitialTime,
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
        public override string CreateInputDataFile(QuasiLinearViscoelasticityModelInput input)
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
        public override string CreateSolutionFile(QuasiLinearViscoelasticityModelInput input)
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
