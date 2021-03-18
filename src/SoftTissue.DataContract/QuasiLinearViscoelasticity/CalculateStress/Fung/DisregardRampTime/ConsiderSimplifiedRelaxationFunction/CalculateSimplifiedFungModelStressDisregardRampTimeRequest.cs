using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the request content to CalculateSimplifiedFungModelStressDisregardRampTime operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelStressDisregardRampTimeRequest : CalculateResultRequest<List<CalculateSimplifiedFungModelStressDisregardRampTimeRequestData>>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="data"></param>
        [JsonConstructor]
        public CalculateSimplifiedFungModelStressDisregardRampTimeRequest(double timeStep, double finalTime, List<CalculateSimplifiedFungModelStressDisregardRampTimeRequestData> data) : base(timeStep, finalTime, data) { }

        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelStressDisregardRampTimeRequest"/>.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelStressDisregardRampTimeRequest Create(double timeStep, double finalTime, List<CalculateSimplifiedFungModelStressDisregardRampTimeRequestData> data = null)
        {
            if (data == null)
            {
                return new CalculateSimplifiedFungModelStressDisregardRampTimeRequest(timeStep, finalTime, new List<CalculateSimplifiedFungModelStressDisregardRampTimeRequestData>());
            }

            return new CalculateSimplifiedFungModelStressDisregardRampTimeRequest(timeStep, finalTime, data);
        }
    }
}
