using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.CalculateResult
{
    /// <summary>
    /// It represents the request content to CalculateResult operations.
    /// </summary>
    public abstract class CalculateResultRequest : OperationRequestBase
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        [JsonConstructor]
        protected CalculateResultRequest(double timeStep, double finalTime)
        {
            this.TimeStep = timeStep;
            this.FinalTime = finalTime;
        }

        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; protected set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; protected set; }
    }

    /// <summary>
    /// It represents the request content to CalculateResult operations.
    /// </summary>
    /// <typeparam name="T">The type of request data.</typeparam>
    public abstract class CalculateResultRequest<T> : CalculateResultRequest
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="data"></param>
        [JsonConstructor]
        protected CalculateResultRequest(
            double timeStep, 
            double finalTime,
            T data) : base(timeStep, finalTime)
        {
            this.Data = data;
        }

        /// <summary>
        /// Request data.
        /// </summary>
        public T Data { get; protected set; }
    }
}
