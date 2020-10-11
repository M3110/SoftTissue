using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to do a sensitivity analysis while calculating the stress to quasi-linear viscoelastic model.
    /// </summary>
    public abstract class CalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<TInput, TResult> : OperationBase<CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest, CalculateQuasiLinearViscoelasticityStressResponse, CalculateQuasiLinearViscoelasticityStressResponseData>, ICalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<TInput, TResult>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        private readonly IQuasiLinearViscoelasticityModel<TInput, TResult> _viscoelasticModel;

        /// <summary>
        /// The base path to files.
        /// </summary>
        private static readonly string TemplateBasePath = Path.Combine(Directory.GetCurrentDirectory(), "Solutions");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateQuasiLinearViscoelasticityStressSensitivityAnalysis(IQuasiLinearViscoelasticityModel<TInput, TResult> viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method builds an input list based on request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual List<TInput> BuildInputList(CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest request)
        {
            var inputList = new List<TInput>();
            int index = 0;

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
                                    inputList.Add(new TInput
                                    {
                                        StrainRate = strainRate,
                                        MaximumStrain = maximumStrain,
                                        ElasticStressConstant = elasticStressConstant,
                                        ElasticPowerConstant = elasticPowerConstant,
                                        ReducedRelaxationFunctionInput = null,
                                        SimplifiedReducedRelaxationFunctionInput = simplifiedReducedRelaxationFunctionData,
                                        UseSimplifiedReducedRelaxationFunction = request.UseSimplifiedReducedRelaxationFunction,
                                        Index = index
                                    });

                                    index++;
                                }
                            }
                            else
                            {
                                foreach (double relaxationIndex in request.RelaxationIndexList)
                                {
                                    foreach (double slowRelaxationTime in request.SlowRelaxationTimeList)
                                    {
                                        foreach (double fastRelaxationTime in request.FastRelaxationTimeList)
                                        {
                                            inputList.Add(new TInput
                                            {
                                                StrainRate = strainRate,
                                                MaximumStrain = maximumStrain,
                                                ElasticStressConstant = elasticStressConstant,
                                                ElasticPowerConstant = elasticPowerConstant,
                                                ReducedRelaxationFunctionInput = new ReducedRelaxationFunctionData
                                                {
                                                    RelaxationIndex = relaxationIndex,
                                                    SlowRelaxationTime = slowRelaxationTime,
                                                    FastRelaxationTime = fastRelaxationTime
                                                },
                                                SimplifiedReducedRelaxationFunctionInput = null,
                                                UseSimplifiedReducedRelaxationFunction = request.UseSimplifiedReducedRelaxationFunction,
                                                Index = index
                                            });

                                            index++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return inputList;
        }

        /// <summary>
        /// This method creates the path to save all inputs on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual string CreateInputFile()
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"InputData.csv"));

            if (fileInfo.Directory.Exists == false)
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
        public virtual string CreateSolutionFile(string functionName)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Solution_{functionName}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="streamWriter"></param>
        public virtual void WriteInputData(List<TInput> inputList, StreamWriter streamWriter)
        {
            List<double> initialTimeList = new List<double>();
            List<double> timeStepList = new List<double>();
            List<double> finalTimeList = new List<double>();
            List<double> strainRateList = new List<double>();
            List<double> maximumStrainList = new List<double>();
            List<double> elasticStressConstantList = new List<double>();
            List<double> elasticPowerConstantList = new List<double>();

            StringBuilder header = new StringBuilder("Parameter;");
            int index = 0;

            foreach (var input in inputList)
            {
                initialTimeList.Add(input.InitialTime);
                timeStepList.Add(input.TimeStep);
                finalTimeList.Add(input.FinalTime);
                strainRateList.Add(input.StrainRate);
                maximumStrainList.Add(input.MaximumStrain);
                elasticStressConstantList.Add(input.ElasticStressConstant);
                elasticPowerConstantList.Add(input.ElasticPowerConstant);

                header.Append($"Input {index};");
                index++;
            }

            header.Append("Unity");

            streamWriter.WriteLine(header);
            streamWriter.WriteLine($"Initial Time;{string.Join(';', initialTimeList)};s");
            streamWriter.WriteLine($"Time Step;{string.Join(';', initialTimeList)};s");
            streamWriter.WriteLine($"Final Time;{string.Join(';', initialTimeList)};s");
            streamWriter.WriteLine($"Strain Rate;{string.Join(';', initialTimeList)};/s");
            streamWriter.WriteLine($"Maximum Strain;{string.Join(';', initialTimeList)};");
            streamWriter.WriteLine($"Elastic Stress Constant (A);{string.Join(';', initialTimeList)};MPa");
            streamWriter.WriteLine($"Elastic Power Constant (B);{string.Join(';', initialTimeList)};");
        }

        /// <summary>
        /// This method creates the file header.
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public virtual StringBuilder CreteFileHeader(List<TInput> inputList)
        {
            StringBuilder fileHeader = new StringBuilder("Time;");

            for (int i = 0; i < inputList.Count; i++)
            {
                fileHeader.Append($"Input {i};");
            }

            return fileHeader;
        }

        /// <summary>
        /// This method calculates the results and writes them in a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="initialTime"></param>
        /// <param name="finalTime"></param>
        /// <param name="timeStep"></param>
        public virtual void CalculateAndWriteResults(List<TInput> inputList, double initialTime, double finalTime, double timeStep)
        {
            using (StreamWriter strainStreamWriter = new StreamWriter(this.CreateSolutionFile(functionName: "Strain")))
            using (StreamWriter reducedRelaxationFunctionStreamWriter = new StreamWriter(this.CreateSolutionFile(functionName: "ReducedRelaxationFunction")))
            using (StreamWriter elasticResponseStreamWriter = new StreamWriter(this.CreateSolutionFile(functionName: "ElasticResponse")))
            using (StreamWriter stressStreamWriter = new StreamWriter(this.CreateSolutionFile(functionName: "Stress")))
            {
                StringBuilder fileHeader = this.CreteFileHeader(inputList);

                strainStreamWriter.WriteLine(fileHeader);
                reducedRelaxationFunctionStreamWriter.WriteLine(fileHeader);
                elasticResponseStreamWriter.WriteLine(fileHeader);
                stressStreamWriter.WriteLine(fileHeader);

                double time = initialTime;

                while (time <= finalTime)
                {
                    StringBuilder strainResults = new StringBuilder($"{time};");
                    StringBuilder reducedRelaxationFunctionResults = new StringBuilder($"{time};");
                    StringBuilder elasticResponseResults = new StringBuilder($"{time};");
                    StringBuilder stressResults = new StringBuilder($"{time};");

                    foreach (var input in inputList)
                    {
                        double strain = this._viscoelasticModel.CalculateStrain(input, time);
                        strainResults.Append($"{strain};");

                        double reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
                        reducedRelaxationFunctionResults.Append($"{reducedRelaxationFunction};");

                        double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);
                        elasticResponseResults.Append($"{elasticResponse};");

                        double stress = this._viscoelasticModel.CalculateStress(input, time);
                        stressResults.Append($"{stress};");
                    }

                    strainStreamWriter.WriteLine(strainResults);
                    reducedRelaxationFunctionStreamWriter.WriteLine(reducedRelaxationFunctionResults);
                    elasticResponseStreamWriter.WriteLine(elasticResponseResults);
                    stressStreamWriter.WriteLine(stressResults);

                    time += timeStep;
                }
            }
        }

        /// <summary>
        /// This method executes an analysis to calculate the stress for a quasi-linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateQuasiLinearViscoelasticityStressResponse> ProcessOperation(CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest request)
        {
            var response = new CalculateQuasiLinearViscoelasticityStressResponse { Data = new CalculateQuasiLinearViscoelasticityStressResponseData() };
            response.SetSuccessCreated();

            List<TInput> inputList = this.BuildInputList(request);

            using (StreamWriter streamWriter = new StreamWriter(this.CreateInputFile()))
            {
                this.WriteInputData(inputList, streamWriter);
            }

            this.CalculateAndWriteResults(inputList, request.InitialTime, request.FinalTime, request.TimeStep);

            return Task.FromResult(response);
        }
    }
}