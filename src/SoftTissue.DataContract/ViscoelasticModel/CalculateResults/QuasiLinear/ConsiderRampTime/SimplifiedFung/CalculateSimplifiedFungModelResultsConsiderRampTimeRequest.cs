using SoftTissue.DataContract.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the request content to CalculateSimplifiedFungModelResultsConsiderRampTime operation.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsConsiderRampTimeRequest : CalculateQuasiLinearModelResultsConsiderRampTimeRequest<CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData, SimplifiedReducedRelaxationFunctionData> 
    {
        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelResultsConsiderRampTimeRequest"/>.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="experimentalModel"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelResultsConsiderRampTimeRequest Create(double timeStep, double finalTime, ExperimentalModel experimentalModel)
        {
            return new CalculateSimplifiedFungModelResultsConsiderRampTimeRequest
            {
                TimeStep = timeStep,
                FinalTime = finalTime,
                DataList = new List<CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData>
                {
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(experimentalModel)
                }
            };
        }

        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelResultsConsiderRampTimeRequest"/>.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelResultsConsiderRampTimeRequest Create(double timeStep, double finalTime)
        {
            var request = new CalculateSimplifiedFungModelResultsConsiderRampTimeRequest
            {
                TimeStep = timeStep,
                FinalTime = finalTime,
                DataList = new List<CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData> 
                {
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation),
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation),
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(ExperimentalModel.LateralCollateralLigamentFirstRelaxation),
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(ExperimentalModel.LateralCollateralLigamentSecondRelaxation),
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(ExperimentalModel.MedialCollateralLigamentFirstRelaxation),
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(ExperimentalModel.MedialCollateralLigamentSecondRelaxation),
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation),
                    CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData.Create(ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation),
                }
            };

            return request;
        }
    }
}
