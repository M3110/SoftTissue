using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the request content to CalculateFungModelStressDisregardRampTime operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateFungModelStressDisregardRampTimeRequest : CalculateResultRequest<List<CalculateFungModelStressDisregardRampTimeRequestData>>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="data"></param>
        [JsonConstructor]
        public CalculateFungModelStressDisregardRampTimeRequest(double timeStep, double finalTime, List<CalculateFungModelStressDisregardRampTimeRequestData> data) : base(timeStep, finalTime, data) { }
    }
}
