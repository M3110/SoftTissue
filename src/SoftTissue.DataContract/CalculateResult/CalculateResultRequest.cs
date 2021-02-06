using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.CalculateResult
{
    /// <summary>
    /// It represents the request content to CalculateResult operations.
    /// </summary>
    public class CalculateResultRequest : OperationRequestBase
    {
        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; set; }
    }

    /// <summary>
    /// It represents the request content to CalculateResult operations.
    /// </summary>
    /// <typeparam name="T">The type of request data.</typeparam>
    public class CalculateResultRequest<T> : CalculateResultRequest
    {
        /// <summary>
        /// Request data.
        /// </summary>
        public T Data { get; set; }
    }
}
