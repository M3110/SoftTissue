using SoftTissue.Core.ExtensionMethods;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis;
using SoftTissue.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace SoftTissue.Application.Extensions
{
    /// <summary>
    /// It is responsible to build a request from another request.
    /// </summary>
    public static class HttpRequestBuilder
    {
        /// <summary>
        /// This method creates <see cref="CalculateStressSensitivityAnalysisRequest"/> based on <see cref="CalculateStressSensitivityAnalysisExplicitRequest"/>.
        /// </summary>
        /// <param name="analysisExplicitRequest"></param>
        /// <returns></returns>
        public static CalculateStressSensitivityAnalysisRequest MapToSensitivityAnalysis(this CalculateStressSensitivityAnalysisExplicitRequest analysisExplicitRequest)
        {
            if (analysisExplicitRequest == null)
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
        /// This method creates <see cref="CalculateSimplifiedFungModelStressDisregardRampTimeRequest"/> based on <see cref="CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelStressDisregardRampTimeRequest CreateQuasiLinearRequest(this CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest request)
        {
            var calculateQuasiLinearViscoelasticityStressRequest = new CalculateSimplifiedFungModelStressDisregardRampTimeRequest
            {
                InitialTime = request.InitialTime,
                TimeStep = request.TimeStep,
                FinalTime = request.FinalTime,
                Data = new List<CalculateSimplifiedFungModelStressDisregardRampTimeRequestData>()
            };

            if (request.ExperimentalModel == ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.Data.Add(new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
                {
                    SoftTissueType = request.ExperimentalModel.ToString(),
                    Strain = request.Strain,
                    InitialStress = 2.48,
                    ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        FirstViscoelasticStiffness = 1.85068 / request.InitialStress,
                        IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                        {
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 27.91285,
                                ViscoelasticStiffness = 0.27817 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 27.89834,
                                ViscoelasticStiffness = 0.21177 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 27.96472,
                                ViscoelasticStiffness = 0.12388 / request.InitialStress
                            },
                        }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.Data.Add(new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
                {
                    SoftTissueType = request.ExperimentalModel.ToString(),
                    Strain = request.Strain,
                    InitialStress = 2.02,
                    ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        FirstViscoelasticStiffness = 1.60098 / request.InitialStress,
                        IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                        {
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 3.46446,
                                ViscoelasticStiffness = 0.05836 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 26.32488,
                                ViscoelasticStiffness = 0.095 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 258.84436,
                                ViscoelasticStiffness = 0.24713 / request.InitialStress
                            },
                        }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.LateralCollateralLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.Data.Add(new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
                {
                    SoftTissueType = request.ExperimentalModel.ToString(),
                    Strain = request.Strain,
                    InitialStress = 4.41,
                    ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        FirstViscoelasticStiffness = 3.79214 / request.InitialStress,
                        IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                        {
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 1.17552,
                                ViscoelasticStiffness = 0.11477 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 8.24771,
                                ViscoelasticStiffness = 0.18532 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 56.76771,
                                ViscoelasticStiffness = 0.3221 / request.InitialStress
                            },
                        }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.LateralCollateralLigamentSecondRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.Data.Add(new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
                {
                    SoftTissueType = request.ExperimentalModel.ToString(),
                    Strain = request.Strain,
                    InitialStress = 4.1,
                    ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        FirstViscoelasticStiffness = 3.5735 / request.InitialStress,
                        IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                        {
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 2.1685,
                                ViscoelasticStiffness = 0.19165 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 16.72283,
                                ViscoelasticStiffness = 0.17699 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 155.50761,
                                ViscoelasticStiffness = 0.24084 / request.InitialStress
                            },
                        }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.MedialCollateralLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.Data.Add(new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
                {
                    SoftTissueType = request.ExperimentalModel.ToString(),
                    Strain = request.Strain,
                    InitialStress = 0.97,
                    ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        FirstViscoelasticStiffness = 0.85415 / request.InitialStress,
                        IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                        {
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 0.9759,
                                ViscoelasticStiffness = 0.03055 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 8.14266,
                                ViscoelasticStiffness = 0.04098 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 61.36836,
                                ViscoelasticStiffness = 0.0579 / request.InitialStress
                            },
                        }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.MedialCollateralLigamentSecondRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.Data.Add(new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
                {
                    SoftTissueType = request.ExperimentalModel.ToString(),
                    Strain = request.Strain,
                    InitialStress = 0.91,
                    ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        FirstViscoelasticStiffness = 0.8084 / request.InitialStress,
                        IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                        {
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 1.84464,
                                ViscoelasticStiffness = 0.04431 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 15.65552,
                                ViscoelasticStiffness = 0.03257 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 163.02567,
                                ViscoelasticStiffness = 0.04534 / request.InitialStress
                            },
                        }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.Data.Add(new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
                {
                    SoftTissueType = request.ExperimentalModel.ToString(),
                    Strain = request.Strain,
                    InitialStress = 1.65,
                    ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        FirstViscoelasticStiffness = 1.45 / request.InitialStress,
                        IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                        {
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 1.07199,
                                ViscoelasticStiffness = 0.03191 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 7.32883,
                                ViscoelasticStiffness = 0.05549 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 54.66115,
                                ViscoelasticStiffness = 0.11294 / request.InitialStress
                            },
                        }
                    }
                });
            }
            else if (request.ExperimentalModel == ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation)
            {
                calculateQuasiLinearViscoelasticityStressRequest.Data.Add(new CalculateSimplifiedFungModelStressDisregardRampTimeRequestData
                {
                    SoftTissueType = request.ExperimentalModel.ToString(),
                    Strain = request.Strain,
                    InitialStress = 1.53,
                    ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                    {
                        FirstViscoelasticStiffness = 1.38406 / request.InitialStress,
                        IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                        {
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 1.87972,
                                ViscoelasticStiffness = 0.03412 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 14.66308,
                                ViscoelasticStiffness = 0.04735 / request.InitialStress
                            },
                            new SimplifiedReducedRelaxationFunctionIteratorValues
                            {
                                RelaxationTime = 139.22114,
                                ViscoelasticStiffness = 0.07758 / request.InitialStress
                            },
                        }
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
