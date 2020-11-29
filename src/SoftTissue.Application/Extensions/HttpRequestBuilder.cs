using SoftTissue.Core.ExtensionMethods;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis;
using SoftTissue.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace SoftTissue.Application.Extensions
{
    public static class HttpRequestBuilder
    {
        /// <summary>
        /// This method creates <see cref="CalculateStressSensitivityAnalysisRequest"/> based on <see cref="CalculateStressSensitivityAnalysisExplicitRequest"/>.
        /// </summary>
        /// <param name="analysisExplicitRequest"></param>
        /// <returns></returns>
        public static CalculateStressSensitivityAnalysisRequest MapToSensitivityAnalysis(this CalculateStressSensitivityAnalysisExplicitRequest analysisExplicitRequest)
        {
            if(analysisExplicitRequest == null)
            {
                return null;
            }

            return new CalculateStressSensitivityAnalysisRequest
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
        /// This method creates <see cref="CalculateStressRequest"/> based on <see cref="CalculateStressToExperimentalModelRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CalculateStressRequest CreateQuasiLinearRequest(this CalculateStressToExperimentalModelRequest request)
        {
            var calculateQuasiLinearViscoelasticityStressRequest = new CalculateStressRequest
            {
                InitialTime = request.InitialTime,
                TimeStep = request.TimeStep,
                FinalTime = request.FinalTime,
                RequestData = new List<CalculateStressRequestData>()
            };

            if (request.ExperimentalModel == ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.RequestData.Add(new CalculateStressRequestData
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
                calculateQuasiLinearViscoelasticityStressRequest.RequestData.Add(new CalculateStressRequestData
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
                calculateQuasiLinearViscoelasticityStressRequest.RequestData.Add(new CalculateStressRequestData
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
                calculateQuasiLinearViscoelasticityStressRequest.RequestData.Add(new CalculateStressRequestData
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
                calculateQuasiLinearViscoelasticityStressRequest.RequestData.Add(new CalculateStressRequestData
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
                calculateQuasiLinearViscoelasticityStressRequest.RequestData.Add(new CalculateStressRequestData
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
                calculateQuasiLinearViscoelasticityStressRequest.RequestData.Add(new CalculateStressRequestData
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
                calculateQuasiLinearViscoelasticityStressRequest.RequestData.Add(new CalculateStressRequestData
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
