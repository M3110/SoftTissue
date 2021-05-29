using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung;
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
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelResultsDisregardRampTimeRequest"/> based on <see cref="CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelResultsDisregardRampTimeRequest CreateQuasiLinearRequest(this CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest request)
        {
            var calculateQuasiLinearViscoelasticityStressRequest = new CalculateSimplifiedFungModelResultsDisregardRampTimeRequest
            {
                TimeStep = request.TimeStep,
                FinalTime = request.FinalTime,
                DataList = new List<CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData>()
            };

            switch (request.ExperimentalModel)
            {
                case ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation:
                    calculateQuasiLinearViscoelasticityStressRequest.DataList.Add(new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
                    {
                        SoftTissueType = request.ExperimentalModel.ToString(),
                        Strain = request.Strain,
                        InitialStress = 2.48,
                        ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                        {
                            FirstViscoelasticStiffness = 1.85068 / 2.48,
                            IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                            {
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 27.91285,
                                    ViscoelasticStiffness = 0.27817 / 2.48
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 27.89834,
                                    ViscoelasticStiffness = 0.21177 / 2.48
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 27.96472,
                                    ViscoelasticStiffness = 0.12388 / 2.48
                                },
                            }
                        }
                    });
                    break;

                case ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation:
                    calculateQuasiLinearViscoelasticityStressRequest.DataList.Add(new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
                    {
                        SoftTissueType = request.ExperimentalModel.ToString(),
                        Strain = request.Strain,
                        InitialStress = 2.02,
                        ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                        {
                            FirstViscoelasticStiffness = 1.60098 / 2.02,
                            IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                            {
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 3.46446,
                                    ViscoelasticStiffness = 0.05836 / 2.02
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 26.32488,
                                    ViscoelasticStiffness = 0.095 / 2.02
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 258.84436,
                                    ViscoelasticStiffness = 0.24713 / 2.02
                                },
                            }
                        }
                    });
                    break;

                case ExperimentalModel.LateralCollateralLigamentFirstRelaxation:
                    calculateQuasiLinearViscoelasticityStressRequest.DataList.Add(new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
                    {
                        SoftTissueType = request.ExperimentalModel.ToString(),
                        Strain = request.Strain,
                        InitialStress = 4.41,
                        ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                        {
                            FirstViscoelasticStiffness = 3.79214 / 4.41,
                            IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                            {
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 1.17552,
                                    ViscoelasticStiffness = 0.11477 / 4.41
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 8.24771,
                                    ViscoelasticStiffness = 0.18532 / 4.41
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 56.76771,
                                    ViscoelasticStiffness = 0.3221 / 4.41
                                },
                            }
                        }
                    });
                    break;

                case ExperimentalModel.LateralCollateralLigamentSecondRelaxation:
                    calculateQuasiLinearViscoelasticityStressRequest.DataList.Add(new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
                    {
                        SoftTissueType = request.ExperimentalModel.ToString(),
                        Strain = request.Strain,
                        InitialStress = 4.1,
                        ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                        {
                            FirstViscoelasticStiffness = 3.5735 / 4.1,
                            IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                            {
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 2.1685,
                                    ViscoelasticStiffness = 0.19165 / 4.1
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 16.72283,
                                    ViscoelasticStiffness = 0.17699 / 4.1
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 155.50761,
                                    ViscoelasticStiffness = 0.24084 / 4.1
                                },
                            }
                        }
                    });
                    break;

                case ExperimentalModel.MedialCollateralLigamentFirstRelaxation:
                    calculateQuasiLinearViscoelasticityStressRequest.DataList.Add(new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
                    {
                        SoftTissueType = request.ExperimentalModel.ToString(),
                        Strain = request.Strain,
                        InitialStress = 0.97,
                        ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                        {
                            FirstViscoelasticStiffness = 0.85415 / 0.97,
                            IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                            {
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 0.9759,
                                    ViscoelasticStiffness = 0.03055 / 0.97
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 8.14266,
                                    ViscoelasticStiffness = 0.04098 / 0.97
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 61.36836,
                                    ViscoelasticStiffness = 0.0579 / 0.97
                                },
                            }
                        }
                    });
                    break;

                case ExperimentalModel.MedialCollateralLigamentSecondRelaxation:
                    calculateQuasiLinearViscoelasticityStressRequest.DataList.Add(new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
                    {
                        SoftTissueType = request.ExperimentalModel.ToString(),
                        Strain = request.Strain,
                        InitialStress = 0.91,
                        ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                        {
                            FirstViscoelasticStiffness = 0.8084 / 0.91,
                            IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                            {
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 1.84464,
                                    ViscoelasticStiffness = 0.04431 / 0.91
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 15.65552,
                                    ViscoelasticStiffness = 0.03257 / 0.91
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 163.02567,
                                    ViscoelasticStiffness = 0.04534 / 0.91
                                },
                            }
                        }
                    });
                    break;

                case ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation:
                    calculateQuasiLinearViscoelasticityStressRequest.DataList.Add(new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
                    {
                        SoftTissueType = request.ExperimentalModel.ToString(),
                        Strain = request.Strain,
                        InitialStress = 1.65,
                        ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                        {
                            FirstViscoelasticStiffness = 1.45 / 1.65,
                            IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                            {
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 1.07199,
                                    ViscoelasticStiffness = 0.03191 / 1.65
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 7.32883,
                                    ViscoelasticStiffness = 0.05549 / 1.65
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 54.66115,
                                    ViscoelasticStiffness = 0.11294 / 1.65
                                },
                            }
                        }
                    });
                    break;

                case ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation:
                    calculateQuasiLinearViscoelasticityStressRequest.DataList.Add(new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
                    {
                        SoftTissueType = request.ExperimentalModel.ToString(),
                        Strain = request.Strain,
                        InitialStress = 1.53,
                        ReducedRelaxationFunctionData = new SimplifiedReducedRelaxationFunctionData
                        {
                            FirstViscoelasticStiffness = 1.38406 / 1.53,
                            IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValues>
                            {
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 1.87972,
                                    ViscoelasticStiffness = 0.03412 / 1.53
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 14.66308,
                                    ViscoelasticStiffness = 0.04735 / 1.53
                                },
                                new SimplifiedReducedRelaxationFunctionIteratorValues
                                {
                                    RelaxationTime = 139.22114,
                                    ViscoelasticStiffness = 0.07758 / 1.53
                                },
                            }
                        }
                    });
                    break;

                default:
                    throw new Exception($"An invalid experimental model was passed.");
            }

            return calculateQuasiLinearViscoelasticityStressRequest;
        }
    }
}
