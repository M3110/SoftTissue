﻿using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request;
using SoftTissue.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It is responsible to do a sensitivity analysis while calculating the stress to quasi-linear viscoelastic model.
    /// </summary>
    public abstract class CalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<TInput, TResult> : CalculateResultSensitivityAnalysis<CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest, CalculateQuasiLinearViscoelasticityStressResponse, CalculateQuasiLinearViscoelasticityStressResponseData, TInput>, ICalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<TInput, TResult>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        private readonly IQuasiLinearViscoelasticityModel<TInput, TResult> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateQuasiLinearViscoelasticityStressSensitivityAnalysis(IQuasiLinearViscoelasticityModel<TInput, TResult> viscoelasticModel)
            : base(viscoelasticModel)
        {
            _viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method builds an input list based on request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<TInput> BuildInputList(CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest request)
        {
            var inputList = new List<TInput>();

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
                                        InitialTime = request.InitialTime,
                                        TimeStep = request.TimeStep,
                                        FinalTime = request.FinalTime
                                    });
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
                                                InitialTime = request.InitialTime,
                                                TimeStep = request.TimeStep,
                                                FinalTime = request.FinalTime
                                            });
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
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="streamWriter"></param>
        /// <param name="useSimplifiedReducedRelaxationFunction"></param>
        public virtual void WriteInputData(List<TInput> inputList, StreamWriter streamWriter, bool useSimplifiedReducedRelaxationFunction)
        {
            List<double> initialTimeList = new List<double>();
            List<double> timeStepList = new List<double>();
            List<double> finalTimeList = new List<double>();
            List<double> strainRateList = new List<double>();
            List<double> maximumStrainList = new List<double>();
            List<double> elasticStressConstantList = new List<double>();
            List<double> elasticPowerConstantList = new List<double>();

            // That variables will be used if the parameter UseSimplifiedReducedRelaxationFunction was false.
            List<double> relaxationIndexList = new List<double>();
            List<double> fastRelaxationTimeList = new List<double>();
            List<double> slowRelaxationTimeList = new List<double>();

            // That variables will be used if the parameter UseSimplifiedReducedRelaxationFunction was true.
            List<string> variablesEList = new List<string>();
            List<string> relaxationTimesList = new List<string>();

            StringBuilder header = new StringBuilder("Parameter;");

            int index = 1;

            foreach (var input in inputList)
            {
                initialTimeList.Add(input.InitialTime);
                timeStepList.Add(input.TimeStep);
                finalTimeList.Add(input.FinalTime);
                strainRateList.Add(input.StrainRate);
                maximumStrainList.Add(input.MaximumStrain);
                elasticStressConstantList.Add(input.ElasticStressConstant);
                elasticPowerConstantList.Add(input.ElasticPowerConstant);

                if (useSimplifiedReducedRelaxationFunction == false)
                {
                    relaxationIndexList.Add(input.ReducedRelaxationFunctionInput.RelaxationIndex);
                    fastRelaxationTimeList.Add(input.ReducedRelaxationFunctionInput.FastRelaxationTime);
                    slowRelaxationTimeList.Add(input.ReducedRelaxationFunctionInput.SlowRelaxationTime);
                }
                else
                {
                    variablesEList.Add(string.Join('-', input.SimplifiedReducedRelaxationFunctionInput.VariableEList));
                    relaxationTimesList.Add(string.Join('-', input.SimplifiedReducedRelaxationFunctionInput.RelaxationTimeList));
                }

                header.Append($"Input {index};");
                index++;
            }

            header.Append("Unity");

            streamWriter.WriteLine(header);
            streamWriter.WriteLine($"Initial Time;{string.Join(';', initialTimeList)};s");
            streamWriter.WriteLine($"Time Step;{string.Join(';', timeStepList)};s");
            streamWriter.WriteLine($"Final Time;{string.Join(';', finalTimeList)};s");
            streamWriter.WriteLine($"Strain Rate;{string.Join(';', strainRateList)};/s");
            streamWriter.WriteLine($"Maximum Strain;{string.Join(';', maximumStrainList)};");
            streamWriter.WriteLine($"Elastic Stress Constant (A);{string.Join(';', elasticStressConstantList)};MPa");
            streamWriter.WriteLine($"Elastic Power Constant (B);{string.Join(';', elasticPowerConstantList)};");

            if (useSimplifiedReducedRelaxationFunction == false)
            {
                streamWriter.WriteLine($"Relaxation Index (C);{string.Join(';', relaxationIndexList)};");
                streamWriter.WriteLine($"Fast relaxation time (Tau 1);{string.Join(';', fastRelaxationTimeList)};s");
                streamWriter.WriteLine($"Slow relaxation time (Tau 2);{string.Join(';', slowRelaxationTimeList)};s");
            }
            else
            {
                streamWriter.WriteLine($"Variables E;{string.Join(';', variablesEList)};");
                streamWriter.WriteLine($"Relaxation Times;{string.Join(';', relaxationTimesList)};s");
            }
        }

        /// <summary>
        /// This method calculates the results and writes them in a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="initialTime"></param>
        /// <param name="finalTime"></param>
        /// <param name="timeStep"></param>
        public override void CalculateAndWriteResults(List<TInput> inputList, double initialTime, double finalTime, double timeStep)
        {
            using (StreamWriter strainStreamWriter = new StreamWriter(CreateSolutionFile(functionName: "Strain")))
            using (StreamWriter reducedRelaxationFunctionStreamWriter = new StreamWriter(CreateSolutionFile(functionName: "ReducedRelaxationFunction")))
            using (StreamWriter elasticResponseStreamWriter = new StreamWriter(CreateSolutionFile(functionName: "ElasticResponse")))
            using (StreamWriter stressStreamWriter = new StreamWriter(CreateSolutionFile(functionName: "Stress")))
            {
                StringBuilder fileHeader = CreteFileHeader(inputList);

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
                        double strain = _viscoelasticModel.CalculateStrain(input, time);
                        strainResults.Append($"{strain};");

                        double reducedRelaxationFunction;
                        if (input.UseSimplifiedReducedRelaxationFunction == true) reducedRelaxationFunction = _viscoelasticModel.CalculateReducedRelaxationFunctionSimplified(input, time);
                        else reducedRelaxationFunction = _viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
                        reducedRelaxationFunctionResults.Append($"{reducedRelaxationFunction};");

                        double elasticResponse = _viscoelasticModel.CalculateElasticResponse(input, time);
                        elasticResponseResults.Append($"{elasticResponse};");

                        double stress = _viscoelasticModel.CalculateStress(input, time);
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

            List<TInput> inputList = BuildInputList(request);

            using (StreamWriter streamWriter = new StreamWriter(CreateInputFile()))
            {
                WriteInputData(inputList, streamWriter, request.UseSimplifiedReducedRelaxationFunction);
            }

            try
            {
                CalculateAndWriteResults(inputList, request.InitialTime, request.FinalTime, request.TimeStep);
            }
            catch (Exception ex)
            {
                response.AddError(OperationErrorCode.InternalServerError, $"Error trying to calculate and write the solutions in file. {ex.Message}.", HttpStatusCode.InternalServerError);
                response.SetInternalServerError();
            }

            return Task.FromResult(response);
        }
    }
}