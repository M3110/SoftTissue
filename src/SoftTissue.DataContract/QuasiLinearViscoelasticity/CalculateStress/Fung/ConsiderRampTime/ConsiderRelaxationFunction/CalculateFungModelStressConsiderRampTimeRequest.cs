using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the request content to CalculateFungModelStressConsiderRampTime operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateFungModelStressConsiderRampTimeRequest : CalculateResultRequest<List<CalculateFungModelStressConsiderRampTimeRequestData>>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="data"></param>
        [JsonConstructor]
        public CalculateFungModelStressConsiderRampTimeRequest(double timeStep, double finalTime, List<CalculateFungModelStressConsiderRampTimeRequestData> data) : base(timeStep, finalTime, data) { }
    }
}
