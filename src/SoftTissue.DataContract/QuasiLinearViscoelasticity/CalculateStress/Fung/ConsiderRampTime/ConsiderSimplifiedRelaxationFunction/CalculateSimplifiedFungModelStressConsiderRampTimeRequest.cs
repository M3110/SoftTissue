using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the request content to CalculateSimplifiedFungModelStressConsiderRampTime operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelStressConsiderRampTimeRequest : CalculateResultRequest<List<CalculateSimplifiedFungModelStressConsiderRampTimeRequestData>>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="data"></param>
        [JsonConstructor]
        public CalculateSimplifiedFungModelStressConsiderRampTimeRequest(double timeStep, double finalTime, List<CalculateSimplifiedFungModelStressConsiderRampTimeRequestData> data) : base(timeStep, finalTime, data) { }
    }
}
