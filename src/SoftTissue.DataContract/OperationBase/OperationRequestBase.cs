namespace SoftTissue.DataContract.OperationBase
{
    /// <summary>
    /// It represents the request content to operations.
    /// </summary>
    public class OperationRequestBase
    {
        /// <summary>
        /// Initial time.
        /// Unity: s (second).
        /// </summary>
        public double InitialTime { get; set; }

        /// <summary>
        /// Time step.
        /// Unity: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unity: s (second).
        /// </summary>
        public double FinalTime { get; set; }
    }

    /// <summary>
    /// It represents the essencial request content to operations.
    /// </summary>
    /// <typeparam name="T">The type of request data.</typeparam>
    public class OperationRequestBase<T> : OperationRequestBase
    {
        /// <summary>
        /// Request data.
        /// </summary>
        public T RequestData { get; set; }
    }
}
