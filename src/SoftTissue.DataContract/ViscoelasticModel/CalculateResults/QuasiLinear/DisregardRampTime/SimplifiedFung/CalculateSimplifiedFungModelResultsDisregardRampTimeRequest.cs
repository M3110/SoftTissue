using System.Collections.Generic;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the request content to CalculateSimplifiedFungModelResultsDisregardRampTime operation.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsDisregardRampTimeRequest : CalculateResultsSentivityAnalysisRequest<CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData>
    {
        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelResultsDisregardRampTimeRequest"/>.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelResultsDisregardRampTimeRequest Create(double timeStep, double finalTime, List<CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData> data = null)
        {
            return new CalculateSimplifiedFungModelResultsDisregardRampTimeRequest
            {
                TimeStep = timeStep,
                FinalTime = finalTime,
                DataList = data ?? new List<CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData>()
            };
        }
    }
}
