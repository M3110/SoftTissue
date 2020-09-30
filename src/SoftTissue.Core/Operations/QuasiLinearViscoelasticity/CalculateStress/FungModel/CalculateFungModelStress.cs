using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Fung model.
    /// </summary>
    public class CalculateFungModelStress : CalculateQuasiLinearViscoelasticityStress<CalculateFungModelStressRequest, FungModelInput>, ICalculateFungModelStress
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
        public override string SolutionFileHeader => "Time;Strain;Reduced Relaxation Function;Elastic Response;Stress with dSigma;Stress with dG;Stress with integral derivative";


        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<FungModelInput> BuildInputList(CalculateFungModelStressRequest request)
        {
            var inputList = new List<FungModelInput>();

            foreach (var requestData in request.RequestDataList)
            {
                var input = new FungModelInput
                {
                    ElasticStressConstant = requestData.ElasticStressConstant,
                    ElasticPowerConstant = requestData.ElasticPowerConstant,
                    MaximumStrain = requestData.MaximumStrain,
                    StrainRate = requestData.StrainRate,
                    FinalTime = requestData.FinalTime ?? request.FinalTime,
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
                    InitialTime = requestData.InitialTime ?? request.InitialTime,
                    SoftTissueType = requestData.SoftTissueType,
                    UseSimplifiedReducedRelaxationFunction = requestData.UseSimplifiedReducedRelaxationFunction
                };

                if (requestData.UseSimplifiedReducedRelaxationFunction == true)
                {
                    var simplifiedReducedRelaxationFunctionInputList = new List<SimplifiedRelaxationFunctionRequestData>();

                    input.SimplifiedReducedRelaxationFunctionInputList = simplifiedReducedRelaxationFunctionInputList;

                    throw new NotImplementedException($"The method '{nameof(BuildInputList)}' does not have an implementation to '{nameof(requestData.UseSimplifiedReducedRelaxationFunction)}' when it is {requestData.UseSimplifiedReducedRelaxationFunction}.");
                }
                else
                {
                    var reducedRelaxationFunctionInput = new ReducedRelaxationFunctionInput
                    {
                        RelaxationIndex = requestData.RelaxationIndex,
                        FastRelaxationTime = requestData.FastRelaxationTime,
                        SlowRelaxationTime = requestData.SlowRelaxationTime,
                    };

                    input.ReducedRelaxationFunctionInput = reducedRelaxationFunctionInput;
                }

                inputList.Add(input);
            }

            return inputList;
        }

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="streamWriter"></param>
        public override void WriteInputDataInFile(FungModelInput input, StreamWriter streamWriter)
        {
            base.WriteInputDataInFile(input, streamWriter);

            if (input.UseSimplifiedReducedRelaxationFunction == true)
            {
                var variablesE = new List<double>();
                var relaxationTimes = new List<double>();
                foreach (var simplifiedReducedRelaxationFunctionInput in input.SimplifiedReducedRelaxationFunctionInputList)
                {
                    variablesE.Add(simplifiedReducedRelaxationFunctionInput.VariableE);
                    relaxationTimes.Add(simplifiedReducedRelaxationFunctionInput.RelaxationTime);
                }

                streamWriter.WriteLine($"Variables E: {string.Join(";", variablesE)}");
                streamWriter.WriteLine($"Relaxation Times: {string.Join(";", relaxationTimes)}");
            }
            else
            {
                streamWriter.WriteLine($"Relaxation Index (C): {input.ReducedRelaxationFunctionInput.RelaxationIndex}");
                streamWriter.WriteLine($"Fast Relaxation Time (Tau1): {input.ReducedRelaxationFunctionInput.FastRelaxationTime} s");
                streamWriter.WriteLine($"Slow Relaxation Time (Tau2): {input.ReducedRelaxationFunctionInput.SlowRelaxationTime} s");
            }
        }

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override string CreateInputDataFile(FungModelInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"InputData_{input.SoftTissueType}.csv"));

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
        public override string CreateSolutionFile(FungModelInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Solution_{input.SoftTissueType}.csv"));

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
        public override void CalculateAndWriteResults(FungModelInput input, double time, StreamWriter streamWriter)
        {
            double strain = this._viscoelasticModel.CalculateStrain(input, time);

            double reducedRelaxationFunction;
            if (input.UseSimplifiedReducedRelaxationFunction == true)
            {
                reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunctionSimplified(input, time);
            }
            else
            {
                reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
            }

            double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);

            // TODO: Alterar métodos que calculam a tensão para usar a equação certa do ReducedRelaxationFunction.
            double stressWithElasticResponseDerivative = this._viscoelasticModel.CalculateStress(input, time);

            double stressWithReducedRelaxationFunctionDerivative = this._viscoelasticModel.CalculateStressByReducedRelaxationFunctionDerivative(input, time);

            double stressWithIntegrationDerivative = this._viscoelasticModel.CalculateStressByIntegrationDerivative(input, time);

            streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stressWithElasticResponseDerivative};{stressWithReducedRelaxationFunctionDerivative};{stressWithIntegrationDerivative}");
        }
    }
}
