using SoftTissue.DataContract.Models;
using System;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelResultsConsiderRampTime operation request.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData : CalculateQuasiLinearModelResultsConsiderRampTimeRequestData<SimplifiedReducedRelaxationFunctionData>
    {
        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData"/>.
        /// </summary>
        /// <param name="strain"></param>
        /// <param name="experimentalModel"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData Create(ExperimentalModel experimentalModel, double? timeStep = null, double? finalTime = null)
        {
            double elasticStressConstant;
            double elasticPowerConstant;

            switch (experimentalModel)
            {
                case ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation:
                    elasticStressConstant = 112.96;
                    elasticPowerConstant = 0.34;
                    break;

                case ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation:
                    elasticStressConstant = 54.88;
                    elasticPowerConstant = 0.86;
                    break;

                case ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation:
                    elasticStressConstant = 1.86;
                    elasticPowerConstant = 10.15;
                    break;

                case ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation:
                    elasticStressConstant = 1.77;
                    elasticPowerConstant = 12.79;
                    break;

                case ExperimentalModel.LateralCollateralLigamentFirstRelaxation:
                    elasticStressConstant = 3.94;
                    elasticPowerConstant = 1.19;
                    break;

                case ExperimentalModel.LateralCollateralLigamentSecondRelaxation:
                    elasticStressConstant = 7.27;
                    elasticPowerConstant = 6.17;
                    break;

                case ExperimentalModel.MedialCollateralLigamentFirstRelaxation:
                    elasticStressConstant = 9.47;
                    elasticPowerConstant = 1.39;
                    break;

                case ExperimentalModel.MedialCollateralLigamentSecondRelaxation:
                    elasticStressConstant = 11.58;
                    elasticPowerConstant = 1.77;
                    break;

                default:
                    throw new Exception($"An invalid experimental model was passed.");
            }

            return new CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData
            {
                // Relaxation parameters
                ViscoelasticConsideration = ViscoelasticConsideration.GeneralViscoelasctiEffect,
                NumerOfRelaxations = 2,
                // Strain parameters
                StrainRate = 0.002,
                StrainDecreaseRate = 0.002,
                MaximumStrain = 0.06,
                MinimumStrain = 0.03,
                TimeWithConstantMaximumStrain = 100,
                TimeWithConstantMinimumStrain = 100,
                // Elastic parameters
                ElasticStressConstant = elasticStressConstant,
                ElasticPowerConstant = elasticPowerConstant,
                // Reduced Relaxation Function parameters
                ReducedRelaxationFunctionData = SimplifiedReducedRelaxationFunctionData.Create(experimentalModel),
                // General parameters
                TimeStep = timeStep,
                FinalTime = finalTime,
                SoftTissueType = experimentalModel.ToString()
            };
        }
    }
}
