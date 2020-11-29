using SoftTissue.Core.ExtensionMethods;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request;
using SoftTissue.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace SoftTissue.Application.Extensions
{
    public static class HttpRequestBuilder
    {
        /// <summary>
        /// This method creates <see cref="CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest"/> based on <see cref="CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisExplicitRequest"/>.
        /// </summary>
        /// <param name="analysisExplicitRequest"></param>
        /// <returns></returns>
        public static CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest MapToSensitivityAnalysis(this CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisExplicitRequest analysisExplicitRequest)
        {
            if(analysisExplicitRequest == null)
            {
                return null;
            }

            return new CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest
            {
                InitialTime = analysisExplicitRequest.InitialTime,
                TimeStep = analysisExplicitRequest.TimeStep,
                FinalTime = analysisExplicitRequest.FinalTime,
                UseSimplifiedReducedRelaxationFunction = analysisExplicitRequest.UseSimplifiedReducedRelaxationFunction,
                MaximumStrainList = analysisExplicitRequest.MaximumStrainList.ToEnumerable(),
                StrainRateList = analysisExplicitRequest.StrainRateList.ToEnumerable(),
                ElasticStressConstantList = analysisExplicitRequest.ElasticStressConstantList.ToEnumerable(),
                ElasticPowerConstantList = analysisExplicitRequest.ElasticPowerConstantList.ToEnumerable(),
                RelaxationIndexList = analysisExplicitRequest.RelaxationIndexList.ToEnumerable(),
                FastRelaxationTimeList = analysisExplicitRequest.FastRelaxationTimeList.ToEnumerable(),
                SlowRelaxationTimeList = analysisExplicitRequest.SlowRelaxationTimeList.ToEnumerable(),
                SimplifiedReducedRelaxationFunctionDataList = analysisExplicitRequest.SimplifiedReducedRelaxationFunctionDataList
            };
        }

        /// <summary>
        /// This method creates <see cref="CalculateQuasiLinearViscoelasticityStressRequest"/> based on <see cref="CalculateQuasiLinearViscoelasticityStressToExperimentalModelRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CalculateQuasiLinearViscoelasticityStressRequest CreateQuasiLinearRequest(this CalculateQuasiLinearViscoelasticityStressToExperimentalModelRequest request)
        {
            var calculateQuasiLinearViscoelasticityStressRequest = new CalculateQuasiLinearViscoelasticityStressRequest
            {
                InitialTime = request.InitialTime,
                TimeStep = request.TimeStep,
                FinalTime = request.FinalTime,
                RequestDataList = new List<CalculateQuasiLinearViscoelasticityStressRequestData>()
            };

            if (request.ExperimentalModel == ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestDataList.Add(new CalculateQuasiLinearViscoelasticityStressRequestData
                {
                    SoftTissueType = ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation.ToString(),
                    StrainRate = request.StrainRate,
                    MaximumStrain = request.MaximumStrain,
                    ElasticPowerConstant = 0,
                    ElasticStressConstant = 0,
                    UseSimplifiedReducedRelaxationFunction = true,
                    ReducedRelaxationFunctionData = null,
                    SimplifiedReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        IndependentVariable = 0,
                        RelaxationTimeList = new List<double> { },
                        VariableEList = new List<double> { }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestDataList.Add(new CalculateQuasiLinearViscoelasticityStressRequestData
                {
                    SoftTissueType = ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation.ToString(),
                    StrainRate = request.StrainRate,
                    MaximumStrain = request.MaximumStrain,
                    ElasticPowerConstant = 0,
                    ElasticStressConstant = 0,
                    UseSimplifiedReducedRelaxationFunction = true,
                    ReducedRelaxationFunctionData = null,
                    SimplifiedReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        IndependentVariable = 0,
                        RelaxationTimeList = new List<double> { },
                        VariableEList = new List<double> { }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.LateralCollateralLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestDataList.Add(new CalculateQuasiLinearViscoelasticityStressRequestData
                {
                    SoftTissueType = ExperimentalModel.LateralCollateralLigamentFirstRelaxation.ToString(),
                    StrainRate = request.StrainRate,
                    MaximumStrain = request.MaximumStrain,
                    ElasticPowerConstant = 0,
                    ElasticStressConstant = 0,
                    UseSimplifiedReducedRelaxationFunction = true,
                    ReducedRelaxationFunctionData = null,
                    SimplifiedReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        IndependentVariable = 0,
                        RelaxationTimeList = new List<double> { },
                        VariableEList = new List<double> { }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.LateralCollateralLigamentSecondRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestDataList.Add(new CalculateQuasiLinearViscoelasticityStressRequestData
                {
                    SoftTissueType = ExperimentalModel.LateralCollateralLigamentSecondRelaxation.ToString(),
                    StrainRate = request.StrainRate,
                    MaximumStrain = request.MaximumStrain,
                    ElasticPowerConstant = 0,
                    ElasticStressConstant = 0,
                    UseSimplifiedReducedRelaxationFunction = true,
                    ReducedRelaxationFunctionData = null,
                    SimplifiedReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        IndependentVariable = 0,
                        RelaxationTimeList = new List<double> { },
                        VariableEList = new List<double> { }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.MedialCollateralLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestDataList.Add(new CalculateQuasiLinearViscoelasticityStressRequestData
                {
                    SoftTissueType = ExperimentalModel.MedialCollateralLigamentFirstRelaxation.ToString(),
                    StrainRate = request.StrainRate,
                    MaximumStrain = request.MaximumStrain,
                    ElasticPowerConstant = 0,
                    ElasticStressConstant = 0,
                    UseSimplifiedReducedRelaxationFunction = true,
                    ReducedRelaxationFunctionData = null,
                    SimplifiedReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        IndependentVariable = 0,
                        RelaxationTimeList = new List<double> { },
                        VariableEList = new List<double> { }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.MedialCollateralLigamentSecondRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestDataList.Add(new CalculateQuasiLinearViscoelasticityStressRequestData
                {
                    SoftTissueType = ExperimentalModel.MedialCollateralLigamentSecondRelaxation.ToString(),
                    StrainRate = request.StrainRate,
                    MaximumStrain = request.MaximumStrain,
                    ElasticPowerConstant = 0,
                    ElasticStressConstant = 0,
                    UseSimplifiedReducedRelaxationFunction = true,
                    ReducedRelaxationFunctionData = null,
                    SimplifiedReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        IndependentVariable = 0,
                        RelaxationTimeList = new List<double> { },
                        VariableEList = new List<double> { }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestDataList.Add(new CalculateQuasiLinearViscoelasticityStressRequestData
                {
                    SoftTissueType = ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation.ToString(),
                    StrainRate = request.StrainRate,
                    MaximumStrain = request.MaximumStrain,
                    ElasticPowerConstant = 0,
                    ElasticStressConstant = 0,
                    UseSimplifiedReducedRelaxationFunction = true,
                    ReducedRelaxationFunctionData = null,
                    SimplifiedReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        IndependentVariable = 0,
                        RelaxationTimeList = new List<double> { },
                        VariableEList = new List<double> { }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestDataList.Add(new CalculateQuasiLinearViscoelasticityStressRequestData
                {
                    SoftTissueType = ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation.ToString(),
                    StrainRate = request.StrainRate,
                    MaximumStrain = request.MaximumStrain,
                    ElasticPowerConstant = 0,
                    ElasticStressConstant = 0,
                    UseSimplifiedReducedRelaxationFunction = true,
                    ReducedRelaxationFunctionData = null,
                    SimplifiedReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        IndependentVariable = 0,
                        RelaxationTimeList = new List<double> { },
                        VariableEList = new List<double> { }
                    }
                });
            }
            else
            {
                throw new Exception($"An invalid experimental model was passed.");
            }

            return calculateQuasiLinearViscoelasticityStressRequest;
        }
    }
}
