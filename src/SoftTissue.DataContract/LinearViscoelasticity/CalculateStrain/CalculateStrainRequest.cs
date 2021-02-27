using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It represents the request content to CalculateStrain operation of Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStrainRequest : CalculateResultRequest<List<CalculateStrainRequestData>>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        [JsonConstructor]
        public CalculateStrainRequest(double timeStep, double finalTime, List<CalculateStrainRequestData> data) : base(timeStep, finalTime, data) { }
    }
}
