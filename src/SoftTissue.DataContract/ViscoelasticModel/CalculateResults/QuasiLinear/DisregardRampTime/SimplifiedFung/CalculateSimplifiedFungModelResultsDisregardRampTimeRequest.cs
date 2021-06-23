using SoftTissue.DataContract.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the request content to CalculateSimplifiedFungModelResultsDisregardRampTime operation.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsDisregardRampTimeRequest : CalculateQuasiLinearModelResultsDisregardRampTimeRequest<CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData, SimplifiedReducedRelaxationFunctionData>
    {
        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelResultsDisregardRampTimeRequest"/>.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="experimentalModel"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelResultsDisregardRampTimeRequest Create(double timeStep, double finalTime, ExperimentalModel experimentalModel)
        {
            return new CalculateSimplifiedFungModelResultsDisregardRampTimeRequest
            {
                TimeStep = timeStep,
                FinalTime = finalTime,
                DataList = new List<CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData>
                {
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(experimentalModel)
                }
            };
        }

        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelResultsDisregardRampTimeRequest"/>.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelResultsDisregardRampTimeRequest Create(double timeStep, double finalTime)
        {
            var request = new CalculateSimplifiedFungModelResultsDisregardRampTimeRequest
            {
                TimeStep = timeStep,
                FinalTime = finalTime,
                DataList = new List<CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData>
                {
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation),
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation),
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(ExperimentalModel.LateralCollateralLigamentFirstRelaxation),
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(ExperimentalModel.LateralCollateralLigamentSecondRelaxation),
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(ExperimentalModel.MedialCollateralLigamentFirstRelaxation),
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(ExperimentalModel.MedialCollateralLigamentSecondRelaxation),
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation),
                    CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData.Create(ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation),
                }
            };

            return request;
        }
    }
}
