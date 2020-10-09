using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using System;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the stress to Fung model.
    /// </summary>
    public class CalculateFungModelStressSentivityAnalysis : CalculateQuasiLinearViscoelasticityStress<CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest, FungModelInput, FungModelResult>, ICalculateFungModelStressSentivityAnalysis
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

        public override List<FungModelInput> BuildInputList(CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest request)
        {
            var inputList = new List<FungModelInput>();

            foreach (double strainRate in request.StrainRateList)
            {
                foreach (double maximumStrain in request.MaximumStrainList)
                {
                    foreach (double elasticStressConstant in request.ElasticStressConstantList)
                    {
                        foreach (double elasticPowerConstant in request.ElasticPowerConstantList)
                        {
                            if (request.UseSimplifiedReducedRelaxationFunction == true)
                            {
                                foreach (var simplifiedReducedRelaxationFunctionData in request.SimplifiedReducedRelaxationFunctionDataList)
                                {
                                    inputList.Add(new FungModelInput
                                    {
                                        StrainRate = strainRate,
                                        MaximumStrain = maximumStrain,
                                        ElasticStressConstant = elasticStressConstant,
                                        ElasticPowerConstant = elasticPowerConstant,
                                        ReducedRelaxationFunctionInput = null,
                                        SimplifiedReducedRelaxationFunctionInput = simplifiedReducedRelaxationFunctionData,
                                        UseSimplifiedReducedRelaxationFunction = request.UseSimplifiedReducedRelaxationFunction,
                                        FinalTime = request.FinalTime,
                                        TimeStep = request.TimeStep,
                                        InitialTime = request.InitialTime
                                    });
                                }
                            }
                            else
                            {
                                foreach (var reducedRelaxationFunctionData in request.ReducedRelaxationFunctionDataList)
                                {
                                    inputList.Add(new FungModelInput
                                    {
                                        StrainRate = strainRate,
                                        MaximumStrain = maximumStrain,
                                        ElasticStressConstant = elasticStressConstant,
                                        ElasticPowerConstant = elasticPowerConstant,
                                        ReducedRelaxationFunctionInput = reducedRelaxationFunctionData,
                                        SimplifiedReducedRelaxationFunctionInput =  null,
                                        UseSimplifiedReducedRelaxationFunction = request.UseSimplifiedReducedRelaxationFunction,
                                        FinalTime = request.FinalTime,
                                        TimeStep = request.TimeStep,
                                        InitialTime = request.InitialTime
                                    });
                                }
                            }
                        }
                    }
                }
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
                streamWriter.WriteLine($"Variables E: {string.Join(";", input.SimplifiedReducedRelaxationFunctionInput.VariableEList)}");
                streamWriter.WriteLine($"Relaxation Times: {string.Join(";", input.SimplifiedReducedRelaxationFunctionInput.RelaxationTimeList)}");
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
        public override string CreateSolutionFile(FungModelInput input)
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
        public override FungModelResult CalculateAndWriteResults(FungModelInput input, double time, StreamWriter streamWriter)
        {
            double strain = this._viscoelasticModel.CalculateStrain(input, time);

            double reducedRelaxationFunction;
            if (input.UseSimplifiedReducedRelaxationFunction == true) reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunctionSimplified(input, time);
            else reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);

            double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);

            // TODO: Alterar métodos que calculam a tensão para usar a equação certa do ReducedRelaxationFunction.
            double stressWithElasticResponseDerivative = this._viscoelasticModel.CalculateStress(input, time);

            double stressWithReducedRelaxationFunctionDerivative = this._viscoelasticModel.CalculateStressByReducedRelaxationFunctionDerivative(input, time);

            double stressWithIntegrationDerivative = this._viscoelasticModel.CalculateStressByIntegrationDerivative(input, time);

            streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stressWithElasticResponseDerivative};{stressWithReducedRelaxationFunctionDerivative};{stressWithIntegrationDerivative}");

            return new FungModelResult
            {
                Strain = strain,
                ReducedRelaxationFunction = reducedRelaxationFunction,
                ElasticResponse = elasticResponse,
                Stress = stressWithElasticResponseDerivative,
                StressWithReducedRelaxationFunctionDerivative = stressWithReducedRelaxationFunctionDerivative,
                StressWithIntegralDerivative = stressWithIntegrationDerivative
            };
        }
    }
}
