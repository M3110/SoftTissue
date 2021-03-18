using Newtonsoft.Json;

namespace SoftTissue.DataContract.OperationBase
{
    /// <summary>
    /// It represents the 'data' content to CalculateResult operations request.
    /// </summary>
    public abstract class CalculateResultRequestData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="softTissueType"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        [JsonConstructor]
        protected CalculateResultRequestData(
            string softTissueType, 
            double? timeStep, 
            double? finalTime)
        {
            this.SoftTissueType = softTissueType;
            this.TimeStep = timeStep;
            this.FinalTime = finalTime;
        }

        /// <summary>
        /// The type of soft tissue.
        /// </summary>
        public string SoftTissueType { get; protected set; }

        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double? TimeStep { get; protected set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double? FinalTime { get; protected set; }
    }
}
