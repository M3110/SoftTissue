using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the request content to CalculateStress operation of Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStressRequest : CalculateResultRequest<List<CalculateStressRequestData>>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="data"></param>
        [JsonConstructor]
        public CalculateStressRequest(double timeStep, double finalTime, List<CalculateStressRequestData> data) : base(timeStep, finalTime, data) { }
    }
}
